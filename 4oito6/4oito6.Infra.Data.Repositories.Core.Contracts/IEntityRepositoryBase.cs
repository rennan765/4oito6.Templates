using _4oito6.Infra.Data.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace _4oito6.Infra.Data.Repositories.Core.Contracts
{
    public interface IEntityRepositoryBase<TEntity, TId> : IDisposable
        where TEntity : DataModelBase
        where TId : struct
    {
        Task<IEnumerable<TEntity>> ListAsync(Expression<Func<TEntity, bool>> where = null);

        Task<TEntity> GetByIdAsync(TId id);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where = null);

        Task<TEntity> InsertAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<TEntity> DeleteAsync(TEntity entity);
    }
}