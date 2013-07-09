using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Models;

namespace Cats.Areas.Procurement.Models
{
    public class BidViewModel
    {
        public int BidID { get; set; }
        public string BidNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime OpeningDate { get; set; }
        public int StatusID { get; set; }
        public List<BidDetail> BidDetails { get; set; }
    }
}