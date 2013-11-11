using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public partial class Notification
    {
        public int NotificationId { get; set; }
        public Nullable<int> ReqID { get; set; }
        public int TransportOrderID { get; set; }
        public bool IsRead { get; set; }
        public Nullable<int> NotifiedUser { get; set; }
        public System.DateTime NotificationDate { get; set; }
    }
}
