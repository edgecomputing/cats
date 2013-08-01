using System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models
{
    public class RegionalPSNPPlan
    {

        //RegionalPSNPPlanID
        [Display(Name = "ID")]
        public int RegionalPSNPPlanID { get; set; }

        //Year
        [Display(Name = "Year")]
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

    }
}