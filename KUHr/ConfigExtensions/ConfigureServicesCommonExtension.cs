

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;


namespace KUHr.WebAPI.ConfigExtensions
{
    public static class ConfigureServicesCommonExtension
    {
        private static DateTime? Date;
        public static IServiceCollection ServicesRegisterCommonConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
            services.SwaggerConfig(configuration);
            services.AddHttpContextAccessor();
            services.LocalizationConfig(configuration);


            return services;
        }
        
        private static void SwaggerConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "KUHr", Version = "v1" });

                var documentName = configuration["SwaggerDocmentName"];
                var filePath = Path.Combine(AppContext.BaseDirectory, documentName);
                //options.IncludeXmlComments(filePath);
                
                var security = new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            //Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                };

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                options.AddSecurityRequirement(security);
                options.OperationFilter<AddLanguageHeaderParameter>();
            });
        }


       
        private static void LocalizationConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure((Action<RequestLocalizationOptions>)(options =>
            {
                var supportedCultures = new[]
                    {
                        new CultureInfo("en-US"),
                        new CultureInfo("ar-EG"),
                        new CultureInfo("fr-FR")
                    };
                options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(async context =>
                {
                                     
                    var lang = context.Request.Headers["lang"].ToString();
                    return new ProviderCultureResult(string.IsNullOrWhiteSpace(lang) ? "en-US" : lang);
                }));
            }));
        }

     

       
    }
    public class AddLanguageHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "lang",
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema
                {
                    Type = "String"
                },
                Required = false
            });
        }
    }
}