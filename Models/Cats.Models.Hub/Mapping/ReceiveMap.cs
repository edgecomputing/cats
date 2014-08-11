using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Hubs.Mapping
{
    public class ReceiveMap : EntityTypeConfiguration<Receive>
    {
        public ReceiveMap()
        {
            // Primary Key
            this.HasKey(t => t.ReceiveID);

            // Properties
            this.Property(t => t.GRN)
                .IsRequired()
                .HasMaxLength(7);

            this.Property(t => t.PlateNo_Prime)
                .HasMaxLength(50);

            this.Property(t => t.PlateNo_Trailer)
                .HasMaxLength(50);

            this.Property(t => t.DriverName)
                .HasMaxLength(50);

            this.Property(t => t.WeightBridgeTicketNumber)
                .HasMaxLength(10);

            this.Property(t => t.WayBillNo)
                .HasMaxLength(50);

            this.Property(t => t.Remark)
                .HasMaxLength(4000);

            this.Property(t => t.VesselName)
                .HasMaxLength(50);

            this.Property(t => t.ReceivedByStoreMan)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.PortName)
                .HasMaxLength(50);

            this.Property(t => t.PurchaseOrder)
                .HasMaxLength(50);

            this.Property(t => t.SupplierName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Receive");
            this.Property(t => t.ReceiveID).HasColumnName("ReceiveID");
            this.Property(t => t.PartitionId).HasColumnName("PartitionId");
            this.Property(t => t.HubID).HasColumnName("HubID");
            this.Property(t => t.GRN).HasColumnName("GRN");
            this.Property(t => t.CommodityTypeID).HasColumnName("CommodityTypeID");
            this.Property(t => t.SourceDonorID).HasColumnName("SourceDonorID");
            this.Property(t => t.ResponsibleDonorID).HasColumnName("ResponsibleDonorID");
            this.Property(t => t.TransporterID).HasColumnName("TransporterID");
            this.Property(t => t.PlateNo_Prime).HasColumnName("PlateNo_Prime");
            this.Property(t => t.PlateNo_Trailer).HasColumnName("PlateNo_Trailer");
            this.Property(t => t.DriverName).HasColumnName("DriverName");
            this.Property(t => t.WeightBridgeTicketNumber).HasColumnName("WeightBridgeTicketNumber");
            this.Property(t => t.WeightBeforeUnloading).HasColumnName("WeightBeforeUnloading").HasPrecision(18, 4);
            this.Property(t => t.WeightAfterUnloading).HasColumnName("WeightAfterUnloading").HasPrecision(18, 4); 
            this.Property(t => t.ReceiptDate).HasColumnName("ReceiptDate");
            this.Property(t => t.UserProfileID).HasColumnName("UserProfileID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.WayBillNo).HasColumnName("WayBillNo");
            this.Property(t => t.CommoditySourceID).HasColumnName("CommoditySourceID");
            this.Property(t => t.Remark).HasColumnName("Remark");
            this.Property(t => t.VesselName).HasColumnName("VesselName");
            this.Property(t => t.ReceivedByStoreMan).HasColumnName("ReceivedByStoreMan");
            this.Property(t => t.PortName).HasColumnName("PortName");
            this.Property(t => t.PurchaseOrder).HasColumnName("PurchaseOrder");
            this.Property(t => t.SupplierName).HasColumnName("SupplierName");
            this.Property(t => t.ReceiptAllocationID).HasColumnName("ReceiptAllocationID");

            // Relationships
            this.HasRequired(t => t.CommoditySource)
                .WithMany(t => t.Receives)
                .HasForeignKey(d => d.CommoditySourceID);
            this.HasRequired(t => t.CommodityType)
                .WithMany(t => t.Receives)
                .HasForeignKey(d => d.CommodityTypeID);
            //this.HasOptional(t => t.Donor)
            //    .WithMany(t => t.Receives)
            //    .HasForeignKey(d => d.SourceDonorID);
            //this.HasOptional(t => t.Donor1)
            //    .WithMany(t => t.Receives1)
            //    .HasForeignKey(d => d.ResponsibleDonorID);
            this.HasRequired(t => t.Hub)
                .WithMany(t => t.Receives)
                .HasForeignKey(d => d.HubID);
            this.HasOptional(t => t.ReceiptAllocation)
                .WithMany(t => t.Receives)
                .HasForeignKey(d => d.ReceiptAllocationID);
            this.HasRequired(t => t.Transporter)
                .WithMany(t => t.Receives)
                .HasForeignKey(d => d.TransporterID);
            this.HasRequired(t => t.UserProfile)
                .WithMany(t => t.Receives)
                .HasForeignKey(d => d.UserProfileID);

        }
    }
}
