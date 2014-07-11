using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Models.Hubs
{
    public class SummaryFreeAndPhysicalStockModel
    {
        public string HubName { get; set; }
        public string Program { get; set; }
        public string CommodityName { get; set; }
        public decimal FreeStock { get; set; }
        public decimal PhysicalStock { get; set; }
    }
}