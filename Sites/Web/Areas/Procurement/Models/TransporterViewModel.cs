using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Procurement.Models
{
    public class TransporterViewModel
    {
        public int TransporterID { get; set; }
        public string TransporterName { get; set; }
        public string BidContract { get; set; }
    }
}