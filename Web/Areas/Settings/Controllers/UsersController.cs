using Cats.Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cats.Areas.Settings.Controllers
{
    public class UsersController : Controller
    {
        private IUserAccountService accountService;

        public UsersController(IUserAccountService service)
        {
            accountService = service;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            return View();
        }

        public JsonResult GetUsers()
        {
            var users = accountService.GetAll();
            return Json(users.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}
