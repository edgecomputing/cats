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
            this.Property(t => t.CommodityID).HasColumnName("CommodityID");
            this.Property(t => t.DistributedAmount).HasColumnName("DistributedAmount");

            this.Property(t => t.StartingBalance).HasColumnName("StartingBalance");
            this.Property(t => t.EndingBalance).HasColumnName("EndingBalance");
            this.Property(t => t.TotalIn).HasColumnName("TotalIn");
            this.Property(t => t.TotoalOut).HasColumnName("TotalOut");
            this.Property(t => t.DistributionStartDate).HasColumnName("DistributionStartDate");
            this.Property(t => t.DistributionEndDate).HasColumnName("DistributionEndDate");
            this.Property(t => t.LossAmount).HasColumnName("LossAmount");
            this.Property(t => t.LossReason).HasColumnName("LossReason");
            this.Property(t => t.PartitionId).HasColumnName("PartitionId");

           

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
