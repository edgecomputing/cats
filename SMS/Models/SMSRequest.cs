using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Models
{
    public class SMSRequest
    {
        public int version { get; set; }
        public string phone_number { get; set; }
        public string action { get; set; }
        public string log { get; set; }
        public int send_limit { get; set; }
        public int settings_version { get; set; }
        public int battery { get; set; }
        public int power { get; set; }
        public string network { get; set; }
        public long now { get; set; }

        public string message_type { get; set; }
        public string message { get; set; }
        public string from { get; set; }
        public string timestamp { get; set; }

        public string id { get; set; }
        public string status { get; set; }
        public string error { get; set; }
    }
}