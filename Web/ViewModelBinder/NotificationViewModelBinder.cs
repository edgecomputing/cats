using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Helpers;
using Cats.Models;
namespace Cats.ViewModelBinder
{
    public class NotificationViewModelBinder
    {

        public static IEnumerable<NotificationViewModel> ReturnNotificationViewModel(List<Notification> notification)
        {


            return notification.Select(notify => new NotificationViewModel()
                                                     {
                                                         NotificationId = notify.NotificationId,
                                                         Text = notify.Text,

                                                         Url = notify.Url,
                                                         RecordId = notify.RecordId,
                                                         IsRead = notify.IsRead,
                                                         TypeOfNotification = notify.TypeOfNotification,
                                                         StrCreatedDate = notify.CreatedDate.ToCTSPreferedDateFormat(UserAccountHelper.UserCalendarPreference()),
                                                         Role = notify.Role
                                                     });
        }
    }
}