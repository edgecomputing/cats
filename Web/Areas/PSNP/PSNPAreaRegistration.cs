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
        }
    }
}
