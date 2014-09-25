using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Procurement.Models
{
    public class DelayedTransportOrderViewModel
    {
        public int TransportOrderID { get; set; }
        public string TransportOrderNo { get; set; }
        public string SignedDate { get; set; }
        public string StartedOn { get; set; }
        public string TransporterName { get; set; }
        public string OrderExpiryDate { get; set; }
        public double DaysPassedAfterSigned { get; set; }
    }
}