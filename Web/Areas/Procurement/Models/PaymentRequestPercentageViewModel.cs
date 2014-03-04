using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Procurement.Models
{
    public class PaymentRequestPercentageViewModel
    {
        public decimal Requested { get; set; }
        public decimal Submitted { get; set; }
        public decimal Approved { get; set; }
        public decimal Rejected { get; set; }
        public decimal CheckIssued { get; set; }
        public decimal CheckCashed { get; set; }
    }
}