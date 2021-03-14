using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AugustusIntegrations.ExternalAPI.Dto;
using AugustusWebApp.Facade;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AugustusWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IPostFacade _postFacade;

        public PostController(ILogger<UserController> logger, IPostFacade postFacade)
                              => (_logger, _postFacade) = (logger, postFacade);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ///
        [Route("GetPost")]
        [HttpGet]
        [ProducesResponseType(typeof(IList<PostDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<IList<PostDTO>>> GetPost(string userId)
        {
            var response = await _postFacade.GetPostAsync(userId);
            if (response == null)
                return NotFound($"Post for user Id: {userId} Not found");

            return Ok(response);
        }
    }
}
