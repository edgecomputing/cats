using System;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class PaymentRequestMap : EntityTypeConfiguration<PaymentRequest>
    {
        public PaymentRequestMap()
        {
            this.ToTable("PaymentRequest");
            this.HasKey(t => t.PaymentRequestID);

            this.Property(t => t.TransportOrderID).HasColumnName("TransportOrderID");

            this.Property(t => t.BusinessProcessID).HasColumnName("BusinessProcessID");
            this.Property(t => t.RequestedAmount).HasColumnName("RequestedAmount");
            this.Property(t => t.TransportedQuantityInQTL).HasColumnName("TransportedQuantityInQTL");
            this.Property(t => t.ReferenceNo).HasColumnName("ReferenceNo");
            this.Property(t => t.LabourCostRate).HasColumnName("LabourCostRate");
            this.Property(t => t.LabourCost).HasColumnName("LabourCost");
            this.Property(t => t.RejectedAmount).HasColumnName("RejectedAmount");
            this.Property(t => t.RejectionReason).HasColumnName("RejectionReason");

            this.HasRequired(t => t.BusinessProcess)
              .WithMany(t => t.PaymentRequests)
              .HasForeignKey(d => d.BusinessProcessID);

            this.HasRequired(t => t.TransportOrder)
              .WithMany(t => t.PaymentRequests)
              .HasForeignKey(d => d.TransportOrderID);

            
        }
    }
}
