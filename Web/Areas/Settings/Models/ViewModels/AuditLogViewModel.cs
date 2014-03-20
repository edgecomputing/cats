using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Models;
using System.ComponentModel.DataAnnotations;

namespace Cats.Areas.Settings.Models.ViewModels
{
    public class AuditLogViewModel
    {
       

        public string Action { get; set; }
        [Display(Name="Column Name")]
        public string ColumnName { get; set; }
        [Display(Name = "Table Name")]
        public string TableName { get; set; }
        [Display(Name="Audit")]
        public string AuditID { get; set; }
        public DateTime LogDate { get; set; }
        public int LoginID { get; set; }
        public string UserName { get; set; }
        public Nullable<int> HubID { get; set; }
        [Display(Name = "New Value")]
        public string NewValue { get; set; }
        [Display(Name = "Old Value")]
        public string OldValue { get; set; }
        
    }
}