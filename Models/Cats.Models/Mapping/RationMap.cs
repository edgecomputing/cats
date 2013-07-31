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
            this.Property(t => t.RationID);

            // Table & Column Mappings
            this.ToTable("Ration");
            this.Property(t => t.RationID).HasColumnName("RationID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.IsDefaultRation).HasColumnName("IsDefaultRation");
            this.Property(t => t.RefrenceNumber).HasColumnName("RefrenceNumber");
        }
    }
}
