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
            this.Property(t => t.Remark)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("NeedAssessmentDetail", "EarlyWarning");
            this.Property(t => t.NAId).HasColumnName("NAId");
            this.Property(t => t.Woreda).HasColumnName("Woreda");
            this.Property(t => t.ProjectedMale).HasColumnName("ProjectedMale");
            this.Property(t => t.ProjectedFemale).HasColumnName("ProjectedFemale");
            this.Property(t => t.RegularPSNP).HasColumnName("RegularPSNP");
            this.Property(t => t.PSNP).HasColumnName("PSNP");
            this.Property(t => t.NonPSNP).HasColumnName("NonPSNP");
            this.Property(t => t.Contingencybudget).HasColumnName("Contingencybudget");
            this.Property(t => t.TotalBeneficiaries).HasColumnName("TotalBeneficiaries");
            this.Property(t => t.PSNPFromWoredasMale).HasColumnName("PSNPFromWoredasMale");
            this.Property(t => t.PSNPFromWoredasFemale).HasColumnName("PSNPFromWoredasFemale");
            this.Property(t => t.PSNPFromWoredasDOA).HasColumnName("PSNPFromWoredasDOA");
            this.Property(t => t.NonPSNPFromWoredasMale).HasColumnName("NonPSNPFromWoredasMale");
            this.Property(t => t.NonPSNPFromWoredasFemale).HasColumnName("NonPSNPFromWoredasFemale");
            this.Property(t => t.NonPSNPFromWoredasDOA).HasColumnName("NonPSNPFromWoredasDOA");
            this.Property(t => t.Remark).HasColumnName("Remark");

            // Relationships
            this.HasOptional(t => t.AdminUnit)
                .WithMany(t => t.NeedAssessmentDetails)
                .HasForeignKey(d => d.Woreda);
            this.HasOptional(t => t.NeedAssessmentHeader)
                .WithMany(t => t.NeedAssessmentDetails)
                .HasForeignKey(d => d.NeedAId);

        }
    }
}
