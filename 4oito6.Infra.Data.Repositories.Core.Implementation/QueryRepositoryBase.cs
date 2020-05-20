using _4oito6.Infra.Data.Core.Connection;
using _4oito6.Infra.Data.Repositories.Core.Contracts;
using System;

namespace _4oito6.Infra.Data.Repositories.Core.Implementation
{
    public abstract class QueryRepositoryBase : IQueryRepositoryBase
    {
        private bool _disposedValue;

        protected IAsyncDbConnection Connection { get; private set; }

        protected QueryRepositoryBase(IAsyncDbConnection connection)
        {
            _disposedValue = false;
            Connection = connection ?? throw new ArgumentNullException(nameof(connection));
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