using _4oito6.Contact.Infra.Data.Repositories;
using _4oito6.Contact.Infra.Data.Repositories.Contracts.Interfaces.Repositories;
using _4oito6.Infra.Data.Transactions.Contracts.Interfaces;
using _4oito6.Infra.Data.Transactions.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace _4oito6.Contact.Infra.CrossCutting.Ioc.Resolvers
{
    public static class RepositoriesResolver
    {
        public static IServiceCollection ResolveRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IViewAddressRepository, ViewAddressRepository>()
                .AddScoped<IViewPhoneRepository, ViewPhoneRepository>();
        }
    }
}