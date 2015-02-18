using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
   public class Transfer
    {
       //public int TransferID { get; set; }
       //public int ShippingInstructionID { get; set; }
       //public int SourceHubID { get; set; }
       //public int ProgramID { get; set; }
       //public int CommoditySourceID { get; set; }
       //public int CommodityID { get; set; }
       //public int DestinationHubID { get; set; }
       //public string ProjectCode { get; set; }
       //public decimal Quantity { get; set; }
       //public DateTime CreatedDate { get; set; }
       //public string ReferenceNumber { get; set; }
       //public int StatusID { get; set; }
       //public string Remark { get; set; }


       // #region Navigation Properties

       //public virtual ShippingInstruction ShippingInstruction { get; set; }
       //public virtual Hub Hub { get; set; }
       //public virtual Hub Hub1 { get; set; }
       //public virtual Program Program { get; set; }
       //public virtual CommoditySource CommoditySource { get; set; }
       //public virtual Commodity Commodity { get; set; }

       // #endregion
        public int TransferID { get; set; }
        public int ShippingInstructionID { get; set; }
        public int SourceHubID { get; set; }
        public int ProgramID { get; set; }
        public int CommoditySourceID { get; set; }
        public int CommodityID { get; set; }
        public int DestinationHubID { get; set; }
        public string ProjectCode { get; set; }
        public decimal Quantity { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ReferenceNumber { get; set; }
        public int StatusID { get; set; }
        public string Remark { get; set; }

        public int SourceSwap { get; set; }
        public int DestinationSwap { get; set; }

       public virtual Commodity Commodity { get; set; }
        public virtual CommoditySource CommoditySource { get; set; }
        public virtual Hub Hub { get; set; }
        public virtual Hub Hub1 { get; set; }

        public virtual Hub Hub2 { get; set; }
        public virtual Hub Hub3 { get; set; }
       public virtual Program Program { get; set; }
        public virtual ShippingInstruction ShippingInstruction { get; set; }
      
        //public virtual Transfer Transfer1 { get; set; }
        //public virtual Transfer Transfer2 { get; set; }
    }
}
