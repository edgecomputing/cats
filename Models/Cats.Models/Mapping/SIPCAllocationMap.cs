using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class SIPCAllocationMap : EntityTypeConfiguration<SIPCAllocation>
    {
        public SIPCAllocationMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ISPCAllocationID, t.FDPID, t.RequisitionDetailID, t.Code, t.AllocatedAmount, t.AllocationType });

            // Table & Column Mappings
            this.ToTable("SIPCAllocation");
            this.Property(t => t.ISPCAllocationID).HasColumnName("ISPCAllocationID");
            this.Property(t => t.FDPID).HasColumnName("FDPID");
            this.Property(t => t.RequisitionDetailID).HasColumnName("RequisitionDetailID");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.AllocatedAmount).HasColumnName("AllocatedAmount");
            this.Property(t => t.AllocationType).HasColumnName("AllocationType");

            // Relationships
            this.HasRequired(t => t.ReliefRequisitionDetail)
                .WithMany(t => t.SIPCAllocations)
                .HasForeignKey(d => d.RequisitionDetailID);

        }
    }
}
