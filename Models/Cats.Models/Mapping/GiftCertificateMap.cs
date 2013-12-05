using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class GiftCertificateMap : EntityTypeConfiguration<GiftCertificate>
    {
        public GiftCertificateMap()
        {
            // Primary Key
            this.HasKey(t => t.GiftCertificateID);

            // Properties
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
           // this.Property(t => t.SINumber).HasColumnName("SINumber");
            this.Property(t => t.ShippingInstructionID).HasColumnName("ShippingInstructionID");
            this.Property(t => t.ReferenceNo).HasColumnName("ReferenceNo");
            this.Property(t => t.Vessel).HasColumnName("Vessel");
            this.Property(t => t.ETA).HasColumnName("ETA");
            this.Property(t => t.IsPrinted).HasColumnName("IsPrinted");
            this.Property(t => t.ProgramID).HasColumnName("ProgramID");
            this.Property(t => t.DModeOfTransport).HasColumnName("DModeOfTransport");
            this.Property(t => t.PortName).HasColumnName("PortName");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
            this.Property(t => t.DeclarationNumber).HasColumnName("DeclarationNumber");
            this.Property(t => t.TransactionGroupID).HasColumnName("TransactionGroupID");

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
            this.HasRequired(t => t.ShippingInstruction)
                .WithMany(t => t.GiftCertificates)
                .HasForeignKey(t => t.ShippingInstructionID);

        }
    }
}
