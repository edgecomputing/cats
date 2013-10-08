using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public class ShippingInstruction
    {
        public ShippingInstruction()
        {
            //this.DispatchAllocations = new List<DispatchAllocation>();
            //this.OtherDispatchAllocations = new List<OtherDispatchAllocation>();
            this.ProjectCodeAllocations = new List<ProjectCodeAllocation>();
            this.Transactions = new List<Transaction>();
        }

        public int ShippingInstructionID { get; set; }
        public string Value { get; set; }
        //public virtual ICollection<DispatchAllocation> DispatchAllocations { get; set; }
        //public virtual ICollection<OtherDispatchAllocation> OtherDispatchAllocations { get; set; }
        public virtual ICollection<ProjectCodeAllocation> ProjectCodeAllocations { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public List<ShippingInstruction> GetSIList()
        {
            List<ShippingInstruction> shippingInstructions = new List<ShippingInstruction>();

            shippingInstructions.Add(new Cats.Models.ShippingInstruction { ShippingInstructionID = 104, Value = "00013753" });

            shippingInstructions.Add(new Cats.Models.ShippingInstruction { ShippingInstructionID = 102, Value = "00014110" });

            return shippingInstructions;

        }
    }
            
}

