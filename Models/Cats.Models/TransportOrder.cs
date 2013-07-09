using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
   public class TransportOrder
    {
       public TransportOrder()
       {
           this.TransportOrderDetails=new Collection<TransportOrderDetail>();

       }
       public int TransportOrderID { get; set; }
       public string  TransportOrderNo { get; set; }
       public string ContractNumber { get; set; }
       public DateTime OrderDate { get; set; }
       public DateTime RequestedDispatchDate { get; set; }
       public DateTime OrderExpiryDate { get; set; }
       public string BidDocumentNo { get; set; }
       public string PerformanceBondReceiptNo { get; set; }
       public int TransporterID { get; set; }

       public string ConsignerName { get; set; }
       public string TransporterSignedName { get; set; }
       public DateTime ConsignerDate { get; set; }
       public DateTime TransporterSignedDate { get; set; }
     
       public virtual Transporter Transporter { get; set; }
    
       public virtual ICollection<TransportOrderDetail> TransportOrderDetails { get; set; }

    }
}
