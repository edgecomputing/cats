using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public class TransporterAgreementVersion
    {
        public int TransporterAgreementVersionID { get; set; }
        public int BidID { get; set; }
        public int TransporterID { get; set; }
        public byte[] AgreementDocxFile { get; set; }
        public System.DateTime IssueDate { get; set; }
        public bool Current { get; set; }
        public int Status { get; set; }
        public virtual Bid Bid { get; set; }
        public virtual Transporter Transporter { get; set; }
    }
}
