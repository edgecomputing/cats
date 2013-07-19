using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Models;
namespace Cats.Areas.Procurement.Models
{
    public class PriceQuotationFilterViewModel
    {
        public int BidPlanID { get; set; }
        public int TransporterID { get; set; }
        public int RegionID { get; set; }
    }
    public class GoodsMovementDetailViewModel
    {
       // public string SourceWarehouse { get; set; }
        public string SourceName { get; set; }
        public int SourceID { get; set; }
    //    public Hub SourceWarehouse { get; set; }
        public string DestinationZone { get; set; }
    //    public AdminUnit DestinationWoreda { get; set; }
        public string DestinationName { get; set; }
        public int DestinationID { get; set; }
       
        public string RegionName { get; set; }
        public int RegionID { get; set; }
    }

    public class PriceQuotationDetailViewModel : GoodsMovementDetailViewModel
    {
        public int QuotationID { get; set; }
        public double Tariff { get; set; }
        public string Remark { get; set; }
    }
}