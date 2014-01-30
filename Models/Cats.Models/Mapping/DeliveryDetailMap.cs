using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class DeliveryDetailMap : EntityTypeConfiguration<DeliveryDetail>
    {
        public DeliveryDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.DeliveryDetailID);

            // Properties
            // Table & Column Mappings
            this.ToTable("DistributionDetail");
            this.Property(t => t.DeliveryDetailID).HasColumnName("DistributionDetailID");
            this.Property(t => t.CommodityID).HasColumnName("CommodityID");
            this.Property(t => t.UnitID).HasColumnName("UnitID");
            this.Property(t => t.SentQuantity).HasColumnName("SentQuantity");
            this.Property(t => t.ReceivedQuantity).HasColumnName("ReceivedQuantity");
            this.Property(t => t.DeliveryID).HasColumnName("DistributionID");

            // Relationships
            this.HasRequired(t => t.Commodity)
                .WithMany(t => t.DistributionDetails)
                .HasForeignKey(d => d.CommodityID);
            this.HasRequired(t => t.Delivery)
                .WithMany(t => t.DeliveryDetails)
                .HasForeignKey(d => d.DeliveryID);
            this.HasRequired(t => t.Unit)
                .WithMany(t => t.DistributionDetails)
                .HasForeignKey(d => d.UnitID);

        }
    }
}
