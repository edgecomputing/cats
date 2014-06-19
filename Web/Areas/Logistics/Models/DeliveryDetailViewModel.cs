using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cats.Areas.Logistics.Models
{
    public class DeliveryDetailViewModel
    {
        public System.Guid DeliveryDetailID { get; set; }
        public int CommodityID { get; set; }
        public int UnitID { get; set; }
          
        public decimal SentQuantity { get; set; }
          [Remote("CheckDeliveredQuanity", "Dispatch", AdditionalFields = "SentQuantity")]
        public decimal ReceivedQuantity { get; set; }
        public System.Guid DeliveryID { get; set; }
        public string Commodity { get; set; }
        public string Unit { get; set; }
        public string DeliveryBy { get; set; }
    }
}