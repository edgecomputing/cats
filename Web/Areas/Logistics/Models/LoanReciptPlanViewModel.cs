using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Logistics.Models
{
    public class LoanReciptPlanViewModel
    {
        public int LoanReciptPlanID { get; set; }
        public int ProgramID { get; set; }
        public string ProgramName { get; set; }
        public int HubID { get; set; }
        public string HubName { get; set; }
        public int CommoditySourceID  { get; set; }
        public int CommodityID { get; set; }
        public int SourceHubID { get; set; }
        public string SourceHubName { get; set; }
        public string CommodityName { get; set; }
        public string CommoditySourceName { get; set; }
        public string RefeenceNumber { get; set; }
        public decimal Quantity { get; set; }
        public string ProjectCode { get; set; }
        public string SiNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public int StatusID { get; set; }
        public string Status { get; set; }


    }
}