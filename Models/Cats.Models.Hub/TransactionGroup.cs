using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hubs
{
    public partial class TransactionGroup
    {
        public TransactionGroup()
        {
            this.Adjustments = new List<Adjustment>();
            this.DispatchDetails = new List<DispatchDetail>();
            this.InternalMovements = new List<InternalMovement>();
            this.ReceiveDetails = new List<ReceiveDetail>();
            this.Transactions = new List<Transaction>();
        }
        [Key]
        public System.Guid TransactionGroupID { get; set; }
        public int? PartitionId { get; set; }
        public virtual ICollection<Adjustment> Adjustments { get; set; }
        public virtual ICollection<DispatchDetail> DispatchDetails { get; set; }
        public virtual ICollection<InternalMovement> InternalMovements { get; set; }
        public virtual ICollection<ReceiveDetail> ReceiveDetails { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
