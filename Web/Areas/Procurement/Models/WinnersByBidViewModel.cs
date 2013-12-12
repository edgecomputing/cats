using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Procurement.Models
{
    public class WinnersByBidViewModel
    {
        public int BidID { get; set; }
        public IEnumerable<BidWinnerViewModel> BidWinners { get; set; }
    }
}