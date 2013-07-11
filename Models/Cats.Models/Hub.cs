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
            this.TransportOrderDeatils = new List<TransportOrderDetail>();
            this.HubAllocations = new List<HubAllocation>();
            this.BidWinners=new List<BidWinner>();
        }
        public int HubId { get; set; }
        public string Name { get; set; }
        public int HubOwnerId { get; set; }

        public virtual ICollection<TransportBidPlanDetail> TransportBidPlanSources { get; set; }

        public virtual ICollection<TransportOrderDetail> TransportOrderDeatils { get; set; }
        public virtual ICollection<HubAllocation> HubAllocations { get; set; }
        public virtual ICollection<BidWinner> BidWinners  { get; set; }

    }
}