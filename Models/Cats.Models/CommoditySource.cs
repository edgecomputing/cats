using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models
{
    public partial class CommoditySource
    {
        public CommoditySource()
        {
            this.ReceiptAllocations = new List<ReceiptAllocation>();
            this.LoanReciptPlans=new List<LoanReciptPlan>();
           // this.Receives = new List<Receive>();
        }
        [Key]
        public int CommoditySourceID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ReceiptAllocation> ReceiptAllocations { get; set; }
        public virtual ICollection<LoanReciptPlan> LoanReciptPlans { get; set; } 
      
    }
}
