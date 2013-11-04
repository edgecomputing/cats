using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
    public class AllocationByRegionMap : EntityTypeConfiguration<AllocationByRegion>
    {
        public AllocationByRegionMap()
        {
            // Primary Key
            this.HasKey(t => t.Hub);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(50);

            this.Property(t => t.Hub)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("AllocationByRegion");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.RegionID).HasColumnName("RegionID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.BenficiaryNo).HasColumnName("BenficiaryNo");
            this.Property(t => t.Hub).HasColumnName("Hub");
        }
    }
}
