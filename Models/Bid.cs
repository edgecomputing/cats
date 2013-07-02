using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
   public class Bid
    {
       public Bid()
       {
           this.BidDetails=new List<BidDetail>();
       }
        public int BidID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string BidNumber { get; set; }

        #region Navigation Properties

        public ICollection<BidDetail> BidDetails { get; set; }
        #endregion
    }
}
