using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cats.Models.Hub.ViewModels.Report.Data
{
    public class DeliveryReport:BaseReport
    {
        public List<DeliveryRows> Rows { get; set; }
    }

    public class DeliveryRows
    {
        public string SINumber { get; set; }
        public string Donor { get; set; }
        public string Commodity { get; set; }
        public string DeliveryReferenceNumber { get; set; }
        public string DeliveryOrderNumber { get; set; }
        public string Date { get; set; }
        public string ReceiptType { get; set; }
        public string VehiclePlateNumber { get; set; }
        public string DeliveryType { get; set; }
        public string PortName { get; set; }
        public string HubOwner { get; set; }
        public string Hub { get; set; }
        public int WareHouseNumber { get; set; }
        public string Vessel { get; set; }
        public string TransportedBy { get; set; }
        public string ShippedBy { get; set; }
        public decimal DeliveryQuantity { get; set; }
        public decimal DeliveryBag { get; set; }
        public decimal DeliveryNet { get; set; }
        public string Unit { get; set; }
        public string SubCommodity { get; set; }
        public string Project { get; set; }
    }
}
