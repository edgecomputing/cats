using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
