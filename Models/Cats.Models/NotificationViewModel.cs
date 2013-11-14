using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
   public class NotificationViewModel
    {
        public int NotificationId { get; set; }
        public string Text { get; set; }
        [MaxLength(200)]
        public string Url { get; set; }
        public int RecordId { get; set; }
        public bool IsRead { get; set; }
        public string TypeOfNotification { get; set; }
        public System.DateTime CreatedDate { get; set; }
       public string StrCreatedDate { get; set; }   
        public int Role { get; set; }
    }
}
