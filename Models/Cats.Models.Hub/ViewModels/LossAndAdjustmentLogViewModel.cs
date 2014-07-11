using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hubs.ViewModels
{
    public class LossAndAdjustmentLogViewModel
    {
        public Guid TransactionId { get; set; }
        // Loss or Adjustment 
        [Display(Name="Type")]
        public string Type { get; set; }

        [Display(Name="Commodity Name")]
        public string CommodityName { get; set; }

        [Display(Name="Project Code")]
        public string ProjectCodeName { get; set; }

        [Display(Name="Date")]
        public DateTime Date { get; set; }

        [Display(Name="Memo Number")]
        public string MemoNumber { get; set; }

        [Display(Name="SI")]
        public string ShippingInstructionName { get; set; }

        [Display(Name="Store")]
        public string Store { get; set; }

        [Display(Name="Store Man")]
        public string StoreMan { get; set; }

        [Display(Name="Reason")]
        public string Reason { get; set; }
        public string Description { get; set; }
        [Display(Name="Unit")]
        public string Unit { get; set; }
        [Display(Name="Quantity In Unit")]
        public decimal QuantityInUnit { get; set; }
        [Display(Name="Quantity In MT")]
        public decimal QuantityInMt { get; set; }
        [Display(Name="Approved By")]
        public string ApprovedBy { get; set; }

        //Methods

        public LossAndAdjustmentLogViewModel()
        {
        }

    }
}
