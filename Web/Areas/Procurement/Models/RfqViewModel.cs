using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Procurement.Models
{
    public class RfqViewModel
    {
        public string SourceWarehouse { get; set; }
        public string DestinationZone { get; set; }
        public string DestinationWoreda { get; set; }
        public string RegionName { get; set; }
        public int RegionID { get; set; }
        public decimal Quantity { get; set; }
    }
    public class RfqPrintViewModel
    {
        public int BidPlanID { get; set; }
        public string Source { get; set; }
        public string Zone { get; set; }
        public string Woreda { get; set; }
        public int WoredaID { get; set; }
        public string Region { get; set; }
        public string BidReference { get; set; }
        public int ProgramID { get; set; }
        public decimal ReliefAmount { get; set; }
        public decimal PsnpAmount { get; set; }
        public decimal Total { get; set; }
        public decimal Quantity { get; set; }
        public string BidOpeningdate { get; set; }
        public string BidOpeningTime { get; set; }
        //public string BidSUbmissionDate { get; set; }

                            
    }
}