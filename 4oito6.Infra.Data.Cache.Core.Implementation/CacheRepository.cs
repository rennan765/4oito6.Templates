using _4oito6.Infra.Data.Cache.Core.Contracts;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;

namespace _4oito6.Infra.Data.Cache.Core.Implementation
{
    public class CacheRepository : ICacheRepository
    {
        private IDistributedCache _cache;
        private bool _disposedValue;

        public CacheRepository(IDistributedCache cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _disposedValue = false;
        }

        public Task<string> GetAsync(string key) => _cache.GetStringAsync(key);

        public Task RemoveAsync(string key) => _cache.RemoveAsync(key);

        public Task SetAsync(string key, string value) => _cache.SetStringAsync(key, value);

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                _cache = null;

                _disposedValue = true;
            }
        }

        ~CacheRepository()
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