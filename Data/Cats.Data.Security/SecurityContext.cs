
using System.Data.Entity;
using Cats.Models.Security;
using Cats.Models.Security.Mapping;

namespace Cats.Data.Security
{
    public class SecurityContext : DbContext
    {
        static SecurityContext()
        {
            Database.SetInitializer<SecurityContext>(null);
        }

        public SecurityContext() : base("Name=SecurityContext") { }

        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<UserPreference> UserPreferences { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<UserInfo> UsersInfos { get; set; }
        public DbSet<ForgetPasswordRequest> ForgetPasswordRequests { get; set;}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserAccountMap());
            modelBuilder.Configurations.Add(new UserPreferenceMap());
            modelBuilder.Configurations.Add(new UserProfileMap());
            modelBuilder.Configurations.Add(new UserInfoMap());
            modelBuilder.Configurations.Add(new ForgetPasswordRequestMap());
        }
    }
}
