using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;


namespace Cats.Models.Mapping
{
    public class NeedAssessmentMap:EntityTypeConfiguration<NeedAssement>
    {
        public NeedAssessmentMap()
        {
            // Primary Key
            this.HasKey(t => t.NAId);

            // Properties
            // Table & Column Mappings
            this.ToTable("NeedAssement", "EarlyWarning");
            this.Property(t => t.NAId).HasColumnName("NAId");
            this.Property(t => t.VPoorNoOfM).HasColumnName("VPoorNoOfM");
            this.Property(t => t.VPoorNoOfB).HasColumnName("VPoorNoOfB");
            this.Property(t => t.PoorNoOfM).HasColumnName("PoorNoOfM");
            this.Property(t => t.PoorNoOfB).HasColumnName("PoorNoOfB");
            this.Property(t => t.MiddleNoOfM).HasColumnName("MiddleNoOfM");
            this.Property(t => t.MiddleNoOfB).HasColumnName("MiddleNoOfB");
            this.Property(t => t.BOffNoOfM).HasColumnName("BOffNoOfM");
            this.Property(t => t.BOffNoOfB).HasColumnName("BOffNoOfB");
            this.Property(t => t.Zone).HasColumnName("Zone");
            this.Property(t => t.District).HasColumnName("District");

            // Relationships
            //this.HasOptional(t => t.AdminUnit)
            //    .WithMany(t => t.NeedAssements)
            //    .HasForeignKey(d => d.Zone);
            //this.HasOptional(t => t.AdminUnit1)
            //    .WithMany(t => t.NeedAssements1)
            //    .HasForeignKey(d => d.District);
        }
    }
}
