using System.Web.Mvc;

namespace Cats.Areas.Procurement
{
    public class ProcurementAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Procurement";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Procurement_default",
                "Procurement/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Procurement_start",
                "Procurement/",
                new { controller="Home",action ="Index"},
                new []{"Cats.Areas.Procurement.Controllers"} 
                );
        }
    }
}
