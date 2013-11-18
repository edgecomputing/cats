using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Services.Security;
using Logistics.Security;

namespace Cats.Helpers
{
    public static class MainMenuExtensionHelper
    {
        public static MvcHtmlString EarlyWarningOperationMenuItem(this HtmlHelper helper, string url, EarlyWarningCheckAccess.Operation operation, string text = "", string ccsClass = "", string dataButtontype = "")
        {
            var user = (UserIdentity)HttpContext.Current.User.Identity;
            var checkAccessHelper = DependencyResolver.Current.GetService<IEarlyWarningCheckAccess>();
            var dbUser = checkAccessHelper.Storage.GetDBUser(user.Profile.UserName).CustomSid;
            
            var html = string.Empty;

            if (checkAccessHelper.CheckAccess(operation, dbUser))
            {
                html = @"<a data-buttontype=" + dataButtontype + "  class=" + ccsClass + " href=" + url + ">" + text + "</a>";
            }
            return MvcHtmlString.Create(html);
        }

        public static MvcHtmlString EarlyWarningOperationButton(this HtmlHelper helper, string url, EarlyWarningCheckAccess.Operation operation, string text = "", string ccsClass = "", string dataButtontype = "", string id = "")
        {
            var user = (UserIdentity)HttpContext.Current.User.Identity;
            var checkAccessHelper = DependencyResolver.Current.GetService<IEarlyWarningCheckAccess>();
            var dbUser = checkAccessHelper.Storage.GetDBUser(user.Profile.UserName).CustomSid;

            var html = string.Empty;

            if (checkAccessHelper.CheckAccess(operation, dbUser))
            {
                html = "<a href=" + url;
                if (ccsClass != "")
                {
                    html += " class=" + ccsClass;
                }
                if (id != "")
                {
                    html += " id=" + id;
                }
                if (dataButtontype != "")
                {
                    html += " data-buttontype=" + dataButtontype;
                }
                if(text!="")
                {
                    html += " >" + text + "</a>";
                }
                else
                {
                    html += " ></a>";
                }
            }
            return MvcHtmlString.Create(html);
        }

        public static MvcHtmlString PSNPOperationMenuItem(this HtmlHelper helper, string text, string url, PSNPCheckAccess.Operation operation, string ccsClass="", string dataButtontype="")
        {
            var user = (UserIdentity)HttpContext.Current.User.Identity;
            var checkAccessHelper = DependencyResolver.Current.GetService<IPSNPCheckAccess>();
            var dbUser = checkAccessHelper.Storage.GetDBUser(user.Profile.UserName).CustomSid;

            var html = string.Empty;

            if (checkAccessHelper.CheckAccess(operation, dbUser))
            {
                html = @"<a data-buttontype=" + dataButtontype + "  class=" + ccsClass + " href=" + url + ">" + text + "</a>";
            }
            return MvcHtmlString.Create(html);
        }

        public static MvcHtmlString PSNPOperationButton(this HtmlHelper helper, string url, PSNPCheckAccess.Operation operation, string text = "", string ccsClass = "", string dataButtontype = "", string id = "")
        {
            var user = (UserIdentity)HttpContext.Current.User.Identity;
            var checkAccessHelper = DependencyResolver.Current.GetService<IPSNPCheckAccess>();
            var dbUser = checkAccessHelper.Storage.GetDBUser(user.Profile.UserName).CustomSid;

            var html = string.Empty;

            if (checkAccessHelper.CheckAccess(operation, dbUser))
            {
                html = "<a href=" + url;
                if (ccsClass != "")
                {
                    html += " class=" + ccsClass;
                }
                if (id != "")
                {
                    html += " id=" + id;
                }
                if (dataButtontype != "")
                {
                    html += " data-buttontype=" + dataButtontype;
                }
                if (text != "")
                {
                    html += " >" + text + "</a>";
                }
                else
                {
                    html += " ></a>";
                }
            }
            return MvcHtmlString.Create(html);
        }

        public static MvcHtmlString LogisticOperationMenuItem(this HtmlHelper helper, string text, string url, LogisticsCheckAccess.Operation operation, string ccsClass="", string dataButtontype="")
        {
            var user = (UserIdentity)HttpContext.Current.User.Identity;
            var checkAccessHelper = DependencyResolver.Current.GetService<ILogisticsCheckAccess>();
            var dbUser = checkAccessHelper.Storage.GetDBUser(user.Profile.UserName).CustomSid;

            var html = string.Empty;

            if (checkAccessHelper.CheckAccess(operation, dbUser))
            {
                html = @"<a data-buttontype=" + dataButtontype + "  class=" + ccsClass + " href=" + url + ">" + text + "</a>";
            }
            return MvcHtmlString.Create(html);
        }

        public static MvcHtmlString LogisticsOperationButton(this HtmlHelper helper, string url, LogisticsCheckAccess.Operation operation, string text = "", string ccsClass = "", string dataButtontype = "", string id = "")
        {
            var user = (UserIdentity)HttpContext.Current.User.Identity;
            var checkAccessHelper = DependencyResolver.Current.GetService<ILogisticsCheckAccess>();
            var dbUser = checkAccessHelper.Storage.GetDBUser(user.Profile.UserName).CustomSid;

            var html = string.Empty;

            if (checkAccessHelper.CheckAccess(operation, dbUser))
            {
                html = "<a href=" + url;
                if (ccsClass != "")
                {
                    html += " class=" + ccsClass;
                }
                if (id != "")
                {
                    html += " id=" + id;
                }
                if (dataButtontype != "")
                {
                    html += " data-buttontype=" + dataButtontype;
                }
                if (text != "")
                {
                    html += " >" + text + "</a>";
                }
                else
                {
                    html += " ></a>";
                }
            }
            return MvcHtmlString.Create(html);
        }

        public static MvcHtmlString ProcurementOperationMenuItem(this HtmlHelper helper, string text, string url, ProcurementCheckAccess.Operation operation, string ccsClass="", string dataButtontype="")
        {
            var user = (UserIdentity)HttpContext.Current.User.Identity;
            var checkAccessHelper = DependencyResolver.Current.GetService<IProcurementCheckAccess>();
            var dbUser = checkAccessHelper.Storage.GetDBUser(user.Profile.UserName).CustomSid;

            var html = string.Empty;

            if (checkAccessHelper.CheckAccess(operation, dbUser))
            {
                html = @"<a data-buttontype=" + dataButtontype + "  class=" + ccsClass + " href=" + url + ">" + text + "</a>";
            }
            return MvcHtmlString.Create(html);
        }

        public static MvcHtmlString ProcurementOperationButton(this HtmlHelper helper, string url, ProcurementCheckAccess.Operation operation, string text = "", string ccsClass = "", string dataButtontype = "", string id = "")
        {
            var user = (UserIdentity)HttpContext.Current.User.Identity;
            var checkAccessHelper = DependencyResolver.Current.GetService<IProcurementCheckAccess>();
            var dbUser = checkAccessHelper.Storage.GetDBUser(user.Profile.UserName).CustomSid;

            var html = string.Empty;

            if (checkAccessHelper.CheckAccess(operation, dbUser))
            {
                html = "<a href=" + url;
                if (ccsClass != "")
                {
                    html += " class=" + ccsClass;
                }
                if (id != "")
                {
                    html += " id=" + id;
                }
                if (dataButtontype != "")
                {
                    html += " data-buttontype=" + dataButtontype;
                }
                if (text != "")
                {
                    html += " >" + text + "</a>";
                }
                else
                {
                    html += " ></a>";
                }
            }
            return MvcHtmlString.Create(html);
        }
    }
}