using System.ComponentModel.DataAnnotations.Schema;

namespace Systematix.WebAPI.Models.LoginUsersInfo
{
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        [NotMapped]
        public List<string> Roles { get; set; }

        //public List<string> Roles { get; set; }

        // Navidation Property
        public List<User_Role> UserRoles { get; set; }
    }
}
