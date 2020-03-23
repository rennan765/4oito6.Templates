using _4oito6.Infra.CrossCutting.Token.Implementation;
using _4oito6.Infra.CrossCutting.Token.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace _4oito6.Infra.CrossCutting.IoC.Token
{
    public static class ServiceResolver
    {
        public static IServiceCollection ResolveTokenServices(this IServiceCollection services)
            => services.AddSingleton<ITokenBuilderService, TokenBuilderService>();
    }
}