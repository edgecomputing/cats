using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hubs.ViewModels
{
    public class ReceiveNewViewModel
    {
        #region Properties 

        public Guid ReceiveId { get; set; }

        [RegularExpression("[0-9]*", ErrorMessage = @"Only numbers allowed")]
        [Required]
        [Display(Name = "GRN")]
        public string Grn { get; set; }

        [Required]
        [Display(Name = "Receipt Date")]
        public DateTime ReceiptDate { get; set; }

        [Display(Name = "Commodity Source")]
        public string CommoditySource { get; set; }

        [Display(Name = "Source Hub")]
        public string SourceHub { get; set; } //For loan 

        [Display(Name = "SI / Batch Number")]
        public string SiNumber { get; set; }

        [Display(Name = "Project Code")]
        public string ProjectCode { get; set; }

        [Required]
        [Display(Name = "Purchase Order")]
        public String PurchaseOrder { get; set; }

        [Required]
        [Display(Name = "Supplier Name")]
        public String SupplierName { get; set; }

        [Display(Name = "Program")]
        public string Program { get; set; }

        [Required(ErrorMessage = "Waybill number is requied")]
        [StringLength(50)]
        [Display(Name = "WayBill Number")]
        public string WayBillNo { get; set; }

        [Display(Name = "Commodity Type")]
        public string CommodityType { get; set; }


        [Required(ErrorMessage = "Store is required")]
        [Display(Name = "Store")]
        public int StoreId { get; set; }

        public List<Store> Stores { get; set; }

        [Required(ErrorMessage = "Stack number is required")]
        [Display(Name = "Stack Number")]
        public int StackNumber { get; set; }

        [Required(ErrorMessage = "Name of store man is required")]
        [Display(Name = "Received By Store Man")]
        [StringLength(50)]
        [UIHint("AmharicTextBox")]
        public string ReceivedByStoreMan { get; set; }

        public Guid? ReceiptAllocationID { get; set; }


        public int CommoditySourceTypeId { get; set; }

        public IEnumerable<ReceiveDetailNewViewModel> ReceiveDetailNewViewModels { get; set; }

        #endregion

    }
}
