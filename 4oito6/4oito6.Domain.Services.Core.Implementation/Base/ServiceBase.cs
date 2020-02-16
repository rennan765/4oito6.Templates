using _4oito6.Domain.Services.Core.Contracts.Interfaces;
using System;
using System.Linq;

namespace _4oito6.Domain.Services.Core.Implementation.Base
{
    public abstract class ServiceBase : IServiceBase
    {
        private bool _disposedValue;
        private IDisposable[] _repositories;

        public ServiceBase(IDisposable[] repositories)
        {
            _repositories = repositories;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _repositories.ToList().ForEach(r => r?.Dispose());

                    _repositories = null;
                }

                _disposedValue = true;
            }
        }

        ~ServiceBase()
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