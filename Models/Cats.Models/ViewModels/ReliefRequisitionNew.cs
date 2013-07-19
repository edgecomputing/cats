using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Cats.Models.ViewModels
{
    public class ReliefRequisitionNew
    {





        public int RequisitionID { get; set; }
        public string Commodity { get; set; }
        public string Region { get; set; }
        public string Zone { get; set; }
        public Nullable<int> Round { get; set; }
         [Display(Name = "Requisition No")]
        public string RequisitionNo { get; set; }
         [Display(Name = "Requested By")]
        public string RequestedBy { get; set; }
         [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
         [Display(Name = "Requested Date")]
        public Nullable<System.DateTime> RequestedDate { get; set; }
            [Display(Name = "Approved By")]
        public string ApprovedBy { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Display(Name = "Approved Date")]
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public Nullable<int> Status { get; set; }
        public string Program { get; set; }
        public int RegionalRequestId { get; set; }
        public ReliefRequisitionNewInput Input { get; set; }
        public class ReliefRequisitionNewInput
        {
            [Display(Name="Requisition No")]
            [Required(ErrorMessage = "Requisition No. can't be emplty.")]
            public string RequisitionNo { get; set; }
            public int Number { get; set; }
        }
    }

}