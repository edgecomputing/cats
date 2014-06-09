using System;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.MetaData
{
    public class BidMetaData
    {
        [Required(ErrorMessage ="Please Enter Start Date")]
        //[DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        
        [Required(ErrorMessage ="Please Enter End Date")]
        //[DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Required (ErrorMessage ="Please Enter Bid Number",AllowEmptyStrings = false)]
        [Display(Name = "Bid Number")]
        public string BidNumber { get; set; }

        [Required(ErrorMessage = "Please Enter Region", AllowEmptyStrings = false)]
        public int RegionID { get; set; }
        //[DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Display(Name = "Opening Date")]
        public DateTime OpeningDate { get; set; }
        [Required (ErrorMessage = "Please Select Status")]
        public int StatusID { get; set; }
        [Required (ErrorMessage = "Please Select Bid Plan")]
        public int TransportBidPlanID { get; set; }
    }
}
