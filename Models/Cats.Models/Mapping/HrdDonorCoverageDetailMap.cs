using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
    public class HrdDonorCoverageDetailMap : EntityTypeConfiguration<HrdDonorCoverageDetail>
   {
        public HrdDonorCoverageDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.HRDDonorCoverageDetailID);

            // Properties
            this.Property(t => t.HRDDonorCoverageID)
                .IsRequired();
            this.Property(t => t.WoredaID).IsRequired();

            // Table & Column Mappings
            this.ToTable("HRDDonorCoverageDetail", "EarlyWarning");
            this.Property(t => t.HRDDonorCoverageDetailID).HasColumnName("HRDDonorCoverageDetailID");
            this.Property(t => t.HRDDonorCoverageID).HasColumnName("HRDDonorCoverageID");
            this.Property(t => t.WoredaID).HasColumnName("WoredaID");
           

            // Relationships
            this.HasRequired(t => t.HrdDonorCoverage)
                .WithMany(t => t.HrdDonorCoverageDetails)
                .HasForeignKey(d => d.HRDDonorCoverageID);
            this.HasRequired(t => t.AdminUnit)
                .WithMany(t => t.HrdDonorCoverageDetails )
                .HasForeignKey(d => d.WoredaID);
        }
   }
}
