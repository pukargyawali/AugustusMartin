using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AugustusIntegrations.Cache;
using AugustusIntegrations.ExternalAPI;
using AugustusIntegrations.ExternalAPI.Dto;
using Microsoft.Extensions.Configuration;

namespace AugustusWebApp.Facade
{
    public class PostFacade : IPostFacade
    {
        private readonly IRestClient _jsonPlaceholderRestClient;
        private readonly ICacheStorage _redisCacheStorage;
        private readonly IConfiguration _configuration;

        private bool _isCacheActivated => _configuration.GetValue<bool>("IsCacheActivated");        

        public PostFacade(IRestClient jsonPlaceholderRestClient, ICacheStorage redisCacheStorage, IConfiguration configuration)
                => (_jsonPlaceholderRestClient, _redisCacheStorage, _configuration) = (jsonPlaceholderRestClient, redisCacheStorage, configuration);        

        public async Task<IList<PostDTO>> GetPostAsync(string userId)
        {
            try
            {
                var uri = $"users/{userId}/posts";
                if (_isCacheActivated)
                {
                    //get a from cache
                    var result = await _redisCacheStorage.GetValueAsync<IList<PostDTO>>(userId);
                    if (result == null)
                    {
                        //get data from API
                        var response = (await _jsonPlaceholderRestClient.GetDataAsync<IList<PostDTO>>(uri));
                        await _redisCacheStorage.AddValueAsync(userId, response);                        
                    }
                    return result;
                }
                else
                {
                    var response = (await _jsonPlaceholderRestClient.GetDataAsync<IList<PostDTO>>(uri));
                    return response;
                }               
            }
            catch (Exception ex)
            {
                //Use logger to log the exception                
                throw new Exception("Error occured while processing your request (" + ex.Message + ")", ex);
            }
        }
    }
}
