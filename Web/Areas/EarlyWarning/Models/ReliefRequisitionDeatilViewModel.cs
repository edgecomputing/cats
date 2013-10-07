using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.EarlyWarning.Models
{
    public class ReliefRequisitionDetailViewModel
    {
        public int RequisitionDetailID { get; set; }
        public int RequisitionID { get; set; }
        public int CommodityID { get; set; }
        public string Commodity { get; set; }
        public string Zone { get; set; }
        public string Woreda { get; set; }
        public int BenficiaryNo { get; set; }
        public decimal Amount { get; set; }
        public string FDP { get; set; }
        public int FDPID { get; set; }
        public Nullable<int> DonorID { get; set; }
        public string Donor { get; set; }
        public decimal RationAmount { get; set; }
    }
}