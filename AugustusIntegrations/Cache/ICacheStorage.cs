using System;
using System.Threading.Tasks;

namespace AugustusIntegrations.Cache
{
    public interface ICacheStorage
    {
        Task<T> AddValueAsync<T>(string key, T value);

        Task<T> GetValueAsync<T>(string key);

        Task<bool> RemoveValueAsync(string key);
    }
}
