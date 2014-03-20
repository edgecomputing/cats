using System.Web.Mvc;

namespace Cats.Areas.Hub
{
    public class HubAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Hub";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
               name: "Hub_start",
               url: "Hub/",
               defaults: new { controller = "Home", action = "Index" },
               namespaces: new[] { "Cats.Areas.Hub.Controllers" }
              );

            context.MapRoute(
                "Hub_default",
                "Hub/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}