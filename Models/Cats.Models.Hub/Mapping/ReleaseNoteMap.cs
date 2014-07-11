using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Hubs.Mapping
{
    public class ReleaseNoteMap : EntityTypeConfiguration<ReleaseNote>
    {
        public ReleaseNoteMap()
        {
            // Primary Key
            this.HasKey(t => t.ReleaseNoteID);

            // Properties
            this.Property(t => t.ReleaseName)
                .HasMaxLength(50);

            this.Property(t => t.Notes)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ReleaseNote");
            this.Property(t => t.ReleaseNoteID).HasColumnName("ReleaseNoteID");
            this.Property(t => t.ReleaseName).HasColumnName("ReleaseName");
            this.Property(t => t.BuildNumber).HasColumnName("BuildNumber");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.Notes).HasColumnName("Notes");
            this.Property(t => t.Details).HasColumnName("Details");
            this.Property(t => t.DBuildQuality).HasColumnName("DBuildQuality");
            this.Property(t => t.Comments).HasColumnName("Comments");
        }
    }
}
