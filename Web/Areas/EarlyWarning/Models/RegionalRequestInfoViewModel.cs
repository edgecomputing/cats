using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.EarlyWarning.Models
{
    public class RegionalRequestInfoViewModel
    {
        public int RegionID { get; set; }
        public string RegionName { get; set; }
        public int Requested { get; set; }
        public int Remaining { get; set; }

    }

}