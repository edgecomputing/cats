using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
    public class UtilizationHeaderMap : EntityTypeConfiguration<UtilizationHeader>
    {
        public UtilizationHeaderMap()
        {
            // Primary Key
            this.HasKey(t => t.DistributionId);

            // Properties
            this.Property(t => t.Remark)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("UtilizationHeader");
            this.Property(t => t.DistributionId).HasColumnName("DistributionId");
            this.Property(t => t.RequisitionId).HasColumnName("RequisitionId");
            this.Property(t => t.WoredaID).HasColumnName("WoredaID");
            this.Property(t => t.DistributionDate).HasColumnName("DistributionDate");
            this.Property(t => t.DistributedBy).HasColumnName("DistributedBy");

            this.Property(t => t.MaleLessThan5Years).HasColumnName("MaleLessThan5Years");
            this.Property(t => t.FemaleLessThan5Years).HasColumnName("FemaleLessThan5Years");

            this.Property(t => t.MaleBetween5And18Years).HasColumnName("MaleBetween5And18Years");
            this.Property(t => t.FemaleBetween5And18Years).HasColumnName("FemaleBetween5And18Years");

            this.Property(t => t.MaleAbove18Years).HasColumnName("MaleAbove18Years");
            this.Property(t => t.FemaleAbove18Years).HasColumnName("FemaleAbove18Years");

            this.Property(t => t.Remark).HasColumnName("Remark");

            // Relationships
            this.HasOptional(t => t.UserProfile)
                .WithMany(t => t.UtilizationHeaders)
                .HasForeignKey(d => d.DistributedBy);

        }
    }
}
