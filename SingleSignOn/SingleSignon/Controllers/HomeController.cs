using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SingleSignon.Controllers
{
    public class HomeController : Controller
    {
        public void Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            RedirectToRoute(FormatRedirectUrl("Cats/Home"));
            //return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public static string FormatRedirectUrl(string redirectUrl)
        {
            var c = System.Web.HttpContext.Current;
            //Don’t append the forms auth ticket for unauthenticated users 
            //   or
            //for users authenticated with a different mechanism
            if (!c.User.Identity.IsAuthenticated ||
                c.User.Identity.AuthenticationType != "Forms")
                return redirectUrl;
            //Determine if we need to append to an existing query-string or
            //  not
            string qsSpacer;

            if (redirectUrl.IndexOf('?') > 0)
                qsSpacer = "&";
            else
                qsSpacer = "?";
            //Build the new redirect URL. Assuming that currently using
            //forms authentication. Change the below FormsIdentity if required.
            //string newRedirectUrl;
            var fi = (FormsIdentity)c.User.Identity;
            var newRedirectUrl = redirectUrl + qsSpacer +
                                    FormsAuthentication.FormsCookieName + "=" +
                                    FormsAuthentication.Encrypt(fi.Ticket);
            return newRedirectUrl;
        }
    }
}
