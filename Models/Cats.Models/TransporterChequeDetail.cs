using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public class TransporterChequeDetail
    {
        public int TransporterChequeDetailID { get; set; }
        public int TransporterChequeID { get; set; }
        public int TransporterPaymentRequestID { get; set; }
        public int PartitionId { get; set; }

        public virtual TransporterCheque TransporterCheque { get; set; }
        public virtual TransporterPaymentRequest TransporterPaymentRequest { get; set; }
    }
}
