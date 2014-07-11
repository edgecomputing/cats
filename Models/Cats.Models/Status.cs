using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public class Status
    {
       public Status()
       {
           this.Bids = new List<Bid>();
       }
        public int StatusID { get; set; }
        public string Name { get; set; }

        #region Navigation Properties

        public virtual ICollection<Bid> Bids { get; set; }
       
        
        #endregion
    }
}
