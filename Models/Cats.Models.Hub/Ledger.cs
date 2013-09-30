using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hub
{
    public partial class Ledger
    {
        public Ledger()
        {
            this.Transactions = new List<Transaction>();
        }

        [Key]
        public int LedgerID { get; set; }
        public string Name { get; set; }
        public int LedgerTypeID { get; set; }
        public virtual LedgerType LedgerType { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
