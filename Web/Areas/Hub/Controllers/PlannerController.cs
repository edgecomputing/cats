using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Services.Hub;
using Cats.Web.Hub;

namespace Cats.Areas.Hub.Controllers
{
    public class PlannerController : BaseController
    {
        public PlannerController(IUserProfileService userProfileService)
            : base(userProfileService)
        {
            
        }
        //
        // GET: /Planner/

        public ActionResult Index()
        {
            return View();
        }

    }
}
