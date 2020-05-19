using _4oito6.Infra.Data.Transactions.Contracts.Interfaces;
using _4oito6.Infra.Data.Transactions.Implementation;
using _4oito6.Template.Infra.Data.Repositories.Contracts.Entity;
using _4oito6.Template.Infra.Data.Repositories.Implementation.Entity;
using Microsoft.Extensions.DependencyInjection;

namespace _4oito6.Contact.Infra.CrossCutting.Ioc.Resolvers
{
    public static class RepositoriesResolver
    {
        public static IServiceCollection ResolveRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IAddressRepository, AddressRepository>()
                .AddScoped<IPhoneRepository, PhoneRepository>();
        }
    }
}