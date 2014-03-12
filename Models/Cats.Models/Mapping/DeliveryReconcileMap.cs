using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
    public class DeliveryReconcileMap : EntityTypeConfiguration<DeliveryReconcile>
    {
        public DeliveryReconcileMap()
        {
            // Primary Key
            this.HasKey(t => t.DeliveryReconcileID);

            // Properties
            this.Property(t => t.GRN)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.WayBillNo)
                .HasMaxLength(50);

            this.Property(t => t.RequsitionNo)
                .HasMaxLength(50);

            this.Property(t => t.GIN)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("DeliveryReconcile");
            this.Property(t => t.DeliveryReconcileID).HasColumnName("DeliveryReconcileID");
            this.Property(t => t.GRN).HasColumnName("GRN");
            this.Property(t => t.FDPID).HasColumnName("FDPID");
            this.Property(t => t.DispatchID).HasColumnName("DispatchID");
            this.Property(t => t.WayBillNo).HasColumnName("WayBillNo");
            this.Property(t => t.RequsitionNo).HasColumnName("RequsitionNo");
            this.Property(t => t.HubID).HasColumnName("HubID");
            this.Property(t => t.GIN).HasColumnName("GIN");
            this.Property(t => t.ReceivedAmount).HasColumnName("ReceivedAmount");
            this.Property(t => t.ReceivedDate).HasColumnName("ReceivedDate");
            this.Property(t => t.LossAmount).HasColumnName("LossAmount");
            this.Property(t => t.LossReason).HasColumnName("LossReason");
            this.Property(t => t.TransactionGroupID).HasColumnName("TransactionGroupID");

            // Relationships
            this.HasRequired(t => t.Dispatch)
                .WithMany(t => t.DeliveryReconciles)
                .HasForeignKey(d => d.DispatchID);
            this.HasRequired(t => t.FDP)
                .WithMany(t => t.DeliveryReconciles)
                .HasForeignKey(d => d.FDPID);
            this.HasRequired(t => t.Hub)
                .WithMany(t => t.DeliveryReconciles)
                .HasForeignKey(d => d.HubID);
            this.HasRequired(t => t.TransactionGroup)
                .WithMany(t => t.DeliveryReconciles)
                .HasForeignKey(d => d.TransactionGroupID);
        }
    }
}
