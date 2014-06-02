using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Procurement.Models
{
    public class WarehouseProgramViewModel
    {
        public decimal PSNP { get; set; }
        public decimal Relief { get; set; }
        public string WarehouseName { get; set; }
        public int WarehouseID { get; set; }
        public int BidPlanID { get; set; }
        public int WoredaID { get; set; }

        public decimal Total { get { return PSNP + Relief; } }

    }
}