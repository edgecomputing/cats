using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Cats.Models
{
    public  partial class NeedAssessment
    {
        public NeedAssessment()
        {
            this.NeedAssessmentHeaders = new List<NeedAssessmentHeader>();
        }

        [Key]
        public int NeedAID { get; set; }
        public int Region { get; set; }
        public string Season { get; set; }
        public DateTime? NeedADate { get; set; }
        public Nullable<int> NeddACreatedBy { get; set; }
        public Nullable<bool> NeedAApproved { get; set; }
        public Nullable<int> NeedAApprovedBy { get; set; }
        public string TypeOfNeedAssessment { get; set; }
        public string Remark { get; set; }
        public virtual AdminUnit AdminUnit { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public virtual UserProfile UserProfile1 { get; set; }
        public virtual ICollection<NeedAssessmentHeader> NeedAssessmentHeaders { get; set; }
    }
}
