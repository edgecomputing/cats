using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Hub.Mapping
{
    public class SessionHistoryMap : EntityTypeConfiguration<SessionHistory>
    {
        public SessionHistoryMap()
        {
            // Primary Key
            this.HasKey(t => t.SessionHistoryID);

            // Properties
            this.Property(t => t.UserName)
                .HasMaxLength(50);

            this.Property(t => t.Password)
                .HasMaxLength(50);

            this.Property(t => t.WorkstationName)
                .HasMaxLength(50);

            this.Property(t => t.IPAddress)
                .HasMaxLength(50);

            this.Property(t => t.ApplicationName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SessionHistory");
            this.Property(t => t.SessionHistoryID).HasColumnName("SessionHistoryID");
            this.Property(t => t.PartitionID).HasColumnName("PartitionID");
            this.Property(t => t.UserProfileID).HasColumnName("UserProfileID");
            this.Property(t => t.RoleID).HasColumnName("RoleID");
            this.Property(t => t.LoginDate).HasColumnName("LoginDate");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.WorkstationName).HasColumnName("WorkstationName");
            this.Property(t => t.IPAddress).HasColumnName("IPAddress");
            this.Property(t => t.ApplicationName).HasColumnName("ApplicationName");

            // Relationships
            this.HasOptional(t => t.Role)
                .WithMany(t => t.SessionHistories)
                .HasForeignKey(d => d.RoleID);
            this.HasOptional(t => t.UserProfile)
                .WithMany(t => t.SessionHistories)
                .HasForeignKey(d => d.UserProfileID);

        }
    }
}
