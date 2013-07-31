using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
    public class RationDetailMap : EntityTypeConfiguration<RationDetail>
    {
        public RationDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.RationDetatilID);

            // Properties
            // Table & Column Mappings
            this.ToTable("RationDetail");
            this.Property(t => t.RationDetatilID).HasColumnName("RationDetailID");
            this.Property(t => t.RationID).HasColumnName("RationID");
            this.Property(t => t.CommodityID).HasColumnName("CommodityID");
            this.Property(t => t.Amount).HasColumnName("Amount");

            // Relationships
            this.HasRequired(t => t.Commodity)
                .WithMany(t => t.RationDetails)
                .HasForeignKey(d => d.CommodityID);
            this.HasRequired(t => t.Ration)
                .WithMany(t => t.RationDetails)
                .HasForeignKey(d => d.RationID);

        }
    }
}
