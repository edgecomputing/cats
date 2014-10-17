using System;
using System.ComponentModel.DataAnnotations;

namespace Cats.Areas.Settings.Models
{
    public class UserActivityViewModel
    {

            public string Action { get; set; }

            [Display(Name = "Column Name")]
            public string ColumnName { get; set; }

            [Display(Name = "Table Name")]
            public string TableName { get; set; }

            [Display(Name = "Audit")]
            public string AuditID { get; set; }

            public string LogDate { get; set; }

           

            public string UserName { get; set; }

            public Nullable<int> HubID { get; set; }

            [Display(Name = "New Value")]
            public string NewValue { get; set; }

            [Display(Name = "Old Value")]
            public string OldValue { get; set; }

        
    }
}