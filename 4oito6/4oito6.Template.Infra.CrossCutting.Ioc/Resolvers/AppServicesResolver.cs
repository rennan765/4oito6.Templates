using _4oito6.Template.Domain.Application.Contracts.Interfaces;
using _4oito6.Template.Domain.Application.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace _4oito6.Template.Infra.CrossCutting.Ioc.Resolvers
{
    public static class AppServicesResolver
    {
        public static IServiceCollection ResolveAppServices(this IServiceCollection services)
        {
            return services.AddScoped<IUserAppService, UserAppService>();
        }
    }
}