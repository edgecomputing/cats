using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public class AllocationDetailLine
    {
        public int AllocationDetailLineID { get; set; }
        public int ReliefRequisitionDetailID { get; set; }
        public int CommodityID { get; set; }
        public Decimal Amount { get; set; }

        public virtual ReliefRequisitionDetail ReliefRequisitionDetail { get; set; }

    }
}
