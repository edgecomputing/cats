using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
    public class WoredasByDonorMap : EntityTypeConfiguration<WoredasByDonor>
    {
        public WoredasByDonorMap()
        {
            // Primary Key
            this.HasKey(t => t.DonorWoredaId);

            // Properties
            this.Property(t => t.WoredaId)
                .IsRequired();
               

            this.Property(t => t.Remark)
                .HasMaxLength(1000);

            // Table & Column Mappings
            this.ToTable("WoredasByDonor", "EarlyWarning");
            this.Property(t => t.DonorWoredaId).HasColumnName("DonorWoredaId");
            this.Property(t => t.DonorId).HasColumnName("DonorId");
            this.Property(t => t.WoredaId).HasColumnName("WoredaId");
            this.Property(t => t.Remark).HasColumnName("Remark");

            // Relationships
            this.HasRequired(t => t.AdminUnit)
                .WithMany(t => t.WoredasByDonors)
                .HasForeignKey(d => d.WoredaId);
            this.HasRequired(t => t.Donor)
                .WithMany(t => t.WoredasByDonors)
                .HasForeignKey(d => d.DonorId);
        }
    }
}
