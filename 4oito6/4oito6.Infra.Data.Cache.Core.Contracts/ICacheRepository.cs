using System;
using System.Threading.Tasks;

namespace _4oito6.Infra.Data.Cache.Core.Contracts
{
    public interface ICacheRepository : IDisposable
    {
        Task GetAsync(string key);

        Task RemoveAsync(string key);

        Task SetAsync(string key, string value);
    }
}