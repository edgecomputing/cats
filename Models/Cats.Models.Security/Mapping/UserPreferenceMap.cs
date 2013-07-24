using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Security.Mapping
{
    public class UserPreferenceMap : EntityTypeConfiguration<UserPreference>
    {
        public UserPreferenceMap()
        {
            // Primary Key
            this.HasKey(t => t.UserAccountId);

            // Properties
            this.Property(t => t.UserAccountId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.LanguageCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.Calendar)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.Keyboard)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.PreferedWeightMeasurment)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.DefaultTheme)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("UserPreference");
            this.Property(t => t.UserAccountId).HasColumnName("UserAccountId");
            this.Property(t => t.LanguageCode).HasColumnName("LanguageCode");
            this.Property(t => t.Calendar).HasColumnName("Calendar");
            this.Property(t => t.Keyboard).HasColumnName("Keyboard");
            this.Property(t => t.PreferedWeightMeasurment).HasColumnName("PreferedWeightMeasurment");
            this.Property(t => t.DefaultTheme).HasColumnName("DefaultTheme");

            // Relationships
            this.HasRequired(t => t.UserAccount)
                .WithOptional(t => t.UserPreference);

        }
    }
}
