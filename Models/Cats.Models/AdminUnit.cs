using System;
using System.Collections.Generic;

namespace Cats.Models
{
    public partial class AdminUnit
    {
        public AdminUnit()
        {
            this.AdminUnit1 = new List<AdminUnit>();
            this.BidDetails = new List<BidDetail>();
            this.FDPs = new List<FDP>();
            this.RegionalRequests = new List<RegionalRequest>();
            this.ReliefRequisitions = new List<ReliefRequisition>();
            this.ReliefRequisitions1 = new List<ReliefRequisition>();
            this.TransportOrderDetails = new List<TransportOrderDetail>();
            this.BidWinners=new List<BidWinner>();


            this.NeedAssessments = new List<NeedAssessment>();
            this.NeedAssessmentDetails = new List<NeedAssessmentDetail>();
            this.NeedAssessmentHeaders = new List<NeedAssessmentHeader>();

            this.HrdDetails=new List<HRDDetail>();
            this.HrdDonorCoverageDetails = new List<HrdDonorCoverageDetail>();
            this.TransportBidQuotations = new List<TransportBidQuotation>();
            this.TransportBidQuotationHeaders = new List<TransportBidQuotationHeader>();
        }

        public int AdminUnitID { get; set; }
        public string Name { get; set; }
        public string NameAM { get; set; }
        public Nullable<int> AdminUnitTypeID { get; set; }
        public Nullable<int> ParentID { get; set; }
        public virtual ICollection<AdminUnit> AdminUnit1 { get; set; }
        public virtual AdminUnit AdminUnit2 { get; set; }
        public virtual AdminUnitType AdminUnitType { get; set; }
        public virtual ICollection<BidDetail> BidDetails { get; set; }
        public virtual ICollection<FDP> FDPs { get; set; }
        public virtual ICollection<NeedAssessment> NeedAssessments { get; set; }
        public virtual ICollection<NeedAssessmentDetail> NeedAssessmentDetails { get; set; }
        public virtual ICollection<NeedAssessmentHeader> NeedAssessmentHeaders { get; set; }
        public ICollection<RegionalRequest> RegionalRequests { get; set; }
        public ICollection<ReliefRequisition> ReliefRequisitions { get; set; }
        public virtual ICollection<ReliefRequisition> ReliefRequisitions1 { get; set; }

        public virtual ICollection<TransportBidPlanDetail> TransportBidPlanDestinations { get; set; }
        public virtual ICollection<HRDDetail> HrdDetails { get; set; }    

        public virtual ICollection<TransportOrderDetail> TransportOrderDetails { get; set; }
        public virtual ICollection<BidWinner> BidWinners { get; set; }
        public virtual ICollection<TransportBidQuotation> TransportBidQuotations { get; set; }
      
        
        public virtual ICollection<TransportRequisition> TransportRequisitions { get; set; }
        public virtual ICollection<WoredaHubLink> WoredaHubLinks { get; set; }
        public virtual ICollection<HrdDonorCoverageDetail> HrdDonorCoverageDetails { get; set; }
        public virtual ICollection<TransportBidQuotationHeader> TransportBidQuotationHeaders { get; set; }

    }
}
