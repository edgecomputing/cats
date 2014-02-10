using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
   public class LocalPurchase
    {
       public LocalPurchase()
       {
           this.LocalPurchaseDetails=new List<LocalPurchaseDetail>();
       }
       
       public int LocalPurchaseID { get; set; }
       public int? GiftCertificateID { get; set; }
       public int ShippingInstructionID { get; set; }
       public string ProjectCode { get; set; }
       public int CommodityID { get; set; }
       public int DonorID { get; set; }
       public int ProgramID { get; set; }
       public decimal Quantity { get; set; }
       public DateTime DateCreated { get; set; }
       public string PurchaseOrder { get; set; }
       public string SupplierName { get; set; }
       public string ReferenceNumber { get; set; }
       public int StatusID { get; set; }
       public string Remark { get; set; }



       public virtual GiftCertificate GiftCertificate { get; set; }
       public virtual ICollection<LocalPurchaseDetail> LocalPurchaseDetails  { get; set; }
       public virtual ShippingInstruction ShippingInstruction { get; set; }
       public virtual Commodity Commodity { get; set; }
       public virtual Donor Donor { get; set; }
       public virtual Program Program { get; set; }

    }
}
