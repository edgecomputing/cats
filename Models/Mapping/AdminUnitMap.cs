using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class AdminUnitMap : EntityTypeConfiguration<AdminUnit>
    {
        public AdminUnitMap()
        {
            // Primary Key
            this.HasKey(t => t.AdminUnitID);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(50);

            this.Property(t => t.NameAM)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("AdminUnit");
            this.Property(t => t.AdminUnitID).HasColumnName("AdminUnitID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NameAM).HasColumnName("NameAM");
            this.Property(t => t.AdminUnitTypeID).HasColumnName("AdminUnitTypeID");
            this.Property(t => t.ParentID).HasColumnName("ParentID");

            // Relationships
            this.HasOptional(t => t.AdminUnit2)
                .WithMany(t => t.AdminUnit1)
                .HasForeignKey(d => d.ParentID);
            this.HasOptional(t => t.AdminUnitType)
                .WithMany(t => t.AdminUnits)
                .HasForeignKey(d => d.AdminUnitTypeID);

        }
    }
}
