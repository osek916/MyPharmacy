using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyPharmacy.Entities;
using MyPharmacy.Services;
using AutoMapper;
using MyPharmacy.Middleware;
using Microsoft.AspNetCore.Identity;
using FluentValidation;
using MyPharmacy.Models;
using MyPharmacy.Models.Validators;
using FluentValidation.AspNetCore;
using Microsoft.IdentityModel.Tokens;
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
            services.AddScoped<ErrorHandlingMiddleware>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IValidator<UserRegisterDto>, UserRegisterDtoValidator>();
            services.AddScoped<IValidator<CreateDrugCategoryDto>, CreateDrugCategoryDtoValidator>();
            services.AddScoped<IValidator<CreateDrugDto>, CreateDrugDtoValidator>();
            services.AddScoped<IValidator<CreateDrugInformationDto >, CreateDrugInformationDtoValidator>();
            services.AddScoped<IValidator<CreatePharmacyDto>, CreatePharmacyDtoValidator>();
            services.AddScoped<IValidator<UpdateDrugCategoryDto>,  UpdateDrugCategoryDtoValidator > ();
            services.AddScoped<IValidator<UpdateDrugDto>, UpdateDrugDtoValidator > ();
            services.AddScoped<IValidator<UpdateDrugInformationDto>, UpdateDrugInformationDtoValidator>();
            services.AddScoped<IValidator<UpdatePharmacyDto>, UpdatePharmacyDtoValidator>();
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
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, PharmacySeeder seeder)
        {
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
