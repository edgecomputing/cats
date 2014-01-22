using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
    public class UtilizationDetailMap : EntityTypeConfiguration<UtilizationDetail>
    {
        public UtilizationDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.DistributionDetailId);

            // Properties
            // Table & Column Mappings
            this.ToTable("DetailDistribution");
            this.Property(t => t.DistributionDetailId).HasColumnName("DistributionDetailId");
            this.Property(t => t.DistributionHeaderId).HasColumnName("DistributionHeaderId");
            this.Property(t => t.FdpId).HasColumnName("FdpId");
            this.Property(t => t.DistributedQuantity).HasColumnName("DistributedQuantity");

            // Relationships
            this.HasRequired(t => t.FDP)
                .WithMany(t => t.DetailDistributions)
                .HasForeignKey(d => d.FdpId);
            this.HasRequired(t => t.HeaderDistribution)
                .WithMany(t => t.DetailDistributions)
                .HasForeignKey(d => d.DistributionHeaderId);

        }
    }
}
