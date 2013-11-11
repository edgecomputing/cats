using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hubs
{
    public partial class DispatchDetail
    {
        [Key]
        public System.Guid DispatchDetailID { get; set; }
        public int PartitionID { get; set; }
        public Nullable<System.Guid> TransactionGroupID { get; set; }
        public Nullable<System.Guid> DispatchID { get; set; }
        public int CommodityID { get; set; }
        public decimal RequestedQunatityInUnit { get; set; }
        public int UnitID { get; set; }
        public decimal RequestedQuantityInMT { get; set; }
        public string Description { get; set; }
        public virtual Commodity Commodity { get; set; }
        public virtual Dispatch Dispatch { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual TransactionGroup TransactionGroup { get; set; }
    }
}
