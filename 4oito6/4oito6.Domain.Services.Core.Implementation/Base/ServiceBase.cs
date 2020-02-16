using _4oito6.Domain.Services.Core.Contracts.Interfaces;
using _4oito6.Domain.Specs.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _4oito6.Domain.Services.Core.Implementation.Base
{
    public abstract class ServiceBase : IServiceBase
    {
        private bool _disposedValue;
        private IList<IBusinessSpec> _businessSpecs;
        private IDisposable[] _repositories;

        public ServiceBase(IDisposable[] repositories)
        {
            _repositories = repositories;
            _businessSpecs = new List<IBusinessSpec>();
            _disposedValue = false;
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

                _businessSpecs = null;

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

        public virtual void AddSpec(IBusinessSpec businessSpec)
        {
            _businessSpecs.Add(businessSpec);
        }

        public bool IsSatisfied() => !_businessSpecs.Any(b => !b.IsSatisfied());
    }
}