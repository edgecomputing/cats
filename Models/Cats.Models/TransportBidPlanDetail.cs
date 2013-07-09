using System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models
{
    public class TransportBidPlanDetail
    {

        //TransportBidPlanDetailID
        [Display(Name = "ID")]
        public int TransportBidPlanDetailID { get; set; }

        //BidPlan
        [Display(Name = "ID")]
        public TransportBidPlan BidPlan { get; set; }

        //BidPlanID
        [Display(Name = "ID ID")]
        public int BidPlanID { get; set; }


        //Destination
        [Display(Name = "Destination")]
        public virtual AdminUnit Destination { get; set; }

        //DestinationID
        [Display(Name = "Destination ID")]
        public int DestinationID { get; set; }


        //Source
        [Display(Name = "Source")]
        public virtual Hub Source { get; set; }

        //SourceID
        [Display(Name = "Source ID")]
        public int SourceID { get; set; }


        //Program
        [Display(Name = "Program")]
        public virtual Program Program { get; set; }

        //ProgramID
        [Display(Name = "Program ID")]
        public int ProgramID { get; set; }


        //Quantity
        [Display(Name = "Quantity")]
        public decimal Quantity { get; set; }
    }
}