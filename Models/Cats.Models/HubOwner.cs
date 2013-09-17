using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public partial class HubOwner
    {
        public HubOwner()
        {
            this.Hubs = new List<Hub>();
            this.Transactions = new List<Transaction>();
        }

        public int HubOwnerID { get; set; }
        public string Name { get; set; }
        public string LongName { get; set; }
        public virtual ICollection<Hub> Hubs { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
