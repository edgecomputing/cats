using System;
using System.Collections.Generic;
using System.Web.Security;
using Cats.Helpers;
using Cats.Models.Security;
using Cats.Services.Security;
using System.Linq;
using System.Web.Mvc;
using Cats.Areas.Settings.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Cats.Models.Security.ViewModels;
using Cats.Helpers;

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

        public ActionResult UsersList([DataSourceRequest] DataSourceRequest request)
        {
            var users = userService.GetUsers();
            return Json(users.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult New()
        {
            var model = new UserViewModel();
            //List<Cats.Models.Security.ViewModels.Application> Applications = userService.GetApplications("CATS");

            //model.Applications = Applications;
            return View(model);
        }



        [HttpPost]
        public ActionResult New(UserViewModel userInfo)
        {
            var messages = new List<string>();

            // Check business rule and validations
            if (userInfo.UserName == string.Empty)
                messages.Add("User name cannot be empty");
            if (userInfo.FirstName == string.Empty)
                messages.Add("First name cannot be empty");
            if (userInfo.LastName == string.Empty)
                messages.Add("Last Name cannot be empty");
            if (userInfo.Password == string.Empty)
                messages.Add("Password cannot be empty");
            if (userInfo.Password != userInfo.PasswordConfirm)
                messages.Add("Passwords do not match");


            if (messages.Count > 0)
                return View();

            // If the supplied information is correct then persist it to the database
            var user = new UserProfile();

            user.UserName = userInfo.UserName;
            user.Password = userService.HashPassword(userInfo.Password);

            // Set default values for required fields
            user.Disabled = false;
            user.LockedInInd = false;
            user.ActiveInd = true;


            //List<Cats.Models.Security.ViewModels.Application> app = userInfo.Applications;
            Dictionary<string, List<string>> roles = new Dictionary<string, List<string>>();
            //List<string> Roles;
            //foreach (var application in app)
            //{
            //    Roles = new List<string>();
            //    foreach (var role in application.Roles)
            //    {
            //        if (role.IsChecked)
            //            Roles.Add(role.RoleName);
            //    }
            //    if (Roles.Count > 0)
            //        roles.Add(application.ApplicationName, Roles);
            //}

            user.FirstName = userInfo.FirstName;
            user.LastName = userInfo.LastName;

            user.LanguageCode = "EN";
            user.Keyboard = "AM";
            user.PreferedWeightMeasurment = "MT";
            user.DatePreference = "GC";
            user.DefaultTheme = "Default";
            user.FailedAttempts = 0;
            user.LoggedInInd = false;

            userService.Add(user, roles);


            return View();

        }

        public ActionResult UserProfile(int id)
        {
            var user = userService.GetUserInfo(id);
            return View(user);
        }

        public JsonResult GetUsers()
        {
            var users = userService.GetAll();
            return Json(users.ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EditUserRoles(string UserName)
        {
            var model = new UserViewModel();
            model.UserName = UserName;
            List<Cats.Models.Security.ViewModels.Application> Applications = userService.GetUserPermissions(UserName);

            model.Applications = Applications;
            return View(model);
        }

        [HttpPost]
        public ActionResult EditUserRoles(UserViewModel userInfo)
        {
            //// 

            List<Cats.Models.Security.ViewModels.Application> app = userInfo.Applications;
            var roles = new Dictionary<string, List<Role>>();
            var Roles = new List<Role>();
            foreach (var application in app)
            {
                foreach (var role in application.Roles)
                {
                    if (role.IsChecked)
                        Roles = new List<Role>() { new Role() { RoleName = role.RoleName } };
                }
                if (Roles.Count > 0)
                    roles.Add(application.ApplicationName, Roles);
            }

            var user = new UserProfile();

            user.UserName = userInfo.UserName;
            userService.EditUserRole(userInfo.UserName, userInfo.UserName, roles);

            return View();
        }
        public ActionResult ChangePassword()
        {
            //var userInfo=userService.FindById(id);
            var model = new ChangePasswordModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            var userid = UserAccountHelper.GetUser(HttpContext.User.Identity.Name).UserProfileID;
            var oldpassword = userService.HashPassword(model.OldPassword);
            if (ModelState.IsValid)
            {
                bool changePasswordSucceeded;

                if (userService.GetUserDetail(userid).Password == oldpassword)
                {
                    try
                    {
                        changePasswordSucceeded = userService.ChangePassword(userid, model.NewPassword);
                    }
                    catch (Exception e)
                    {
                        changePasswordSucceeded = false;
                        //ModelState.AddModelError("Errors", e.Message);
                    }
                    if (changePasswordSucceeded)
                        ModelState.AddModelError("Success", "Password Successfully Changed.");
                    //return RedirectToAction("ChangePasswordSuccess");
                    else
                        ModelState.AddModelError("Errors", "The new password is invalid.");

                }
                else ModelState.AddModelError("Errors", "The current password is incorrect ");
            }
            return View(model);
        }
        //public ActionResult ChangePasswordSuccess()
        //{
        //    ModelState.AddModelError("Sucess", "Password Successfully Changed.");
        //    return View();
        //}
       
    }
}
