using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models
{
    public partial class AdminUnitType
    {
        public AdminUnitType()
        {
            this.AdminUnits = new List<AdminUnit>();
            this.NeedAssessments = new List<NeedAssessment>();
            this.NeedAssessmentDetails = new List<NeedAssessmentDetail>();
            this.NeedAssessmentHeaders = new List<NeedAssessmentHeader>();
        }

        [Key]
        public int AdminUnitTypeID { get; set; }
        public string Name { get; set; }
        public string NameAM { get; set; }
        public int SortOrder { get; set; }
        public virtual ICollection<AdminUnit> AdminUnits { get; set; }
        public virtual ICollection<NeedAssessment> NeedAssessments { get; set; }
        public virtual ICollection<NeedAssessmentDetail> NeedAssessmentDetails { get; set; }
        public virtual ICollection<NeedAssessmentHeader> NeedAssessmentHeaders { get; set; }
    }
}
