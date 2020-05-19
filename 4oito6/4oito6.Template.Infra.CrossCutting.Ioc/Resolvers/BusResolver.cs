using _4oito6.Template.Infra.Data.Bus.Contracts.Interfaces;
using _4oito6.Template.Infra.Data.Bus.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace _4oito6.Template.Infra.CrossCutting.Ioc.Resolvers
{
    public static class BusResolver
    {
        public static IServiceCollection ResolveBus(this IServiceCollection services)
        {
            return services
                .AddScoped<IAddressBus, AddressBus>()
                .AddScoped<IPhoneBus, PhoneBus>()
                .AddScoped<IUserBus, UserBus>();
        }
    }
}