using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MyPharmacy.Entities;
using MyPharmacy.Middleware;
using MyPharmacy.Models;
using MyPharmacy.Models.UserDtos;
using MyPharmacy.Models.Validators;
using MyPharmacy.Models.Validators.DrugInformation;
using MyPharmacy.Models.Validators.Pharmacy;
using MyPharmacy.Models.Validators.SearchEngine;
using MyPharmacy.Services;
using System.Text;

namespace MyPharmacy
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var authenticationSettings = new AuthenticationSettings();
            Configuration.GetSection("Authentication").Bind(authenticationSettings); 

            services.AddSingleton(authenticationSettings);
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "Bearer";
                option.DefaultScheme = "Bearer";
                option.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false; 
                cfg.SaveToken = true;             
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authenticationSettings.JwtIssuer,
                    ValidAudience = authenticationSettings.JwtIssuer, 
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)) 
                };
            });
            
            services.AddControllers().AddFluentValidation();
            services.AddDbContext<PharmacyDbContext>();
            services.AddAutoMapper(this.GetType().Assembly);
            services.AddScoped<IDrugService, DrugService>();
            services.AddScoped<IPharmacyService, PharmacyService>();
            services.AddScoped<ISearchEngineService, SearchEngineService>();
            services.AddScoped<IDrugInformationService, DrugInformationService>();
            services.AddScoped<IDrugCategoryService, DrugCategoryService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ErrorHandlingMiddleware>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddSingleton<FileExtensionContentTypeProvider>();
            //Validators
            services.AddScoped<IValidator<UserRegisterDto>, UserRegisterDtoValidator>();
            services.AddScoped<IValidator<CreateDrugCategoryDto>, CreateDrugCategoryDtoValidator>();
            services.AddScoped<IValidator<CreateDrugDto>, CreateDrugDtoValidator>();
            services.AddScoped<IValidator<CreateDrugInformationDto >, CreateDrugInformationDtoValidator>();
            services.AddScoped<IValidator<CreatePharmacyDto>, CreatePharmacyDtoValidator>();
            services.AddScoped<IValidator<UpdateDrugCategoryDto>,  UpdateDrugCategoryDtoValidator > ();
            services.AddScoped<IValidator<UpdateDrugDto>, UpdateDrugDtoValidator > ();
            services.AddScoped<IValidator<UpdateDrugInformationDto>, UpdateDrugInformationDtoValidator>();
            services.AddScoped<IValidator<UpdatePharmacyDto>, UpdatePharmacyDtoValidator>();
            services.AddScoped<IValidator<UpdateUserDtoWithRole>, UpdateUserDtoWithRoleValidator>();
            services.AddScoped<IValidator<UpdateUserDto>, UpdateUserDtoValidator>();
            services.AddScoped<IValidator<UpdateUserRoleAndPharmacyId>, UpdateUserRoleAndPharmacyIdValidator>();
            services.AddScoped<IValidator<SearchEngineDrugInformationQuery>, SearchEngineDrugInformationQueryValidator>();
            services.AddScoped<IValidator<SearchEnginePharmacyQuery>, SearchEnginePharmacyQueryValidator>();
            services.AddScoped<IValidator<SearchEngineDrugQuery>, SearchEngineDrugQueryValidator>();
            services.AddScoped<IValidator<SortParameters>, SortParametersValidator>();
            services.AddScoped < IValidator<GetAllDrugInformationQuery>, GetAllDrugInformationQueryValidator>();
            services.AddScoped<IValidator<PharmacyGetAllQuery>, PharmacyGetAllQueryValidator>();

            services.AddScoped<IUserContextService, UserContextService>();
            services.AddHttpContextAccessor();//allows injection to constructor UserContextService => IHttpContextAccessor 
            services.AddScoped<PharmacySeeder>();
            services.AddSwaggerGen();
            
            services.AddCors(options =>
            {
                options.AddPolicy("ClientFront", builder =>
                    builder.AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins("http://localhost:8080")

                );
            });
            
            //services.AddDbContext<PharmacyDbContext>(options => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=PharmacyDb;Trusted_Connection=True;"));
            services.AddDbContext<PharmacyDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Server = (localdb)\\mssqllocaldb; Database = PharmacyDb; Trusted_Connection = True; ")));
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, PharmacySeeder seeder)
        {
            app.UseStaticFiles();
            app.UseCors("ClientFront"); 
            seeder.Seed();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "MyPharmacy API");
            }); // https://localhost:5001/swagger/index.html

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
