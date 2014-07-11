using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Procurement.Models
{
    public class TransactionDetail
    {
        public string Region { get; set; }
        public string Zone { get; set; }
        public string Woreda { get; set; }
        public string BidNumber { get; set; }
        public string BidStratingDate { get; set; }
        public string Warehouse { get; set; }
        public string DistanceFromOrigin { get; set; }
        public decimal Tariff { get; set; }
    }
}