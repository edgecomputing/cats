using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class RegionalPSNPPlanMap : EntityTypeConfiguration<RegionalPSNPPlan>
    {
        public RegionalPSNPPlanMap()
        {
            this.ToTable("RegionalPSNPPlan");
            this.Property(t => t.RegionalPSNPPlanID).HasColumnName("RegionalPSNPPlanID");

            this.Property(t => t.Year).HasColumnName("Year");

            this.Property(t => t.Duration).HasColumnName("Duration");

            this.HasRequired(t => t.Region)
                    .WithMany(t => t.RegionalPSNPPlans)
                    .HasForeignKey(d => d.RegionID);
            
            this.HasRequired(t => t.Ration)
                   .WithMany(t => t.RegionalPSNPPlans)
                   .HasForeignKey(d => d.RationID);

            this.HasRequired(t => t.AttachedBusinessProcess)
           .WithMany(t => t.RegionalPSNPPlans)
           .HasForeignKey(d => d.StatusID);

            this.HasRequired(t => t.Plan)
           .WithMany(t => t.RegionalPSNPPlans)
           .HasForeignKey(d => d.PlanId);
           
        }
    }
}