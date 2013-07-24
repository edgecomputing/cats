using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Cats.Areas.Logistics.Models
{
    public class TransportRequisitionViewModel
    {
        public int TransportRequisitionID { get; set; }
        public string TransportRequisitionNo { get; set; }
        public int RequestedBy { get; set; }
        public string Status { get; set; }
        public System.DateTime RequestedDate { get; set; }
        public string RequestedDateET { get; set; }

        public int CertifiedBy { get; set; }
        public System.DateTime CertifiedDate { get; set; }
        public string CertifiedDateET { get; set; }
        public string Remark { get; set; }
        public int StatusID { get; set; }
    }
}