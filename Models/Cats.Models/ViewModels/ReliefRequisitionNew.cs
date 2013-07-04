using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Models.ViewModels
{
    public class ReliefRequisitionNew
    {
        public int RequisitionID { get; set; }
        public string Commodity { get; set; }
        public string Region { get; set; }
        public string Zone { get; set; }
        public Nullable<int> Round { get; set; }
        public string RequisitionNo { get; set; }
        public string RequestedBy { get; set; }
        public Nullable<System.DateTime> RequestedDate { get; set; }
        public string ApprovedBy { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public Nullable<int> Status { get; set; }
        public string Program { get; set; }
        public ReliefRequisitionNewInput Input { get; set; }
        public class ReliefRequisitionNewInput
        {
            public string RequisitionNo { get; set; }
            public int Number { get; set; }
        }
    }

}