using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Procurement.Models
{
    public class TransportRequisitionSelect
    {
        public int TransportRequisitionID { get; set; }
        public string TransportRequisitionNo { get; set; }
        public int RequestedBy { get; set; }
        public DateTime RequestedDate { get; set; }
        public int CertifiedBy { get; set; }
        public DateTime CertifiedDate { get; set; }
      //  public string Remark { get; set; }
        public int Status { get; set; }
        public TransportRequisitionSelectInput Input { get; set; }

      public class   TransportRequisitionSelectInput
      {
          public int Number { get; set; }
          public bool IsSelected { get; set; }
      }
    }
}