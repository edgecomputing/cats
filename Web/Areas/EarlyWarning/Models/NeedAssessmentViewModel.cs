using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cats.Areas.EarlyWarning.Models
{
    public class NeedAssessmentViewModel
    {

        
        public int NAId { get; set; }
        public int NaHeaderId { get; set; }
        [Display(Name = "No. Months")]
        public int VPoorNoOfM { get; set; }
         [Display(Name = "Beneficiaries")]
        public int VPoorNoOfB { get; set; }
         [Display(Name = "No. Months")]
        public int PoorNoOfM { get; set; }
         [Display(Name = "Beneficiaries")]
        public int PoorNoOfB { get; set; }
         [Display(Name = "No. Months")]
        public int MiddleNoOfM { get; set; }
         [Display(Name = "Beneficiaries")]
        public int MiddleNoOfB { get; set; }
         [Display(Name = "No. Months")]
        public int BOffNoOfM { get; set; }
         [Display(Name = "Beneficiaries")]
        public int BOffNoOfB { get; set; }
        public int Zone { get; set; }
        public int District { get; set; }

        public string ZoneName { get; set; }
        public string DistrictName { get; set; }

        public Nullable<System.DateTime> NeedACreatedDate { get; set; }
        public Nullable<int> NeedACreatedBy { get; set; }
        public Nullable<bool> NeedAApproved { get; set; }
        public string Remark { get; set; }

        public string CreaterUser { get; set; }

    }
}