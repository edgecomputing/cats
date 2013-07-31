using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.EarlyWarning.Models
{
    public class RationDetailViewModel
    {
        public int RationDetailID { get; set; }
        public int RationID { get; set; }
        public int CommodityID { get; set; }
        public decimal Amount { get; set; }
        public string Commodity { get; set; }
      
    }
}