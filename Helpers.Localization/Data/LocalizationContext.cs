
using System.Data.Entity;
using Helpers.Localization.Models;
//using Cats.Models.Security.Mapping;

namespace Helpers.Localization.Data
{
    public partial class LocalizationContext : DbContext
    {
        static LocalizationContext()
        {
            Database.SetInitializer<LocalizationContext>(null);
        }

        public LocalizationContext() : base("Name=LocalizationContext") { }

        // TODO: Add properties to access set of Poco classes
        public DbSet<LocalizedText> LocalizedTexts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           /* modelBuilder.Configurations.Add(new UserAccountMap());
            modelBuilder.Configurations.Add(new UserPreferenceMap());
            modelBuilder.Configurations.Add(new UserProfileMap());
            modelBuilder.Configurations.Add(new UserInfoMap());*/
        }
    }
}
