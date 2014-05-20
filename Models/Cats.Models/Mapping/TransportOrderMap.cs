using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
    public class TransportOrderMap : EntityTypeConfiguration<TransportOrder>
    {
        public TransportOrderMap()
        {
            // Primary Key
            this.HasKey(t => t.TransportOrderID);

            // Properties
            this.Property(t => t.TransportOrderNo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.BidDocumentNo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.PerformanceBondReceiptNo)
                .HasMaxLength(50);

            this.Property(t => t.ConsignerName)
                .HasMaxLength(50);

            this.Property(t => t.TransporterSignedName)
                .HasMaxLength(50);

            this.Property(t => t.ContractNumber)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TransportOrder", "Procurement");
            this.Property(t => t.TransportOrderID).HasColumnName("TransportOrderID");
            this.Property(t => t.TransportOrderNo).HasColumnName("TransportOrderNo");
            this.Property(t => t.OrderDate).HasColumnName("OrderDate");
            this.Property(t => t.RequestedDispatchDate).HasColumnName("RequestedDispatchDate");
            this.Property(t => t.OrderExpiryDate).HasColumnName("OrderExpiryDate");
            this.Property(t => t.BidDocumentNo).HasColumnName("BidDocumentNo");
            this.Property(t => t.PerformanceBondReceiptNo).HasColumnName("PerformanceBondReceiptNo");
            this.Property(t => t.PerformanceBondAmount).HasColumnName("PerformanceBondAmount");
            this.Property(t => t.TransporterID).HasColumnName("TransporterID");
            this.Property(t => t.ConsignerName).HasColumnName("ConsignerName");
            this.Property(t => t.TransporterSignedName).HasColumnName("TransporterSignedName");
            this.Property(t => t.ConsignerDate).HasColumnName("ConsignerDate");
            this.Property(t => t.TransporterSignedDate).HasColumnName("TransporterSignedDate");
            this.Property(t => t.ContractNumber).HasColumnName("ContractNumber");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.PartitionId).HasColumnName("PartitionId");
            // Relationships
            this.HasRequired(t => t.Transporter)
                .WithMany(t => t.TransportOrders)
                .HasForeignKey(d => d.TransporterID);

        }
    }
}