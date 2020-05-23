using _4oito6.AuditTrail.Infra.CrossCutting.IoC;
using _4oito6.Infra.CrossCutting.Configuration.Connection;
using _4oito6.Infra.CrossCutting.Ioc.Swagger;
using _4oito6.Infra.CrossCutting.IoC.Token;
using _4oito6.Template.Infra.CrossCutting.Ioc.Resolvers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace _4oito6.Template.Infra.CrossCutting.Ioc
{
    public static class IoC
    {
        public static IServiceCollection ResolveTemplate(this IServiceCollection services)
        {
            return services
                .AddSingleton<IConnectionConfiguration, ConnectionConfiguration>()
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                .ResolveAuditTrail()
                .ResolveDatabase()
                .ResolveRepositories()
                .ResolveBus()
                .ResolveServices()
                .ResolveAppServices()
                .ConfigureSwagger()
                .ResolveToken()
                .ResolveTokenServices();
        }
    }
}