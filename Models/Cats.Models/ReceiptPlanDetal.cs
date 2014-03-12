using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public partial class ReceiptPlanDetail
    {
        public int ReceiptDetailId { get; set; }
        public int ReceiptHeaderId { get; set; }
        public int HubId { get; set; }
        public decimal Allocated { get; set; }
        public Nullable<decimal> Received { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public virtual Hub Hub { get; set; }
        public virtual ReceiptPlan ReceiptPlan { get; set; }
    }
}
