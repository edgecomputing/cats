using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Cats.Models;

namespace Cats.Models.Mapping
{

    public class PromisedContributionMap : EntityTypeConfiguration<PromisedContribution>
    {
        public PromisedContributionMap()
        {
            // Properties
            this.HasKey(t => t.PromisedContributionId);

            // Table & Column Mappings

            this.ToTable("PromisedContribution");

            this.Property(t => t.PromisedContributionId).HasColumnName("PromisedContributionId");
            this.Property(t => t.PromisedQuantity).HasColumnName("PromisedQuantity");
            this.Property(t => t.DeliveredQuantity).HasColumnName("DeliveredQuantity");
            this.Property(t => t.ExpectedTimeOfArrival).HasColumnName("ExpectedTimeOfArrival");
            this.Property(t => t.DonorId).HasColumnName("DonorId");
            this.Property(t => t.CommodityId).HasColumnName("CommodityId");
            this.Property(t => t.HubId).HasColumnName("HubId");
            
            // Relationships
            this.HasRequired(t => t.Commodity)
                .WithMany(t => t.PromisedContributions)
                .HasForeignKey(d => d.CommodityId);

            this.HasRequired(t => t.Hub)
                .WithMany(t => t.PromisedContributions)
                .HasForeignKey(d => d.HubId);

            this.HasRequired(t => t.Donor)
                .WithMany(t => t.PromisedContributions)
                .HasForeignKey(d => d.DonorId);

            /**/
             
          }   

    }
}
