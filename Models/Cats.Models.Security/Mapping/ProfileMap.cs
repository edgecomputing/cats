using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Security.Mapping
{
    public class ProfileMap : EntityTypeConfiguration<Profile>
    {
        public ProfileMap()
        {
            // Primary Key
            this.HasKey(t => t.ProfileId);

            // Properties
            // Table & Column Mappings
            this.ToTable("Profile");
            this.Property(t => t.ProfileId).HasColumnName("ProfileId");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.UILanguage).HasColumnName("UILanguage");
            this.Property(t => t.KeyboardLanguage).HasColumnName("KeyboardLanguage");
            this.Property(t => t.Calendar).HasColumnName("Calendar");
        }
    }
}
