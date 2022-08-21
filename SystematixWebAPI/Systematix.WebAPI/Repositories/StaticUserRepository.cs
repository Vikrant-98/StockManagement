using Systematix.WebAPI.Models.LoginUsersInfo;
using Systematix.WebAPI.Repositories.UserRepositories;

namespace Systematix.WebAPI.Repositories
{
    public class StaticUserRepository : IUserRepository
    {
        private List<User> User = new List<User>()
        {
            //new User()
            //{
            //    FirstName = "Ram",
            //    LastName = "Wade",
            //    EmailAddress = "Ram@Gmail.com",
            //    ID = 1,
            //    UserName = "Ram",
            //    Password = "Ram@123",
            //    Roles = new List<string> { "Employee" }
            //},
            //new User()
            //{
            //    FirstName = "Sham",
            //    LastName = "Kadam",
            //    EmailAddress = "Sham@Gmail.com",
            //    ID = 2,
            //    UserName = "Systematix_Sham",
            //    Password = "Sham@123",
            //    Roles = new List<string> { "Admin","Employee" }
            //}
        };

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = User.Find(x => x.UserName.Equals(username, StringComparison.InvariantCultureIgnoreCase) &&
                x.Password == password);

            if (user != null)
            {
                return user;
            }

            return user;
        }
    }
}
