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
                "EarlyWarning_default",
                "EarlyWarning/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
