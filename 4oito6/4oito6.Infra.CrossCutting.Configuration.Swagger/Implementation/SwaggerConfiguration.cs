using _4oito6.Infra.CrossCutting.Configuration.Swagger.Interfaces;
using _4oito6.Infra.CrossCutting.Configuration.Swagger.Models;
using Newtonsoft.Json;
using System;

namespace _4oito6.Infra.CrossCutting.Configuration.Swagger.Implementation
{
    public class SwaggerConfiguration : ISwaggerConfiguration
    {
        public SwaggerConfigModel ConfigModel
            => JsonConvert
                .DeserializeObject<SwaggerConfigModel>
                (
                    Environment.GetEnvironmentVariable("SwaggerConfigModel")
                );
    }
}