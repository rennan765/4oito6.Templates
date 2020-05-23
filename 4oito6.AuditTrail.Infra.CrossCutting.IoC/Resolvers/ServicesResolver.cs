using _4oito6.AuditTrail.Domain.Services.Contract.Interfaces;
using _4oito6.AuditTrail.Domain.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace _4oito6.AuditTrail.Infra.CrossCutting.IoC.Resolvers
{
    public static class ServicesResolver
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
            => services.AddScoped<IAuditTrailService, AuditTrailService>();
    }
}