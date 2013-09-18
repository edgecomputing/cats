using System;
using System.Collections.Generic;

namespace Cats.Models.Hub
{
    public partial class Audit
    {
        public System.Guid AuditID { get; set; }
        public Nullable<int> HubID { get; set; }
        public int PartitionID { get; set; }
        public int LoginID { get; set; }
        public DateTime DateTime { get; set; }
        public string Action { get; set; }
        public string TableName { get; set; }
        public string PrimaryKey { get; set; }
        public string ColumnName { get; set; }
        public string NewValue { get; set; }
        public string OldValue { get; set; }
    }
}
