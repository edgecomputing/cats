using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cats.Models
{
    public class CommodityTypeDetail
    {
        public int CommodityTypeDetailID { get; set; }
        public int HRDDetailID { get; set; }
        public int CommodityID { get; set; }
        public long Amount { get; set; }

        public virtual HRDDetail HrdDetail { get; set; }
        public virtual Commodity Commodity { get; set; }

    }
}
