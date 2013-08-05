using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class RegionalPSNPPlanDetailMap : EntityTypeConfiguration<RegionalPSNPPlanDetail>
    {
        public RegionalPSNPPlanDetailMap()
        {
            this.ToTable("RegionalPSNPPlanDetail");
            this.Property(t => t.RegionalPSNPPlanDetailID).HasColumnName("RegionalPSNPPlanDetailID");
            this.Property(t => t.PlanedFDPID).HasColumnName("PlanedFDPID");

            this.HasRequired(t => t.RegionalPSNPPlan)
                    .WithMany(t => t.RegionalPSNPPlanDetails)
                    .HasForeignKey(d => d.RegionalPSNPPlanID);

            this.HasRequired(t => t.PlanedFDP)
                    .WithMany(t => t.RegionalPSNPPlanDetails)
                    .HasForeignKey(d => d.PlanedFDPID);

            this.Property(t => t.BeneficiaryCount).HasColumnName("BeneficiaryCount");

            this.Property(t => t.FoodRatio).HasColumnName("FoodRatio");

            this.Property(t => t.CashRatio).HasColumnName("CashRatio");

            this.Property(t => t.Item3Ratio).HasColumnName("Item3Ratio");

            this.Property(t => t.Item4Ratio).HasColumnName("Item4Ratio");

        }
    }
}