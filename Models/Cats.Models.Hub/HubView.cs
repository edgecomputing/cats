using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Hubs
{
    public class HubView
    {
        public int HubId { get; set; }
        public string Name { get; set; }
    }

    public class ProgramView
    {
        public int ProgramId { get; set; }
        public string Name { get; set; }
    }

    public class HubFreeStockView
    {
        public string CommodityName { get; set; }
        public decimal FreeStock { get; set; }
        public decimal PhysicalStock { get; set; }
   }

    public class HubFreeStockSummaryView {
        public int HubID { get; set; }
        public string HubName { get; set; }
        public decimal TotalFreestock { get; set; }
        public decimal TotalPhysicalStock { get; set; }
    }
}