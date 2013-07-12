
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class TransportOrderDetailMap : EntityTypeConfiguration<TransportOrderDetail>
    {
        public TransportOrderDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.TransportOrderDetailID);

            // Properties
            // Table & Column Mappings
            this.ToTable("TransportOrderDetail", "Procurement");
            this.Property(t => t.TransportOrderDetailID).HasColumnName("TransportOrderDetailID");
            this.Property(t => t.TransportOrderID).HasColumnName("TransportOrderID");
            this.Property(t => t.FdpID).HasColumnName("FdpID");
            this.Property(t => t.SourceWarehouseID).HasColumnName("SourceWarehouseID");
            this.Property(t => t.QuantityQtl).HasColumnName("QuantityQtl");
            this.Property(t => t.DistanceFromOrigin).HasColumnName("DistanceFromOrigin");
            this.Property(t => t.TariffPerQtl).HasColumnName("TariffPerQtl");
            this.Property(t => t.RequisitionID).HasColumnName("RequisitionID");
            this.Property(t => t.CommodityID).HasColumnName("CommodityID");
            this.Property(t => t.ZoneID).HasColumnName("ZoneID");
            this.Property(t => t.DonorID).HasColumnName("DonorID");

            // Relationships
            this.HasOptional(t => t.AdminUnit)
                .WithMany(t => t.TransportOrderDetails)
                .HasForeignKey(d => d.ZoneID);
            this.HasRequired(t => t.Commodity)
                .WithMany(t => t.TransportOrderDetails)
                .HasForeignKey(d => d.CommodityID);
            this.HasOptional(t => t.Donor)
                .WithMany(t => t.TransportOrderDetails)
                .HasForeignKey(d => d.DonorID);
            this.HasRequired(t => t.FDP)
                .WithMany(t => t.TransportOrderDetails)
                .HasForeignKey(d => d.FdpID);
            this.HasRequired(t => t.Hub)
                .WithMany(t => t.TransportOrderDetails)
                .HasForeignKey(d => d.SourceWarehouseID);
            this.HasRequired(t => t.ReliefRequisition)
                .WithMany(t => t.TransportOrderDetails)
                .HasForeignKey(d => d.RequisitionID);
            this.HasRequired(t => t.TransportOrder)
                .WithMany(t => t.TransportOrderDetails)
                .HasForeignKey(d => d.TransportOrderID);



        }
    }
}
