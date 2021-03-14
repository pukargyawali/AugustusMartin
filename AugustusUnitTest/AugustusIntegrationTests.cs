using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AugustusIntegrations.Cache;
using AugustusIntegrations.ExternalAPI;
using AugustusIntegrations.ExternalAPI.Dto;
using AugustusWebApp.Facade;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;


namespace AugustusUnitTest
{
    public class AugustusIntegrationTests
    {
        private Mock<IRestClient> _restUserClientMock;
        private Mock<IRestClient> _restPostClientMock;
        private Mock<ICacheStorage> _cacheStorageMock;        
        private IConfiguration _configuration;

        [SetUp]
        public void Setup()
        {
            var mocUserRestClient = new Mock<IRestClient>();
            var apiResult = new UserDTO { UserId = "1", UserName = "test123", Email = "test123@gmail.co,", WebSite ="www.blah.com" };



            mocUserRestClient.Setup(c => c.GetDataAsync<UserDTO>("https://jsonplaceholder.typicode.com/users"))
                                     .ReturnsAsync(apiResult);

            _restUserClientMock = mocUserRestClient;

            var mocRestClient = new Mock<IRestClient>();
            var apiPostResult = new PostDTO { UserId = "1", PostId = "4", Title = "blah", Body = "blah blah blah" };
            var posts = new List<PostDTO> { apiPostResult };

            _restPostClientMock = mocUserRestClient;

            mocRestClient.Setup(c => c.GetDataAsync<IList<PostDTO>>("https://jsonplaceholder.typicode.com/users/1/posts"))
                               .ReturnsAsync(posts);


            var myConfiguration = new Dictionary<string, string>
            {
                {"IsCacheActivated", "false"}
            };

            _configuration = new ConfigurationBuilder()
              .AddInMemoryCollection(myConfiguration)
              .Build();

            var mocCache = new Mock<ICacheStorage>();


        }
        public async Task TestGetPostAsync()
        {
            
            var postFacade = new PostFacade(_restPostClientMock.Object, _cacheStorageMock.Object, _configuration);
            var result = await postFacade.GetPostAsync("1");
            Assert.IsNotNull(result);
        }

        public async Task TestGetUserAsync()
        {
            IPostFacade postFacade = new PostFacade(_restPostClientMock.Object, _cacheStorageMock.Object, _configuration);
            var userFacade = new UserFacade(_restUserClientMock.Object, _cacheStorageMock.Object, postFacade, _configuration);
            var result = await userFacade.GetAllUsers();
            Assert.IsNotNull(result);
        }
    }
}
