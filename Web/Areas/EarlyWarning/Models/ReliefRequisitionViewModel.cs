using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cats.Areas.EarlyWarning.Models
{
    public class ReliefRequisitionViewModel
    {
        public int RequisitionID { get; set; }
        public Nullable<int> CommodityID { get; set; }
        public string Commodity { get; set; }
        public Nullable<int> RegionID { get; set; }
        public string Region { get; set; }
        public Nullable<int> ZoneID { get; set; }
        public string Zone { get; set; }
        public Nullable<int> Round { get; set; }
        public string RequisitionNo { get; set; }
        public Nullable<int> RequestedBy { get; set; }
        public DateTime RequestedDate { get; set; }
        [Display(Name = "Request Date")]
        public string RequestedDateEt { get; set; }
        public Nullable<int> ApprovedBy { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        [Display(Name = "Approved Date ")]
        public string ApprovedDateEt { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string Status { get; set; }
        public Nullable<int> ProgramID { get; set; }
        public string Program { get; set; }
        public Nullable<int> RegionalRequestID { get; set; }

    }
}