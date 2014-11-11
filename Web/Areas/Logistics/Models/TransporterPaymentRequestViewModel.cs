using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Models;

namespace Cats.Areas.Logistics.Models
{
    public class TransporterPaymentRequestViewModel
    {
        public int TransporterPaymentRequestID { get; set; }
        public string ReferenceNo { get; set; }
        public int TransportOrderID { get; set; }
        public System.Guid DeliveryID { get; set; }
        public decimal ShortageQty { get; set; }
        public Nullable<decimal> ShortageBirr { get; set; }
        public Nullable<decimal> LabourCostRate { get; set; }
        public Nullable<decimal> LabourCost { get; set; }
        public Nullable<decimal> RejectedAmount { get; set; }
        public string RejectionReason { get; set; }
        public DateTime RequestedDate { get; set; }
        public int BusinessProcessID { get; set; }
        public Cats.Models.Hubs.Program Program { get; set; }
        public string RequisitionNo { get; set; }
        public string GIN { get; set; }
        public string GRN { get; set; }
        public string Commodity { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public decimal ReceivedQty { get; set; }
        public decimal SentQty { get; set; }
        public decimal Tarrif { get; set; }
        public decimal FreightCharge { get; set; }
        public BusinessProcess BusinessProcess { get; set; }
        public Cats.Models.Hubs.Transporter Transporter { get; set; }
    }
}