using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public partial class ReceiptPlan
    {
        public ReceiptPlan()
        {
            this.ReceiptPlanDetails = new List<ReceiptPlanDetail>();
        }

        public int ReceiptHeaderId { get; set; }
        public int GiftCertificateDetailId { get; set; }
        public Nullable<System.DateTime> ReceiptDate { get; set; }
        public Nullable<int> EnteredBy { get; set; }
        public Nullable<bool> IsClosed { get; set; }
        public string Remark { get; set; }
        public virtual GiftCertificateDetail GiftCertificateDetail { get; set; }
        public virtual ICollection<ReceiptPlanDetail> ReceiptPlanDetails { get; set; }
        public int? PartitionId { get; set; }
    }
}
