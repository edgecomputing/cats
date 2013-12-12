using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Procurement.Models
{
    public class AgreementVersionViewModel
    {
        public int TransportAgreementVersionID { get; set; }
        public int TransporterID { get; set; }
        public string Transporter { get; set; }
        public int BidID { get; set; }
        public string BidNo { get; set; }
        public string IssueDate { get; set; }
    }
}