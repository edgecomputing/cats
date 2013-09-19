using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hub.ViewModels
{
    public class InternalMovementLogViewModel
    {
        public Guid TransactionId { get; set; }
        [Display(Name="From Store")]
        public string FromStore { get; set; }
        [Display(Name="Date")]
        public DateTime SelectedDate { get; set; }
        [Display(Name="From Stack")]
        public int FromStack { get; set; }
        [Display(Name="Referance Number")]
        public string RefernaceNumber { get; set; }
        [Display(Name="Commodity Name")]
        public string CommodityName { get; set; }
        [Display(Name="Program")]
        public string Program { get; set; }
        [Display(Name="Project Code")]
        public string ProjectCodeName { get; set; }
        [Display(Name="SI Number")]
        public string ShippingInstructionNumber { get; set; }
        [Display(Name="Unit")]
        public string Unit { get; set; }
        [Display(Name = "Quantity In Unit")]
        public decimal QuantityInUnit { get; set; }
        [Display(Name = "Quantit In Mt")]
        public decimal QuantityInMt { get; set; }
        [Display(Name = "To Store")]
        public string ToStore { get; set; }
        [Display(Name = "To Stack")]
        public int ToStack { get; set; }
        [Display(Name = "Reason")]
        public string Reason { get; set; }
        [Display(Name="Note")]
        public string Note { get; set; }
        [Display(Name = "Approved By")]
        public string ApprovedBy { get; set; }
    }
}
