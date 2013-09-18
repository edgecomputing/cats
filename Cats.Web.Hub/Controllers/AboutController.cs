using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Web.Hub;

namespace Cats.Web.Hub.Controllers
{
    public class AboutController : BaseController
    {


        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

    }
}
