using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping

{
    public class HubAllocationMap : EntityTypeConfiguration<HubAllocation>
    {
        public HubAllocationMap()
        {
            // Primary Key
            this.HasKey(t => t.HubAllocationID);

            // Properties
            // Table & Column Mappings
            this.ToTable("HubAllocation", "Logistics");
            this.Property(t => t.HubAllocationID).HasColumnName("HubAllocationID");
            this.Property(t => t.RequisitionID).HasColumnName("RequisitionID");
            this.Property(t => t.HubID).HasColumnName("HubID");
            this.Property(t => t.AllocationDate).HasColumnName("AllocationDate");
            this.Property(t => t.AllocatedBy).HasColumnName("AllocatedBy");
            this.Property(t => t.PartitionId).HasColumnName("PartitionId");
             // Relationships
            this.HasRequired(t => t.Hub)
                .WithMany(t => t.HubAllocations)
                .HasForeignKey(d => d.HubID);
            //this.HasRequired(t => t.UserProfile)
            //    .WithMany(t => t.HubAllocations)
            //    .HasForeignKey(d => d.AllocatedBy);
            this.HasRequired(t => t.ReliefRequisition)
                .WithMany(t => t.HubAllocations)
                .HasForeignKey(d => d.RequisitionID);


        }
    }
}
