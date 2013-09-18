using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Hub.Mapping
{
    public class AdjustmentMap : EntityTypeConfiguration<Adjustment>
    {
        public AdjustmentMap()
        {
            // Primary Key
            this.HasKey(t => t.AdjustmentID);

            // Properties
            this.Property(t => t.AdjustmentDirection)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.ApprovedBy)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Remarks)
                .HasMaxLength(500);

            this.Property(t => t.ReferenceNumber)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.StoreManName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Adjustment");
            this.Property(t => t.AdjustmentID).HasColumnName("AdjustmentID");
            this.Property(t => t.PartitionID).HasColumnName("PartitionID");
            this.Property(t => t.TransactionGroupID).HasColumnName("TransactionGroupID");
            this.Property(t => t.HubID).HasColumnName("HubID");
            this.Property(t => t.AdjustmentReasonID).HasColumnName("AdjustmentReasonID");
            this.Property(t => t.AdjustmentDirection).HasColumnName("AdjustmentDirection");
            this.Property(t => t.AdjustmentDate).HasColumnName("AdjustmentDate");
            this.Property(t => t.ApprovedBy).HasColumnName("ApprovedBy");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.UserProfileID).HasColumnName("UserProfileID");
            this.Property(t => t.ReferenceNumber).HasColumnName("ReferenceNumber");
            this.Property(t => t.StoreManName).HasColumnName("StoreManName");

            // Relationships
            this.HasRequired(t => t.AdjustmentReason)
                .WithMany(t => t.Adjustments)
                .HasForeignKey(d => d.AdjustmentReasonID);
            this.HasOptional(t => t.TransactionGroup)
                .WithMany(t => t.Adjustments)
                .HasForeignKey(d => d.TransactionGroupID);
            this.HasRequired(t => t.UserProfile)
                .WithMany(t => t.Adjustments)
                .HasForeignKey(d => d.UserProfileID);

        }
    }
}
