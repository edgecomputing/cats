using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Cats.Models
{
    public partial class Hub
    {
        public Hub()
        {
            //this.DispatchAllocations = new List<DispatchAllocation>();
            //this.HubAllocations = new List<HubAllocation>();
            //this.ReceiptAllocations = new List<ReceiptAllocation>();
            //this.ReceiptAllocations1 = new List<ReceiptAllocation>();
            //this.Transactions = new List<Transaction>();
            //this.TransportOrderDetails = new List<TransportOrderDetail>();
           // this.HubOwner = new HubOwner();
            this.TransportBidQuotations = new List<TransportBidQuotation>();
        }

        public int HubID { get; set; }
        public string Name { get; set; }
        public int HubOwnerID { get; set; }
        public virtual HubOwner HubOwner { get; set; }
        public virtual ICollection<DispatchAllocation> DispatchAllocations { get; set; }
        public virtual ICollection<HubAllocation> HubAllocations { get; set; }
        public virtual ICollection<ReceiptAllocation> ReceiptAllocations { get; set; }
        public virtual ICollection<ReceiptAllocation> ReceiptAllocations1 { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<TransportOrderDetail> TransportOrderDetails { get; set; }
        public virtual ICollection<BidWinner> BidWinners { get; set; }

        public virtual ICollection<TransportBidPlanDetail> TransportBidPlanSources { get; set; }

        public virtual ICollection<TransportBidQuotation> TransportBidQuotations { get; set; }

        public virtual ICollection<PromisedContribution> PromisedContributions { get; set; }
        public virtual ICollection<Distribution> Distributions { get; set; }

        public virtual ICollection<WoredaHubLink> WoredaHubLinks { get; set; }
    }
}