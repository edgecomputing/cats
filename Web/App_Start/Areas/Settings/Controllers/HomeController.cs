using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cats.Areas.Settings.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Settings/Home/

        public ActionResult Index()
        {
            return View();
        }

    }
}
