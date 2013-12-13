using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
    public class WoredaHubLinkMap : EntityTypeConfiguration<WoredaHubLink>
    {
        public WoredaHubLinkMap()
        {
            // Primary Key
            this.HasKey(t => t.WoredaHubLinkID);

            // Properties
            // Table & Column Mappings
            this.ToTable("WoredaHubLink");
            this.Property(t => t.WoredaHubLinkID).HasColumnName("WoredaHubLinkID");
            this.Property(t => t.WoredaHubID).HasColumnName("WoredaHubID");
            this.Property(t => t.WoredaID).HasColumnName("WoredaID");
            this.Property(t => t.HubID).HasColumnName("HubID");

            // Relationships
            this.HasRequired(t => t.AdminUnit)
                .WithMany(t => t.WoredaHubLinks)
                .HasForeignKey(d => d.WoredaID);
            this.HasRequired(t => t.Hub)
                .WithMany(t => t.WoredaHubLinks)
                .HasForeignKey(d => d.HubID);
            this.HasRequired(t => t.WoredaHub)
                .WithMany(t => t.WoredaHubLinks)
                .HasForeignKey(d => d.WoredaHubID);

        }
    }
}
