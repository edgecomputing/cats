using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Helpers;
using Cats.Models.Security;
using Cats.Services.Dashboard;
using Cats.Services.EarlyWarning;

namespace Cats.Areas.Regional.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAdminUnitService _adminUnitService;

        public HomeController(IAdminUnitService adminUnitService)
        {
            _adminUnitService = adminUnitService;
        }
        //

        // GET: /Regional/Home/
        public ActionResult Index()
        {
			var currentUser = UserAccountHelper.GetUser(HttpContext.User.Identity.Name);
            
            if (!currentUser.RegionalUser || currentUser.RegionID==null)
            {
                ModelState.AddModelError("Errors", @"You are not assigned to any region");    
            }
            
			ViewBag.RegionID = currentUser.RegionID;
            ViewBag.RegionName = currentUser.RegionID != null ? _adminUnitService.FindById(currentUser.RegionID ?? 0).Name : "";
            return View();
        }
    }
}