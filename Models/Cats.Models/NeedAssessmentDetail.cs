using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Cats.Models
{
    public partial class NeedAssessmentDetail
    {
        public int NAId { get; set; }
        public Nullable<int> NeedAId { get; set; }
        public Nullable<int> Woreda { get; set; }
        public Nullable<int> ProjectedMale { get; set; }
        public Nullable<int> ProjectedFemale { get; set; }
        public Nullable<int> RegularPSNP { get; set; }
        public Nullable<int> PSNP { get; set; }
        public Nullable<int> NonPSNP { get; set; }
        public Nullable<int> Contingencybudget { get; set; }
        public Nullable<int> TotalBeneficiaries { get; set; }
        public Nullable<int> PSNPFromWoredasMale { get; set; }
        public Nullable<int> PSNPFromWoredasFemale { get; set; }
        public Nullable<int> PSNPFromWoredasDOA { get; set; }
        public Nullable<int> NonPSNPFromWoredasMale { get; set; }
        public Nullable<int> NonPSNPFromWoredasFemale { get; set; }
        public Nullable<int> NonPSNPFromWoredasDOA { get; set; }
        public string Remark { get; set; }
        public virtual AdminUnitType AdminUnitType { get; set; }
        public virtual NeedAssessmentHeader NeedAssessmentHeader { get; set; }
    }
}
