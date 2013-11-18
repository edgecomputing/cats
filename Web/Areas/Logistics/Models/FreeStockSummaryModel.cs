using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Logistics.Models
{
    public class FreeStockSummaryModel
    {
        public decimal freeStock { get; set; }
        public decimal physicalStock { get; set; }
    }
}