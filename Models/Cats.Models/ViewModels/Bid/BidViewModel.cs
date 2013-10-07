using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.ViewModels.Bid
{
    public class BidViewModel
    {
        public int BidID { get; set; }
        public DateTime StartDate { get; set; }
        public string StartDatePref { get; set; }
        public DateTime EndDate { get; set; }
        public string EndDatePref { get; set; }
        public string BidNumber { get; set; }
        public DateTime OpeningDate { get; set; }
        public string OpeningDatePref { get; set; }
        public int StatusID { get; set; }
        public string Status { get; set; }
        public int TransportBidPlanID { get; set; }
        public int command { get; set; }
        public List<BidDetail> BidDetails { get; set; }
    }
}
