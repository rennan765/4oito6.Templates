using _4oito6.Contact.Infra.CrossCutting.Configuration;
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
        private static IServiceCollection ResolvePostalCode(this IServiceCollection services)
        {
            return services.AddScoped<IPostalCodeClientService>
            (
                (sp) =>
                {
                    var config = sp.GetService<IContactConfiguration>();

                    return new PostalCodeClientService
                    (
                        client: new HttpClient(),
                        url: config.PostalCodeWsUrl
                    );
                }
            );
        }

        public static IServiceCollection ResolveContact(this IServiceCollection services)
        {
            return services
                .AddSingleton<IContactConfiguration, ContactConfiguration>()
                .AddSingleton<IConnectionConfiguration, ConnectionConfiguration>()
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                .ResolvePostalCode()
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