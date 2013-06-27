using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Logistics.Models
{
    public class HubAllocation
    {
        int TransportRequisitionID { get; set; }
        string TransportRequisitionNo { get; set; }
        int RequisitionID { get; set; }
        int CommodityID { get; set; }
        decimal Amount { get; set; }
        int RegionID { get; set; }
        int ZoneID { get; set; }
        public int HubID { get; set; }


    }
}