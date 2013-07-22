using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Procurement.Models
{
    public class DispatchLocationViewModel
    {
        public int TransportOrerDetailID { get; set; }
        public string RequisitionNumber { get; set; }
        public string Warehouse { get; set; }
        public string Zone { get; set; }
        public string Woreda { get; set; }
        public string Destination { get; set; }
        public string Item { get; set; }
        public decimal Quantity { get; set; }
        public decimal Tariff { get; set; }
        public decimal Total { get { return Quantity * Tariff; } }

    }
}