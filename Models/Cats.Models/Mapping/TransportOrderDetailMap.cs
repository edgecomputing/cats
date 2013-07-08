using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            // Relationships
            this.HasRequired(t => t.FDP)
                .WithMany(t => t.TransportOrderDetails)
                .HasForeignKey(d => d.FdpID);
            this.HasRequired(t => t.Hub)
                .WithMany(t => t.TransportOrderDeatils)
                .HasForeignKey(d => d.SourceWarehouseID);
            this.HasRequired(t => t.TransportOrder)
                .WithMany(t => t.TransportOrderDetails)
                .HasForeignKey(d => d.TransportOrderID);

        }
    }
}
