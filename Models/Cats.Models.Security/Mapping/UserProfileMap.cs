using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Security.Mapping
{
    public class UserProfileMap : EntityTypeConfiguration<UserProfile>
    {
        public UserProfileMap()
        {
            // Primary Key
            this.HasKey(t => t.UserAccountId);

            // Properties
            this.Property(t => t.UserAccountId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.GrandFatherName)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Email)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("UserProfile");
            this.Property(t => t.UserAccountId).HasColumnName("UserProfileId");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.GrandFatherName).HasColumnName("GrandFatherName");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.CaseTeam).HasColumnName("CaseTeam");

            // Relationships
            this.HasRequired(t => t.UserAccount)
                .WithOptional(t => t.UserProfile);

        }
    }
}
