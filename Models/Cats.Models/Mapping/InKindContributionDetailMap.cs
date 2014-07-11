using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
    public class InKindContributionDetailMap : EntityTypeConfiguration<InKindContributionDetail>
    {
        public InKindContributionDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.InKindContributionDetailID);

          
            // Table & Column Mappings
            this.ToTable("InKindContributionDetail");
            this.Property(t => t.InKindContributionDetailID).HasColumnName("InKindContributionDetailID");
            this.Property(t => t.ContributionID).HasColumnName("ContributionID");
            this.Property(t => t.ReferenceNumber).HasColumnName("ReferenceNumber");
            this.Property(t => t.ContributionDate).HasColumnName("ContributionDate");
            this.Property(t => t.CommodityID).HasColumnName("CommodityID");
            this.Property(t => t.Amount).HasColumnName("Amount");

            // Relationships
            this.HasRequired(t => t.Commodity)
                .WithMany(t => t.InKindContributionDetails)
                .HasForeignKey(d => d.CommodityID);
            this.HasRequired(t => t.Contribution)
                .WithMany(t => t.InKindContributionDetails)
                .HasForeignKey(d => d.ContributionID);
        }
    }
}
