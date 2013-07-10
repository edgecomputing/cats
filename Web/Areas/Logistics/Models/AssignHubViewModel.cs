using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Logistics.Models
{
    public class AssignHubViewModel
    {
        public int RequisitionId { get; set; }
        public string RegionName { get; set; }
        public int RegionId { get; set; }
        public  string ZoneName { get; set; }
        public int ZoneId { get; set; }
        public string RequisitionNo { get; set; }
        public string Commodity { get; set; }
        public int CommodityId { get; set; }
        public string Hub { get; set; }
        public int HubId { get; set; }

    }
}