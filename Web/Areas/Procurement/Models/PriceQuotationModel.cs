using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Models;
namespace Cats.Areas.Procurement.Models
{
    public class PriceQuotationModel
    {
        public int BidPlanID { get; set; }
        public int TransporterID { get; set; }
        public int RegionID { get; set; }
    }
    public class PriceQuotationModelDetailModel
    {
       // public string SourceWarehouse { get; set; }
        public Hub SourceWarehouse { get; set; }
        public string DestinationZone { get; set; }
        public AdminUnit DestinationWoreda { get; set; }
        public string DestinationWoredaName { get; set; }
        
        public string RegionName { get; set; }
        public int RegionID { get; set; }
    }
}