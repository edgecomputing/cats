using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public partial class Master
    {
        public Master()
        {
            this.Details = new List<Detail>();
        }

        public int MasterID { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }
        public virtual ICollection<Detail> Details { get; set; }
    }
}
