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
       public int GiftCertificateID { get; set; }
       public DateTime DateCreated { get; set; }
       public int PurchaseOrder { get; set; }
       public string SupplierName { get; set; }
       public string ReferenceNumber { get; set; }
       public int StatusID { get; set; }
       public string Remark { get; set; }



       public virtual GiftCertificate GiftCertificate { get; set; }
       public virtual ICollection<LocalPurchaseDetail> LocalPurchaseDetails  { get; set; }

    }
}
