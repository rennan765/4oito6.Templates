using _4oito6.Infra.Data.Repositories.Core.Contracts;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _4oito6.Infra.Data.Repositories.Mongo.Core.Implementation
{
    public abstract class MongoRepositoryBase<TEntity> : IMongoRepositoryBase<TEntity>
        where TEntity : class
    {
        protected readonly IMongoCollection<TEntity> Collection;

        public MongoRepositoryBase(IMongoCollection<TEntity> collection)
        {
            Collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await Collection.InsertOneAsync(entity).ConfigureAwait(false);
            return entity;
        }

        public async Task<IList<TEntity>> GetAsync()
            => (await Collection.FindAsync(x => true).ConfigureAwait(false)).ToList();
    }
}