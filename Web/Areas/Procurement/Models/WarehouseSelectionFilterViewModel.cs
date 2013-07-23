using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Procurement.Models
{
    public class WarehouseSelectionFilterViewModel
    {
        public int BidPlanID { get; set; }
        public int RegionID { get; set; }
        public int SourceID { get; set; }

    }
}