using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public partial class Round
    {
        public Round()
        {
           this.ReliefRequistions=new List<ReliefRequistion>();
        }

        public int RoundID { get; set; }
        public int RoundNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual ICollection<ReliefRequistion> ReliefRequistions { get; set; }


    }
}
