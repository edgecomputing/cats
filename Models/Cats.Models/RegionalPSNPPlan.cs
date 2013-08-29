using System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LanguageHelpers.Localization.DataAnnotations;
namespace Cats.Models
{
    public class RegionalPSNPPlan
    {

        //RegionalPSNPPlanID
        [Display(Name = "ID")]
        
        public int RegionalPSNPPlanID { get; set; }

        //Year
        [Display(Name = "Year")]
        [Required_]
        public int Year { get; set; }

        //Duration
        [Display(Name = "Duration")]
        public int Duration { get; set; }

        //Region
        [Display(Name = "Region")]
        public virtual AdminUnit Region { get; set; }

        //RegionID
        [Display(Name = "Region ID")]
        public int RegionID { get; set; }

        //Ration
        [Display(Name = "Ration")]
        public virtual Ration Ration { get; set; }

        //RationID
        [Display(Name = "Ration ID")]
        public int RationID { get; set; }

       
        //Status
        [Display(Name = "Status")]
        public virtual BusinessProcess AttachedBusinessProcess { get; set; }

        //StatusID
        [Display(Name = "Status ID")]
        public int StatusID { get; set; }
       
        public string ShortName
        {
            get { return this.Year + "-" + this.Region.Name; }
        }
        public virtual ICollection<RegionalPSNPPlanDetail> RegionalPSNPPlanDetails { get; set; }

        public virtual ICollection<RegionalPSNPPledge> RegionalPSNPPledges { get; set; }
    }
}