using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.EarlyWarning.Models
{
    public class ReliefRequisitionNew
    {
        public int ReliefRequisitionID { get; set; }
        public int CommodityID { get; set; }
        public int RegionID { get; set; }
        public int ZoneID { get; set; }
        public int Round { get; set; }
        public int RequestedBy { get; set; }
        public DateTime RequisitionDate { get; set; }
        public int ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public int Status { get; set; }
        public int ProgramID { get; set; }
        public ReliefRequisitionNewInput Input { get; set; }
        public class ReliefRequisitionNewInput
        {
            public string RequisitionNo { get; set; }
            public int Number { get; set; }
        }
    }

}