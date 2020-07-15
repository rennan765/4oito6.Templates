using _4oito6.Contact.Infra.Data.Context;
using _4oito6.Contact.Infra.Data.Repositories;
using _4oito6.Contact.Infra.Data.Repositories.Contracts.Interfaces.Repositories;
using _4oito6.Infra.Data.Core.Connection;
using _4oito6.Infra.Data.Transactions.Contracts.Interfaces;
using _4oito6.Infra.Data.Transactions.Implementation;
using _4oito6.Template.Infra.Data.Context;
using Microsoft.Extensions.DependencyInjection;

namespace _4oito6.Contact.Infra.CrossCutting.Ioc.Resolvers
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
                        var contactContext = sp.GetService<ContactContext>();
                        var conn = sp.GetService<IAsyncDbConnection>();

                        return new UnitOfWork(templateContext, contactContext, conn);
                    }
                )
                .AddScoped<IViewAddressRepository, ViewAddressRepository>()
                .AddScoped<IViewPhoneRepository, ViewPhoneRepository>();
        }
    }
}