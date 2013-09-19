using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Cats.Models
{
    public class Hub
    {
        public Hub()
        {
            this.TransportOrderDetails = new List<TransportOrderDetail>();
            this.HubAllocations = new List<HubAllocation>();
            this.BidWinners=new List<BidWinner>();
            this.Stores=new List<Store>();
        }
        public int HubId { get; set; }
        public string Name { get; set; }
        public int HubOwnerId { get; set; }

        public virtual ICollection<TransportBidPlanDetail> TransportBidPlanSources { get; set; }

        public virtual ICollection<TransportOrderDetail> TransportOrderDetails { get; set; }
        public virtual ICollection<HubAllocation> HubAllocations { get; set; }
        public virtual ICollection<BidWinner> BidWinners  { get; set; }
        public virtual ICollection<TransportBidQuotation> TransportBidQuotations { get; set; }
        public virtual ICollection<Store> Stores { get; set; }

    }
}