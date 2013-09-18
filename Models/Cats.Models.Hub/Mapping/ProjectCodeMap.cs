using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Hub.Mapping
{
    public class ProjectCodeMap : EntityTypeConfiguration<ProjectCode>
    {
        public ProjectCodeMap()
        {
            // Primary Key
            this.HasKey(t => t.ProjectCodeID);

            // Properties
            this.Property(t => t.Value)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ProjectCode");
            this.Property(t => t.ProjectCodeID).HasColumnName("ProjectCodeID");
            this.Property(t => t.Value).HasColumnName("Value");
        }
    }
}
