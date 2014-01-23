using System;

namespace Cats.Areas.Logistics.Models
{
    public class UtilizationDetailViewModel
    {
        public int FdpId { get; set; }
        public string FDP { get; set; }
        public int NumberOfBeneficiaries { get; set; }
        public decimal AllocatedAmount { get; set; }
        public decimal DispatchedToFDPAmount { get; set; }
        public decimal ReceivedAtFDPAmount { get; set; }
        public decimal RequestedAmount { get; set; }
        public int RegionId { get; set; }
        public string Region { get; set; }
        public int  RequisitionId { get; set; }
        public string RequistionNo { get; set; }
        public int Programid { get; set; }
        public string Program { get; set; }

        public decimal DistributedQuantity { get; set; }
        public DateTime DistributionStartDate { get; set; }
        public DateTime DistributionEndDate { get; set; }
    }
}
