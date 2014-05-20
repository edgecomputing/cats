using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hubs
{
    public class SMS
    {
        public int SMSID { get; set; }
        public string InOutInd { get; set; }
        public string MobileNumber { get; set; }
        public string Text { get; set; }
        public Nullable<System.DateTime> RequestDate { get; set; }
        public Nullable<System.DateTime> SendAfterDate { get; set; }
        public Nullable<System.DateTime> QueuedDate { get; set; }
        public Nullable<System.DateTime> SentDate { get; set; }
        public int Status { get; set; }
        public Nullable<System.DateTime> StatusDate { get; set; }
        public Nullable<int> Attempts { get; set; }
        public Nullable<System.DateTime> LastAttemptDate { get; set; }
        public string EventTag { get; set; }
        public int? PartitionId { get; set; }
    }
}