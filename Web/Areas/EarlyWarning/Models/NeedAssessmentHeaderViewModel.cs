using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.EarlyWarning.Models
{
    public class NeedAssessmentHeaderViewModel
    {
        public int NAId { get; set; }
        public int NaHeaderId { get; set; }


        public int Zone { get; set; }
        public int District { get; set; }

        public string ZoneName { get; set; }
        public string DistrictName { get; set; }

        public Nullable<System.DateTime> NeedACreatedDate { get; set; }
        public Nullable<int> NeedACreatedBy { get; set; }
        public Nullable<bool> NeedAApproved { get; set; }
        public string Remark { get; set; }
        public string CreaterUser { get; set; }
    }
}