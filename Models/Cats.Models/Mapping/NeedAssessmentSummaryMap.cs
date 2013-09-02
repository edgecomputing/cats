using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
    public class NeedAssessmentSummaryMap : EntityTypeConfiguration<NeedAssessmentSummary>
    {
        public NeedAssessmentSummaryMap() {

            this.ToTable("vw_NeedAssessment");
            this.Property(t => t.NeedAID).HasColumnName("NeedAID");
            this.Property(t => t.RegionName).HasColumnName("Name");
            this.Property(t => t.Season).HasColumnName("Season");
            this.Property(t => t.TypeOfNeedAssessment).HasColumnName("TypeOfNeedAssessment");
            this.Property(t => t.NeedAApproved).HasColumnName("NeedAApproved");
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.PSNPFromWoredasMale).HasColumnName("PSNPFromWoredasMale");
            this.Property(t => t.PSNPFromWoredasFemale).HasColumnName("PSNPFromWoredasFemale");
            this.Property(t => t.NonPSNPFromWoredasFemale).HasColumnName("NonPSNPFromWoredasFemale");
            this.Property(t => t.NonPSNPFromWoredasMale).HasColumnName("NonPSNPFromWoredasMale");
            this.Property(t => t.TotalBeneficiaries).HasColumnName("TotalBeneficiaries");
        } 
    }
}