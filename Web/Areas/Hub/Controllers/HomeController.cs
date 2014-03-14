using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models.Hubs;
using System.Text;
using System.Security.Cryptography;
using Cats.Services.Hub;
using Cats.Services.Security;
using Cats.Web.Hub;
using Cats.Web.Hub.Helpers;
using Cats.Security;
using Cats.Helpers;


namespace Cats.Areas.Hub.Controllers
{

    public class HomeController : BaseController
    {
        public HomeController(IUserProfileService userProfileService)
            : base(userProfileService)
        {

        }
        public ActionResult Index()
        {
            ViewBag.RegionName = "Adama Hub";
            return View();
        }

        [EarlyWarningAuthorize(operation = EarlyWarningConstants.Operation.View_request)]
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Redirect2Cats()
        {
            return Redirect("/Cats");
        }
    }
}
