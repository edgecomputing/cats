using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
    public class TransporterPaymentRequestMap : EntityTypeConfiguration<TransporterPaymentRequest>
    {
        public TransporterPaymentRequestMap()
        {
            // Primary Key
            this.HasKey(t => t.TransporterPaymentRequestID);

            // Properties
            this.Property(t => t.TransporterPaymentRequestID);

            this.Property(t => t.ReferenceNo)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TransporterPaymentRequest");
            this.Property(t => t.TransporterPaymentRequestID).HasColumnName("TransporterPaymentRequestID");
            this.Property(t => t.ReferenceNo).HasColumnName("ReferenceNo");
            this.Property(t => t.TransportOrderID).HasColumnName("TransportOrderID");
            this.Property(t => t.DeliveryID).HasColumnName("DeliveryID");
            this.Property(t => t.ShortageBirr).HasColumnName("ShortageBirr");
            this.Property(t => t.LabourCostRate).HasColumnName("LabourCostRate");
            this.Property(t => t.LabourCost).HasColumnName("LabourCost");
            this.Property(t => t.RejectedAmount).HasColumnName("RejectedAmount");
            this.Property(t => t.RejectionReason).HasColumnName("RejectionReason");
            this.Property(t => t.RequestedDate).HasColumnName("RequestedDate");
            this.Property(t => t.BusinessProcessID).HasColumnName("BusinessProcessID");
            this.Property(t => t.PartitionId).HasColumnName("PartitionId");

            // Relationships
            this.HasRequired(t => t.BusinessProcess)
                .WithMany(t => t.TransporterPaymentRequests)
                .HasForeignKey(d => d.BusinessProcessID);
            this.HasRequired(t => t.Delivery)
                .WithMany(t => t.TransporterPaymentRequests)
                .HasForeignKey(d => d.DeliveryID);
            this.HasRequired(t => t.TransportOrder)
                .WithMany(t => t.TransporterPaymentRequests)
                .HasForeignKey(d => d.TransportOrderID);

        }
    }
}
