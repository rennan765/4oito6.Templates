using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace _4oito6.Template.Api.Controllers.Base
{
    public abstract class BaseController : Controller, IDisposable
    {
        private bool _disposedValue;
        private IDisposable[] _appServices;

        public BaseController(IDisposable[] appServices)
        {
            _appServices = appServices ?? throw new ArgumentNullException(nameof(appServices));
            _disposedValue = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _appServices.ToList().ForEach(a => a.Dispose());
                    _appServices = null;
                }

                base.Dispose(disposing);

                _disposedValue = true;
            }
        }

        ~BaseController()
        {
            Dispose(false);
        }

        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}