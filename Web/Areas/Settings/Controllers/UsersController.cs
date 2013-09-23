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
using Cats.Web.Hub.Infrastructure;


namespace Cats.Areas.Settings.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private IUserAccountService userService;
        private IForgetPasswordRequestService _forgetPasswordRequestService;
        private ISettingService _settingService;

        public UsersController(IUserAccountService service,IForgetPasswordRequestService forgetPasswordRequestService,ISettingService settingService)
        {
            userService = service;
            _forgetPasswordRequestService = forgetPasswordRequestService;
            _settingService = settingService;
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

            user.Disabled = false;
            user.LockedInInd = false;
            user.ActiveInd = true;

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

                        //changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                        changePasswordSucceeded = userService.ChangePassword(userid, model.NewPassword);
                    }
                    catch (Exception e)
                    {
                        changePasswordSucceeded = false;
                    }
                    if (changePasswordSucceeded)
                        ModelState.AddModelError("Success", "Password Successfully Changed.");
                        //return RedirectToAction("ChangePasswordSuccess");
                    else
                        ModelState.AddModelError("Errors","The new password is invalid.");

                }
                else ModelState.AddModelError("Errors","The current password is incorrect ");
            }
            return View(model);
        }
        public ActionResult ChangePasswordSuccess()
        {
            ModelState.AddModelError("Sucess", "Password Successfully Changed.");
            return View();
        }
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
                    var forgetPasswordRequest = new ForgetPasswordRequest()
                        {
                            Completed = false,
                            ExpieryDate = DateTime.Now.AddMonths(2),
                            GeneratedDate = DateTime.Now,

                            RequestKey = MD5Hashing.MD5Hash(Guid.NewGuid().ToString()),
                            UserAccountID = user.UserProfileID

                            //RequestKey = MD5Hashing.MD5Hash(Guid.NewGuid().ToString()),
                            //UserAccountID = user.UserAccountId
                       };
                    if (_forgetPasswordRequestService.AddForgetPasswordRequest(forgetPasswordRequest))
                    {

                        string to = user.Email;
                        string subject = "Password Change Request";
                        string link = "localhost"+ Request.Url.Port + "/Settings/Users/ForgetPassword/?key=" + forgetPasswordRequest.RequestKey;
                        string body = string.Format(@"Dear {1}
                                                            <br /><br />
                                                        A password reset request has been submitted for your Email account. If you submitted this password reset request, please follow the following link. 
                                                        <br /><br />
                                                        <a href='{0}'>Please Follow this Link</a> <br />
                                                        <br /><br />
                                                        Please ignore this message if the password request was not submitted by you. This request will expire in 24 hours.
                                                        <br /><br />
                                                        Thank you,<br />
                                                        Administrator.
                                                        ", link, user.UserName);
                        try
                        {
                            // Read the configuration table for smtp settings.

                            string from = _settingService.GetSettingValue("SMTP_EMAIL_FROM");
                            string smtp = _settingService.GetSettingValue("SMTP_ADDRESS");
                            int port = Convert.ToInt32(_settingService.GetSettingValue("SMTP_PORT"));
                            string userName = _settingService.GetSettingValue("SMTP_USER_NAME");
                            string password = _settingService.GetSettingValue("SMTP_PASSWORD");
                            // send the email using the utilty method in the shared dll.
                            Cats.Helpers.SendMail mail = new Helpers.SendMail(from, to, subject, body, null, true, smtp, userName, password, port);

                            ModelState.AddModelError("Sucess", "Email has Sent to your email Address.");
                            //return RedirectToAction("ConfirmPasswordChange");
                        }
                        catch (Exception e)
                        {
                            ViewBag.ErrorMessage = "The user name or email address you provided is not correct. Please try again.";
                        }
                    }

                    ModelState.AddModelError("Sucess", "Email has Sent to your email Address.");
                }
               // ModelState.AddModelError("Errors", "Invalid User Name "+ model.UserName);
            }
            return View();
        }

        public ActionResult ForgetPassword(string key)
        {
            ForgetPasswordRequest req = _forgetPasswordRequestService.GetValidRequest(key);
            if (req != null)
            {
                ForgotPasswordModel model = new Models.ForgotPasswordModel();
                model.UserAccountID = req.UserAccountID;
                return View(model);
            }
            return new EmptyResult();
        }
        [HttpPost]
        public ActionResult ForgetPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (userService.ChangePassword(model.UserAccountID, model.Password))
                {
                    _forgetPasswordRequestService.InvalidateRequest(model.UserAccountID);
                }

                return RedirectToAction("Index");
            }
            return View( model);
        }
    }
}
