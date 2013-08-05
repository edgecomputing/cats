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
        public Nullable<System.DateTime> NeedACreatedDate { get; set; }
        public Nullable<int> NeddACreatedBy { get; set; }
        public Nullable<bool> NeedAApproved { get; set; }
        public string Remark { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public virtual ICollection<NeedAssessmentDetail> NeedAssessmentDetails { get; set; }
    }
}
