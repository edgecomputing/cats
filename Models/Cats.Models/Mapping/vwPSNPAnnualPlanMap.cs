using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
    public class vwPSNPAnnualPlanMap : EntityTypeConfiguration<vwPSNPAnnualPlan>
    {
        public vwPSNPAnnualPlanMap()
        {
            // Primary Key
             this.HasKey(t => new { t.RegionalPSNPPlanID, t.WoredaID, t.ZoneID });

            //// Properties
            //this.Property(t => t.RegionalPSNPPlanID);

            
            //this.Property(t => t.FDPName)
            //    .IsRequired()
            //    .HasMaxLength(50);

            this.Property(t => t.WoredaName)
                .HasMaxLength(50);

            this.Property(t => t.WoredaID);

            this.Property(t => t.ZoneID);

            this.Property(t => t.ZoneName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("vwPSNPAnnualPlan");
            this.Property(t => t.FoodRatio).HasColumnName("FoodRatio");
            this.Property(t => t.CashRatio).HasColumnName("CashRatio");
            this.Property(t => t.BeneficiaryCount).HasColumnName("BeneficiaryCount");
            //this.Property(t => t.PlanedFDPID).HasColumnName("PlanedFDPID");
            this.Property(t => t.Duration).HasColumnName("Duration");
          
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.RegionalPSNPPlanID).HasColumnName("RegionalPSNPPlanID");
          
           // this.Property(t => t.FDPName).HasColumnName("FDPName");
            this.Property(t => t.WoredaName).HasColumnName("WoredaName");
            this.Property(t => t.WoredaID).HasColumnName("WoredaID");
            this.Property(t => t.ZoneID).HasColumnName("ZoneID");
            this.Property(t => t.ZoneName).HasColumnName("ZoneName");
            this.Property(t => t.RegionName).HasColumnName("Region");
        }
    }
}
