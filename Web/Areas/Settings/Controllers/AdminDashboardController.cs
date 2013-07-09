using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cats.Areas.Settings.Controllers
{
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

    }
}
