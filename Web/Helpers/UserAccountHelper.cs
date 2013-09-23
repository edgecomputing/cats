using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Services.Security;
using Cats.Models.Security;

namespace Cats.Helpers
{
    public static class UserAccountHelper
    {
        public static string GetUserName(this HtmlHelper helper)
        {
            try
            {
                var user = (UserIdentity)HttpContext.Current.User.Identity;
                return user.FullName;
            }
            catch (Exception)
            {
                return "Guest User";
            }                       
        }
        public static string UserLanguagePreference(this HtmlHelper helper)
        {
            try
            {
                var user = (UserIdentity)HttpContext.Current.User.Identity;
                return GetUser(user.Name).LanguageCode;
            }
            catch (Exception)
            {
                return "Guest User";
            }
        }
        public static UserInfo GetUser(string userName)
        {
            var service = (IUserAccountService)DependencyResolver.Current.GetService(typeof (IUserAccountService));
            return service.GetUserInfo(userName);
        }

        public static string UserCalendarPreference(this HtmlHelper helper)
        {
            return UserCalendarPreference();
        }

        public static string UserCalendarPreference()
        {
            var preference = "EN";
            var user = GetUser(HttpContext.Current.User.Identity.Name);
            try
            {
                preference = user.DatePreference;
            }
            catch (Exception ex)
            {
                // TODO: Log exception hrere
            }

            return preference.ToUpper();
        }
    }
}