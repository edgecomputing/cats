using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.EarlyWarning.Models
{
    public class InKindContributionDetailViewModel
    {
        public int InKindContributionDetailID { get; set; }
        public int ContributionID { get; set; }
        public string ReferencNumber { get; set; }
        public DateTime ContributionDate { get; set; }
        public string Commodity { get; set; }
        public int CommodityID { get; set; }
        public decimal Amount { get; set; }

    }
}