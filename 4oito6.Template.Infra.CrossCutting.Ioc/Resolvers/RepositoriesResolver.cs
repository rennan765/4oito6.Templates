using _4oito6.Infra.Data.Core.Connection;
using _4oito6.Infra.Data.Transactions.Contracts.Interfaces;
using _4oito6.Infra.Data.Transactions.Implementation;
using _4oito6.Template.Infra.Data.Context;
using _4oito6.Template.Infra.Data.Repositories.Contracts.Entity;
using _4oito6.Template.Infra.Data.Repositories.Implementation.Entity;
using Microsoft.Extensions.DependencyInjection;

namespace _4oito6.Template.Infra.CrossCutting.Ioc.Resolvers
{
    public static class RepositoriesResolver
    {
        public static IServiceCollection ResolveRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<IUnitOfWork>
                (
                    (sp) =>
                    {
                        var templateContext = sp.GetService<TemplateContext>();
                        var conn = sp.GetService<IAsyncDbConnection>();

                        return new UnitOfWork(templateContext, conn);
                    }
                )
                .AddScoped<IAddressRepository, AddressRepository>()
                .AddScoped<IPhoneRepository, PhoneRepository>()
                .AddScoped<IUserRepository, UserRepository>();
        }
    }
}