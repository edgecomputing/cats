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
using Cats.Helpers;
using LanguageHelpers.Localization.DataAnnotations;
using StackExchange.Profiling;
using log4net.Core;
using NetSqlAzMan;
using NetSqlAzMan.Interfaces;
using NetSqlAzMan.Cache;
using System.Configuration;

namespace Cats
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Clear all ViewEngines except Razor
            ViewEngines.Engines.Clear(); 
            ViewEngines.Engines.Add(new RazorViewEngine());
           
            AreaRegistration.RegisterAllAreas();

            DependencyResolver.SetResolver(new NinjectDependencyResolver());
            GlobalConfiguration.Configuration.Filters.Add(new ElmahErrorAttribute());
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelMetadataProviders.Current = new LocalizedDataAnnotationsModelMetadataProvider();

            log4net.Config.XmlConfigurator.Configure();
            DependencyResolver.Current.GetService<ILogger>();

            // EF Profiler
            //HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();

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

        protected void Application_BeginRequest()
        {
            if (Request.IsLocal)
            {
                //MiniProfiler.Start();
                //MiniProfilerEF.InitializeEF42();
            }
        }

        protected void Application_EndRequest()
        {
            MiniProfiler.Stop();
        }

        protected void Session_Start()
        {
            var user = (UserIdentity)HttpContext.Current.User.Identity;            
            IAzManStorage storage = new SqlAzManStorage(ConfigurationManager.ConnectionStrings["CatsContext"].ConnectionString);
            IAzManDBUser dbUser = storage.GetDBUser(user.Name);

            // Early Warning user permissions
            UserPermissionCache earlyWarningPermissionCache = new UserPermissionCache(storage, CatsGlobals.CATS, CatsGlobals.EARLY_WARNING,dbUser,true,true);
            Session[CatsGlobals.EARLY_WARNING_PERMISSIONS] = earlyWarningPermissionCache;


            //PSNP user permission
            UserPermissionCache psnpPermissionCache = new UserPermissionCache(storage, CatsGlobals.CATS, CatsGlobals.PSNP, dbUser, true, true);
            Session[CatsGlobals.PSNP_PERMISSIONS] = psnpPermissionCache;

            // Logistics user permissions
            UserPermissionCache logisticsPermissionCache = new UserPermissionCache(storage, CatsGlobals.CATS, CatsGlobals.LOGISTICS, dbUser, true, true);
            Session[CatsGlobals.PSNP_PERMISSIONS] = logisticsPermissionCache;

            // Procurement user permissions


            // Hub user permissions

            // Whatever permission we are going to have!

        }
    }
}