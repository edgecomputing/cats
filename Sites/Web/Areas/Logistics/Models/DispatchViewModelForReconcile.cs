using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cats.Areas.Logistics.Models
{
    public class DispatchViewModelForReconcile
    {
        public Guid DispatchID { get; set; }


        public string FDP { get; set; }
        public string Region { get; set; }
        public string Zone { get; set; }
        public string Woreda { get; set; }

        [Required]
        [Display(Name = "Store Man")]
        [UIHint("AmharicTextBox")]
        public string DispatchedByStoreMan { get; set; }

        [Required]
        [Display(Name = "Dispatch Date")]
        public DateTime DispatchDate { get; set; }
        [Display(Name = "Requisition No")]
        public string RequisitionNo { get; set; }

        [Required]
        [Display(Name = "GIN")]
        [RegularExpression("[0-9]*", ErrorMessage = "Only numbers allowed for GIN")]
        [Remote("NotUnique", "Dispatch", AdditionalFields = "DispatchID")]
        [StringLength(7, ErrorMessage = "Length Must be less than or equal to 7")]
        public string GIN { get; set; }

        public string BidNumber { get; set; }



        //[RegularExpression("[a-zA-Z\\s]*", ErrorMessage = "Only letters are allowed for Driver name")]
        [Required(ErrorMessage = "Name of the driver taking the commodities is a required field.")]
        [Display(Name = "Received By (Driver Name)")]
        [UIHint("AmharicTextBox")]
        public string DriverName { get; set; }


        [Required(ErrorMessage = "Prime Plate Number is a required field")]
        [Display(Name = "Plate No. Prime")]
        public string PlateNo_Prime { get; set; }


        [Display(Name = "Plate No. Trailer")]
        public string PlateNo_Trailer { get; set; }


        public string Transporter { get; set; }

        [Display(Name = "Project Code")]
        public string ProjectNumber { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public string MonthYear { get; set; }

        public String Program { get; set; }


        [Display(Name = "Weigh Bridge Ticket Number")]
        public string WeighBridgeTicketNumber { get; set; }


        public int Round { get; set; }



        public string SINumber { get; set; }

        public int CommodityID { get; set; }
        public string Commodity { get; set; }
        [Display(Name = "Remark")]
        [UIHint("AmharicTextArea")]
        public string Remark { get; set; }

        public int Type { get; set; }
        [Display(Name = "Quantity In Unit")]
        public decimal QuantityInUnit { get; set; }
        public decimal Quantity { get; set; }
        [Display(Name = "Unit")]
        public int UnitID { get; set; }

        public Guid DispatchAllocationID { get; set; }
        public int TransporterID { get; set; }

        public int ProgramID
        {
            get;
            set;
        }

        public int HubID
        {
            get;
            set;
        }

        public int FDPID
        {
            get;
            set;
        }
        [Display(Name = "Created Date")]
        public DateTime CreatedDate
        {
            get;
            set;
        }
        [Display(Name = "Created Date")]
        public string CreatedDatePref { get; set; }
        [Display(Name = "Dispatched Date")]
        public string DispatchDatePref { get; set; }
        public int UserProfileID { get; set; }
        public int? ShippingInstructionID { get; set; }
        public int? ProjectCodeID { get; set; }

        public bool GRNReceived { get; set; }
        public bool GRNReconciled { get; set; }
        public Guid DeliveryID { get; set; }
        [Display(Name = "Quantity Per Unit")]
        public decimal? QuantityPerUnit { get; set; }

        //Reconcilation Data: If only it exists 
        public int? DeliveryReconcileID { get; set; }
        public string GRN { get; set; }
        public string WayBillNo { get; set; }
        public decimal? ReceivedAmount { get; set; }
        public System.DateTime? ReceivedDate { get; set; }
        public Nullable<decimal> LossAmount { get; set; }
        public string LossReason { get; set; }
        public Nullable<System.Guid> TransactionGroupID { get; set; }
        public string Hub { get; set; }
    }
}