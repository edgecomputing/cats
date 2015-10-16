using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cats.Areas.Logistics.Models
{
    public class LoanReciptPlanViewModel
    {
        public int LoanReciptPlanID { get; set; }
        public int ProgramID { get; set; }
        public string ProgramName { get; set; }
        //public int HubID { get; set; }
        //public string HubName { get; set; }
        public int CommoditySourceID  { get; set; }
        public int CommodityID { get; set; }
        public int? LoanSource { get; set; }
        public string Donor { get; set; }
        public string SourceHubName { get; set; }
        public int ParentCommodityID { get; set; }
        public string ParentCommodity { get; set; }
        public string CommodityName { get; set; }
        public string CommoditySourceName { get; set; }
        public string RefeenceNumber { get; set; }
        public decimal Quantity { get; set; }
        public string ProjectCode { get; set; }
        public string SiNumber { get; set; }
        public string CreatedDate { get; set; }
        public int StatusID { get; set; }
        public string Status { get; set; }
        public bool IsFalseGRN { get; set; }


    }
    public class LoanReciptPlanWithDetailViewModel//:IValidatableObject
    {
        public int LoanReciptPlanDetailID { get; set; }
        public int LoanReciptPlanID { get; set; }
        public int HubID { get; set; }
        public string HubName { get; set; }
        public string MemoRefrenceNumber { get; set; }
        public decimal TotalAmount { get; set; }
        [Required(ErrorMessage = @"Amount is Required")]
         //[Range(0, "Remaining", ErrorMessage = "Minimum value allowed is 0")]
        public decimal Amount { get; set; }
        public decimal Remaining { get; set; }
        public string CreatedDate { get; set; }


        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (Amount > Remaining )
        //    {
        //        yield return new ValidationResult("Amount can not be more than Remaining Amount.");
        //    }
        //}
    }
}