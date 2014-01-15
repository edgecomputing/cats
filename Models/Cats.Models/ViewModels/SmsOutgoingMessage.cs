using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.ViewModels
{
    class SmsOutgoingMessage
    {
        public Guid Id { get; set; }
        public string To { get; set; }
        public string Message { get; set; }
        public int Priority { get; set; }
        public string Type { get; set; }
    }

    internal class SmsEventSend
    {
        public string Event { get; set; }
        public ICollection<SmsOutgoingMessage> Messages { get; set; }
    }
}