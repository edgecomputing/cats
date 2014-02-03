using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
   public class DistributionByAgeDetailMap:EntityTypeConfiguration<DistributionByAgeDetail>
    {
       public DistributionByAgeDetailMap()
       {
           this.HasKey(t => t.DistributionByAgeDetailID);

           // Properties
           // Table & Column Mappings
           this.ToTable("DistributionByAgeDetail");
           this.Property(t => t.DistributionByAgeDetailID).HasColumnName("DistributionByAgeDetailID");
           this.Property(t => t.DistributionHeaderID).HasColumnName("DistributionHeaderID");
           this.Property(t => t.FDPID).HasColumnName("FDPID");
           this.Property(t => t.MaleLessThan5Years).HasColumnName("MaleLessThan5Years");
           this.Property(t => t.FemaleLessThan5Years).HasColumnName("FemaleLessThan5Years");

           this.Property(t => t.MaleBetween5And18Years).HasColumnName("MaleBetween5And18Years");
           this.Property(t => t.FemaleBetween5And18Years).HasColumnName("FemaleBetween5And18Years");

           this.Property(t => t.MaleAbove18Years).HasColumnName("MaleAbove18Years");
           this.Property(t => t.FemaleAbove18Years).HasColumnName("FemaleAbove18Years");

           //Relationships
          // this.HasRequired(t => t.WoredaStockDistribution)
          //.WithMany(t => t.DistributionByAgeDetails)
          //.HasForeignKey(d => d.DistributionHeaderID);

           this.HasRequired(t => t.FDP)
               .WithMany(t => t.DistributionByAgeDetails)
               .HasForeignKey(d => d.FDPID);

       }

    }
}
