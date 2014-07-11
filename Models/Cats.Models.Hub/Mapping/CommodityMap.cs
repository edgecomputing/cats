using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Hubs.Mapping
{
    public class CommodityMap : EntityTypeConfiguration<Commodity>
    {
        public CommodityMap()
        {
            // Primary Key
            this.HasKey(t => t.CommodityID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.LongName)
                .HasMaxLength(500);

            this.Property(t => t.NameAM)
                .HasMaxLength(50);

            this.Property(t => t.CommodityCode)
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("Commodity");
            this.Property(t => t.CommodityID).HasColumnName("CommodityID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.LongName).HasColumnName("LongName");
            this.Property(t => t.NameAM).HasColumnName("NameAM");
            this.Property(t => t.CommodityCode).HasColumnName("CommodityCode");
            this.Property(t => t.CommodityTypeID).HasColumnName("CommodityTypeID");
            this.Property(t => t.ParentID).HasColumnName("ParentID");

            // Relationships
            this.HasOptional(t => t.Commodity2)
                .WithMany(t => t.Commodity1)
                .HasForeignKey(d => d.ParentID);
            this.HasRequired(t => t.CommodityType)
                .WithMany(t => t.Commodities)
                .HasForeignKey(d => d.CommodityTypeID);

        }
    }
}
