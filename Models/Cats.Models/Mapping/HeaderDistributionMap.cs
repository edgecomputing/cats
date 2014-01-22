using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
    public class HeaderDistributionMap : EntityTypeConfiguration<HeaderDistribution>
    {
        public HeaderDistributionMap()
        {
            // Primary Key
            this.HasKey(t => t.DistributionId);

            // Properties
            this.Property(t => t.Remark)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("HeaderDistribution");
            this.Property(t => t.DistributionId).HasColumnName("DistributionId");
            this.Property(t => t.RequisitionId).HasColumnName("RequisitionId");
            this.Property(t => t.DistributionDate).HasColumnName("DistributionDate");
            this.Property(t => t.DistributedBy).HasColumnName("DistributedBy");
            this.Property(t => t.Remark).HasColumnName("Remark");

            // Relationships
            this.HasOptional(t => t.UserProfile)
                .WithMany(t => t.HeaderDistributions)
                .HasForeignKey(d => d.DistributedBy);

        }
    }
}
