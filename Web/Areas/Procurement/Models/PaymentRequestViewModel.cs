using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Procurement.Models
{
    public class PaymentRequestViewModel
    {
        public int PaymentRequestID { get; set; }
        public int TransportOrderID { get; set; }
        public string TransporterName { get; set; }
        public string TransportOrderNo { get; set; }
        public decimal RequestedAmount { get; set; }
        public string ReferenceNo { get; set; }
        public int BusinessProcessID { get; set; }
    }
}