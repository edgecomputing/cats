using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using Cats.Services.Security;
using Cats.Services.Common;
namespace Cats.Helpers
{
    public static class  NotificationHelper
    {
       

        public static int GetUnreadNotifications(this HtmlHelper helper)
        {
            try
            {

                var notificationService = (INotificationService)DependencyResolver.Current.GetService(typeof(INotificationService));
                var totallUnread = notificationService.GetAllNotification().Count(n => n.IsRead == false);

                return totallUnread;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public static HtmlString GetActiveNotifications(this HtmlHelper helper)
        {
            try
            {

               // var user = HttpContext.Current.User.IsInRole("Admin");
               
             
            
                var str = "<ul>";
                var notificationService = (INotificationService)DependencyResolver.Current.GetService(typeof(INotificationService));
                var totallUnread = notificationService.GetAllNotification().Where(n => n.IsRead == false).ToList();
               
                
                foreach(var item in totallUnread)
                {
                    str = str + "<li>";

                    str = str + "<a href=" + item.Url + ">";
                    str = str + item.Text;
                    str = str + "</li>";
                    str = str + "</a>";
                }
                str = str + "</ul>";
                return MvcHtmlString.Create(str);
            }
            catch (Exception)
            {
                return MvcHtmlString.Create("");
            }
        }


        public static void MakeNotificationRead(int recordId)
        {
            try
            {
                var notificationService = (INotificationService)DependencyResolver.Current.GetService(typeof(INotificationService));
                var notification = notificationService.FindBy(i => i.RecordId == recordId).Single();
                notification.IsRead = true;
                notificationService.EditNotification(notification); 
            }
            catch (Exception)
            {
                
               
            }
           
        }
    }
}