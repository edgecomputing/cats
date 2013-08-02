using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cats.Models
{
    public class HRDCommodityDetail
    {
        public int HRDCommodityDetailID { get; set; }
        public int HRDDetailID { get; set; }
        public int CommodityID { get; set; }
        public decimal Amount { get; set; }

        //public virtual HRDDetail HrdDetail { get; set; }
        public virtual Commodity Commodity { get; set; }

    }
}
