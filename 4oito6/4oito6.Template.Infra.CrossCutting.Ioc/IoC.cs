using _4oito6.Infra.CrossCutting.Configuration.Connection;
using _4oito6.Template.Infra.CrossCutting.Ioc.Resolvers;
using Microsoft.Extensions.DependencyInjection;

namespace _4oito6.Template.Infra.CrossCutting.Ioc
{
    public static class IoC
    {
        public static IServiceCollection ResolveTemplate(this IServiceCollection services)
        {
            return services
                .AddSingleton<IConnectionConfiguration, ConnectionConfiguration>()
                .ResolveDatabase()
                .ResolveRepositories()
                .ResolveBus()
                .ResolveServices()
                .ResolveAppServices();
        }
    }
}