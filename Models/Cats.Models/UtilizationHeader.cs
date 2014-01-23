using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public partial class UtilizationHeader
    {
        public UtilizationHeader()
        {
            this.UtilizationDetails = new List<UtilizationDetail>();
        }

        public int DistributionId { get; set; }
        public int RequisitionId { get; set; }
        public System.DateTime DistributionDate { get; set; }
        public Nullable<int> DistributedBy { get; set; }
        public string Remark { get; set; }
        public virtual ICollection<UtilizationDetail> UtilizationDetails { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
