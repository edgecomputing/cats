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
        public int ContributionID { get; set; }
        //public int CommodityID { get; set; }
        public string PledgeReferenceNo { get; set; }
        public DateTime PledgeDate { get; set; }
        public decimal Amount { get; set; }
        public int CurrencyID { get; set; }


        public virtual Currency Currency { get; set; }
        public virtual Contribution Contribution { get; set; }

    }
}
