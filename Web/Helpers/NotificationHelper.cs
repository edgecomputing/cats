using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using Cats.Services.Security;
using Cats.Services.Common;
using Cats.Services.EarlyWarning;
namespace Cats.Helpers
{
    public static class  NotificationHelper
    {
       

        public static int GetUnreadNotifications(this HtmlHelper helper)
        {
            try
            {
                var accountService = (IUserAccountService)DependencyResolver.Current.GetService(typeof(IUserAccountService));
                var user = HttpContext.Current.User.Identity.Name;
                var roles = accountService.GetUserPermissions(user).Select(a => a.Roles).ToList();
                var allUserRollsInAllApplications = new List<string>();

                foreach (var app in roles)
                {
                    allUserRollsInAllApplications.AddRange(app.Select(role => role.RoleName));
                }
                var notificationService = (INotificationService)DependencyResolver.Current.GetService(typeof(INotificationService));
                var totallUnread = notificationService.GetAllNotification().Where(n => n.IsRead == false && allUserRollsInAllApplications.Contains(n.RoleName)).ToList();
                

                
               

                return totallUnread.Count();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int GetUnreadNotifications()
        {
            try
            {
                var accountService = (IUserAccountService)DependencyResolver.Current.GetService(typeof(IUserAccountService));
                var user = HttpContext.Current.User.Identity.Name;
                var roles = accountService.GetUserPermissions(user).Select(a => a.Roles).ToList();
                var allUserRollsInAllApplications = new List<string>();

                foreach (var app in roles)
                {
                    allUserRollsInAllApplications.AddRange(app.Select(role => role.RoleName));
                }
                var notificationService = (INotificationService)DependencyResolver.Current.GetService(typeof(INotificationService));
                var totallUnread = notificationService.GetAllNotification().Where(n => n.IsRead == false && allUserRollsInAllApplications.Contains(n.RoleName)).ToList();
                return totallUnread.Count();
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
                var accountService = (IUserAccountService)DependencyResolver.Current.GetService(typeof(IUserAccountService));
              
                var user = HttpContext.Current.User.Identity.Name;

                var roles = accountService.GetUserPermissions(user).Select(a => a.Roles).ToList();
                var allUserRollsInAllApplications = new List<string>();

                foreach (var app in roles)
                {
                    allUserRollsInAllApplications.AddRange(app.Select(role => role.RoleName));
                }


                var str = "<ul>";
                var notificationService = (INotificationService)DependencyResolver.Current.GetService(typeof(INotificationService));
                var totallUnread = notificationService.GetAllNotification().Where(n => n.IsRead == false && allUserRollsInAllApplications.Contains(n.RoleName)).ToList();
                int max = 0;

                if (totallUnread.Count < 1)
                    return MvcHtmlString.Create("");
                max = totallUnread.Count > 5 ? 5 : totallUnread.Count;

                for (int i = 0; i < max;i ++ )
                {
                    str = str + "<li>";
                    str = str + "<a href=" + totallUnread[i].Url + ">";
                    str = str + totallUnread[i].Text;
                    str = str + "</li>";
                    str = str + "</a>";
                }
                    
                str = str + "</ul>";
                if (totallUnread.Count > 5)
                {
                    str = str + "<a href=/Home/GetUnreadNotificationDetail>" + "More...</a>";
                }
               
                return MvcHtmlString.Create(str);
            }
            catch (Exception)
            {
                return MvcHtmlString.Create("");
            }
        }

        public static HtmlString GetActiveNotifications()
        {
            try
            {
                var accountService = (IUserAccountService)DependencyResolver.Current.GetService(typeof(IUserAccountService));

                var user = HttpContext.Current.User.Identity.Name;

                var roles = accountService.GetUserPermissions(user).Select(a => a.Roles).ToList();
                var allUserRollsInAllApplications = new List<string>();

                foreach (var app in roles)
                {
                    allUserRollsInAllApplications.AddRange(app.Select(role => role.RoleName));
                }


                var str = "<ul>";
                var notificationService = (INotificationService)DependencyResolver.Current.GetService(typeof(INotificationService));
                var totallUnread = notificationService.GetAllNotification().Where(n => n.IsRead == false && allUserRollsInAllApplications.Contains(n.RoleName)).ToList();
                int max = 0;

                if (totallUnread.Count < 1)
                    return MvcHtmlString.Create("");
                max = totallUnread.Count > 5 ? 5 : totallUnread.Count;

                for (int i = 0; i < max; i++)
                {
                    str = str + "<li>";
                    str = str + "<a href=" + totallUnread[i].Url + ">";
                    str = str + totallUnread[i].Text;
                    str = str + "</li>";
                    str = str + "</a>";
                }

                str = str + "</ul>";
                if (totallUnread.Count > 5)
                {
                    str = str + "<a href=/Home/GetUnreadNotificationDetail>" + "More...</a>";
                }

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
                //var reliefRequisitionService =
                //    (IReliefRequisitionService)DependencyResolver.Current.GetService(typeof(IReliefRequisitionService));

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