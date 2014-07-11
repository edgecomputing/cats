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
        public int RequisitionID { get; set; }
        public int CommodityID { get; set; }
        public Nullable<int> ZoneID { get; set; }
        public Nullable<int> DonorID { get; set; }
        public virtual AdminUnit AdminUnit { get; set; }
        public virtual Commodity Commodity { get; set; }
        public virtual Donor Donor { get; set; }
        public virtual FDP FDP { get; set; }
        public virtual Hub Hub { get; set; }
        public virtual ReliefRequisition ReliefRequisition { get; set; }
        public virtual TransportOrder TransportOrder { get; set; }
   
    }
}
