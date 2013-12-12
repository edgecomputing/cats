using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models
{
    
   public partial class Bid
    {
       public Bid()
       {
           this.BidDetails=new List<BidDetail>();
           this.BidWinners=new List<BidWinner>();
       }
        public int BidID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string BidNumber { get; set; }
        public DateTime OpeningDate { get; set; }
        public int StatusID { get; set; }
        public int TransportBidPlanID { get; set; }

        #region Navigation Properties

        public ICollection<BidDetail> BidDetails { get; set; }
        public ICollection<BidWinner> BidWinners { get; set; }
        public ICollection<TransportBidQuotation> TransportBidQuotations { get; set; }
       public virtual ICollection<TransporterAgreementVersion> TransporterAgreementVersions { get; set; }
        #endregion
    }
}