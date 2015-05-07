using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hubs.ViewModels
{
    public class ReceiveNewViewModel
    {
        #region View Properties 

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


        [Required(ErrorMessage = "Stack number is required")]
        [Display(Name = "Stack Number")]
        public int StackNumber { get; set; }

        [Required(ErrorMessage = "Name of store man is required")]
        [Display(Name = "Received By Store Man")]
        [StringLength(50)]
        [UIHint("AmharicTextBox")]
        public string ReceivedByStoreMan { get; set; }

        public Guid ReceiptAllocationId { get; set; }

        public bool IsFalseGRN { get; set; }
        public Guid? SelectedGRN { get; set; }
        public int CommoditySourceTypeId { get; set; }
        

        public int CurrentHub { get; set; }
        public bool IsTransporterDetailVisible { get; set;}
        public IEnumerable<ReceiveDetailNewViewModel> ReceiveDetailNewViewModels { get; set; }

        public ReceiveDetailNewViewModel ReceiveDetailNewViewModel { get; set; }

        #endregion

        #region Properties 

        public int CommodityTypeId { get; set; }
        [Display(Name = "Source Donor")]
        public int? SourceDonorId { get; set; }
        [Display(Name = "Responsible Donor / Implementor")]
        public int? ResponsibleDonorId { get; set; }
        [Display(Name = "Transporter")]
        public int TransporterId { get; set; }
        [Display(Name = "Plate Number (Prime)")]
        public string PlateNoPrime { get; set; }
        [Display(Name = "Plate No Trailer")]
        public string PlateNoTrailer { get; set; }
        [Display(Name = "Delivered By (Driver Name))")]
        public string DriverName { get; set; }
        [Display(Name = "Weight Bridge Ticket Number")]
        public string WeightBridgeTicketNumber { get; set; }
        [Display(Name = "Weight Before Unloading")]
        public decimal? WeightBeforeUnloading { get; set; }
        [Display(Name = "Weight After Unloading")]
        public decimal? WeightAfterUnloading { get; set; }
        [Display(Name = "Vessel Name")]
        public string VesselName { get; set; }
        [Display(Name = "Port Name")]
        public string PortName { get; set; }
        [Display(Name = "Remark")]
        public string Remark { get; set; }

        public int ProgramId { get; set; }
        public int? SourceHubId { get; set; }

        public int UserProfileId { get; set; }

        public AllocationStatusViewModel AllocationStatusViewModel { get; set; }

        #endregion 
    }
}
