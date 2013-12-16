using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Procurement.Models
{
    public class ActiveTransportOrderViewModel
    {
        public int TransportOrderID { get; set; }
        public string TransportOderNo { get; set; }
        public string SignedDate { get; set; }
        public string StartedOn { get; set; }
        public string RemainingDays { get; set; }
        public string Progress { get; set; }
    }
}