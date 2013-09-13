using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Cats.Models;

namespace Cats.Models
{
    public partial class Commodity
    {
        public Commodity()
        {
            this.DispatchAllocations = new List<DispatchAllocation>();
            this.Commodity1 = new List<Commodity>();
           
            this.ReliefRequisitionDetails = new List<ReliefRequisitionDetail>();
            this.TransportOrderDetails = new List<TransportOrderDetail>();
            this.BidWinners=new List<BidWinner>();
            this.RationDetails=new List<RationDetail>();
            this.HRDCommodityDetails=new List<HRDCommodityDetail>();
            this.GiftCertificateDetails = new List<GiftCertificateDetail>();
            this.InKindContributionDetails=new List<InKindContributionDetail>();
            
        }

        public int CommodityID { get; set; }
        public string Name { get; set; }
        public string LongName { get; set; }
        public string NameAM { get; set; }
        public string CommodityCode { get; set; }
        public int CommodityTypeID { get; set; }
        public Nullable<int> ParentID { get; set; }
        public virtual ICollection<DispatchAllocation> DispatchAllocations { get; set; }
        public virtual ICollection<Commodity> Commodity1 { get; set; }
        public virtual Commodity Commodity2 { get; set; }
      
        public virtual CommodityType CommodityType { get; set; }
     
        public virtual ICollection<ReliefRequisitionDetail> ReliefRequisitionDetails { get; set; }
        public virtual ICollection<ReliefRequisition> ReliefRequisitions { get; set; }
        public virtual ICollection<TransportOrderDetail> TransportOrderDetails { get; set; }
        public virtual ICollection<BidWinner> BidWinners  { get; set; }
        public virtual ICollection<RationDetail> RationDetails { get; set; }
        public virtual ICollection<HRDCommodityDetail> HRDCommodityDetails  { get; set; }
        public virtual ICollection<RequestDetailCommodity> RequestDetailCommodities { get; set; }
        public virtual ICollection<GiftCertificateDetail> GiftCertificateDetails { get; set; }

        public virtual ICollection<RegionalPSNPPledge> RegionalPSNPPledges { get; set; }

        public virtual ICollection<InKindContributionDetail> InKindContributionDetails{ get; set; }
 
    }
}
