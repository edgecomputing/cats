using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
    public class WoredaStockDistributionDetailMap : EntityTypeConfiguration<WoredaStockDistributionDetail>
    {
        public WoredaStockDistributionDetailMap()
        {
            this.HasKey(t => t.WoredaStockDistributionDetailID);

            // Properties
          

            // Table & Column Mappings
            this.ToTable("WoredaStockDistributionDetail");
            this.Property(t => t.WoredaStockDistributionDetailID).HasColumnName("WoredaStockDistributionDetailID");
            this.Property(t => t.WoredaStockDistributionID).HasColumnName("WoredaStockDistributionID");
            this.Property(t => t.FdpId).HasColumnName("FDPID");
            this.Property(t => t.DistributedAmount).HasColumnName("DistributedAmount");
           

            // Relationships
            this.HasRequired(t => t.FDP)
                .WithMany(t => t.WoredaStockDistributionDetails)
                .HasForeignKey(d => d.FdpId);
            this.HasRequired(t => t.WoredaStockDistribution)
                .WithMany(t => t.WoredaStockDistributionDetails)
                .HasForeignKey(d => d.WoredaStockDistributionID);

        }
    }
}
