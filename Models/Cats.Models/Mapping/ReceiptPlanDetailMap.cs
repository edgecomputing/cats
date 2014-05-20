using System.Data.Entity.ModelConfiguration;
using Cats.Models;
namespace Cats.Models.Mapping
{
    public class ReceiptPlanDetailMap : EntityTypeConfiguration<ReceiptPlanDetail>
    {
        public ReceiptPlanDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.ReceiptDetailId);

            // Properties
            // Table & Column Mappings
            this.ToTable("ReceiptPlanDetail");
            this.Property(t => t.ReceiptDetailId).HasColumnName("ReceiptDetailId");
            this.Property(t => t.ReceiptHeaderId).HasColumnName("ReceiptHeaderId");
            this.Property(t => t.HubId).HasColumnName("HubId");
            this.Property(t => t.Allocated).HasColumnName("Allocated");
            this.Property(t => t.Received).HasColumnName("Received");
            this.Property(t => t.Balance).HasColumnName("Balance");
            this.Property(t => t.PartitionId).HasColumnName("PartitionId");

            // Relationships
            this.HasRequired(t => t.Hub)
                .WithMany(t => t.ReceiptPlanDetails)
                .HasForeignKey(d => d.HubId);
            this.HasRequired(t => t.ReceiptPlan)
                .WithMany(t => t.ReceiptPlanDetails)
                .HasForeignKey(d => d.ReceiptHeaderId);


        }
    }
}
