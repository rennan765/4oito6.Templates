using _4oito6.Infra.Data.Model.Core;
using _4oito6.Infra.Data.Repositories.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace _4oito6.Infra.Data.Repositories.Core.Implementation
{
    public abstract class EntityRepositoryBase<TEntity, TId> : IEntityRepositoryBase<TEntity, TId>
        where TEntity : DataModelBase
        where TId : struct
    {
        private bool _disposedValue;

        protected DbContext Context { get; private set; }

        protected EntityRepositoryBase(DbContext context)
        {
            _disposedValue = false;
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<TEntity> DeleteAsync(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            return Task.FromResult(entity);
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> where)
            => await Context.Set<TEntity>().AnyAsync(where).ConfigureAwait(false);

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where = null)
        {
            var dbSet = Context.Set<TEntity>();

            return await
            (
                where == null ?
                dbSet.FirstOrDefaultAsync() :
                dbSet.FirstOrDefaultAsync(where)
            ).ConfigureAwait(false);
        }

        public virtual async Task<TEntity> GetByIdAsync(TId id)
            => await Context.Set<TEntity>().FindAsync(id).ConfigureAwait(false);

        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity).ConfigureAwait(false);
            return entity;
        }

        public virtual async Task<IEnumerable<TEntity>> ListAsync(Expression<Func<TEntity, bool>> where = null)
        {
            var dbSet = Context.Set<TEntity>();

            return await
            (
                where == null ?
                dbSet.ToListAsync() :
                dbSet.Where(where).ToListAsync()
            ).ConfigureAwait(false);
        }

        public virtual Task<TEntity> UpdateAsync(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
            return Task.FromResult(entity);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    Context?.Dispose();
                    Context = null;
                }

                _disposedValue = true;
            }
        }

        ~EntityRepositoryBase()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}