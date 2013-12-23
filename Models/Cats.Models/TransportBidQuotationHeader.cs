using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public partial class TransportBidQuotationHeader
    {
        public TransportBidQuotationHeader()
        {
            this.TransportBidQuotations = new List<TransportBidQuotation>();
        }

        public int TransportBidQuotationHeaddrID { get; set; }
        public Nullable<System.DateTime> BidQuotationDate { get; set; }
        public Nullable<float> BidBondAmount { get; set; }
       
        public Nullable<int> EnteredBy { get; set; }
        public Nullable<int> Status { get; set; }
        public virtual ICollection<TransportBidQuotation> TransportBidQuotations { get; set; }
    }
}
