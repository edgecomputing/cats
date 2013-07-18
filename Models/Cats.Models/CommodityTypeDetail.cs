using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cats.Models
{
    public class CommodityTypeDetail
    {
        public int CommodityTypeDetailID { get; set; }
        public int HumanitarianRequirementDetailID { get; set; }
        public int CommodityTypeID { get; set; }
        public long Amount { get; set; }

        public virtual HumanitarianRequirementDetail HumanitarianRequirementDetail { get; set; }
        public virtual CommodityType CommodityType { get; set; }

    }
}
