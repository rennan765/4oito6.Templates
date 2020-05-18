using _4oito6.Infra.CrossCutting.Token.Implementation;
using _4oito6.Infra.CrossCutting.Token.Interfaces;
using _4oito6.Infra.Data.Cache.Core.Contracts;
using _4oito6.Infra.Data.Cache.Core.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace _4oito6.Infra.CrossCutting.IoC.Token
{
    public static class ServiceResolver
    {
        public static IServiceCollection ResolveTokenServices(this IServiceCollection services)
            => services.AddSingleton<ICacheRepository, CacheRepository>()
                .AddSingleton<ITokenBuilderService, TokenBuilderService>();
    }
}