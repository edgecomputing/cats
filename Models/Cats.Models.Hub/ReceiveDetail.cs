using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hub
{
    public partial class ReceiveDetail
    {
        [Key]
        public System.Guid ReceiveDetailID { get; set; }
        public int PartitionID { get; set; }
        public Nullable<System.Guid> ReceiveID { get; set; }
        public Nullable<System.Guid> TransactionGroupID { get; set; }
        public int CommodityID { get; set; }
        public decimal SentQuantityInUnit { get; set; }
        public int UnitID { get; set; }
        public decimal SentQuantityInMT { get; set; }
        public string Description { get; set; }
        public virtual Commodity Commodity { get; set; }
        public virtual Receive Receive { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual TransactionGroup TransactionGroup { get; set; }
    }
}
