using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Hub.Mapping
{
    public class StackEventTypeMap : EntityTypeConfiguration<StackEventType>
    {
        public StackEventTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.StackEventTypeID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("StackEventType");
            this.Property(t => t.StackEventTypeID).HasColumnName("StackEventTypeID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Periodic).HasColumnName("Periodic");
            this.Property(t => t.DefaultFollowUpDuration).HasColumnName("DefaultFollowUpDuration");
        }
    }
}
