using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Cats.Areas.Logistics.Models
{
    public class TransportRequisitionViewModel
    {
        public int TransportRequisitionID { get; set; }
        [Display(Name="No.")]
        public string TransportRequisitionNo { get; set; }
        [Display(Name="Requested By")]
        public string RequestedBy { get; set; }
        public string Status { get; set; }
        [Display(Name="Request Date")]
        public System.DateTime RequestedDate { get; set; }
        [Display(Name = "Request Date")]
        public string DateRequested { get; set; }
        public string Region { get; set; }
        public string Program { get; set; }
        [Display(Name="Certified By")]
        public string CertifiedBy { get; set; }
        [Display(Name="Certified Date")]
        public System.DateTime CertifiedDate { get; set; }
        [Display(Name = "Certified Date")]
        public string DateCertified { get; set; }
        public string Remark { get; set; }
        public int StatusID { get; set; }
        public List<TransportRequisitionDetailViewModel> TransportRequisitionDetailViewModels { get; set; }
    }
}