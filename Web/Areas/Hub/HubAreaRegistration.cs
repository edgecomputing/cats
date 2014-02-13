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
                "Hub_default",
                "Hub/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
