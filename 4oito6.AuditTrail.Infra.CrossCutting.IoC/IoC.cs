using _4oito6.AuditTrail.Infra.CrossCutting.IoC.Resolvers;
using Microsoft.Extensions.DependencyInjection;

namespace _4oito6.AuditTrail.Infra.CrossCutting.IoC
{
    public static class IoC
    {
        public static IServiceCollection ResolveAuditTrail(this IServiceCollection services)
        {
            return services.ConfigureRepositories()
                .ConfigureBus()
                .ConfigureServices()
                .ConfigureAppServices();
        }
    }
}