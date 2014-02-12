using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public partial class TransactionGroup
    {
        public TransactionGroup()
        {
            //this.Adjustments = new List<Adjustment>();
            //this.DispatchDetails = new List<DispatchDetail>();
            this.InternalMovements = new List<InternalMovement>();
          
            this.Transactions = new List<Transaction>();
        }

        public System.Guid TransactionGroupID { get; set; }
        public int PartitionID { get; set; }
        //public virtual ICollection<Adjustment> Adjustments { get; set; }
        //public virtual ICollection<DispatchDetail> DispatchDetails { get; set; }
        public virtual ICollection<InternalMovement> InternalMovements { get; set; }
        public virtual ICollection<DeliveryReconcile> DeliveryReconciles { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<DispatchDetail> DispatchDetails { get; set; }
    }
}
