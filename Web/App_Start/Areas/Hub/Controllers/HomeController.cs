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
        private readonly IAdminUnitService _adminUnitService;
        private readonly IHubService _hubService;

        public HomeController(IUserProfileService userProfileService, IAdminUnitService adminUnitService, IHubService hubService)
            : base(userProfileService)
        {
            _adminUnitService = adminUnitService;
            _hubService = hubService;

        }
        public ActionResult Index()
        {
            var currentUser = UserAccountHelper.GetUser(HttpContext.User.Identity.Name);
            ViewBag.HubName = currentUser.DefaultHub != null ? _hubService.FindById(currentUser.DefaultHub ?? 1).Name : "";
            ViewBag.HubID = currentUser.DefaultHub ?? 0;
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