using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{

    public partial class RequestDetailCommodity
    {
        public int RequestCommodityID { get; set; }
        public int RegionalRequestDetailID { get; set; }
        public int CommodityID { get; set; }

        public decimal Amount { get; set; }
        public Nullable<int> UnitID { get; set; }
        public virtual Commodity Commodity { get; set; }
        public virtual RegionalRequestDetail RegionalRequestDetail { get; set;}
    }

    public partial class RequestDetailCommodityGroupedByWoreda
    {
        public int commodityID { get; set; }
        public decimal Amount { get; set; }
        public string CommodityName { get; set; }
    }

}