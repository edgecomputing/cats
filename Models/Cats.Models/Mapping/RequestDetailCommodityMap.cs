using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
    public class RequestDetailCommodityMap : EntityTypeConfiguration<RequestDetailCommodity>
    {
        public RequestDetailCommodityMap()
        {
            // Primary Key
            this.HasKey(t => t.RequestCommodityID);

            // Properties
            // Table & Column Mappings
            this.ToTable("RequestDetailCommodity", "EarlyWarning");
            this.Property(t => t.RequestCommodityID).HasColumnName("RequestCommodityID");
            this.Property(t => t.RegionalRequestDetailID).HasColumnName("RegionalRequestDetailID");
            this.Property(t => t.CommodityID).HasColumnName("CommodityID");
            this.Property(t => t.Amount).HasColumnName("Amount").HasPrecision(18,4);
            this.Property(t => t.UnitID).HasColumnName("UnitID");

            // Relationships
            this.HasRequired(t => t.Commodity)
                .WithMany(t => t.RequestDetailCommodities)
                .HasForeignKey(d => d.CommodityID);
            this.HasRequired(t => t.RegionalRequestDetail)
                .WithMany(t => t.RequestDetailCommodities)
                .HasForeignKey(d => d.RegionalRequestDetailID);

        }
    }
}
