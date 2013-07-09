using System;
using System.Collections.Generic;

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
        public DateTime OpeningDate { get; set; }
        public int StatusID { get; set; }

        #region Navigation Properties

        public ICollection<BidDetail> BidDetails { get; set; }
       public virtual Status Status { get; set; }
        #endregion
    }
}
