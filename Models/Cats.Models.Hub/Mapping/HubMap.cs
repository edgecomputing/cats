using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Hub.Mapping
{
    public class HubMap : EntityTypeConfiguration<Hub>
    {
        public HubMap()
        {
            // Primary Key
            this.HasKey(t => t.HubID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Hub");
            this.Property(t => t.HubID).HasColumnName("HubID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.HubOwnerID).HasColumnName("HubOwnerID");

            // Relationships
            this.HasRequired(t => t.HubOwner)
                .WithMany(t => t.Hubs)
                .HasForeignKey(d => d.HubOwnerID);
            

        }
    }
}
