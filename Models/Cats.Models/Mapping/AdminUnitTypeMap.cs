using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class AdminUnitTypeMap : EntityTypeConfiguration<AdminUnitType>
    {
        public AdminUnitTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.AdminUnitTypeID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.NameAM)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("AdminUnitType");
            this.Property(t => t.AdminUnitTypeID).HasColumnName("AdminUnitTypeID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NameAM).HasColumnName("NameAM");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
        }
    }
}
