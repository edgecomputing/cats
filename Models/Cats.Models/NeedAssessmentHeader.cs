using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public partial class NeedAssessmentHeader
    {
        public NeedAssessmentHeader()
        {
            this.NeedAssessmentDetails = new List<NeedAssessmentDetail>();
        }

        public int NAHeaderId { get; set; }
        public Nullable<int> NeedAID { get; set; }
        public Nullable<int> Zone { get; set; }
        public string Remark { get; set; }
        public virtual AdminUnit AdminUnit { get; set; }
        public virtual NeedAssessment NeedAssessment { get; set; }
        public virtual ICollection<NeedAssessmentDetail> NeedAssessmentDetails { get; set; }
    }
}
