using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Hubs
{
    public partial class VWTransferredStock
    {
        public int ProgramID { get; set; }
        public Nullable<int> HubID { get; set; }
        public Nullable<int> CommodityID { get; set; }
        public Nullable<int> ProjectCodeID { get; set; }
        public Nullable<int> ShippingInstructionID { get; set; }
      
        public string Program { get; set; }
        public string Hub { get; set; }
        public string Commodity { get; set; }
        public string ShippingInstruction { get; set; }
        public string ProjectCode { get; set; }
     
        public decimal DonorAll { get; set; }
        public decimal Received { get; set; }
        public decimal Expected { get; set; }
        public decimal Commited { get; set; }
        public decimal Dispatched { get; set; }
    }
}
