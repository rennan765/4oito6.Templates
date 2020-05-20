using _4oito6.Contact.Domain.Application.Contracts.Interfaces;
using _4oito6.Contact.Domain.Application.Implementation.Implmentation;
using Microsoft.Extensions.DependencyInjection;

namespace _4oito6.Contact.Infra.CrossCutting.Ioc.Resolvers
{
    public static class AppServicesResolver
    {
        public static IServiceCollection ResolveAppServices(this IServiceCollection services)
        {
            return services.AddScoped<IContactAppService, ContactAppService>();
        }
    }
}