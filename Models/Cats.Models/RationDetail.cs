using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cats.Models
{
    public class RationDetail
    {
        public int RationDetailID { get; set; }
        public int RationID { get; set; }
        public int CommodityTypeID { get; set; }
        public decimal Rate { get; set; }

        public virtual Ration Ration { get; set; }
    }
}
