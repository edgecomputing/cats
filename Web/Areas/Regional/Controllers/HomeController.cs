using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Services.Dashboard;

namespace Cats.Areas.Regional.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRegionalDashboard _regionalDashboard;

        public HomeController(IRegionalDashboard regionalDashboard)
        {
            _regionalDashboard = regionalDashboard;
        }


        //
        // GET: /Regional/Home/
        public ActionResult Index()
        {
            ViewBag.RegionName = "Afar";
            return View();
        }

        public JsonResult Requests()
        {
            var requests = _regionalDashboard.GetRecentRequests();
            return Json(requests, JsonRequestBehavior.AllowGet);
        }

    }
}