using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Logistics.Models
{
    public class HubStockStatus{
        public int HubId;
        public string HubName;
        public decimal PhysicalStock;
        public decimal FreeStock;
    }

    public class FreeStockStatusViewModel
    {
        public string CommodityName {get;set;}
        public decimal TotalPhysicalStock {get;set;}
        public decimal TotalFreeStock {get;set;}
        public decimal Shortage  {get;set;}
        List<HubStockStatus> Detail  {get;set;}
    }
}