using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
            return GetUserName();
        }

        public static string GetUserName()
        {
            try
            {
                var user = (UserInfo)HttpContext.Current.Session["USER_INFO"];
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
            return GetUserInfo(userName);
        }

        private static UserInfo GetUserInfo(string userName)
        {
            // Initialize the user object with the incoming user name to avoid 'Use of uninitialized variable exception'
            UserInfo user = new UserInfo { UserName = userName };
            try
            {
                // Check to see if we already have the user profile loaded in the session.
                if (null != HttpContext.Current.Session["USER_INFO"])
                {
                    user = (UserInfo)HttpContext.Current.Session["USER_INFO"];
                }
                else
                {
                    // Fetch a copy from the database if we don't have a session variable already loaded in memory
                    var service = (IUserAccountService)DependencyResolver.Current.GetService(typeof(IUserAccountService));
                    user = service.GetUserInfo(userName);
                }
            }
            catch (Exception ex)
            {
                //TODO: Log error here
                Logger.Log(ex);
            }

            return user;
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
        public static string UserUnitPreference(this HtmlHelper helper)
        {
            return UserUnitPreference();
        }
        public static string UserUnitPreference()
        {
            var preference = "MT";
            var user = GetUser(HttpContext.Current.User.Identity.Name);
            try
            {
                preference = user.PreferedWeightMeasurment;
            }
            catch (Exception ex)
            {
                // TODO: Log exception hrere
            }

            return preference.ToUpper();
        }

    }
}