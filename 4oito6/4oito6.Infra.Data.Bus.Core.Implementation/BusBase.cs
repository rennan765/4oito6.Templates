using _4oito6.Infra.Data.Bus.Core.Contracts;
using System;
using System.Linq;

namespace _4oito6.Infra.Data.Bus.Core.Implementation
{
    public abstract class BusBase : IBusBase
    {
        private bool _disposedValue;
        private IDisposable[] _repositories;

        public BusBase(IDisposable[] bus)
        {
            _disposedValue = false;
            _repositories = bus;
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

        ~BusBase()
        {
            Dispose(false);
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}