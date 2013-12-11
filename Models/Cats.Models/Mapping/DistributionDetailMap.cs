using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class DistributionDetailMap : EntityTypeConfiguration<DistributionDetail>
    {
        public DistributionDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.DistributionDetailID);

            // Properties
            // Table & Column Mappings
            this.ToTable("DistributionDetail");
            this.Property(t => t.DistributionDetailID).HasColumnName("DistributionDetailID");
            this.Property(t => t.CommodityID).HasColumnName("CommodityID");
            this.Property(t => t.UnitID).HasColumnName("UnitID");
            this.Property(t => t.SentQuantity).HasColumnName("SentQuantity");
            this.Property(t => t.ReceivedQuantity).HasColumnName("ReceivedQuantity");
            this.Property(t => t.DistributionID).HasColumnName("DistributionID");

            // Relationships
            this.HasRequired(t => t.Commodity)
                .WithMany(t => t.DistributionDetails)
                .HasForeignKey(d => d.CommodityID);
            this.HasRequired(t => t.Distribution)
                .WithMany(t => t.DistributionDetails)
                .HasForeignKey(d => d.DistributionID);
            this.HasRequired(t => t.Unit)
                .WithMany(t => t.DistributionDetails)
                .HasForeignKey(d => d.UnitID);

        }
    }
}
