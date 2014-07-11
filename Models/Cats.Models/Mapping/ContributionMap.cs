using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
    public class ContributionMap : EntityTypeConfiguration<Contribution>
    {
        public ContributionMap()
        {
            // Primary Key
            this.HasKey(t => t.ContributionID);

            // Properties
            // Table & Column Mappings
            this.ToTable("Contribution");
            this.Property(t => t.ContributionID).HasColumnName("ContributionID");
            this.Property(t => t.DonorID).HasColumnName("DonorID");
            this.Property(t => t.HRDID).HasColumnName("HRDID");
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.ContributionType).HasColumnName("ContributionType");
            this.Property(t => t.PartitionId).HasColumnName("PartitionId");

            //this.Property(t => t.ImplementingAgency).HasColumnName("ImplementingAgency");

            // Relationships
            this.HasRequired(t => t.Donor)
                .WithMany(t => t.Contributions)
                .HasForeignKey(d => d.DonorID);
            //this.HasRequired(t => t.Donor)
            //    .WithMany(t => t.Contributions1)
            //    .HasForeignKey(d => d.ImplementingAgency);
            this.HasRequired(t => t.HRD)
                .WithMany(t => t.Contributions)
                .HasForeignKey(d=>d.HRDID);
               

        }
    }
}
