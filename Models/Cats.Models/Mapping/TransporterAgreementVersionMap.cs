using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
    public class TransporterAgreementVersionMap : EntityTypeConfiguration<TransporterAgreementVersion>
    {
        public TransporterAgreementVersionMap()
        {
            // Primary Key
            this.HasKey(t => t.TransporterAgreementVersionID);

            // Properties
            this.Property(t => t.AgreementDocxFile)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("TransporterAgreementVersion");
            this.Property(t => t.TransporterAgreementVersionID).HasColumnName("TransporterAgreementVersionID");
            this.Property(t => t.BidID).HasColumnName("BidID");
            this.Property(t => t.TransporterID).HasColumnName("TransporterID");
            this.Property(t => t.AgreementDocxFile).HasColumnName("AgreementDocxFile");
            this.Property(t => t.IssueDate).HasColumnName("IssueDate");

            // Relationships
            this.HasRequired(t => t.Bid)
                .WithMany(t => t.TransporterAgreementVersions)
                .HasForeignKey(d => d.BidID);
            this.HasRequired(t => t.Transporter)
                .WithMany(t => t.TransporterAgreementVersions)
                .HasForeignKey(d => d.TransporterID);

        }
    }
}
