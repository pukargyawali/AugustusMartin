using System.Collections.Generic;
using System.Threading.Tasks;
using AugustusIntegrations.ExternalAPI.Dto;

namespace AugustusWebApp.Facade
{
    public interface IUserFacade
    {
        Task<IList<UserDTO>> GetAllUsers();

        Task<UserDTO> GetUserById(string Id);
    }
}
