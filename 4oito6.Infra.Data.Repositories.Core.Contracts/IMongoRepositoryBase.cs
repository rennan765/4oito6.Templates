using System.Collections.Generic;
using System.Threading.Tasks;

namespace _4oito6.Infra.Data.Repositories.Core.Contracts
{
    public interface IMongoRepositoryBase<TEntity>
        where TEntity : class
    {
        Task<TEntity> CreateAsync(TEntity entity);

        Task<IList<TEntity>> GetAsync();
    }
}