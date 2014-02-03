using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.ViewModels
{
    public class SmsOutgoingMessage
    {
        public string id { get; set; }
        public string to { get; set; }
        public string message { get; set; }
        //public int Priority { get; set; }
        //public string Type { get; set; }
    }

    public class SmsEventSend
    {
        public string @event { get; set; }
        public ICollection<SmsOutgoingMessage> messages { get; set; }
    }
}