using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cats.Areas.Logistics.Models
{
    public class DistributionViewModel
    {
        public System.Guid DistributionID { get; set; }
         [Display(Name = "Receiving Number")]
        [Required(ErrorMessage="Goods receiving number Can't be empty.")]

        public string ReceivingNumber { get; set; }
        public Nullable<int> DonorID { get; set; }
        public string Donor { get; set; }
        public int TransporterID { get; set; }
        public string Transporter { get; set; }
         [Display(Name = "Plate No Primary")]
        public string PlateNoPrimary { get; set; }
        [Display(Name = "Plate No Trailler")]
        public string PlateNoTrailler { get; set; }
        [Display(Name = "Driver Name")]
        public string DriverName { get; set; }
        public int FDPID { get; set; }
        public string FDP { get; set; }
        public string Region { get; set; }
        public string Zone { get; set; }
        public string Woreda { get; set; }
        public Nullable<System.Guid> DispatchID { get; set; }
        [Display(Name = "Way Bill No")]
        public string WayBillNo { get; set; }
        [Display(Name = "Requisition No")]
        public string RequisitionNo { get; set; }
        public Nullable<int> HubID { get; set; }
        public string Hub { get; set; }
        [Display(Name = "Invoice No")]
        public string InvoiceNo { get; set; }
        [Display(Name = "Delivery By")]
        public string DeliveryBy { get; set; }
        [Display(Name = "Delivery Date")]
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        [Display(Name = "Delivery Date")]
        public string DeliveryDatePref { get; set; }

        [Display(Name = "Received By")]
        public string ReceivedBy { get; set; }
        [Display(Name = "Received Date")]
        public Nullable<System.DateTime> ReceivedDate { get; set; }
        [Display(Name = "Received Date")]
        public string ReceivedDatePref { get; set; }
        [Display(Name = "Document Received Date")]
        public Nullable<System.DateTime> DocumentReceivedDate { get; set; }
        [Display(Name="Document Received By")]
        public string DocumentReceivedDatePref { get; set; }
        [Display(Name = "Document Received By")]
        public Nullable<int> DocumentReceivedBy { get; set; }

        public int? Status { get; set; }
        public string Remark { get; set; }
        public string ActionTypeRemark { get; set; }
        public bool ContainsDiscripancy { get; set; }
    }
}