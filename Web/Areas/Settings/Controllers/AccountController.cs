using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Cats.Services.Security;
using Cats.Areas.Settings.Models.ViewModels;



namespace Cats.Areas.Settings.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private IUserAccountService service;

        public ActionResult Administration()
        {
            return Redirect("/");
        }

    }
}
