using _4oito6.AuditTrail.Infra.Mongo.Repositories.Contracts.Interfaces;
using _4oito6.AuditTrail.Infra.Mongo.Repositories.Contracts.Model;
using _4oito6.AuditTrail.Infra.Mongo.Repositories.Implementation;
using _4oito6.Infra.CrossCutting.Configuration.Connection;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace _4oito6.AuditTrail.Infra.CrossCutting.IoC.Resolvers
{
    public static class RepositoriesResolver
    {
        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            return services.AddScoped<IAuditTrailMongoRepository>
            (
                (sp) =>
                {
                    var config = sp.GetService<IConnectionConfiguration>();

                    var client = new MongoClient(config.MongoConnectionString);
                    var database = client.GetDatabase(config.MongoDbName);

                    return new AuditTrailMongoRepository
                    (
                        collection: database.GetCollection<AuditTrailDto>("AuditTrail")
                    );
                }
            );
        }
    }
}