using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Finance.Models
{
    public class PaymentRequestViewModel
    {
        public int PaymentRequestID { get; set; }
        public int TransportOrderID { get; set; }
        public string TransportOrderNo { get; set; }
        public string Transporter { get; set; }
        public int TransporterID { get; set; }
        public decimal RequestedAmount { get; set; }
        public string ReferenceNo { get; set; }
        public int BusinessProcessID { get; set; }
        public int StateNo { get; set; }
        public Nullable<decimal> LabourCostRate { get; set; }
        public Nullable<decimal> LabourCost { get; set; }
        public Nullable<decimal> RejectedAmount { get; set; }
        public string RejectionReason { get; set; }
        public Nullable<int> Status { get; set; }
    }
}