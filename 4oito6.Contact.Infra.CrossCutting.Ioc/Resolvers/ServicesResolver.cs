using _4oito6.Contact.Domain.Services.Contracts.Interfaces;
using _4oito6.Contact.Domain.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace _4oito6.Contact.Infra.CrossCutting.Ioc.Resolvers
{
    public static class ServicesResolver
    {
        public static IServiceCollection ResolveServices(this IServiceCollection services)
        {
            return services.AddScoped<IContactService, ContactService>();
        }
    }
}