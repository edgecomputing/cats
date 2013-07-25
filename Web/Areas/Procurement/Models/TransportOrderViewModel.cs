using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Procurement.Models
{
    public class TransportOrderViewModel
    {
        public int TransportOrderID { get; set; }
        public string TransportOrderNo { get; set; }
        public string ContractNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderDateET { get; set; }

        public DateTime RequestedDispatchDate { get; set; }
        public string RequestedDispatchDateET { get; set; }
        public DateTime OrderExpiryDate { get; set; }
        public string OrderExpiryDateET { get; set; }
        public string BidDocumentNo { get; set; }
        public string PerformanceBondReceiptNo { get; set; }
        public int TransporterID { get; set; }
        public string Transporter { get; set; }
        public string ConsignerName { get; set; }
        public string TransporterSignedName { get; set; }
        public DateTime ConsignerDate { get; set; }
        public string ConsignerDateET { get; set; }
        public DateTime TransporterSignedDate { get; set; }
        public string TransporterSignedDateET { get; set; }
    }
}