using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cats.Areas.EarlyWarning.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /EarlyWarning/

        public ActionResult Index()
        {
            ModelState.AddModelError("Success", "Sample Error Message. Use in Your Controller: ModelState.AddModelError('Errors', 'Your Error Message.')");
            return View();
        }

    }
}
