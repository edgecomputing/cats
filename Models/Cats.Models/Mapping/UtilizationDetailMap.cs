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
            this.HasKey(t => t.DistributionDetailId);

            // Properties
            this.Property(t => t.LossReason)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("UtilizationDetail");
            this.Property(t => t.DistributionDetailId).HasColumnName("DistributionDetailId");
            this.Property(t => t.DistributionHeaderId).HasColumnName("DistributionHeaderId");
            this.Property(t => t.FdpId).HasColumnName("FdpId");
            this.Property(t => t.DistributedQuantity).HasColumnName("DistributedQuantity");
            this.Property(t => t.DistributionSartDate).HasColumnName("DistributionSartDate");
            this.Property(t => t.DistributionEndDate).HasColumnName("DistributionEndDate");
            this.Property(t => t.LossAmount).HasColumnName("LossAmount");
            this.Property(t => t.LossReason).HasColumnName("LossReason");
            this.Property(t => t.Transfered).HasColumnName("Transfered");

            // Relationships
            this.HasRequired(t => t.FDP)
                .WithMany(t => t.UtilizationDetails)
                .HasForeignKey(d => d.FdpId);
            this.HasRequired(t => t.UtilizationHeader)
                .WithMany(t => t.UtilizationDetails)
                .HasForeignKey(d => d.DistributionHeaderId);

        }
    }
}
