using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class ReceiptAllocationMap : EntityTypeConfiguration<ReceiptAllocation>
    {
        public ReceiptAllocationMap()
        {
            // Primary Key
            this.HasKey(t => t.ReceiptAllocationID);

            // Properties
            this.Property(t => t.ProjectNumber)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SINumber)
                .HasMaxLength(50);

            this.Property(t => t.PurchaseOrder)
                .HasMaxLength(50);

            this.Property(t => t.SupplierName)
                .HasMaxLength(50);

            this.Property(t => t.OtherDocumentationRef)
                .HasMaxLength(50);

            this.Property(t => t.Remark)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ReceiptAllocation");
            this.Property(t => t.ReceiptAllocationID).HasColumnName("ReceiptAllocationID");
            this.Property(t => t.PartitionID).HasColumnName("PartitionId");
            this.Property(t => t.IsCommited).HasColumnName("IsCommited");
            this.Property(t => t.ETA).HasColumnName("ETA");
            this.Property(t => t.ProjectNumber).HasColumnName("ProjectNumber");
            this.Property(t => t.GiftCertificateDetailID).HasColumnName("GiftCertificateDetailID");
            this.Property(t => t.CommodityID).HasColumnName("CommodityID");
            this.Property(t => t.SINumber).HasColumnName("SINumber");
            this.Property(t => t.UnitID).HasColumnName("UnitID");
            this.Property(t => t.QuantityInUnit).HasColumnName("QuantityInUnit");
            this.Property(t => t.QuantityInMT).HasColumnName("QuantityInMT");
            this.Property(t => t.HubID).HasColumnName("HubID");
            this.Property(t => t.DonorID).HasColumnName("DonorID");
            this.Property(t => t.ProgramID).HasColumnName("ProgramID");
            this.Property(t => t.CommoditySourceID).HasColumnName("CommoditySourceID");
            this.Property(t => t.IsClosed).HasColumnName("IsClosed");
            this.Property(t => t.PurchaseOrder).HasColumnName("PurchaseOrder");
            this.Property(t => t.SupplierName).HasColumnName("SupplierName");
            this.Property(t => t.SourceHubID).HasColumnName("SourceHubID");
            this.Property(t => t.OtherDocumentationRef).HasColumnName("OtherDocumentationRef");
            this.Property(t => t.Remark).HasColumnName("Remark");

             //Relationships
            this.HasRequired(t => t.Commodity)
                .WithMany(t => t.ReceiptAllocations)
                .HasForeignKey(d => d.CommodityID);
            this.HasRequired(t => t.CommoditySource)
                .WithMany(t => t.ReceiptAllocations)
                .HasForeignKey(d => d.CommoditySourceID);
            this.HasOptional(t => t.Donor)
                .WithMany(t => t.ReceiptAllocations)
                .HasForeignKey(d => d.DonorID);
            this.HasOptional(t => t.GiftCertificateDetail)
                .WithMany(t => t.ReceiptAllocations)
                .HasForeignKey(d => d.GiftCertificateDetailID);
            this.HasRequired(t => t.Hub)
                .WithMany(t => t.ReceiptAllocations)
                .HasForeignKey(d => d.HubID);
            this.HasOptional(t => t.Hub1)
                .WithMany(t => t.ReceiptAllocations1)
                .HasForeignKey(d => d.SourceHubID);
            this.HasRequired(t => t.Program)
                .WithMany(t => t.ReceiptAllocations)
                .HasForeignKey(d => d.ProgramID);
            this.HasOptional(t => t.Unit)
                .WithMany(t => t.ReceiptAllocations)
                .HasForeignKey(d => d.UnitID);
        }
    }
}
