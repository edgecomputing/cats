using System.Web.Mvc;

namespace Cats.Areas.EarlyWarning
{
    public class EarlyWarningAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "EarlyWarning";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            context.MapRoute(
                name: "EarlyWarning_start",
                url: "EarlyWarning/",
                defaults: new { controller = "Home", action = "Index" },
                namespaces: new[] { "Cats.Areas.EarlyWarning.Controllers" }
               );

            context.MapRoute(
                "EarlyWarning_default",
                "EarlyWarning/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            
        }
    }
}
