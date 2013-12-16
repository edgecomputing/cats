using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Logistics.Models
{
    public class DistributionDetailViewModel
    {
        public System.Guid DistributionDetailID { get; set; }
        public int CommodityID { get; set; }
        public int UnitID { get; set; }
        public decimal SentQuantity { get; set; }
        public decimal ReceivedQuantity { get; set; }
        public System.Guid DistributionID { get; set; }
        public string Commodity { get; set; }
        public string Unit { get; set; }
    }
}