namespace Systematix.WebAPI.Models.LoginUsersInfo
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navidation Property
        public List<User_Role> UserRoles { get; set; }
    }
}
