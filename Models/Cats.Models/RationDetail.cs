using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cats.Models
{
    public class RationDetail
    {
        public int RationDetailID { get; set; }
        public int RationID { get; set; }
        public int CommodityID { get; set; }
        public decimal Amount { get; set; }
        public Nullable<int> UnitID { get; set; }
        public virtual Commodity Commodity { get; set; }
        public virtual Ration  Ration { get; set; }
        public virtual Unit Unit { get; set; }
        public int? PartitionId { get; set; }
       
    }
}
