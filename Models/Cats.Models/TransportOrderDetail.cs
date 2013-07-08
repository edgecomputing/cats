using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public class TransportOrderDetail
    {
        public int TransportOrderDetailID { get; set; }
        public int TransportOrderID { get; set; }
        public int FdpID { get; set; }
        public int SourceWarehouseID { get; set; }
        public decimal QuantityQtl { get; set; }
        public Nullable<decimal> DistanceFromOrigin { get; set; }
        public decimal TariffPerQtl { get; set; }
        public virtual FDP FDP { get; set; }
        public virtual Hub Hub { get; set; }
        public virtual TransportOrder TransportOrder { get; set; }
    }
}
