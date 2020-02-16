using _4oito6.Domain.Application.Core.Contracts.Interfaces;
using System;
using System.Linq;

namespace _4oito6.Domain.Application.Core.Implementation.Base
{
    public class AppServiceBase : IAppServiceBase
    {
        private IDisposable[] _services;
        private bool _disposedValue;

        public AppServiceBase(IDisposable[] services)
        {
            _services = services;
            _disposedValue = false;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _services.ToList().ForEach(s => s?.Dispose());
                    _services = null;
                }

                _disposedValue = true;
            }
        }

        ~AppServiceBase()
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