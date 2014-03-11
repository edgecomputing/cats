using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public partial class TransporterCheque
    {
        public int TransporterChequeId { get; set; }
        public string CheckNo { get; set; }
        public int TransporterId { get; set; }
        public int PreparedBy { get; set; }
        public int AppovedBy { get; set; }
        public System.DateTime AppovedDate { get; set; }
        public int BusinessProcessID { get; set; }
        public virtual Transporter Transporter { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public virtual UserProfile UserProfile1 { get; set; }
    }
}
