using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cats.Areas.Procurement.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Procurement/Home/

        public ActionResult Index()
        {
            return View();
        }

    }
}
