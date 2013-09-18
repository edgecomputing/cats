using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Hub.Mapping
{
    public class CommodityGradeMap : EntityTypeConfiguration<CommodityGrade>
    {
        public CommodityGradeMap()
        {
            // Primary Key
            this.HasKey(t => t.CommodityGradeID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CommodityGrade");
            this.Property(t => t.CommodityGradeID).HasColumnName("CommodityGradeID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
        }
    }
}
