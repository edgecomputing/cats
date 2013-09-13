using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Cats.Models
{
    public partial class TypeOfNeedAssessment
    {
        public TypeOfNeedAssessment()
        {
            this.NeedAssessments = new List<NeedAssessment>();
        }
          [DisplayName("Type of Need Assessfment")]
        public int TypeOfNeedAssessmentID { get; set; }
        public string TypeOfNeedAssessment1 { get; set; }
        public string Remark { get; set; }
        public virtual ICollection<NeedAssessment> NeedAssessments { get; set; }
    }
}
