using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Procurement.Models
{
    public class RfqViewModel
    {
        public string SourceWarehouse { get; set; }
        public string DestinationZone { get; set; }
        public string DestinationWoreda { get; set; }
        public int RegionID { get; set; }
    }
}