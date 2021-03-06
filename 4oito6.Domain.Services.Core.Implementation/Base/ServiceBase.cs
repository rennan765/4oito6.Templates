﻿using _4oito6.Domain.Services.Core.Contracts.Interfaces;
using _4oito6.Domain.Specs.Core.Extensions;
using _4oito6.Domain.Specs.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace _4oito6.Domain.Services.Core.Implementation.Base
{
    public abstract class ServiceBase : IServiceBase
    {
        private bool _disposedValue;
        private IList<IBusinessSpec> _businessSpecs;
        private IDisposable[] _bus;

        public ServiceBase(IDisposable[] bus)
        {
            _bus = bus;
            _businessSpecs = new List<IBusinessSpec>();
            _disposedValue = false;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _bus.ToList().ForEach(r => r?.Dispose());
                    _bus = null;
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

        public string[] GetMessages()
        {
            var list = new List<string>();

            foreach (var spec in _businessSpecs)
                list = list.Concat(spec.Messages.Select(m => m.Message)).ToList();

            return list.ToArray();
        }

        public HttpStatusCode GetStatusCode()
        {
            var specs = _businessSpecs.Where(s => s.Messages.Any()).ToList();

            if (specs.Any())
                return specs.ToHttpStatusCode();

            return HttpStatusCode.OK;
        }
    }
}