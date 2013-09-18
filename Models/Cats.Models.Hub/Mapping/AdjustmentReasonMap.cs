using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Hub.Mapping
{
    public class AdjustmentReasonMap : EntityTypeConfiguration<AdjustmentReason>
    {
        public AdjustmentReasonMap()
        {
            // Primary Key
            this.HasKey(t => t.AdjustmentReasonID);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(50);

            this.Property(t => t.Direction)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("AdjustmentReason");
            this.Property(t => t.AdjustmentReasonID).HasColumnName("AdjustmentReasonID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Direction).HasColumnName("Direction");
        }
    }
}
