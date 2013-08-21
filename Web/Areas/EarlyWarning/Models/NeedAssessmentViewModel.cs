using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cats.Areas.EarlyWarning.Models
{
    public class NeedAssessmentViewModel
    {

        public int Zone { get; set; }
        public int Woreda { get; set; }
        public int Region { get; set; }

        public string ZoneName { get; set; }
        public string WoredaName { get; set; }
        public string RegionName { get; set; }

        public int NeedAID { get; set; }
       
        public string Season { get; set; }
        public System.DateTime NeedADate { get; set; }
        public Nullable<int> NeedACreatedBy { get; set; }
        public Nullable<bool> NeedAApproved { get; set; }
        public Nullable<int> NeedAApprovedBy { get; set; }
        public string Remark { get; set; }

        //from hearder
        public int NAHeaderId { get; set; }
        
        //from detail
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
        
       
       

        public string CreaterUser { get; set; }

    }
}