using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Cats.Models;

namespace Cats.Models.Mapping
{
    public class RegionalPSNPPledgeMap : EntityTypeConfiguration<RegionalPSNPPledge>
    {
        public RegionalPSNPPledgeMap()
        {
            // Primary Key
            this.HasKey(t => t.RegionalPSNPPledgeID);

            // Properties
            // Table & Column Mappings
            this.ToTable("RegionalPSNPPledges");
            this.Property(t => t.RegionalPSNPPledgeID).HasColumnName("RegionalPSNPPledgeID");
            this.Property(t => t.RegionalPSNPPlanDetailID).HasColumnName("RegionalPSNPPlanDetailID");
            this.Property(t => t.DonorID).HasColumnName("DonorID");
            this.Property(t => t.CommodityID).HasColumnName("CommodityID");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.UnitID).HasColumnName("UnitID");
            this.Property(t => t.PledgeDate).HasColumnName("PledgeDate");

            // Relationships
            this.HasRequired(t => t.Commodity)
                .WithMany(t => t.RegionalPSNPPledges)
                .HasForeignKey(d => d.CommodityID);
            this.HasRequired(t => t.Donor)
                .WithMany(t => t.RegionalPSNPPledges)
                .HasForeignKey(d => d.DonorID);
            this.HasRequired(t => t.RegionalPSNPPlanDetail)
                .WithMany(t => t.RegionalPSNPPledges)
                .HasForeignKey(d => d.RegionalPSNPPlanDetailID);
            this.HasRequired(t => t.Unit)
                .WithMany(t => t.RegionalPSNPPledges)
                .HasForeignKey(d => d.UnitID);

        }
    }
}
