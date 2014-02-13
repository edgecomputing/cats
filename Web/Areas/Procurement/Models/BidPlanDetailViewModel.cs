using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Procurement.Models
{
    public class BidPlanDetailViewModel
    {
        public int BidPlanID { get; set; }
        public int BidPlanDetailID { get; set; }
        public int DestinationID { get; set; }
        public int SourceID { get; set; }
        public decimal Quantity { get; set; }
        public string Region { get; set; }
        public string Woreda { get; set; }
        public string Warehouse { get; set; }
        public int ProgramID { get; set; }
        public string Program { get; set; }

    }

    public class BidPlanDetailListViewModel
    {
        public int BidPlanID { get; set; }
        public IEnumerable<BidPlanDetailViewModel> BidPlanDetails { get; set; }
    }
}