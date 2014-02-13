using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Procurement.Models
{
    public class WoredasInCurrentContract
    {
        public string Region { get; set; }
        public string Zone { get; set; }
        public string Woreda { get; set; }
        public string Tariff { get; set; }
    }
}