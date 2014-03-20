using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
    public class TransporterChequeMap : EntityTypeConfiguration<TransporterCheque>
    {
        public TransporterChequeMap()
        {
            // Primary Key
            this.HasKey(t => t.TransporterChequeId);

            // Properties
            this.Property(t => t.CheckNo)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TransporterCheque");
            this.Property(t => t.TransporterChequeId).HasColumnName("TransporterChequeId");
            this.Property(t => t.PaymentRequestID).HasColumnName("PaymentRequestID");
            this.Property(t => t.CheckNo).HasColumnName("CheckNo");
            this.Property(t => t.PaymentVoucherNo).HasColumnName("PaymentVoucherNo");
            this.Property(t => t.BankName).HasColumnName("BankName");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.TransporterId).HasColumnName("TransporterId");
            this.Property(t => t.PreparedBy).HasColumnName("PreparedBy");
            this.Property(t => t.AppovedBy).HasColumnName("AppovedBy");
            this.Property(t => t.AppovedDate).HasColumnName("AppovedDate");
            this.Property(t => t.Status).HasColumnName("Status");

            // Relationships
            this.HasRequired(t => t.Transporter)
                .WithOptional(t => t.TransporterCheque);
            this.HasRequired(t => t.UserProfile)
                .WithMany(t => t.TransporterCheques)
                .HasForeignKey(d => d.AppovedBy);
            this.HasRequired(t => t.UserProfile1)
                .WithMany(t => t.TransporterCheques1)
                .HasForeignKey(d => d.PreparedBy);
        }
    }
}
