using Microsoft.AspNet.OData;
using System;
using System.Linq;

namespace _4oito6.Contact.Api.Controllers.Base
{
    public abstract class ODataBaseController : ODataController, IDisposable
    {
        private IDisposable[] _appServices;
        private bool _disposedValue;

        public ODataBaseController(IDisposable[] appServices)
        {
            _appServices = appServices ?? throw new ArgumentNullException(nameof(appServices));
            _disposedValue = false;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _appServices.ToList().ForEach(a => a.Dispose());
                    _appServices = null;
                }

                _disposedValue = true;
            }
        }

        ~ODataBaseController()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}