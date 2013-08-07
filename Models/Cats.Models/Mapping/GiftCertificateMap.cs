using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
    public class GiftCertificateMap : EntityTypeConfiguration<GiftCertificate>
    {
        public GiftCertificateMap()
        {
            // Primary Key
            this.HasKey(t => t.GiftCertificateID);

            // Properties
            this.Property(t => t.SINumber)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ReferenceNo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Vessel)
                .HasMaxLength(50);

            this.Property(t => t.PortName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("GiftCertificate");
            this.Property(t => t.GiftCertificateID).HasColumnName("GiftCertificateID");
            this.Property(t => t.GiftDate).HasColumnName("GiftDate");
            this.Property(t => t.DonorID).HasColumnName("DonorID");
            this.Property(t => t.SINumber).HasColumnName("SINumber");
            this.Property(t => t.ReferenceNo).HasColumnName("ReferenceNo");
            this.Property(t => t.Vessel).HasColumnName("Vessel");
            this.Property(t => t.ETA).HasColumnName("ETA");
            this.Property(t => t.IsPrinted).HasColumnName("IsPrinted");
            this.Property(t => t.ProgramID).HasColumnName("ProgramID");
            this.Property(t => t.DModeOfTransport).HasColumnName("DModeOfTransport");
            this.Property(t => t.PortName).HasColumnName("PortName");

            // Relationships
            this.HasRequired(t => t.Detail)
                .WithMany(t => t.GiftCertificates)
                .HasForeignKey(d => d.DModeOfTransport);
            this.HasRequired(t => t.Donor)
                .WithMany(t => t.GiftCertificates)
                .HasForeignKey(d => d.DonorID);
            this.HasRequired(t => t.Program)
                .WithMany(t => t.GiftCertificates)
                .HasForeignKey(d => d.ProgramID);

        }
    }
}
