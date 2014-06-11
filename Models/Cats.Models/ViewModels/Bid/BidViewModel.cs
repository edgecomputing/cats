using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.ViewModels.Bid
{
    public class BidViewModel
    {
        public int BidID { get; set; }
        public DateTime StartDate { get; set; }
        [Display(Name="Start Date")]
        public string StartDatePref { get; set; }
        public DateTime EndDate { get; set; }
        [Display(Name="End Date")]
        public string EndDatePref { get; set; }
        public string BidNumber { get; set; }
        public DateTime OpeningDate { get; set; }
        [Display(Name="Opening Date")]
        public string OpeningDatePref { get; set; }
        public int StatusID { get; set; }
        public string Status { get; set; }
        public int TransportBidPlanID { get; set; }
        public int command { get; set; }
        public decimal BidBondAmount { get; set; }
        public List<BidDetail> BidDetails { get; set; }
    }
    public class CreateBidViewModel
    {
        public int? PartitionId { get; set; }
        public int BidID { get; set; }

        public int RegionID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string BidNumber { get; set; }
        public DateTime OpeningDate { get; set; }
        public int StatusID { get; set; }
        public int TransportBidPlanID { get; set; }
        public decimal BidBondAmount { get; set; }
        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }

        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }

        [DataType(DataType.Time)]
        public DateTime BidOpningTime { get; set; }


        //[Required]
        //[RegularExpression(@"^(0[1-9]|1[0-2]):[0-5][0-9] (am|pm|AM|PM)$", ErrorMessage = "Invalid Time.")]
        //public string StartTimeValue
        //{
        //    get { return StartTime.HasValue ? StartTime.Value.ToString("hh:mm tt") : string.Empty; }

        //    set { StartTime = DateTime.Parse(value); }
        //}
    }
}
