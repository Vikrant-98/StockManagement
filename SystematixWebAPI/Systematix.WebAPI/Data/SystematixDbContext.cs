using Microsoft.EntityFrameworkCore;
using Systematix.WebAPI.Models.DTO.ClientDetails;
using Systematix.WebAPI.Models.DTO.Holdings;
using Systematix.WebAPI.Models.DTO.Ledger;
using Systematix.WebAPI.Models.DTO.Stocks;
using Systematix.WebAPI.Models.DTO.UserDetails;
using Systematix.WebAPI.Models.EmployeeDetailsInfo;
using Systematix.WebAPI.Models.LoginUsersInfo;

namespace Systematix.WebAPI.Data
{
    public class SystematixDbContext : DbContext
    {
        public SystematixDbContext(DbContextOptions<SystematixDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User_Role>()
                .HasOne(x => x.Role)
                .WithMany(y => y.UserRoles)
                .HasForeignKey(x => x.RoleId);

            modelBuilder.Entity<User_Role>()
                .HasOne(x => x.User)
                .WithMany(y => y.UserRoles)
                .HasForeignKey(x => x.UserId);
        }

        public DbSet<ClientDetail> tbl_EmployeeDetails { get; set; }
        public DbSet<ClientInformation> tbl_Client { get; set; }
        public DbSet<ClientDetails> tbl_ClientDetails { get; set; }
        public DbSet<ClientAddress> tbl_ClientAddress { get; set; }
        public DbSet<ClientHoldings> tbl_ClientHoldings { get; set; }
        public DbSet<Ledger> tbl_ClientLedger { get; set; }
        public DbSet<UserMaster> tbl_UserMaster { get; set; }
        public DbSet<UserAddress> tbl_UserAddress { get; set; }
        public DbSet<StockDetails> tbl_StockDetails { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User_Role> User_Roles { get; set; }

    }
}

