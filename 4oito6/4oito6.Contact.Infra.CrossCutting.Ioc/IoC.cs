using _4oito6.Contact.Infra.CrossCutting.Ioc.Resolvers;
using _4oito6.Contact.Infra.CrossCutting.PostalCode.Contracts.Interfaces;
using _4oito6.Contact.Infra.CrossCutting.PostalCode.Implementation;
using _4oito6.Infra.CrossCutting.Configuration.Connection;
using _4oito6.Infra.CrossCutting.Ioc.Swagger;
using _4oito6.Infra.CrossCutting.IoC.Token;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace _4oito6.Contact.Infra.CrossCutting.Ioc
{
    public static class IoC
    {
        private static IServiceCollection ResolvePostalCode(this IServiceCollection services, string urlPostalCode)
        {
            return services.AddScoped<IPostalCodeClientService>
            (
                (sp) =>
                {
                    return new PostalCodeClientService
                    (
                        client: new HttpClient(),
                        url: urlPostalCode
                    );
                }
            );
        }

        public static IServiceCollection ResolveContact(this IServiceCollection services, string urlPostalCode)
        {
            return services.ResolvePostalCode(urlPostalCode)
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