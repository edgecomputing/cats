using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hubs
{
    public partial class Store
    {
        public Store()
        {
            this.Transactions = new List<Transaction>();
        }
        [Key]
        public int StoreID { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public int HubID { get; set; }
        public bool IsTemporary { get; set; }
        public bool IsActive { get; set; }
        public int StackCount { get; set; }
        public string StoreManName { get; set; }
        public virtual Hub Hub { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
