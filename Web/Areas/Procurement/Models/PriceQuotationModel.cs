using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Procurement.Models
{
    public class PriceQuotationModel
    {
        public int BidPlanID { get; set; }
        public int TransporterID { get; set; }
        public int RegionID { get; set; }
    }
}