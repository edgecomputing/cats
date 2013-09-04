using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Cats
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name:"Error",
                url:"Error",
                defaults:new{controller="Home",action="Error"}
                );
            routes.MapRoute(
                name:"Login",
                url:"login",
                defaults:new {controller="Account",action="Login"}
                );
            routes.MapRoute(
                name:"Logout",
                url:"logout",
                defaults:new {controller="Account",action="Logout"}
                );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Cats.Controllers" }
            );
        }
    }
}