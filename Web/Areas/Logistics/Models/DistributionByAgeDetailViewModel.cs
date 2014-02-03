using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Logistics.Models
{
    public class WoredaDistributionDetailViewModel
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
        public int RequisitionId { get; set; }
        public string RequistionNo { get; set; }
        public int Programid { get; set; }
        public string Program { get; set; }
        public int WoredaID { get; set; }
        public string WoredaName { get; set; }
        public decimal DistributedAmount { get; set; }
        public int CommodityID { get; set; }
        public string CommodityName { get; set; }
        public int ? Round { get; set; }
        public string Month { get; set; }
        public decimal BeginingBalance { get; set; }
        public decimal EndingBalance { get; set; }
        public decimal TotalIn { get; set; }
        public decimal TotalOut { get; set; }
        public string DistributionStartDate { get; set; }
        public string DistributionEndDate { get; set; }
        public decimal LossAmount { get; set; }
        public string LossReason { get; set; }

        public RequisitionDetailViewModel RequisitionDetailViewModel { get; set; }
        public int WoredaStockDistributionID { get; set; }
        public int WoredaStockDistributionDetailID { get; set; }
        public int MaleLessThan5Years { get; set; }
        public int FemaleLessThan5Years { get; set; }
        public int MaleBetween5And18Years { get; set; }
        public int FemaleBetween5And18Years { get; set; }
        public int MaleAbove18Years { get; set; }
        public int FemaleAbove18Years { get; set; }
       
    }
    public class RequisitionDetailViewModel
    {
        public int CommodityID { get; set; }
        public string CommodityName { get; set; }
        public int BeneficaryNumber { get; set; }
        public decimal AllocatedAmount { get; set; }

    }
}