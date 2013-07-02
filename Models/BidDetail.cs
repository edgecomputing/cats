using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
   public class BidDetail
    {
        public int BidDetailID { get; set; }
        public int BidID { get; set; }
        public int RegionID { get; set; }
        public Decimal AmountForReliefProgram { get; set; }
        public Decimal AmountForPSNPProgram { get; set; }
        public float BidDocumentPrice { get; set; }
        public float CBO { get; set; }

        #region Navigation Properties

        public virtual Bid Bid { get; set; }
        public virtual AdminUnit AdminUnit { get; set; }

        #endregion
    }
}
