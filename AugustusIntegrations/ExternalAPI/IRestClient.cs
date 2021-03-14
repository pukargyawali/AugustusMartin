using System;
using System.Threading.Tasks;

namespace AugustusIntegrations.ExternalAPI
{
    public interface IRestClient
    {
        Task<T> GetDataAsync<T>(string uri);
    }
}
