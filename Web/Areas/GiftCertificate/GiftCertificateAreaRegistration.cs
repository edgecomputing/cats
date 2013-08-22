using System.Web.Mvc;

namespace Cats.Areas.GiftCertificate
{
    public class GiftCertificateAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "GiftCertificate";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "GiftCertificate_default",
                "GiftCertificate/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
