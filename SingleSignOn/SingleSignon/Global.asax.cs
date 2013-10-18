using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Cats.Infrastructure;
using Cats.Services.Security;
using Cats.Web.Hub.Helpers;
using LanguageHelpers.Localization.DataAnnotations;
using log4net.Core;

namespace SingleSignon
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
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelMetadataProviders.Current = new LocalizedDataAnnotationsModelMetadataProvider();

            log4net.Config.XmlConfigurator.Configure();
            DependencyResolver.Current.GetService<ILogger>();
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