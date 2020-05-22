using _4oito6.AuditTrail.Application.Contracts;
using _4oito6.AuditTrail.Application.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace _4oito6.AuditTrail.Infra.CrossCutting.IoC.Resolvers
{
    public static class AppServicesResolver
    {
        public static IServiceCollection ConfigureAppServices(this IServiceCollection services)
            => services.AddScoped<IAuditTrailAppService, AuditTrailAppService>();
    }
}