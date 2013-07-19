using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cats.Models
{
    public class Ration
    {
        public int RationID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RationDetail> RationDetails { get; set; }
    }
}
