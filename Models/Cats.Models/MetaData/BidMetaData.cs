using System;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.MetaData
{
    public class BidMetaData
    {
        [Required(ErrorMessage ="Please Enter Start Date")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage ="Please Enter End Date")]
        public DateTime EndDate { get; set; }
        [Required (ErrorMessage ="Please Enter Bid Number")]
        public string BidNumber { get; set; }
        [Required (ErrorMessage = "Please Select Status")]
        public int StatusID { get; set; }
    }
}
