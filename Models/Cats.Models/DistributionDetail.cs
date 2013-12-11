using System;
using System.Collections.Generic;

namespace Cats.Models
{
    public partial class DistributionDetail
    {
        public System.Guid DistributionDetailID { get; set; }
        public int CommodityID { get; set; }
        public int UnitID { get; set; }
        public decimal SentQuantity { get; set; }
        public decimal ReceivedQuantity { get; set; }
        public System.Guid DistributionID { get; set; }
        public virtual Commodity Commodity { get; set; }
        public virtual Distribution Distribution { get; set; }
        public virtual Unit Unit { get; set; }
    }
}
