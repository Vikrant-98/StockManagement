using Systematix.WebAPI.Models.DTO.ClientDetails;
using Systematix.WebAPI.Models.LoginUsersInfo;

namespace Systematix.WebAPI.Repositories.TokenHandlerRepositories
{
    public interface ITokenHandler
    {
        Task<string> CreateTokenAsync(User user);
        Task<string> TokenAsync(ClientInformation user);
    }
}
