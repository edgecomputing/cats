using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Hubs.Mapping
{
    public class InternalMovementMap : EntityTypeConfiguration<InternalMovement>
    {
        public InternalMovementMap()
        {
            // Primary Key
            this.HasKey(t => t.InternalMovementID);

            // Properties
            this.Property(t => t.ReferenceNumber)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Notes)
                .HasMaxLength(4000);

            this.Property(t => t.ApprovedBy)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("InternalMovement");
            this.Property(t => t.InternalMovementID).HasColumnName("InternalMovementID");
            this.Property(t => t.PartitionID).HasColumnName("PartitionID");
            this.Property(t => t.HubID).HasColumnName("HubID");
            this.Property(t => t.TransactionGroupID).HasColumnName("TransactionGroupID");
            this.Property(t => t.TransferDate).HasColumnName("TransferDate");
            this.Property(t => t.ReferenceNumber).HasColumnName("ReferenceNumber");
            this.Property(t => t.DReason).HasColumnName("DReason");
            this.Property(t => t.Notes).HasColumnName("Notes");
            this.Property(t => t.ApprovedBy).HasColumnName("ApprovedBy");

            // Relationships
            this.HasRequired(t => t.Detail)
                .WithMany(t => t.InternalMovements)
                .HasForeignKey(d => d.DReason);
            this.HasOptional(t => t.TransactionGroup)
                .WithMany(t => t.InternalMovements)
                .HasForeignKey(d => d.TransactionGroupID);

        }
    }
}
