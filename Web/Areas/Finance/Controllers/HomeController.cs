using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cats.Areas.Finance.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Finance/Home/

        public ActionResult Index()
        {
            return View();
        }

    }
}
