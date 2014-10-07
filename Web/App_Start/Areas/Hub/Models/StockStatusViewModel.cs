using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Hub.Models
{
    public class StockStatusViewModel
    {
        public decimal freestockPercent { get; set; }
        public decimal physicalStockPercent { get; set; }
        public decimal freeStockAmount { get; set; }
        public decimal physicalStockAmount { get; set; }
        public decimal totalStock { get; set; }
    }
}