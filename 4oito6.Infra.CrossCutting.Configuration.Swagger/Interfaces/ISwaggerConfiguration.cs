using _4oito6.Infra.CrossCutting.Configuration.Swagger.Models;

namespace _4oito6.Infra.CrossCutting.Configuration.Swagger.Interfaces
{
    public interface ISwaggerConfiguration
    {
        SwaggerConfigModel ConfigModel { get; }
    }
}