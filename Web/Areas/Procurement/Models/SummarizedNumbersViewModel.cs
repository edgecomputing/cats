using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Procurement.Models
{
    public class SummarizedNumbersViewModel
    {
        public int PaymentRequests { get; set; }
        public int PaymentRequestsFromTransporters { get; set; }
        public int PaymentRequestsAtLogistics { get; set; }
        public int ApprovedPaymentRequests { get; set; }
        public int RejectedPaymentRequests { get; set; }
        public int CheckIssuedPaymentRequests { get; set; }
        public int CheckCashedPaymentRequests { get; set; }
    }
}