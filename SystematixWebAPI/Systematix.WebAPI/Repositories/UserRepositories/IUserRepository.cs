using Systematix.WebAPI.Models.LoginUsersInfo;

namespace Systematix.WebAPI.Repositories.UserRepositories
{
    public interface IUserRepository
    {
        Task<User> AuthenticateAsync(string username, string password);
    }
}
