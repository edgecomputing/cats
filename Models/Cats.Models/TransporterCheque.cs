using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public partial class TransporterCheque
    {
        public int TransporterChequeId { get; set; }
        public Nullable<int> PaymentRequestID { get; set; }
        public string CheckNo { get; set; }
        public string PaymentVoucherNo { get; set; }
        public string BankName { get; set; }
        public double Amount { get; set; }
        public int TransporterId { get; set; }
        public Nullable<int> PreparedBy { get; set; }
        public Nullable<int> AppovedBy { get; set; }
        public Nullable<System.DateTime> AppovedDate { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public Nullable<int> PaidBy { get; set; }
        public Nullable<int> Status { get; set; }
        public virtual Transporter Transporter { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public virtual UserProfile UserProfile1 { get; set; }
    }
}
