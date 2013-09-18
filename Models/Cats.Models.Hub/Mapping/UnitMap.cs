using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Hub.Mapping
{
    public class UnitMap : EntityTypeConfiguration<Unit>
    {
        public UnitMap()
        {
            // Primary Key
            this.HasKey(t => t.UnitID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Unit");
            this.Property(t => t.UnitID).HasColumnName("UnitID");
            this.Property(t => t.Name).HasColumnName("Name");
        }
    }
}
