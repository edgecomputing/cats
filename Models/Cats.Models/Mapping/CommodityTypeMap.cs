using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class CommodityTypeMap : EntityTypeConfiguration<CommodityType>
    {
        public CommodityTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.CommodityTypeID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CommodityType");
            this.Property(t => t.CommodityTypeID).HasColumnName("CommodityTypeID");
            this.Property(t => t.Name).HasColumnName("Name");
        }
    }
}
