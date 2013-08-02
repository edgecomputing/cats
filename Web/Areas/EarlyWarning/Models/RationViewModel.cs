using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cats.Areas.EarlyWarning.Models
{
    public class RationViewModel
    {
       
        [Display(Name="Ration")]
        public int RationID { get; set; }
        [Display(Name="Created Date(G.C)")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [Display(Name="Created Date(E.C)")]
        public string CreatedDateEC { get; set; }
        [Display(Name="Created By")]
        public Nullable<int> CreatedBy { get; set; }
        [Display(Name="Updated Date(G.C)")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        [Display(Name="Updated Date(E.C)")]
        public string UpdatedDateEC { get; set; }
        [Display(Name="Updated By")]
        public Nullable<int> UpdatedBy { get; set; }
        [Display(Name="Is Default Ration")]
        public bool IsDefaultRation { get; set; }
        [Display(Name="Reference Number")]
        [Required(ErrorMessage = "Refrence Number can't be empty.")]
        public string ReferenceNumber { get; set; }
    }
}