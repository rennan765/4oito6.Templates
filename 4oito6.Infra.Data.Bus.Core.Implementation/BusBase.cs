using _4oito6.Infra.Data.Bus.Core.Contracts;
using _4oito6.Infra.Data.Transactions.Contracts.Interfaces;
using System;
using System.Linq;

namespace _4oito6.Infra.Data.Bus.Core.Implementation
{
    public abstract class BusBase : IBusBase
    {
        protected IUnitOfWork Unit;
        private IDisposable[] _repositories;
        private bool _disposedValue;

        public BusBase(IUnitOfWork unit, IDisposable[] bus)
        {
            Unit = unit ?? throw new ArgumentNullException(nameof(unit));
            _disposedValue = false;
            _repositories = bus;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    Unit?.Dispose();
                    Unit = null;

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