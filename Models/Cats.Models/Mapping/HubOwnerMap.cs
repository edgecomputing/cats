using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class HubOwnerMap : EntityTypeConfiguration<HubOwner>
    {
        public HubOwnerMap()
        {
            // Primary Key
            this.HasKey(t => t.HubOwnerID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.LongName)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("HubOwner");
            this.Property(t => t.HubOwnerID).HasColumnName("HubOwnerID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.LongName).HasColumnName("LongName");
        }
    }
}
