using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cats.Web.Hub.Controllers
{
    public class ReleaseNotesController : BaseController
    {
        //
        // GET: /ReleaseNotes/

        public ActionResult Index()
        {
            return View();
        }

    }
}
