using _4oito6.Contact.Infra.Data.Bus.Contracts.Interfaces;
using _4oito6.Contact.Infra.Data.Bus.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace _4oito6.Contact.Infra.CrossCutting.Ioc.Resolvers
{
    public static class BusResolver
    {
        public static IServiceCollection ResolveBus(this IServiceCollection services)
        {
            return services
                .AddScoped<IPhoneBus, PhoneBus>()
                .AddScoped<IAddressBus, AddressBus>();
        }
    }
}