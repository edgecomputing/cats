using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Hubs.Mapping
{
    public class MasterMap : EntityTypeConfiguration<Master>
    {
        public MasterMap()
        {
            // Primary Key
            this.HasKey(t => t.MasterID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Master");
            this.Property(t => t.MasterID).HasColumnName("MasterID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
        }
    }
}
