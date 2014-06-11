using System.ComponentModel.DataAnnotations.Schema;

namespace Cats.Models
{

   public class BidDetail
    {
      
        public int BidDetailID { get; set; }
        public int BidID { get; set; }
        //public int RegionID { get; set; }
        public decimal AmountForReliefProgram { get; set; }
        public decimal AmountForPSNPProgram { get; set; }
        public decimal BidDocumentPrice { get; set; }
        public decimal CPO { get; set; }

        #region Navigation Properties

        public virtual Bid Bid { get; set; }
        //public virtual AdminUnit AdminUnit { get; set; }

        #endregion
    }
}
