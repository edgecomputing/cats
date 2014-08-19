using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class SIPCAllocationMap : EntityTypeConfiguration<SIPCAllocation>
    {
        public SIPCAllocationMap()
        {
            // Primary Key
            this.HasKey(t =>  t.SIPCAllocationID);

            // Table & Column Mappings
            this.ToTable("SIPCAllocation");
            this.Property(t => t.SIPCAllocationID).HasColumnName("SIPCAllocationID");
            this.Property(t => t.FDPID).HasColumnName("FDPID");
            this.Property(t => t.RequisitionDetailID).HasColumnName("RequisitionDetailID");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.AllocatedAmount).HasColumnName("AllocatedAmount");
            this.Property(t => t.AllocationType).HasColumnName("AllocationType");
            this.Property(t => t.TransactionGroupID).HasColumnName("TransactionGroupID");
            this.Property(t => t.PartitionId).HasColumnName("PartitionId");
            this.Property(t => t.CommitType).HasColumnName("CommitType");
            // Relationships
            this.HasRequired(t => t.ReliefRequisitionDetail)
                .WithMany(t => t.SIPCAllocations)
                .HasForeignKey(d => d.RequisitionDetailID);

            this.HasRequired(t => t.TransactionGroup)
                .WithMany(t => t.SIPCAllocations)
                .HasForeignKey(d => d.TransactionGroupID);
        }
    }
}
