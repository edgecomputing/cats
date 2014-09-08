using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Finance.Models
{
    public class TransporterChequeViewModel
    {
        public Guid TransporterChequeId { get; set; }
        public int TransporterPaymentRequestID { get; set; }
        public string PaymentRequestRefNo { get; set; }
        public string CheckNo { get; set; }
        public string PaymentVoucherNo { get; set; }
        public string BankName { get; set; }
        public decimal Amount { get; set; }
        public int TransporterId { get; set; }
        public string Transporter { get; set; }
        public int PreparedByID { get; set; }
        public string PreparedBy { get; set; }
        public int? AppovedByID { get; set; }
        public string AppovedBy { get; set; }
        public int Status { get; set; }
        public DateTime? AppovedDate { get; set; }
        public string AppovedDateString { get; set; }
    }
}