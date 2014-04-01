using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Cats.Areas.Settings.Models;
using Cats.Models.Security;
using Cats.Models.ViewModels;
using Cats.Services.Security;
using log4net;
using Cats.Helpers;
using NetSqlAzMan.Interfaces;
using NetSqlAzMan;
using System.Configuration;
using NetSqlAzMan.Cache;

namespace Cats.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private IUserAccountService _userAccountService;
        private readonly ILog _log;
        private IForgetPasswordRequestService _forgetPasswordRequestService;
        private ISettingService _settingService;


        public AccountController(IUserAccountService userAccountService, ILog log,
                                 IForgetPasswordRequestService forgetPasswordRequestService, ISettingService settingService)
        {
            _userAccountService = userAccountService;
            _log = log;
            _forgetPasswordRequestService = forgetPasswordRequestService;
            _settingService = settingService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.HasError = false;
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            // Check if the supplied credentials are correct.
            ViewBag.HasError = false;
            ViewBag.returnUrl = returnUrl;
            try
            {
                if (_userAccountService.Authenticate(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName,true);

                    // Will be refactored                              
                    var user = _userAccountService.GetUserDetail(model.UserName);
                    user.LogginDate = DateTime.Now;
                    user.NumberOfLogins += 1;
                    // Session["USER_PROFILE"] = user;
                    _userAccountService.UpdateUser(user);

                    // Add user information to session variable to avoid frequent trip to the databas
                    var service = (IUserAccountService)DependencyResolver.Current.GetService(typeof(IUserAccountService));
                    var userInfo = service.GetUserInfo(model.UserName);
                    Session["USER_INFO"] = userInfo;

                    // Before trying to go and look for user permissions, check if the user is logged in or not
                    
                    //// Load user permissions
                    IAzManStorage storage = new SqlAzManStorage(ConfigurationManager.ConnectionStrings["CatsContext"].ConnectionString);
                    IAzManDBUser dbUser = storage.GetDBUser(user.UserName);

                    // Early Warning user permissions
                    UserPermissionCache earlyWarningPermissionCache = new UserPermissionCache(storage, CatsGlobals.CATS, CatsGlobals.EARLY_WARNING, dbUser, true, false);
                    Session[CatsGlobals.EARLY_WARNING_PERMISSIONS] = earlyWarningPermissionCache;


                    //PSNP user permission
                    UserPermissionCache psnpPermissionCache = new UserPermissionCache(storage, CatsGlobals.CATS, CatsGlobals.PSNP, dbUser, true, false);
                    Session[CatsGlobals.PSNP_PERMISSIONS] = psnpPermissionCache;

                    // Logistics user permissions
                    UserPermissionCache logisticsPermissionCache = new UserPermissionCache(storage, CatsGlobals.CATS, CatsGlobals.LOGISTICS, dbUser, true, false);
                    Session[CatsGlobals.LOGISTICS_PERMISSIONS] = logisticsPermissionCache;

                    // Procurement user permissions
                    UserPermissionCache procurementPermissionCache = new UserPermissionCache(storage, CatsGlobals.CATS, CatsGlobals.PROCUREMENT, dbUser, true, false);
                    Session[CatsGlobals.PROCUREMENT_PERMISSIONS] = procurementPermissionCache;

                    // Hub user permissions
                    UserPermissionCache hubPermissionCache = new UserPermissionCache(storage, CatsGlobals.CATS, CatsGlobals.HUB, dbUser, true, false);
                    Session[CatsGlobals.HUB_PERMISSIONS] = hubPermissionCache;

                    // Regional user permissions
                    UserPermissionCache regionalPermissionCache = new UserPermissionCache(storage, CatsGlobals.CATS, CatsGlobals.REGION, dbUser, true, false);
                    Session[CatsGlobals.REGION_PERMISSIONS] = regionalPermissionCache;

                    // Whatever permission we are going to have!
                    _log.Info("Logged in User: " + user.UserName);
                    // TODO: Review user permission code
                    //string[] authorization = service.GetUserPermissions(service.GetUserInfo(model.UserName).UserAccountId, "Administrator", "Manage User Account");
                    //service.GetUserPermissions(model.UserName, "CATS", "Finance");
                    return RedirectToLocal(returnUrl);
                }
            }

            catch (Exception exception)
            {
                var log = new Logger();
                log.LogAllErrorsMesseges(exception, _log);
               
                ViewBag.HasError = true;
                ViewBag.Error = exception.ToString();
                ViewBag.ErrorMessage = "Login failed. Try logging in with the right user name and password.";

                ModelState.AddModelError("", exception.Message);
            }
            //ViewBag.HasError = false;
            // If we got this far, something failed, redisplay form            
            return View();
        }

        [Authorize]

        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            _log.Info("Logged out User: " + this.User.Identity.Name);
            return RedirectToAction("Index", "Home");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
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
            if (ModelState.IsValid)
            {

                var user = _userAccountService.GetUserDetail(model.UserName);
                if (user != null)
                {

                    var forgetPasswordRequest = new ForgetPasswordRequest()
                    {
                        Completed = false,
                        ExpieryDate = DateTime.Now.AddMonths(2),
                        GeneratedDate = DateTime.Now,

                        RequestKey = MD5Hashing.MD5Hash(Guid.NewGuid().ToString()),
                        UserProfileID = user.UserProfileID

                        //RequestKey = MD5Hashing.MD5Hash(Guid.NewGuid().ToString()),
                        //UserAccountID = user.UserAccountId
                    };
                    if (_forgetPasswordRequestService.AddForgetPasswordRequest(forgetPasswordRequest))
                    {

                        string to = user.Email;
                        string subject = "Password Change Request";
                        string link = "localhost:" + Request.Url.Port + "/Account/ForgetPassword/?key=" + forgetPasswordRequest.RequestKey;
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

                // ModelState.AddModelError("Errors", "Invalid User Name " + model.UserName);
            }
            return View();
        }
        public ActionResult ISValidUserName(string userName)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                var user = _userAccountService.GetUserDetail(userName);
                if (user != null)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(string.Format("The User name or Email address you provided doesnot exist. please try again."),
                        JsonRequestBehavior.AllowGet);
        }
        public ActionResult ForgetPassword(string key)
        {
            ForgetPasswordRequest req = _forgetPasswordRequestService.GetValidRequest(key);
            if (req != null)
            {
                ForgotPasswordModel model = new ForgotPasswordModel();
                model.UserAccountID = req.UserProfileID;
                return View(model);
            }
            return new EmptyResult();
        }
        [HttpPost]
        public ActionResult ForgetPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (_userAccountService.ChangePassword(model.UserAccountID, model.Password))
                {
                    _forgetPasswordRequestService.InvalidateRequest(model.UserAccountID);
                }

                return RedirectToAction("Login");
            }
            return View(model);
        }


        public ActionResult RedirectToHub()
        {
            return Redirect("/hub");
        }
        public ActionResult Administration()
        {
            return Redirect("/home");
        }
    }
}
