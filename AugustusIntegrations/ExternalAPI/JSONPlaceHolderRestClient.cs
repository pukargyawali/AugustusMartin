using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace AugustusIntegrations.ExternalAPI
{
    public class JSONPlaceHolderRestClient : IRestClient
    {
        private readonly HttpClient _client;        

        public JSONPlaceHolderRestClient(string baseUri)
        {
            _client = new HttpClient { BaseAddress = new Uri(baseUri) };           
        }

        public async Task<T> GetDataAsync<T>(string uri)
        {
            try
            {
                var response = await _client.GetAsync(uri);
                    
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return default(T);
                }
                var data = JsonConvert.DeserializeObject<T>(
                   await response.Content.ReadAsStringAsync());
               
                return data;
            }
            catch (Exception ex)
            {
              throw new Exception("Error occured while calling external API(" + ex.Message + ")", ex);
            }          
           
        }
        
    }
}
