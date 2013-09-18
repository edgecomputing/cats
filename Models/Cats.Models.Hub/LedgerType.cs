using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hub
{
    public partial class LedgerType
    {
        public LedgerType()
        {
            this.Ledgers = new List<Ledger>();
        }
        [Key]
        public int LedgerTypeID { get; set; }
        public string Name { get; set; }
        public string Direction { get; set; }
        public virtual ICollection<Ledger> Ledgers { get; set; }
    }
}
