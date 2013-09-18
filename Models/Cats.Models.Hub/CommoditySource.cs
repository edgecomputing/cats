using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hub
{
    public partial class CommoditySource
    {
        public CommoditySource()
        {
            this.ReceiptAllocations = new List<ReceiptAllocation>();
            this.Receives = new List<Receive>();
        }
        [Key]
        public int CommoditySourceID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ReceiptAllocation> ReceiptAllocations { get; set; }
        public virtual ICollection<Receive> Receives { get; set; }
    }
}
