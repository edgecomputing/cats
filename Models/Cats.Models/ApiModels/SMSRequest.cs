using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Models
{
    public class SMSRequest
    {
        public string action { get; set; }
        public int version { get; set; }
        public string phone_number { get; set; }
        public string phone_id { get; set; }
        public string phone_token { get; set; }
        public int send_limit { get; set; }
        public long now { get; set; }
        public int settings_version { get; set; }
        public int battery { get; set; }
        public int power { get; set; }
        public string network { get; set; }
        public string log { get; set; }
    }
}