using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Hub.Mapping
{
    public class ProgramMap : EntityTypeConfiguration<Program>
    {
        public ProgramMap()
        {
            // Primary Key
            this.HasKey(t => t.ProgramID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(50);

            this.Property(t => t.LongName)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("Program");
            this.Property(t => t.ProgramID).HasColumnName("ProgramID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.LongName).HasColumnName("LongName");
        }
    }
}
