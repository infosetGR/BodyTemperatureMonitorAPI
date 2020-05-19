using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TemperatureMonitorAPI.Data;
using TemperatureMonitorAPI.Repository.IRepository;
using TemperatureMonitorAPI.Repository;
using TemperatureMonitorAPI.Models.Mapper;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using AutoMapper;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TemperatureMonitorAPI
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
            services.AddCors();
           
            string cs= Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<TMContext>(options => options.UseMySql(cs));
            //mySqlOptions => mySqlOptions.ServerVersion(new Version(8, 0, 20), ServerType.MySql)));

            services.AddScoped<ITemperatureLogEntryRepository, TemperatureLogEntryRepository>();
            services.AddScoped<IPatientDetailRepository, PatientDetailRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddAutoMapper(typeof(Mappings));
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();
            services.AddSwaggerGen(options =>
            {
            //    options.SwaggerDoc("TemperatureMonitorOpenAPISpec", new Microsoft.OpenApi.Models.OpenApiInfo()
            //    {
            //        Title = "Body Temperature Monitor API",
            //        Version = "1",
            //        Contact=new Microsoft.OpenApi.Models.OpenApiContact()
            //        {
            //            Email="fotisss@gmail.com",
            //            Name="Fotios Stathopoulos"
            //        },
            //        License = new Microsoft.OpenApi.Models.OpenApiLicense()
            //        {
            //            Name = "MIT License",
            //            Url = new Uri("https://en.wikipedia.org/wiki/MIT_License")
            //        }

            //    });

                //options.SwaggerDoc("TemperatureMonitorOpenAPISpecLog", new Microsoft.OpenApi.Models.OpenApiInfo()
                //{
                //    Title = "Body Temperature Monitor API Log",
                //    Version = "1",
                //    Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                //    {
                //        Email = "fotisss@gmail.com",
                //        Name = "Fotios Stathopoulos"
                //    },
                //    License = new Microsoft.OpenApi.Models.OpenApiLicense()
                //    {
                //        Name = "MIT License",
                //        Url = new Uri("https://en.wikipedia.org/wiki/MIT_License")
                //    }

                //});


                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                   "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
                   "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
                   "Example: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });



        
                
            }
            );
            services.AddControllers();

          
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
         .AddJwtBearer(x => {
             x.RequireHttpsMetadata = false;
             x.SaveToken = true;
             x.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateIssuerSigningKey = true,
                 IssuerSigningKey = new SymmetricSecurityKey(key),
                 ValidateIssuer = false,
                 ValidateAudience = false
             };
         });

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                //var context = serviceScope.ServiceProvider.GetService<TMContext>();
                var DB = serviceScope.ServiceProvider.GetRequiredService<TMContext>();
                DB.Database.EnsureCreatedAsync();

                // Seed the database.
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var desc in provider.ApiVersionDescriptions)
                    options.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json", desc.GroupName.ToUpperInvariant());
                options.RoutePrefix = "";
            });

            //app.UseSwaggerUI(options =>
            //{
            //    options.SwaggerEndpoint("/swagger/TemperatureMonitorOpenAPISpec/swagger.json", "Temperature Monitor API");
            //    //options.SwaggerEndpoint("/swagger/TemperatureMonitorOpenAPISpecPatient/swagger.json", "Temperature Monitor Patient API");
            //    //options.SwaggerEndpoint("/swagger/TemperatureMonitorOpenAPISpecLog/swagger.json", "Temperature Monitor Log API");
            //    options.RoutePrefix = "";
            //});
            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
           
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
