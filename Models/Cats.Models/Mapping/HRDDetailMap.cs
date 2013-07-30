using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
    public class HRDDetailMap : EntityTypeConfiguration<HRDDetail>
    {
        public HRDDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.HRDDetailID);

            // Properties
            // Table & Column Mappings
            this.ToTable("HRDDetail");
            this.Property(t => t.HRDDetailID).HasColumnName("HRDDetailID");
            this.Property(t => t.HRDID).HasColumnName("HRDID");
            this.Property(t => t.DurationOfAssistance).HasColumnName("Duration");
            this.Property(t => t.Woreda).HasColumnName("AdminUnitID");
            this.Property(t => t.NumberOfBeneficiaries).HasColumnName("Beneficiaries");
            this.Property(t => t.StartingMonth).HasColumnName("StartingMonth");

            // Relationships
            //this.HasRequired(t => t.)
                //.WithMany(t => t.HRDDetails)
               // .HasForeignKey(d => d.AdminUnitID);
            this.HasRequired(t => t.HRD)
                .WithMany(t => t.HRDDetails)
                .HasForeignKey(d => d.HRDID);

        }
    }
}
