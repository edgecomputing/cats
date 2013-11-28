using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Hubs
{
    public partial class VWFreePhysicalStock
    {
        public string Hub { get; set; }
        public string Program { get; set; }
        public string Commodity { get; set; }
        public Nullable<decimal> PhysicalStock { get; set; }
        public Nullable<int> HubID { get; set; }
        public Nullable<int> CommodityID { get; set; }
        public int ProgramID { get; set; }
        public decimal FreeStock { get; set; }
    }
}
