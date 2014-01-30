using System;
using System.Collections.Generic;

namespace Cats.Models
{
    public partial class DeliveryDetail
    {
        public System.Guid DeliveryDetailID { get; set; }
        public int CommodityID { get; set; }
        public int UnitID { get; set; }
        public decimal SentQuantity { get; set; }
        public decimal ReceivedQuantity { get; set; }
        public System.Guid DeliveryID { get; set; }
        public virtual Commodity Commodity { get; set; }
        public virtual Delivery Delivery { get; set; }
        public virtual Unit Unit { get; set; }
    }
}
