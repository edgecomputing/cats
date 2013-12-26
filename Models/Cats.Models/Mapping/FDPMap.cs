using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class FDPMap : EntityTypeConfiguration<FDP>
    {
        public FDPMap()
        {
            // Primary Key
            this.HasKey(t => t.FDPID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.NameAM)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("FDP");
            this.Property(t => t.FDPID).HasColumnName("FDPID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NameAM).HasColumnName("NameAM");
            this.Property(t => t.AdminUnitID).HasColumnName("AdminUnitID");

            // Relationships
            this.HasRequired(t => t.AdminUnit)
                .WithMany(t => t.FDPs)
                .HasForeignKey(d => d.AdminUnitID);

        }
    }
}
