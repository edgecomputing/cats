using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
    public class ReceiptPlanMap : EntityTypeConfiguration<ReceiptPlan>
    {
        public ReceiptPlanMap()
        {
            // Primary Key
            this.HasKey(t => t.ReceiptHeaderId);

            // Properties
            this.Property(t => t.Remark)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("ReceiptPlan");
            this.Property(t => t.ReceiptHeaderId).HasColumnName("ReceiptHeaderId");
            this.Property(t => t.GiftCertificateDetailId).HasColumnName("GiftCertificateDetailId");
            this.Property(t => t.ReceiptDate).HasColumnName("ReceiptDate");
            this.Property(t => t.EnteredBy).HasColumnName("EnteredBy");
            this.Property(t => t.IsClosed).HasColumnName("IsClosed");
            this.Property(t => t.Remark).HasColumnName("Remark");

            // Relationships
            this.HasRequired(t => t.GiftCertificateDetail)
                .WithMany(t => t.ReceiptPlans)
                .HasForeignKey(d => d.GiftCertificateDetailId);

        }
    }
}
