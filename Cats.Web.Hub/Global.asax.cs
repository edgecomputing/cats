using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Cats.Web.Hub.Helpers;
using Cats.Web.Hub.Infrastructure;
using Elmah;

namespace Cats.Web.Hub
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ElmahHandledErrorLoggerFilter());
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("mapping/*");
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "Lang", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Language", action = "SetLanguage", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            DependencyResolver.SetResolver(new NinjectDependencyResolver());
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
           
        }

        public override string GetVaryByCustomString(HttpContext context, string arg)
        {
            // It seems this executes multiple times and early, so we need to extract language again from cookie.
            if (arg == "culture") // culture name (e.g. "en-US") is what should vary caching
            {
                string cultureName = null;
                // Attempt to read the culture cookie from Request
                HttpCookie cultureCookie = Request.Cookies["_culture"];
                if (cultureCookie != null)
                    cultureName = cultureCookie.Value;
                else
                    cultureName = Request.UserLanguages[0]; // obtain it from HTTP header AcceptLanguages

                // Validate culture name
                cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe

                return cultureName.ToLower();// use culture name as cache key, "es", "en-us", "es-cl", etc.
            }

            return base.GetVaryByCustomString(context, arg);
        }

        // ELMAH Filtering
        protected void ErrorLog_Filtering(object sender, ExceptionFilterEventArgs e)
        {
            FilterError404(e);
        }

        protected void ErrorMail_Filtering(object sender, ExceptionFilterEventArgs e)
        {
            FilterError404(e);
        }

        // Dismiss 404 errors for ELMAH
        private void FilterError404(ExceptionFilterEventArgs e)
        {
            if (e.Exception.GetBaseException() is HttpException)
            {
                HttpException ex = (HttpException)e.Exception.GetBaseException();
                if (ex.GetHttpCode() == 404)
                    e.Dismiss();
            }
        }
    }
}
