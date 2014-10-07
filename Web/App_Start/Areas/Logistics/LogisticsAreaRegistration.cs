using System.Web.Mvc;

namespace Cats.Areas.Logistics
{
    public class LogisticsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Logistics";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
               name: "Logistics_start",
               url: "Logistics/",
               defaults: new { controller = "Home", action = "Index" },
               namespaces: new[] { "Cats.Areas.Logistics.Controllers" }
               );

            context.MapRoute(
                "Logistics_default",
                "Logistics/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );


        }
    }
}
