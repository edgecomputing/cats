using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
   public class SeasonMap:EntityTypeConfiguration<Season>
   {
       public SeasonMap()
        {
            // Primary Key
            this.HasKey(t => t.SeasonID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Season");
            this.Property(t => t.SeasonID).HasColumnName("SeasonID");
            this.Property(t => t.Name).HasColumnName("Name");
        }

    }
}
