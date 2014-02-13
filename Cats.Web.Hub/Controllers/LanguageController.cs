using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Cats.Services.Hub;
using Cats.Web.Hub.Helpers;

namespace Cats.Web.Hub.Controllers
{
    public partial class LanguageController : BaseController
    {
        public LanguageController(IUserProfileService userProfileService)
            : base(userProfileService)
        {
            
        }
        //
        // GET: /Language/

        public  ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Language/SetLanguage/en

        public virtual ActionResult SetLanguage(string lang)
        {

            string Lang = null;

            Lang = CultureHelper.GetNeutralCulture(CultureHelper.GetImplementedCulture(lang).ToString());

            if (User.Identity.IsAuthenticated) {
                
                this.GetCurrentUserProfile().ChangeLanguage(Lang);
            }
            else {
                HttpCookie cultureCookie = Response.Cookies["_culture"];
                cultureCookie.Value = Lang;
                Response.SetCookie(cultureCookie);
            }
            return Redirect(this.Request.UrlReferrer.PathAndQuery);
        }
    }
}
