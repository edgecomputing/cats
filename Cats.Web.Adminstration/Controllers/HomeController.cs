using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cats.Web.Adminstration.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Redirect2Cats()
        {
            return Redirect("http://192.168.1.248/Cats");
        }
    }
}
