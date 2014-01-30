using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Cats.Models;

namespace Cats.Models
{
    public partial class Unit
    {
        public Unit()
        {
            this.ReceiptAllocations = new List<ReceiptAllocation>();
            this.RegionalPSNPPledges = new List<RegionalPSNPPledge>();
            this.Transactions = new List<Transaction>();
           
        }

        public int UnitID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ReceiptAllocation> ReceiptAllocations { get; set; }
        public virtual ICollection<RegionalPSNPPledge> RegionalPSNPPledges { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public ICollection<RationDetail> RationDetails { get; set; }
        public virtual ICollection<DistributionDetail> DistributionDetails { get; set; }
       
    }
}