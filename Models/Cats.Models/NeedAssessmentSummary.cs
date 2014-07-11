using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models
{
    public partial class NeedAssessmentSummary
    {
        [Key]
        public int NeedAID { get; set;}
        public string RegionName { get; set; }
        public string Season { get; set; }
        public string TypeOfNeedAssessment { get; set; }
        public bool NeedAApproved  { get; set; }
        public int Year { get; set; }
        public int PSNPFromWoredasMale { get; set; }
        public int PSNPFromWoredasFemale {get; set;}
        public int NonPSNPFromWoredasMale { get; set; }
        public int NonPSNPFromWoredasFemale { get; set; }
        public int TotalBeneficiaries { get; set; }
    }
}