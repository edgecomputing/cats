using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Cats.Services.Hub;
using Cats.Services.Security;
using Cats.Web.Hub.Helpers;
using Cats.Web.Hub.Infrastructure;
using Cats.Models.Hubs;
using Cats.Models.Hubs.ViewModels;
using Cats.Web.Hub;

using IForgetPasswordRequestService = Cats.Services.Hub.IForgetPasswordRequestService;
using ISettingService = Cats.Services.Hub.ISettingService;
using log4net;
namespace Cats.Areas.Hub.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountController : BaseController
    {
        private IMembershipWrapper membership;
        //private new IUrlHelperWrapper Url;
        private IFormsAuthenticationWrapper authentication;
        private IUserProfileService _userProfileService;
        private IForgetPasswordRequestService _forgetPasswordRequestService;
        private ISettingService _settingService;
        private IUserAccountService _userAccountService;
        private readonly ILog _log;
        // GET: /Account/LogOn

       
        /// <summary>
        /// Initializes data that might not be available when the constructor is called.
        /// </summary>
        /// <param name="requestContext">The HTTP context and route data.</param>
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            //Url = new UrlHelperWrapper(base.Url);
            if (Url == null)
                Url = new UrlHelperWrapper(requestContext);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        //public AccountController()
        //    : this(new MembershipWrapper(), new FormsAuthenticationWrapper(), null)
        //{
           
        //}

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="membershipObject">The membership object.</param>
        /// <param name="formsAuthenticationObject">The forms authentication object.</param>
        /// <param name="urlHelper">The URL helper.</param>
        public AccountController(IMembershipWrapper membershipObject,
                                    IFormsAuthenticationWrapper formsAuthenticationObject,
                                    //IUrlHelperWrapper urlHelper,
                                    IUserProfileService userProfileService,
                                    IForgetPasswordRequestService forgetPasswordRequestService,
                                     ISettingService settingService,
            IUserAccountService userAccountService, ILog log)
            : base(userProfileService)
        {
            this.membership = membershipObject;
           // this.Url = urlHelper;
            this.authentication = formsAuthenticationObject;
            this._forgetPasswordRequestService = forgetPasswordRequestService;
            this._settingService = settingService;
            this._userProfileService = userProfileService;
            _userAccountService = userAccountService;
            _log = log;
        }

        /// <summary>
        /// Shows Logs the form.
        /// </summary>
        /// <returns></returns>
       
        public ActionResult LogOn()
        {
            CryptoGen x=new CryptoGen();
            var val = x.CreateKey(30);
            var dec = x.CreateKey(30);
            return View();
        }

        //
        // POST: /Account/LogOn

        /// <summary>
        /// Logs on to the system.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl="")
        {
            //if (ModelState.IsValid)
            //{
                
            //    if (Membership.ValidateUser(model.UserName, model.Password))
            //    {
            //        //FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
            //        authentication.SetAuthCookie(model.UserName, model.RememberMe);
            //        //TODO:Check if this could be made runable
            //        //if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
            //        //    && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
            //        //{
            //        //    return Redirect(returnUrl);
            //        //}
            //        //else
            //        //{
            //            return RedirectToAction("Index", "Home");
            //        //}
            //    }
            //    else
            //    {
            //        ModelState.AddModelError("", "The user name or password provided is incorrect.");
            //    }
            //}

            //// If we got this far, something failed, redisplay form
            //return View(model);
            try
            {
                if (_userAccountService.Authenticate(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);

                    // Will be refactored
                    Session["User"] = _userAccountService.GetUserDetail(model.UserName);
                    ////

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
                ViewBag.ErrorMessage = exception.ToString();

                ModelState.AddModelError("", exception.Message);
            }

            // If we got this far, something failed, redisplay form            
            return View(model);
        }

        //
        // GET: /Account/LogOff

        /// <summary>
        /// Logs off.
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        /// <summary>
        /// Registers this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return PartialView();
        }

        public ActionResult NotUnique(string UserName)
        {
            var user = _userProfileService.GetUser(UserName);
            return Json(user==null, JsonRequestBehavior.AllowGet);
        }
        //
        // POST: /Account/Register

        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            //Server side validation for uniqueness of the username given
            var userValid = _userProfileService.GetUser(model.UserName);
            if (userValid != null)
            {
                ModelState.AddModelError("UserName", "There is an existing User with the specified UserName");
            }

            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    UserProfile user =_userProfileService.GetUser(model.UserName);
                    if (user != null)
                    {
                        user.FirstName = model.FirstName;
                        user.LastName = model.LastName;
                        user.GrandFatherName = model.GrandFatherName;
                       _userProfileService.EditInfo(user);
                        return Json(new { success = true });
                    }

                    return Json(new { success = true });    
                   
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return PartialView(model);
        }

        //
        // GET: /Account/ChangePassword

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        /// <summary>
        /// Shows password success page
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion


        /// <summary>
        /// Shows the Forget password page for the first time.
        /// </summary>
        /// <returns></returns>
        public ActionResult ForgetPasswordRequest()
        {
           ForgotPasswordRequestModel model = new ForgotPasswordRequestModel();
            return View("ForgetPasswordRequest", model);
        }

        /// <summary>
        /// Processes the Forget password request.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ForgetPasswordRequest(ForgotPasswordRequestModel model)
        {
            if (ModelState.IsValid)
            {
               UserProfile profile = _userProfileService.GetUser(model.UserName);
                if (profile != null && !profile.LockedInInd)
                {
                   ForgetPasswordRequest req = new ForgetPasswordRequest()
                    {
                        Completed = false,
                        ExpieryDate = DateTime.Now.AddMonths(2),
                        GeneratedDate = DateTime.Now,
                        RequestKey = MD5Hashing.MD5Hash(Guid.NewGuid().ToString()),
                        UserProfileID = profile.UserProfileID
                    };
                    if (_forgetPasswordRequestService.AddForgetPasswordRequest(req))
                    {
                       
                        string to = profile.Email;
                        string subject = "Password Change Request";
                        string link = Request.Url.Host + "/Account/ForgetPassword/?key=" + req.RequestKey;
                        string body = string.Format(@"Dear {1}
                                                            <br /><br />
                                                        A password reset request has been submitted for your DRMFSS - CTS account. If you submitted this password reset request, please follow the following link. 
                                                        <br /><br />
                                                        <a href='{0}'>Please Follow this Link</a> <br />
                                                        <br /><br />
                                                        Please ignore this message if the password request was not submitted by you. This request will expire in 24 hours.
                                                        <br /><br />
                                                       Thank you,<br />
                                                       Administrator.
                                                        ", link, profile.GetFullName());
                        
                        try
                        {
                            // Read the configuration table for smtp settings.

                           string from = _settingService.GetSettingValue("SMTP_EMAIL_FROM");
                           string smtp = _settingService.GetSettingValue("SMTP_ADDRESS");
                           int port = Convert.ToInt32(_settingService.GetSettingValue("SMTP_PORT"));
                           string userName =_settingService.GetSettingValue("SMTP_USER_NAME");
                           string password = _settingService.GetSettingValue("SMTP_PASSWORD");
                            // send the email using the utilty method in the shared dll.
                          SendMail mail = new SendMail(from, to, subject, body, null, true, smtp,userName,password, port);
                           return RedirectToAction("ConfirmPasswordChange");
                        }
                        catch (Exception e)
                        {
                            ViewBag.ErrorMessage = "The user name or email address you provided is not correct. Please try again.";
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "The user name or email address you provided is not correct. Please try again.";
                    }

                }
                else
                {
                    ViewBag.ErrorMessage = "The user name or email address you provided is not correct. Please try again.";
                }
            }
            return View("ForgetPasswordRequest", model);
        }

        /// <summary>
        /// Confirms the password change.
        /// </summary>
        /// <returns></returns>
        public ActionResult ConfirmPasswordChange()
        {
            return View();
        }

        public ActionResult ForgetPassword(string key)
        {
           ForgetPasswordRequest req = _forgetPasswordRequestService.GetValidRequest(key);
            if (req != null)
            {
                ForgotPasswordModel model = new ForgotPasswordModel();
                model.UserProfilID = req.UserProfileID;
                return View("ForgetPassword", model);
            }
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult ForgetPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (_userProfileService.ChangePassword(model.UserProfilID, MD5Hashing.MD5Hash(model.Password)))
                {
                    _forgetPasswordRequestService.InvalidateRequest(model.UserProfilID);
                }

                return RedirectToAction("LogOn");
            }
            return View("ForgetPassword", model);
        }

        public ActionResult ISValidUserName(string userName)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                UserProfile user = _userProfileService.GetUser(userName);
                if (user != null)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(string.Format("The User name or Email address you provided doesnot exist. please try again."),
                        JsonRequestBehavior.AllowGet);
        }

         [HttpPost]
        public ActionResult ChangeUserSettings(UserPreferenceViewModel model)
         {
             // TODO: Do the saving here.
             var UserProfile = _userProfileService.GetUser(User.Identity.Name);
             // The User has to be attachable later. there has to be a better way of doing this than the current one.
             UserProfile = _userProfileService.FindById(UserProfile.UserProfileID);
             UserProfile.DatePreference = model.DateFormatPreference;
             UserProfile.DefaultTheme = model.ThemePreference;
             UserProfile.PreferedWeightMeasurment = model.WeightPrefernce;
             UserProfile.LanguageCode = model.Language;
             _userProfileService.EditUserProfile(UserProfile);

             return Redirect(this.Request.UrlReferrer.PathAndQuery);
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
    }
}
