using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cats.Areas.Settings.Controllers
{
    [Authorize]
    public class AdminDashboardController : Controller
    {
        //
        // GET: /Settings/AdminDashboard/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Users()
        {
            return View();
        }

        public ActionResult Redirect2Admin()
        {
            return Redirect("http://localhost/admin");
        }

    }
}
