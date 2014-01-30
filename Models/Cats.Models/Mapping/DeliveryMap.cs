using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class DeliveryMap : EntityTypeConfiguration<Delivery>
    {
        public DeliveryMap()
        {
            // Primary Key
            this.HasKey(t => t.DeliveryID);

            // Properties
            this.Property(t => t.PlateNoPrimary)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.PlateNoTrailler)
                .HasMaxLength(20);

            this.Property(t => t.DriverName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.WayBillNo)
                .HasMaxLength(50);

            this.Property(t => t.RequisitionNo)
                .HasMaxLength(50);

            this.Property(t => t.DeliveryBy)
                .HasMaxLength(100);

            this.Property(t => t.ReceivedBy)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Delivery");
            this.Property(t => t.DeliveryID).HasColumnName("DeliveryID");
            this.Property(t => t.ReceivingNumber).HasColumnName("ReceivingNumber");
            this.Property(t => t.DonorID).HasColumnName("DonorID");
            this.Property(t => t.TransporterID).HasColumnName("TransporterID");
            this.Property(t => t.PlateNoPrimary).HasColumnName("PlateNoPrimary");
            this.Property(t => t.PlateNoTrailler).HasColumnName("PlateNoTrailler");
            this.Property(t => t.DriverName).HasColumnName("DriverName");
            this.Property(t => t.FDPID).HasColumnName("FDPID");
            this.Property(t => t.DispatchID).HasColumnName("DispatchID");
            this.Property(t => t.WayBillNo).HasColumnName("WayBillNo");
            this.Property(t => t.RequisitionNo).HasColumnName("RequisitionNo");
            this.Property(t => t.HubID).HasColumnName("HubID");
            this.Property(t => t.InvoiceNo).HasColumnName("InvoiceNo");
            this.Property(t => t.DeliveryBy).HasColumnName("DeliveryBy");
            this.Property(t => t.DeliveryDate).HasColumnName("DeliveryDate");
            this.Property(t => t.ReceivedBy).HasColumnName("ReceivedBy");
            this.Property(t => t.ReceivedDate).HasColumnName("ReceivedDate");
            this.Property(t => t.DocumentReceivedDate).HasColumnName("DocumentReceivedDate");
            this.Property(t => t.DocumentReceivedBy).HasColumnName("DocumentReceivedBy");
            this.Property(t => t.TransactionGroupID).HasColumnName("TransactionGroupID");
            // Relationships
            this.HasOptional(t => t.Donor)
                .WithMany(t => t.Deliveries)
                .HasForeignKey(d => d.DonorID);
            this.HasRequired(t => t.FDP)
                .WithMany(t => t.Deliveries)
                .HasForeignKey(d => d.FDPID);
            this.HasOptional(t => t.Hub)
                .WithMany(t => t.Deliveries)
                .HasForeignKey(d => d.HubID);

        }
    }
}
