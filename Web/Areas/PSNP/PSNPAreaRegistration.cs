using System.Web.Mvc;

namespace Cats.Areas.PSNP
{
    public class PSNPAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "PSNP";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                 "PSNP_default",
                 "PSNP/{controller}/{action}/{id}",
                 new {action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                name: "PSNP_Start",
                url:  "PSNP/",
                defaults:  new { controller="Home", action="Index"},
                namespaces:  new []{"Cats.Areas.PSNP.Controllers"} 
                );
            context.MapRoute(
                name: "PSNP_Request",
                url: "PSNP/Request/",
                defaults: new { controller = "Request", action = "Index" },
                namespaces: new[] { "Cats.Areas.EarlyWarning.Controllers" }
                );
        }
    }
}