using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
    public class DonationPlanHeaderMap : EntityTypeConfiguration<DonationPlanHeader>
    {
        public DonationPlanHeaderMap()
        {
            // Primary Key
            this.HasKey(t => t.DonationHeaderPlanID);

            

            this.Property(t => t.Vessel)
                .HasMaxLength(50);

            this.Property(t => t.ReferenceNo)
                .HasMaxLength(50);

            this.Property(t => t.Remark)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("DonationPlanHeader");
            this.Property(t => t.DonationHeaderPlanID).HasColumnName("DonationHeaderPlanID");
            this.Property(t => t.ShippingInstructionId).HasColumnName("ShippingInstructionId");
            this.Property(t => t.GiftCertificateID).HasColumnName("GiftCertificateID");
            this.Property(t => t.CommodityID).HasColumnName("CommodityID");
            this.Property(t => t.DonorID).HasColumnName("DonorID");
            this.Property(t => t.ProgramID).HasColumnName("ProgramID");
            this.Property(t => t.ETA).HasColumnName("ETA");
            this.Property(t => t.Vessel).HasColumnName("Vessel");
            this.Property(t => t.ReferenceNo).HasColumnName("ReferenceNo");
            this.Property(t => t.ModeOfTransport).HasColumnName("ModeOfTransport");
            this.Property(t => t.TransactionGroupID).HasColumnName("TransactionGroupID");
            this.Property(t => t.IsCommited).HasColumnName("IsCommited");
            this.Property(t => t.EnteredBy).HasColumnName("EnteredBy");
            this.Property(t => t.AllocationDate).HasColumnName("AllocationDate");
            this.Property(t => t.Remark).HasColumnName("Remark");

            // Relationships
            this.HasRequired(t => t.Commodity)
                .WithMany(t => t.DonationPlanHeaders)
                .HasForeignKey(d => d.CommodityID);
            this.HasOptional(t => t.CommodityType)
                .WithMany(t => t.DonationPlanHeaders)
                .HasForeignKey(d => d.CommodityTypeID);
            this.HasRequired(t => t.Donor)
                .WithMany(t => t.DonationPlanHeaders)
                .HasForeignKey(d => d.DonorID);
            this.HasRequired(t => t.Program)
                .WithMany(t => t.DonationPlanHeaders)
                .HasForeignKey(d => d.ProgramID);
            this.HasRequired(t => t.ShippingInstruction)
                .WithMany(t => t.DonationPlanHeaders)
                .HasForeignKey(d => d.ShippingInstructionId);
            this.HasOptional(t => t.UserProfile)
                .WithMany(t => t.DonationPlanHeaders)
                .HasForeignKey(d => d.EnteredBy);

        }
    }
}
