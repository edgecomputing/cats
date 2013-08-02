using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class NeedAssessmentDetailMap : EntityTypeConfiguration<NeedAssessmentDetail>
    {
        public NeedAssessmentDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.NAId);

            // Properties
            // Table & Column Mappings
            this.ToTable("NeedAssessmentDetail", "EarlyWarning");
            this.Property(t => t.NAId).HasColumnName("NAId");
            this.Property(t => t.NAHeaderId).HasColumnName("NAHeaderId");
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
            this.HasOptional(t => t.AdminUnit)
                .WithMany(t => t.NeedAssessmentDetails)
                .HasForeignKey(d => d.Zone);
            this.HasOptional(t => t.AdminUnit1)
                .WithMany(t => t.NeedAssessmentDetails1)
                .HasForeignKey(d => d.District);
            this.HasRequired(t => t.NeedAssessmentHeader)
                .WithMany(t => t.NeedAssessmentDetails)
                .HasForeignKey(d => d.NAHeaderId);

        }
    }
}
