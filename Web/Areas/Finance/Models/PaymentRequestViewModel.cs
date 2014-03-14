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
        public decimal RequestedAmount { get; set; }
        public string ReferenceNo { get; set; }
        public int BusinessProcessID { get; set; }
        public decimal LabourCostRate { get; set; }
        public decimal LabourCost { get; set; }
    }
}