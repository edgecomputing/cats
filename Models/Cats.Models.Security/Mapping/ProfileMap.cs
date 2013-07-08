using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Security.Mapping
{
    public class ProfileMap : EntityTypeConfiguration<Profile>
    {
        public ProfileMap()
        {
            // Primary Key
            this.HasKey(t => t.UserId);

            // Properties
            this.Property(t => t.UserId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Profile");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.UILanguage).HasColumnName("UILanguage");
            this.Property(t => t.KeyboardLanguage).HasColumnName("KeyboardLanguage");
            this.Property(t => t.Calendar).HasColumnName("Calendar");

            // Relationships
            //this.HasRequired(t => t.User)
            //    .WithOptional(t => t.Profile);

        }
    }
}
