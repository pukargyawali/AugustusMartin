using System.Collections.Generic;
using System.Threading.Tasks;
using AugustusIntegrations.ExternalAPI.Dto;

namespace AugustusWebApp.Facade
{
    public interface IPostFacade
    {
        Task<IList<PostDTO>> GetPostAsync(string userId);
    }
}
