using _4oito6.AuditTrail.Infra.Data.Bus.Contracts;
using _4oito6.AuditTrail.Infra.Data.Bus.Implementation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace _4oito6.AuditTrail.Infra.CrossCutting.IoC.Resolvers
{
    public static class BusResolver
    {
        public static IServiceCollection ConfigureBus(this IServiceCollection services)
            => services.AddScoped<IAuditTrailBus, AuditTrailBus>();
    }
}