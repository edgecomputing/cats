using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Services.Hub;

namespace Cats.Web.Hub.Controllers
{
    public class HelpController : BaseController
    {
        public HelpController(IUserProfileService userProfileService)
            : base(userProfileService)
        {
            
        }
        //
        // GET: /Help/

        public ActionResult Index()
        {
            return View();
        }

    }
}
