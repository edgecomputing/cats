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
    public class ReliefRequisitionInfoViewModel
    {
        public string RequisitonNumber { get; set; }
        public string Commodity { get; set; }
        public string Zone { get; set; }
        public int Beneficiary { get; set; }
        public decimal Amount { get; set; }
        public string Status     { get; set; }
    }

}