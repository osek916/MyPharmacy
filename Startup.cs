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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var authenticationSettings = new AuthenticationSettings();
            Configuration.GetSection("Authentication").Bind(authenticationSettings); //binduje z appsetting.json do powy�szego obiektu

            services.AddSingleton(authenticationSettings);
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "Bearer";
                option.DefaultScheme = "Bearer";
                option.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false; //nie wymuszamy tylko protoko�u HTTPS
                cfg.SaveToken = true;             //zapisuje token po stronie serwera
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authenticationSettings.JwtIssuer,
                    ValidAudience = authenticationSettings.JwtIssuer, //jakie podmioty mog� u�ywa� tego tokenu. Generujemy je tylko w obr�bie tej aplikacji 
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)) //klucz prywatny wygenerowany na podstawie klucza z appsetting.json
                };
            });

            services.AddControllers().AddFluentValidation();
            services.AddDbContext<PharmacyDbContext>();
            services.AddAutoMapper(this.GetType().Assembly);
            services.AddScoped<IDrugService, DrugService>();
            services.AddScoped<IPharmacyService, PharmacyService>();
            services.AddScoped<ISearchEngineService, SearchEngineService>();
            services.AddScoped<IDrugInformationService, DrugInformationService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ErrorHandlingMiddleware>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IValidator<UserRegisterDto>, UserRegisterDtoValidator>();
            services.AddScoped<IUserContextService, UserContextService>();
            services.AddHttpContextAccessor();//pozwala na wstrzykni�cie do konstruktora UserContextService => IHttpContextAccessor
            services.AddScoped<PharmacySeeder>();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, PharmacySeeder seeder)
        {
            seeder.Seed();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseSwagger();
            //Dodawanie interfejsu
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
