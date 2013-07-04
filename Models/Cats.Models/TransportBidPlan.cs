using System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models
{
    public class TransportBidPlan
    {

        //TransportBidPlanID
        [Key]
        [Display(Name = "ID")]
        public int TransportBidPlanID { get; set; }

        //Year
        [Display(Name = "Year")]
        public int Year { get; set; }

        //YearHalf
        [Display(Name = "Year Half")]
        public int YearHalf { get; set; }

        //RegionID
        [Display(Name = "Region")]
        public int RegionID { get; set; }

        //ProgramID
        [Display(Name = "Program")]
        public int ProgramID { get; set; }

        //Region
        [Display(Name = "Region")]
        public AdminUnit Region { get; set; }

        //Program
        [Display(Name = "Program")]
        public Program Program { get; set; }
    }
}