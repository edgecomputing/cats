using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;



namespace Cats.Models
{
    public  partial class NeedAssessment
    {
        public NeedAssessment()
        {
            this.NeedAssessmentHeaders = new List<NeedAssessmentHeader>();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public int NeedAID { get; set; }

       
        public int Region { get; set; }
      
        public Nullable <int> Season { get; set; }
        public Nullable<int> Year { get; set; }

        [DisplayName("Date")]

        public DateTime? NeedADate { get; set; }
        public Nullable<int> NeddACreatedBy { get; set; }
        public Nullable<bool> NeedAApproved { get; set; }
        public Nullable<int> NeedAApprovedBy { get; set; }
        public int PlanID { get; set; }
        
       
        public int? TypeOfNeedAssessment { get; set; }
        [DataType(DataType.MultilineText)]
        public string Remark { get; set; }
        public virtual AdminUnit AdminUnit { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public virtual UserProfile UserProfile1 { get; set; }
        public virtual Season Season1 { get; set; }
        public virtual TypeOfNeedAssessment TypeOfNeedAssessment1 { get; set; }
        public virtual ICollection<NeedAssessmentHeader> NeedAssessmentHeaders { get; set; }
        public virtual Plan Plan { get; set; }
    }
}
