using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public partial class WoredaHubLink
    {
        public int WoredaHubLinkID { get; set; }
        public int WoredaHubID { get; set; }
        public int WoredaID { get; set; }
        public int HubID { get; set; }
        public virtual AdminUnit AdminUnit { get; set; }
        public virtual Hub Hub { get; set; }
        public virtual WoredaHub WoredaHub { get; set; }
    }
}
