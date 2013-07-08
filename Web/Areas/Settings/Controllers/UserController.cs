using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cats.Areas.Settings.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /Settings/User/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            return View();
        }

    }
}
