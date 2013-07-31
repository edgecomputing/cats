using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class HRDCommodityDetailMap:EntityTypeConfiguration<HRDCommodityDetail>
    {
         public HRDCommodityDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.HRDCommodityDetailID);

            // Properties
            // Table & Column Mappings
            this.ToTable("HRDCommodityDetail", "EarlyWarning");
            this.Property(t => t.HRDCommodityDetailID).HasColumnName("HRDCommodityDetailID");
            this.Property(t => t.HRDDetailID).HasColumnName("HRDDetailID");
            this.Property(t => t.CommodityID).HasColumnName("CommodityID");
            this.Property(t => t.Amount).HasColumnName("Amount");
            

            // Relationships
            this.HasRequired(t => t.HrdDetail)
                .WithMany(t => t.HRDCommodityDetails)
                .HasForeignKey(d => d.HRDDetailID);

            this.HasRequired(t => t.Commodity)
                .WithMany(t => t.HRDCommodityDetails)
                .HasForeignKey(d => d.CommodityID);

        }
    }
}
