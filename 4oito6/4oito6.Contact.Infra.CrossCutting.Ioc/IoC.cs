using _4oito6.Contact.Infra.CrossCutting.Ioc.Resolvers;
using _4oito6.Infra.CrossCutting.Configuration.Connection;
using _4oito6.Infra.CrossCutting.Ioc.Swagger;
using _4oito6.Infra.CrossCutting.IoC.Token;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace _4oito6.Contact.Infra.CrossCutting.Ioc
{
    public static class IoC
    {
        public static IServiceCollection ResolveContact(this IServiceCollection services)
        {
            return services
                .AddSingleton<IConnectionConfiguration, ConnectionConfiguration>()
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
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