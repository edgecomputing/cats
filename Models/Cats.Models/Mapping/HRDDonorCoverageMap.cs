using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
   public class HRDDonorCoverageMap:EntityTypeConfiguration<HrdDonorCoverage>
   {
       public HRDDonorCoverageMap()
       {
           // Primary Key
           this.HasKey(t => t.HRDDOnorCoverageID);

           // Properties
           this.Property(t => t.HRDID)
               .IsRequired();
           this.Property(t => t.DonorID).IsRequired();

           // Table & Column Mappings
           this.ToTable("HRDDonorCoverage", "EarlyWarning");
           this.Property(t => t.HRDDOnorCoverageID).HasColumnName("HrdDonorCovarageID");
           this.Property(t => t.HRDID).HasColumnName("HRDID");
           this.Property(t => t.DonorID).HasColumnName("DonorID");
           this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");

           // Relationships
           this.HasRequired(t => t.Hrd)
               .WithMany(t => t.HrdDonorCovarages)
               .HasForeignKey(d => d.HRDID);
           this.HasRequired(t => t.Donor)
               .WithMany(t => t.HrdDonorCovarages)
               .HasForeignKey(d => d.DonorID);
           
       }
   }
}
