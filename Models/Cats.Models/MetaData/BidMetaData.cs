using System;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.MetaData
{
    public class BidMetaData
    {
        [Required(ErrorMessage ="Please Enter Start Date")]
        //[DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime StartDate { get; set; }
        
        [Required(ErrorMessage ="Please Enter End Date")]
        //[DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime EndDate { get; set; }

        [Required (ErrorMessage ="Please Enter Bid Number",AllowEmptyStrings = false)]
        public string BidNumber { get; set; }

        //[DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime OpeningDate { get; set; }
        [Required (ErrorMessage = "Please Select Status")]
        public int StatusID { get; set; }
        [Required (ErrorMessage = "Please Select Bid Plan")]
        public int TransportBidPlanID { get; set; }
    }
}
