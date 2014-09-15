using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
    public class TransporterChequeDetailMap : EntityTypeConfiguration<TransporterChequeDetail>
    {
        public TransporterChequeDetailMap()
        {

            // Primary Key
            this.HasKey(t => t.TransporterChequeDetailID);

            // Properties
            // Table & Column Mappings
            this.ToTable("TransporterChequeDetail");
            this.Property(t => t.TransporterChequeDetailID).HasColumnName("TransporterChequeDetailID");
            this.Property(t => t.TransporterChequeID).HasColumnName("TransporterChequeID");
            this.Property(t => t.TransporterPaymentRequestID).HasColumnName("TransporterPaymentRequestID");
            this.Property(t => t.PartitionId).HasColumnName("PartitionId");

            this.HasRequired(t => t.TransporterCheque)
                .WithMany(t => t.TransporterChequeDetails)
                .HasForeignKey(d => d.TransporterChequeID);

            this.HasRequired(t => t.TransporterPaymentRequest)
                .WithMany(t => t.TransporterChequeDetails)
                .HasForeignKey(d => d.TransporterPaymentRequestID);
        }
    }
}
