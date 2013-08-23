using System;

namespace Cats.Models
{
    public class NeedAssessmentHeaderViewModel
    {
        public int NAId { get; set; }
        public int NeedAID { get; set; }
        public int Region { get; set; }
        public string RegionName { get; set; }
        public string Season { get; set; }
        public System.DateTime NeedADate { get; set; }
        public Nullable<int> NeedACreatedBy { get; set; }
        public string NeedACreaterName { get; set; }
        public Nullable<bool> NeedAApproved { get; set; }
        public Nullable<int> NeedAApprovedBy { get; set; }
        public string NeedAApproverName { get; set; }
        public string TypeOfNeedAssessment { get; set; }
    }
}