using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Models.Hubs
{
    public class SummaryFreeAndPhysicalStockModel
    {
        public int HubID { get; set; }
        public string HubName { get; set; }
        public int ProgramID { get; set; }
        public string ProgramName { get; set; }
        public int CommodityID { get; set; }
        public string CommodityName { get; set; }
        public decimal FreeStock { get; set; }
        public decimal PhysicalStock { get; set; }
    }
}