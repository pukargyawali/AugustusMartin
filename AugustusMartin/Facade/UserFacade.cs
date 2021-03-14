using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AugustusIntegrations.Cache;
using AugustusIntegrations.ExternalAPI;
using AugustusIntegrations.ExternalAPI.Dto;
using Microsoft.Extensions.Configuration;

namespace AugustusWebApp.Facade
{
    public class UserFacade : IUserFacade
    {
        private readonly IRestClient _jsonPlaceholderRestClient;
        private readonly ICacheStorage _redisCacheStorage;
        private readonly IConfiguration _configuration;
        private readonly IPostFacade _postFacade;

        private bool _isCacheActivated => _configuration.GetValue<bool>("IsCacheActivated");
        private IList<UserDTO> _users;

        public UserFacade(IRestClient jsonPlaceholderRestClient, ICacheStorage redisCacheStorage,IPostFacade postFacade, IConfiguration configuration)
                => (_jsonPlaceholderRestClient, _redisCacheStorage,_postFacade,_configuration)
                = (jsonPlaceholderRestClient, redisCacheStorage,postFacade, configuration);
       

        public async Task<IList<UserDTO>> GetAllUsers()
        {
            try
            {
                var uri = $"users";
                if (_isCacheActivated)
                {
                    //get a from cache
                    var result = await _redisCacheStorage.GetValueAsync<IList<UserDTO>>("users");
                    if (result == null)
                    {
                        //get data from API

                        _users = (await _jsonPlaceholderRestClient.GetDataAsync<IList<UserDTO>>(uri));
                        await PopulatePostCountAsync();
                        await _redisCacheStorage.AddValueAsync("users", _users);
                                           
                    }
                    _users = result;
                }
                else
                {
                   _users = (await _jsonPlaceholderRestClient.GetDataAsync<IList<UserDTO>>(uri));
                    await PopulatePostCountAsync();

                }
                return _users;
            }
            catch (Exception ex)
            {
                //Use logger to log the exception                
                throw new Exception("Error occured while processing your request (" + ex.Message + ")", ex);
            }
           
        }

        public async Task<UserDTO> GetUserById(string Id)
        {
            try
            {
                var uri = $"users/{Id}";
                if (_isCacheActivated)
                {
                    var users = await GetAllUsers();
                    if (users != null)
                        return users.FirstOrDefault(user => user.UserId == Id);

                    //no users found
                    else
                        return null;
                }
                else
                {
                    //get data from API
                    var response = (await _jsonPlaceholderRestClient.GetDataAsync<UserDTO>(uri));
                    return response;
                }               
            }
            catch (Exception ex)
            {
                //Use logger to log the exception                
                throw new Exception("Error occured while processing your request (" + ex.Message + ")", ex);
            }
        }

        private async Task PopulatePostCountAsync()
        {
            foreach (var user in _users)
            {
                var post = await _postFacade.GetPostAsync(user.UserId);
                user.PostCount = post.Count;
            }
        }
        
    }
}
