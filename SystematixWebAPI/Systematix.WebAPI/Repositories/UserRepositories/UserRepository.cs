using Microsoft.EntityFrameworkCore;
using Systematix.WebAPI.Data;
using Systematix.WebAPI.Models.LoginUsersInfo;

namespace Systematix.WebAPI.Repositories.UserRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SystematixDbContext systematixDbContext;
        public UserRepository(SystematixDbContext systematixDbContext)
        {
            this.systematixDbContext = systematixDbContext;
        }

        public SystematixDbContext SystematixDbContext { get; }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await systematixDbContext.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == username.ToLower() &&
                x.Password == password);

            if (user == null)
            {
                return null;
            }

            var userRoles = await systematixDbContext.User_Roles.Where(x => x.UserId == user.ID).ToListAsync();

            if (userRoles.Any())
            {
                user.Roles = new List<string>();
                foreach (var UserR in userRoles)
                {
                    var _role = await systematixDbContext.Roles.FirstOrDefaultAsync(x => x.Id == UserR.RoleId);
                    if (_role != null)
                    {
                        user.Roles.Add(_role.Name);
                    }
                }
            }

            user.Password = null;
            return user;

        }
    }
}
