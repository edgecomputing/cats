using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hubs
{
    public partial class ErrorLog
    {
        [Key]
        public System.Guid ErrorLogID { get; set; }
        public int PartitionID { get; set; }
        public string Application { get; set; }
        public string Host { get; set; }
        public string Type { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
        public string User { get; set; }
        public int StatusCode { get; set; }
        public System.DateTime TimeUtc { get; set; }
        public int Sequence { get; set; }
        public string AllXml { get; set; }
    }
}
