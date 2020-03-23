using _4oito6.Infra.CrossCutting.Configuration.Swagger.Implementation;
using _4oito6.Infra.CrossCutting.Configuration.Swagger.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using System;
using System.IO;

namespace _4oito6.Infra.CrossCutting.Ioc.Swagger
{
    public static class SwaggerResolver
    {
        private static IServiceCollection ConfigureSwaggerConfiguration(this IServiceCollection services)
            => services.AddSingleton<ISwaggerConfiguration, SwaggerConfiguration>();

        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.ConfigureSwaggerConfiguration();

            var config = services.BuildServiceProvider()?
                .GetService<ISwaggerConfiguration>()?
                .ConfigModel;

            if (config == null)
                throw new ArgumentNullException(nameof(config));

            //Applying swagger doc
            services.AddSwaggerGen(x =>
            {
                ////Documentation data
                var info = new OpenApiInfo
                {
                    Title = config.Title ?? throw new ArgumentNullException(nameof(config.Title)),
                    Version = config.Version ?? throw new ArgumentNullException(nameof(config.Version)),
                    Description = config.Description ?? throw new ArgumentNullException(nameof(config.Description))
                };

                if (!string.IsNullOrEmpty(config.ContactName))
                    info.Contact = new OpenApiContact
                    {
                        Name = config.ContactName,
                        Email = config.ContactEmail,
                        Url = new Uri(config.ContactUrl)
                    };

                x.SwaggerDoc("v1", info);

                //Insert token
                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Json Web Token (JWT) Authorization header using the Bearer scheme."
                });

                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                    }
                });

                //Insert comment's XML
                var xmlDocumentPath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, $"{PlatformServices.Default.Application.ApplicationName}.xml");

                if (File.Exists(xmlDocumentPath))
                {
                    x.IncludeXmlComments(xmlDocumentPath);
                }
            });

            return services;
        }
    }
}