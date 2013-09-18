using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Hub.Mapping
{
    public class DetailMap : EntityTypeConfiguration<Detail>
    {
        public DetailMap()
        {
            // Primary Key
            this.HasKey(t => t.DetailID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("Detail");
            this.Property(t => t.DetailID).HasColumnName("DetailID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.MasterID).HasColumnName("MasterID");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");

            // Relationships
            this.HasRequired(t => t.Master)
                .WithMany(t => t.Details)
                .HasForeignKey(d => d.MasterID);

        }
    }
}
