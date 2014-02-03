using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class OtherDispatchAllocationMap : EntityTypeConfiguration<OtherDispatchAllocation>
    {
        public OtherDispatchAllocationMap()
        {
            // Primary Key
            this.HasKey(t => t.OtherDispatchAllocationID);

            // Properties
            this.Property(t => t.ReferenceNumber)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Remark)
                .HasMaxLength(4000);

            // Table & Column Mappings
            this.ToTable("OtherDispatchAllocation");
            this.Property(t => t.OtherDispatchAllocationID).HasColumnName("OtherDispatchAllocationID");
            this.Property(t => t.PartitionID).HasColumnName("PartitionID");
            this.Property(t => t.AgreementDate).HasColumnName("AgreementDate");
            this.Property(t => t.CommodityID).HasColumnName("CommodityID");
            this.Property(t => t.EstimatedDispatchDate).HasColumnName("EstimatedDispatchDate");
            this.Property(t => t.HubID).HasColumnName("HubID");
            this.Property(t => t.ToHubID).HasColumnName("ToHubID");
            this.Property(t => t.ProgramID).HasColumnName("ProgramID");
            this.Property(t => t.UnitID).HasColumnName("UnitID");
            this.Property(t => t.QuantityInUnit).HasColumnName("QuantityInUnit");
            this.Property(t => t.QuantityInMT).HasColumnName("QuantityInMT");
            this.Property(t => t.ReasonID).HasColumnName("ReasonID");
            this.Property(t => t.ReferenceNumber).HasColumnName("ReferenceNumber");
            this.Property(t => t.Remark).HasColumnName("Remark");
            this.Property(t => t.ShippingInstructionID).HasColumnName("ShippingInstructionID");
            this.Property(t => t.ProjectCodeID).HasColumnName("ProjectCodeID");
            this.Property(t => t.TransporterID).HasColumnName("TransporterID");
            this.Property(t => t.IsClosed).HasColumnName("IsClosed");

            // Relationships
            this.HasRequired(t => t.Commodity)
                .WithMany(t => t.OtherDispatchAllocations)
                .HasForeignKey(d => d.CommodityID);
            this.HasRequired(t => t.Hub)
                .WithMany(t => t.OtherDispatchAllocations)
                .HasForeignKey(d => d.HubID);
            this.HasRequired(t => t.Program)
                .WithMany(t => t.OtherDispatchAllocations)
                .HasForeignKey(d => d.ProgramID);
            this.HasRequired(t => t.ProjectCode)
                .WithMany(t => t.OtherDispatchAllocations)
                .HasForeignKey(d => d.ProjectCodeID);
            this.HasRequired(t => t.ShippingInstruction)
                .WithMany(t => t.OtherDispatchAllocations)
                .HasForeignKey(d => d.ShippingInstructionID);
            this.HasOptional(t => t.Transporter)
                .WithMany(t => t.OtherDispatchAllocations)
                .HasForeignKey(d => d.TransporterID);

        }
    }
}
