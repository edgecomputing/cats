using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Hub.Models
{
    public class StockStatusViewModel
    {
        public int freestockPercent { get; set; }
        public int physicalStockPercent { get; set; }
        public int freeStockAmount { get; set; }
        public int physicalStockAmount { get; set; }
        public int totalStock { get; set; }
    }
}