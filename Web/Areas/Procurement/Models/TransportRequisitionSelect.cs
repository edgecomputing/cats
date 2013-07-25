using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cats.Areas.Procurement.Models
{
    public class TransportRequisitionSelect
    {
        public int TransportRequisitionID { get; set; }
        [Display(Name="Transport Requisition No")]
        public string TransportRequisitionNo { get; set; }
         [Display(Name = "Requested By")]
        public int RequestedBy { get; set; }
         [Display(Name = "Request Date(GC)")]
        public DateTime RequestedDate { get; set; }
         [Display(Name = "Request Date(ET)")]
        public string RequestDateET { get; set; }
        [Display(Name = "Certified By")]
        public int CertifiedBy { get; set; }
        [Display(Name = "Certified Date(GC)")]
        public DateTime CertifiedDate { get; set; }
         [Display(Name = "Certified Date(ET)")]
        public string CertifiedDateET { get; set; }
      //  public string Remark { get; set; }
        public int Status { get; set; }
        public string  StatusName { get; set; }
        public TransportRequisitionSelectInput Input { get; set; }

      public class   TransportRequisitionSelectInput
      {
          public int Number { get; set; }
          public bool IsSelected { get; set; }
      }
    }
}