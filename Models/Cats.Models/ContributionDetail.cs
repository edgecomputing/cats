using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
   public partial class ContributionDetail
    {
        public int ContributionDetailID { get; set; }
        public int ContributiionID { get; set; }
        public int CommodityID { get; set; }
        public string PledgeReferenceNo { get; set; }
        public Nullable<DateTime> PledgeDate { get; set; }
        public decimal Quantity { get; set; }


        public virtual Commodity Commodity { get; set; }
        public virtual Contribution Contribution { get; set; }

    }
}
