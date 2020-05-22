using _4oito6.Infra.CrossCutting.Configuration.Connection;
using _4oito6.Infra.Data.Core.Connection;
using _4oito6.Template.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace _4oito6.Contact.Infra.CrossCutting.Ioc.Resolvers
{
    public static class DatabaseResolver
    {
        public static IServiceCollection ResolveDatabase(this IServiceCollection services)
        {
            var config = services.BuildServiceProvider()?.GetService<IConnectionConfiguration>();

            return services
                .AddScoped<IAsyncDbConnection>
                (
                    (sp) =>
                    {
                        if (config == null)
                            config = sp.GetService<IConnectionConfiguration>();

                        return new AsyncDbConnection(new NpgsqlConnection(config.DbConnectionString));
                    }
                )
                .AddDbContext<TemplateContext>(options => options.UseNpgsql(config.DbConnectionString));
        }
    }
}