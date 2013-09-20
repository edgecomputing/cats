using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Models;

namespace Cats.Web.Adminstration.Models.ViewModels
{
    public class AuditLogViewModel
    {
       

        public string Action { get; set; }
        public string ColumnName { get; set; }
        public string TableName { get; set; }
        public Guid AuditID { get; set; }
        public DateTime LogDate { get; set; }
        public int LoginID { get; set; }
        public Nullable<int> HubID { get; set; }
        public string NewValue { get; set; }
        public string OldValue { get; set; }
        
    }
}