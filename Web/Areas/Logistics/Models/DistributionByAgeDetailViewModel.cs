using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Logistics.Models
{
    public class DistributionByAgeDetailViewModel
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

        public int DistributionHeadrID { get; set; }
        public int MaleLessThan5Years { get; set; }
        public int FemaleLessThan5Years { get; set; }
        public int MaleBetween5And18Years { get; set; }
        public int FemaleBetween5And18Years { get; set; }
        public int MaleAbove18Years { get; set; }
        public int FemaleAbove18Years { get; set; }
        public int Total
        {
            get
            {
                return MaleLessThan5Years + MaleBetween5And18Years + MaleAbove18Years +
                       FemaleLessThan5Years + FemaleBetween5And18Years + FemaleAbove18Years;
            }
        }
    }
}