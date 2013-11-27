using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.EarlyWarning.Models
{
    public class AddCommodityViewModel
    {
        public int CommodityID { get; set; }
        public int RegionalRequestDetailID { get; set; }
        public int RegionalRequestID { get; set; }
    }
}