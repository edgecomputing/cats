using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cats.Web.Hub.Controllers
{
    public class PlannerController : BaseController
    {
        //
        // GET: /Planner/

        public ActionResult Index()
        {
            return View();
        }

    }
}
