using System.Web.Mvc;

namespace Cats.Areas.Localization
{
    public class LocalizationAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Localization";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Localization_default",
                "Localization/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                name: "Localization_start",
                url: "Localization/",
                defaults: new { controller = "Home", action = "Index" },
                namespaces: new[] { "Cats.Areas.Localization.Controllers" }
               );
        }
    }
}
