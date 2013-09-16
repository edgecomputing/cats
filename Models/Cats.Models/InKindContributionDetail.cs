using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public class InKindContributionDetail
    {
        public int InKindContributionDetailID { get; set; }
        public int ContributionID { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTime ContributionDate { get; set; }
        public int CommodityID { get; set; }
        public decimal Amount { get; set; }

        public virtual Contribution Contribution { get; set; }
        public virtual Commodity Commodity { get; set; }
    }
}