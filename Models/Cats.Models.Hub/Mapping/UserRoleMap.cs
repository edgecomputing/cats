using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Hub.Mapping
{
    public class UserRoleMap : EntityTypeConfiguration<UserRole>
    {
        public UserRoleMap()
        {
            // Primary Key
            this.HasKey(t => t.UserRoleID);

            // Properties
            // Table & Column Mappings
            this.ToTable("UserRole");
            this.Property(t => t.UserRoleID).HasColumnName("UserRoleID");
            this.Property(t => t.UserProfileID).HasColumnName("UserProfileID");
            this.Property(t => t.RoleID).HasColumnName("RoleID");

            // Relationships
            this.HasRequired(t => t.Role)
                .WithMany(t => t.UserRoles)
                .HasForeignKey(d => d.RoleID);
            this.HasRequired(t => t.UserProfile)
                .WithMany(t => t.UserRoles)
                .HasForeignKey(d => d.UserProfileID);

        }
    }
}
