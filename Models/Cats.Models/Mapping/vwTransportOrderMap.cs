using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
    public class vwTransportOrderMap : EntityTypeConfiguration<vwTransportOrder>
    {
        public vwTransportOrderMap()
        {
            // Primary Key
          //  this.HasKey(t => new { t.TransportOrderID, t.TransportOrderNo, t.OrderDate, t.RequestedDispatchDate, t.OrderExpiryDate, t.BidDocumentNo, t.TransporterID, t.TransportOrderDetailID, t.FdpID, t.SourceWarehouseID, t.QuantityQtl, t.TariffPerQtl, t.RequisitionID, t.CommodityID, t.FDPName, t.HubName, t.CommodityName });
            this.HasKey(t => new {t.TransportOrderDetailID, t.TransportOrderID});
            
            // Properties
            this.Property(t => t.TransportOrderID);
            
            this.Property(t => t.TransportOrderNo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.BidDocumentNo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.PerformanceBondReceiptNo)
                .HasMaxLength(50);

            this.Property(t => t.TransporterID);

            this.Property(t => t.ConsignerName)
                .HasMaxLength(50);

            this.Property(t => t.TransporterSignedName)
                .HasMaxLength(50);

            this.Property(t => t.ContractNumber)
                .HasMaxLength(50);

            this.Property(t => t.TransportOrderDetailID);

            this.Property(t => t.FdpID);

            this.Property(t => t.SourceWarehouseID);

            this.Property(t => t.QuantityQtl);

            this.Property(t => t.TariffPerQtl);

            this.Property(t => t.RequisitionID);

            this.Property(t => t.CommodityID);

            this.Property(t => t.FDPName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.HubName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.RequisitionNo)
                .HasMaxLength(255);

            this.Property(t => t.CommodityName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.DonorName)
                .HasMaxLength(50);

            this.Property(t => t.WoredaName)
                .HasMaxLength(50);

            this.Property(t => t.ZoneName)
                .HasMaxLength(50);

            this.Property(t => t.TransporterName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("vwTransportOrder");
            this.Property(t => t.TransportOrderID).HasColumnName("TransportOrderID");
            this.Property(t => t.TransportOrderNo).HasColumnName("TransportOrderNo");
            this.Property(t => t.OrderDate).HasColumnName("OrderDate");
            this.Property(t => t.RequestedDispatchDate).HasColumnName("RequestedDispatchDate");
            this.Property(t => t.OrderExpiryDate).HasColumnName("OrderExpiryDate");
            this.Property(t => t.BidDocumentNo).HasColumnName("BidDocumentNo");
            this.Property(t => t.PerformanceBondReceiptNo).HasColumnName("PerformanceBondReceiptNo");
            this.Property(t => t.TransporterID).HasColumnName("TransporterID");
            this.Property(t => t.ConsignerName).HasColumnName("ConsignerName");
            this.Property(t => t.TransporterSignedName).HasColumnName("TransporterSignedName");
            this.Property(t => t.ConsignerDate).HasColumnName("ConsignerDate");
            this.Property(t => t.TransporterSignedDate).HasColumnName("TransporterSignedDate");
            this.Property(t => t.ContractNumber).HasColumnName("ContractNumber");
            this.Property(t => t.TransportOrderDetailID).HasColumnName("TransportOrderDetailID");
            this.Property(t => t.FdpID).HasColumnName("FdpID");
            this.Property(t => t.SourceWarehouseID).HasColumnName("SourceWarehouseID");
            this.Property(t => t.QuantityQtl).HasColumnName("QuantityQtl");
            this.Property(t => t.DistanceFromOrigin).HasColumnName("DistanceFromOrigin");
            this.Property(t => t.TariffPerQtl).HasColumnName("TariffPerQtl");
            this.Property(t => t.RequisitionID).HasColumnName("RequisitionID");
            this.Property(t => t.CommodityID).HasColumnName("CommodityID");
            this.Property(t => t.ZoneID).HasColumnName("ZoneID");
            this.Property(t => t.DonorID).HasColumnName("DonorID");
            this.Property(t => t.FDPName).HasColumnName("FDPName");
            this.Property(t => t.HubName).HasColumnName("HubName");
            this.Property(t => t.RequisitionNo).HasColumnName("RequisitionNo");
            this.Property(t => t.CommodityName).HasColumnName("CommodityName");
            this.Property(t => t.DonorName).HasColumnName("DonorName");
            this.Property(t => t.WoredaName).HasColumnName("WoredaName");
            this.Property(t => t.ZoneName).HasColumnName("ZoneName");
            this.Property(t => t.TransporterName).HasColumnName("TransporterName");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
        }
    }
}
