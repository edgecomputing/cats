using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Cats.Web.Administration.Helpers;
using Cats.Services.Security;
using Cats.Web.Adminstration.Infrastructure;

namespace Cats.Web.Adminstration
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            DependencyResolver.SetResolver(new NinjectDependencyResolver());
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
          
            if (authCookie != null)
            {
              
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                var identity = new UserIdentity(UserAccountHelper.GetUser(ticket.Name));
                var principal = new UserPrincipal(identity);
                HttpContext.Current.User = principal;
            }
        }
    }
}