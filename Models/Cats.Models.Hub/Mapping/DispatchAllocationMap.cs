using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Hubs.Mapping
{
    public class DispatchAllocationMap : EntityTypeConfiguration<DispatchAllocation>
    {
        public DispatchAllocationMap()
        {
            // Primary Key
            this.HasKey(t => t.DispatchAllocationID);

            // Properties
            this.Property(t => t.RequisitionNo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.BidRefNo)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("DispatchAllocation");
            this.Property(t => t.DispatchAllocationID).HasColumnName("DispatchAllocationID");
            this.Property(t => t.PartitionId).HasColumnName("PartitionId");
            this.Property(t => t.HubID).HasColumnName("HubID");
            this.Property(t => t.StoreID).HasColumnName("StoreID");
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.Month).HasColumnName("Month");
            this.Property(t => t.Round).HasColumnName("Round");
            this.Property(t => t.DonorID).HasColumnName("DonorID");
            this.Property(t => t.ProgramID).HasColumnName("ProgramID");
            this.Property(t => t.CommodityID).HasColumnName("CommodityID");
            this.Property(t => t.RequisitionNo).HasColumnName("RequisitionNo");
            this.Property(t => t.BidRefNo).HasColumnName("BidRefNo");
            this.Property(t => t.ContractStartDate).HasColumnName("ContractStartDate");
            this.Property(t => t.ContractEndDate).HasColumnName("ContractEndDate");
            this.Property(t => t.Beneficiery).HasColumnName("Beneficiery");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.Unit).HasColumnName("Unit");
            this.Property(t => t.TransporterID).HasColumnName("TransporterID");
            this.Property(t => t.FDPID).HasColumnName("FDPID");
            this.Property(t => t.ShippingInstructionID).HasColumnName("ShippingInstructionID");
            this.Property(t => t.ProjectCodeID).HasColumnName("ProjectCodeID");
            this.Property(t => t.TransportOrderID).HasColumnName("TransportOrderID");
            this.Property(t => t.IsClosed).HasColumnName("IsClosed");

            // Relationships
            this.HasRequired(t => t.Commodity)
                .WithMany(t => t.DispatchAllocations)
                .HasForeignKey(d => d.CommodityID);
            this.HasRequired(t => t.FDP)
                .WithMany(t => t.DispatchAllocations)
                .HasForeignKey(d => d.FDPID);
            this.HasOptional(t => t.Program)
                .WithMany(t => t.DispatchAllocations)
                .HasForeignKey(d => d.ProgramID);
            this.HasRequired(t => t.Hub)
                .WithMany(t => t.DispatchAllocations)
                .HasForeignKey(d => d.HubID);
            this.HasOptional(t => t.ProjectCode)
                .WithMany(t => t.DispatchAllocations)
                .HasForeignKey(d => d.ProjectCodeID);
            this.HasOptional(t => t.ShippingInstruction)
                .WithMany(t => t.DispatchAllocations)
                .HasForeignKey(d => d.ShippingInstructionID);
            this.HasOptional(t => t.Transporter)
                .WithMany(t => t.DispatchAllocations)
                .HasForeignKey(d => d.TransporterID);

        }
    }
}
