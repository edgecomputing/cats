using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hubs
{
    public partial class Unit
    {
        public Unit()
        {
            this.DispatchDetails = new List<DispatchDetail>();
            this.ReceiptAllocations = new List<ReceiptAllocation>();
            this.ReceiveDetails = new List<ReceiveDetail>();
            this.Transactions = new List<Transaction>();
        }
        [Key]
        public int UnitID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<DispatchDetail> DispatchDetails { get; set; }
        public virtual ICollection<ReceiptAllocation> ReceiptAllocations { get; set; }
        public virtual ICollection<ReceiveDetail> ReceiveDetails { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
