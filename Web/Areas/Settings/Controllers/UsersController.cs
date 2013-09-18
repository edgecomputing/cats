﻿using System;
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
            return Json(users.ToDataSourceResult(request),JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult New()
        {
            var model = new UserViewModel();
            List<Cats.Models.Security.ViewModels.Application> Applications = userService.GetApplications("CATS");

            model.Applications = Applications;
            return View(model);
        }



        [HttpPost]
        public ActionResult New(UserViewModel userInfo)
        {
            var messages = new List<string>();

            // Check business rule and validations
            if (userInfo.UserName == string.Empty)
                messages.Add("User name cannot be empty");
            if (userInfo.FullName == string.Empty)
                messages.Add("Full name cannot be empty");
            if (userInfo.Password == string.Empty)
                messages.Add("Password cannot be empty");
            if (userInfo.Password != userInfo.PasswordConfirm)
                messages.Add("Passwords do not match");
           

            if (messages.Count > 0)
                return View();
            
            // If the supplied information is correct then persist it to the database
            var user = new UserAccount();

            user.UserName = userInfo.UserName;                        
            user.Password = userService.HashPassword(userInfo.Password);

            user.Disabled = false;
            user.LoggedIn = false;

            List<Cats.Models.Security.ViewModels.Application> app = userInfo.Applications;
            Dictionary<string, List<string>> roles = new Dictionary<string, List<string>>();
            List<string> Roles;
           foreach(var application in app)
           {
               Roles = new List<string>();
               foreach (var role in application.Roles)
               {
                   if (role.IsChecked)
                       Roles.Add(role.RoleName);
               }
               if (Roles.Count > 0)
                   roles.Add(application.ApplicationName, Roles);
           }

           user.UserProfile.FirstName = "";
           user.UserPreference.LanguageCode = "EN";
           user.UserPreference.Keyboard = "AM";
           user.UserPreference.PreferedWeightMeasurment = "MT";
           user.UserPreference.Calendar = "GC";
           user.UserPreference.DefaultTheme = "Default";

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
            Dictionary<string, List<Role>> roles = new Dictionary<string, List<Role>>();
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

            var user = new UserAccount();

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
            var userid = UserAccountHelper.GetUser(HttpContext.User.Identity.Name).UserAccountId;
            var oldpassword = userService.HashPassword(model.OldPassword);
            if (ModelState.IsValid)
            {
                bool changePasswordSucceeded;

                if (userService.GetUserDetail(userid).Password == oldpassword)
                {
                    try
                    {

                        //changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                        changePasswordSucceeded = userService.ChangePassword(userid, model.NewPassword);
                    }
                    catch (Exception)
                    {
                        changePasswordSucceeded = false;
                    }
                    if (changePasswordSucceeded)
                        ModelState.AddModelError("Sucess", "Password Successfully Changed.");
                        //return RedirectToAction("ChangePasswordSuccess");
                    else
                        ModelState.AddModelError("Errors","The new password is invalid.");

                }
                else ModelState.AddModelError("Errors","The current password is incorrect ");
            }
            return View(model);
        }
        //public ActionResult ChangePasswordSuccess()
        //{
        //    return View();
        //}
        public ActionResult ISValidUserName(string userName)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                var user = userService.GetUserDetail(userName);
                if (user != null)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(string.Format("The User name or Email address you provided doesnot exist. please try again."),
                        JsonRequestBehavior.AllowGet);
        }
        public ActionResult ForgetPasswordRequest()
        {
            //var UserName = UserAccountHelper.GetUser(HttpContext.User.Identity.Name).UserName;
           // userService.ResetPassword(UserName);
            var model = new ForgetPasswordRequestModel();
            return View(model);
           // return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult ForgetPasswordRequest(ForgetPasswordRequestModel model)
        {
            if(ModelState.IsValid)
            {
                var user = userService.GetUserDetail(model.UserName);
                if(user!=null)
                {
                    userService.ResetPassword(user.UserName);

                }
                ModelState.AddModelError("Errors", "Invalid User Name "+ model.UserName);
            }
            return View();
        }
    }
}
