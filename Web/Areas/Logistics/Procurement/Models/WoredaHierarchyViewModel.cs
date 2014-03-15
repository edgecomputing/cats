using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Procurement.Models
{
    public class WoredaHierarchyViewModel
    {
        public int RegionID { get; set; }
        public int ZoneID { get; set; }
        public int WoredaID { get; set; }

        public string RegionName { get; set; }
        public string ZoneName { get; set; }
        public string WoredaName { get; set; }

    }
}