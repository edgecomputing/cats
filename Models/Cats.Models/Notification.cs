using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Cats.Models.Security.ViewModels;

namespace Cats.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public string Text { get; set; }
        [MaxLength(200)]
        public string Url { get; set; }
        public int RecordId { get; set; }
        public bool IsRead { get; set; }
        public string TypeOfNotification { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int Id { get; set; }
        public string Application { get; set; }
       
    }
}
