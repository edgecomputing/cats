using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.EarlyWarning.Models
{
    public class ContributionDetailViewModel
    {
        public int ContributionID { get; set; }
        public int ContributionDetailID { get; set; }
        public string Commodity { get; set; }
        public int CommodityID { get; set; }
        public string PledgeReferenceNumber { get; set; }
        public DateTime PledgeDate { get; set; }
        public decimal Quantity { get; set; }
    }
}