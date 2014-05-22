using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public partial class TransporterCheque
    {
        public int? PartitionId { get; set; }
        public System.Guid TransporterChequeId { get; set; }
        public int PaymentRequestID { get; set; }
        public string CheckNo { get; set; }
        public string PaymentVoucherNo { get; set; }
        public string BankName { get; set; }
        public decimal Amount { get; set; }
        //public int TransporterId { get; set; }
        public int PreparedBy { get; set; }
        public int? AppovedBy { get; set; }
        public int Status { get; set; }
        public DateTime AppovedDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public int? PaidBy { get; set; }
        public virtual PaymentRequest PaymentRequest { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public virtual UserProfile UserProfile1 { get; set; }
       
    }
}
