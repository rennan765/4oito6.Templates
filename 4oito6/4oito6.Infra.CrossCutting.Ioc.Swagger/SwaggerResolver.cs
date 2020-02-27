using _4oito6.Infra.CrossCutting.Configuration.Swagger.Implementation;
using _4oito6.Infra.CrossCutting.Configuration.Swagger.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
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
                //Documentation data
                var info = new Info
                {
                    Title = config.Title ?? throw new ArgumentNullException(nameof(config.Title)),
                    Version = config.Version ?? throw new ArgumentNullException(nameof(config.Version)),
                    Description = config.Description ?? throw new ArgumentNullException(nameof(config.Description))
                };

                if (!string.IsNullOrEmpty(config.ContactName))
                    info.Contact = new Contact
                    {
                        Name = config.ContactName,
                        Email = config.ContactEmail,
                        Url = config.ContactUrl
                    };

                x.SwaggerDoc("v1", info);

                //Insert token
                //Swagger 2.+ support
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                x.AddSecurityDefinition("Bearer",
                    new ApiKeyScheme
                    {
                        In = "header",
                        Description = "Autenticação baseada em Json Web Token (JWT)",
                        Name = "Authorization",
                        Type = "apiKey"
                    });

                x.AddSecurityRequirement(security);

                //Insert comment's XML
                var xmlDocumentPath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, $"{PlatformServices.Default.Application.ApplicationName}.xml");

                if (File.Exists(xmlDocumentPath))
                {
                    x.IncludeXmlComments(xmlDocumentPath);
                }

                x.DescribeAllEnumsAsStrings();
            });

            return services;
        }
    }
}