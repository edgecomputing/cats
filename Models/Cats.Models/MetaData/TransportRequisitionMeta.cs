using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public  class TransportRequisitionMeta
    {
        

        public int TransportRequisitionID { get; set; }
        [Display(Name="Transport Requisition No")]
        [Required(ErrorMessage="Transport Requisition Number can't be emplty.")]
        public string TransportRequisitionNo { get; set; }
        [Display(Name="Requested By")]
        public int RequestedBy { get; set; }
        [Display(Name="Requested Date")]
        [DisplayFormat(DataFormatString="{0:dd-MMM-yyyy}")]
        public System.DateTime RequestedDate { get; set; }
        [Display(Name="Certified By")]
        public int CertifiedBy { get; set; }
        [Display(Name = "Certified Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public System.DateTime CertifiedDate { get; set; }
        public string Remark { get; set; }
        public int Status { get; set; }
      
    }
}
