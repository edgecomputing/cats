using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{

    public class RationMap : EntityTypeConfiguration<Ration>
    {
        public RationMap()
        {
            // Primary Key
            this.HasKey(t => t.RationID);

            // Properties
            // Table & Column Mappings
            this.ToTable("Ration", "Logistics");
            this.Property(t => t.RationID).HasColumnName("RationID");
            this.Property(t => t.CommodityID).HasColumnName("CommodityID");
            this.Property(t => t.Amount).HasColumnName("Amount");
        }
    }
}
