using _4oito6.Infra.Data.Repositories.Core.Contracts;
using System;
using System.Data;

namespace _4oito6.Infra.Data.Repositories.Core.Implementation
{
    public abstract class QueryRepositoryBase : IQueryRepositoryBase
    {
        private bool _disposedValue;

        protected IDbConnection Connection { get; private set; }

        protected QueryRepositoryBase(IDbConnection connection)
        {
            _disposedValue = false;
            Connection = connection;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    Connection?.Dispose();
                    Connection = null;
                }

                _disposedValue = true;
            }
        }

        ~QueryRepositoryBase()
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