using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Areas.Procurement.Models
{
    public class TransportBidPlanViewModel
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

        public string ShortName
        {
            get { return this.Year + "-" + this.YearHalf; }
        }
    }
}