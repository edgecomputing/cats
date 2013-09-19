using System;
using System.Collections.Generic;

using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
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
