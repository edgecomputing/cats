using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Cats.Models;

namespace Cats.Areas.Logistics.Models
{
    public class WoredaDistributionDetailViewModel:IValidatableObject
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
        public int? RequisitionId { get; set; }
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
        [DisplayFormat(DataFormatString ="{0:mm/dd/yy}" ,ApplyFormatInEditMode = true)]
        public DateTime? DistributionStartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:mm/dd/yy}", ApplyFormatInEditMode = true)]
        public DateTime? DistributionEndDate { get; set; }
        public decimal LossAmount { get; set; }
        public string LossReason { get; set; }
        public int LossReasonId { get; set; }
        public decimal dispatched { get; set; }
        public decimal delivered { get; set; }
        public RequisitionDetailViewModel RequisitionDetailViewModel { get; set; }
        public int WoredaStockDistributionID { get; set; }
        public int WoredaStockDistributionDetailID { get; set; }
        public int MaleLessThan5Years { get; set; }
        public int FemaleLessThan5Years { get; set; }
        public int MaleBetween5And18Years { get; set; }
        public int FemaleBetween5And18Years { get; set; }
        public int MaleAbove18Years { get; set; }
        public int FemaleAbove18Years { get; set; }
       

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (TotalOut > (TotalIn + BeginingBalance))
            {
                yield return new ValidationResult("Total out can not be greater than the stock available");
            }

            if (DistributedAmount > TotalOut)
            {
                yield return new ValidationResult("Amount to be distributed can not be greater than the 'Total out' amount");
            }

            if (LossAmount > DistributedAmount)
            {
                yield return new ValidationResult("Loss amount can not be greater than the amount allocated for distribution");
            }
        }
       
    }
    public class RequisitionDetailViewModel
    {
        public int CommodityID { get; set; }
        public string CommodityName { get; set; }
        public int BeneficaryNumber { get; set; }
        public decimal AllocatedAmount { get; set; }

    }
}