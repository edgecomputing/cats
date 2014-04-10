using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Cats.Services.Security;
using Cats.Models.Security;
using System.Web.Security;
using NetSqlAzMan.Cache;

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
            var userName = string.Empty;
            try
            {
                var user = (UserInfo)HttpContext.Current.Session["USER_INFO"];
                userName= user.FullName;
            }
            catch (Exception)
            {
                userName="Guest User";
            }
            return userName;
        }

        public static string UserLanguagePreference(this HtmlHelper helper)
        {
            var userLanguagePreference = string.Empty;
            try
            {
                var user = (UserIdentity)HttpContext.Current.User.Identity;
                userLanguagePreference= GetUser(user.Name).LanguageCode;
            }
            catch (Exception)
            {
                userLanguagePreference ="Guest User";
            }
            return userLanguagePreference;
        }
        public static UserInfo GetUser(string userName)
        {
            return GetUserInfo(userName);
        }

        private static void SignOut()
        {
            FormsAuthentication.SignOut();
        }

        public static UserInfo GetCurrentUser()
        {
            return GetUserInfo(HttpContext.Current.User.Identity.Name);
        }
        private static UserInfo GetUserInfo(string userName)
        {
            // Try returning session stored values if available
            if(null != HttpContext.Current.Session && null!=HttpContext.Current.Session["USER_INFO"])
            {
                return (UserInfo)HttpContext.Current.Session["USER_INFO"]; 
            }

            // Fetch a fresh copy of user information from the database
            var service = (IUserAccountService)DependencyResolver.Current.GetService(typeof(IUserAccountService));
            var user = service.GetUserInfo(userName);

            // Save user information to session state for latter usage
            if (HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session["USER_INFO"] = user;
                HttpContext.Current.Session["USER_PROFILE"] = service.GetUserDetail(userName);
            }

            return user;

            //// Initialize the user object with the incoming user name to avoid 'Use of uninitialized variable exception'
            //UserInfo user = new UserInfo { UserName = userName };

            //try
            //{
            //    // Check to see if we already have the user profile loaded in the session.
            //    if ( HttpContext.Current.Session.Keys.Count>0)
            //    {
            //        if (HttpContext.Current.Session["USER_INFO"]!=null)
            //        {
            //            user = (UserInfo)HttpContext.Current.Session["USER_INFO"];    
            //        }
            //        else
            //        {
            //            // Fetch a copy from the database if we don't have a session variable already loaded in memory
            //            var service = (IUserAccountService)DependencyResolver.Current.GetService(typeof(IUserAccountService));
            //            user = service.GetUserInfo(userName);
            //        }

            //        //to update the "USER_INFO"session as far as the user is engaged 
            //        //HttpContext.Current.Session["USER_INFO"] = user;
            //    }
            //    else
            //    {
            //        // Fetch a copy from the database if we don't have a session variable already loaded in memory
            //        if(HttpContext.Current.User.Identity.IsAuthenticated)
            //        {
            //            var service = (IUserAccountService)DependencyResolver.Current.GetService(typeof(IUserAccountService));
            //            user = service.GetUserInfo(userName);
            //            HttpContext.Current.Session["USER_INFO"] = user;
            //            HttpContext.Current.Session["USER_PROFILE"] = service.GetUserDetail(userName);
            //        }
            //    }
            //}

            //catch (Exception ex)
            //{
            //    //TODO: Log error here
            //    Logger.Log(ex);
            //    SignOut();
            //    return null;
            //}

            //return user;
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

        public static UserPermissionCache GetUserPermissionCache(CatsGlobals.Applications application)
        {
            UserPermissionCache permissionsCache = null;
           
            switch (application)
            {
                case CatsGlobals.Applications.EarlyWarning:
                    permissionsCache = (UserPermissionCache)HttpContext.Current.Session[CatsGlobals.EARLY_WARNING_PERMISSIONS];
                    break;
                case CatsGlobals.Applications.PSNP:
                    permissionsCache = (UserPermissionCache)HttpContext.Current.Session[CatsGlobals.PSNP_PERMISSIONS];
                    break;
                case CatsGlobals.Applications.Logistics:
                    permissionsCache = (UserPermissionCache)HttpContext.Current.Session[CatsGlobals.LOGISTICS_PERMISSIONS];
                    break;
                case CatsGlobals.Applications.Procurement:
                    permissionsCache = (UserPermissionCache)HttpContext.Current.Session[CatsGlobals.PROCUREMENT_PERMISSIONS];
                    break;
                case CatsGlobals.Applications.Finance:
                    permissionsCache = (UserPermissionCache)HttpContext.Current.Session[CatsGlobals.FINANCE_PERMISSIONS];
                    break;
                case CatsGlobals.Applications.Hub:
                    permissionsCache = (UserPermissionCache)HttpContext.Current.Session[CatsGlobals.HUB_PERMISSIONS];
                    break;
                case CatsGlobals.Applications.Administration:
                    permissionsCache = (UserPermissionCache)HttpContext.Current.Session[CatsGlobals.ADMINISTRATION_PERMISSIONS];
                    break;
                case CatsGlobals.Applications.Region:
                    permissionsCache = (UserPermissionCache)HttpContext.Current.Session[CatsGlobals.REGION_PERMISSIONS];
                    break;
            }

            return permissionsCache;
        }

    }
}