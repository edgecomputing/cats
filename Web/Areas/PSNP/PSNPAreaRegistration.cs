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
            context.MapRoute("PSNP_Request",
                "PSNP/Request/",
                new { action = "Index", area = "EarlyWarning", controller = "Request", id = UrlParameter.Optional });

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
                name: "PSNP_Request2",
                url: "PSNP/Request/",
                defaults: new { area = "EarlyWarning", controller = "Request", action = "Index" },
                namespaces: new[] { "Cats.Areas.EarlyWarning.Controllers" }
                );
            

            


        }
    }
}