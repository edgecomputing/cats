using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Hubs.Mapping
{
    public class VWFreePhysicalStockMap : EntityTypeConfiguration<VWFreePhysicalStock>
    {
        public VWFreePhysicalStockMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ProgramID, t.FreeStock });

            // Properties
            this.Property(t => t.Hub)
                .HasMaxLength(50);

            this.Property(t => t.Program)
                .HasMaxLength(50);

            this.Property(t => t.Commodity)
                .HasMaxLength(50);

            this.Property(t => t.ProgramID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FreeStock)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("VWFreePhysicalStock");
            this.Property(t => t.Hub).HasColumnName("Hub");
            this.Property(t => t.Program).HasColumnName("Program");
            this.Property(t => t.Commodity).HasColumnName("Commodity");
            this.Property(t => t.PhysicalStock).HasColumnName("PhysicalStock");
            this.Property(t => t.HubID).HasColumnName("HubID");
            this.Property(t => t.CommodityID).HasColumnName("CommodityID");
            this.Property(t => t.ProgramID).HasColumnName("ProgramID");
            this.Property(t => t.FreeStock).HasColumnName("FreeStock");
        }
    }
}
