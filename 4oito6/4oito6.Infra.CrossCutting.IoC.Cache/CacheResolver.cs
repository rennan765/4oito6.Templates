using _4oito6.Infra.CrossCutting.Configuration.Connection;
using Microsoft.Extensions.DependencyInjection;

namespace _4oito6.Infra.CrossCutting.IoC.Cache
{
    public static class CacheResolver
    {
        public static IServiceCollection ResolveCache(this IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            IConnectionConfiguration conn = provider.GetService<IConnectionConfiguration>();

            return services.AddDistributedRedisCache(options =>
            {
                options.Configuration = conn.CacheConnectionString;
                options.InstanceName = conn.CacheDbName;
            });
        }
    }
}