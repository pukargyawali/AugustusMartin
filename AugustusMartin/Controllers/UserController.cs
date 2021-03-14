using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AugustusIntegrations.ExternalAPI.Dto;
using AugustusWebApp.Facade;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AugustusWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserFacade _userFacade;

        public UserController(ILogger<UserController> logger, IUserFacade userFacade) => (_logger, _userFacade) = (logger, userFacade);
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ///
        [Route("GetAllUsers")]
        [HttpGet]
        [ProducesResponseType(typeof(IList<UserDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<IList<UserDTO>>> GetAllUsers()
        {
            var response = await _userFacade.GetAllUsers();
            if (response == null)
                return NotFound("No users were found");
                        
            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ///
        [Route("GetUserById")]
        [HttpGet]
        [ProducesResponseType(typeof(UserDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<UserDTO>> GetUser(string userId)
        {
            var response = await _userFacade.GetUserById(userId);
            if (response == null)
                return NotFound($"User with user Id: {userId} Not found");
            return Ok(response);
        }
    }
}
