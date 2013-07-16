using System.Collections.Generic;
using Cats.Models.Security;
using Cats.Services.Security;
using System.Linq;
using System.Web.Mvc;
using Cats.Areas.Settings.Models;

namespace Cats.Areas.Settings.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private IUserAccountService userService;

        public UsersController(IUserAccountService service)
        {
            userService = service;
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(UserViewModel userInfo)
        {
            var messages = new List<string>();

            // Check business rule and validations
            if (userInfo.UserName == string.Empty)
                messages.Add("User name cannot be empyt");
            if (userInfo.FullName == string.Empty)
                messages.Add("Full name cannot be empyt");
            if (userInfo.Password == string.Empty)
                messages.Add("Password cannot be empyt");
            if (userInfo.Password != userInfo.PasswordConfirm)
                messages.Add("Passwords do not match");

            if (messages.Count > 0)
                return View();
            
            // If the supplied information is correct then persist it to the database
            var user = new User();
            user.UserName = userInfo.UserName;
            user.FullName = userInfo.FullName;
            user.Password = userService.HashPassword(userInfo.Password);

            userService.Add(user);

            return View();

        }

        public JsonResult GetUsers()
        {
            var users = userService.GetAll();
            return Json(users.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}
