using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Cats.Areas.Settings.Models;
using Cats.Models.Security;
using Cats.Models.ViewModels;
using Cats.Services.Security;
using log4net;
using log4net.Repository.Hierarchy;

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

        [AllowAnonymous]

        public ActionResult LogOn()
        {
            ViewBag.HasError = false;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogOn(LoginModel model, string returnUrl)
        {
            // Check if the supplied credentials are correct.
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
                //var log = new Logger();
                //log.LogAllErrorsMesseges(exception, _log);

                ViewBag.HasError = true;
                ViewBag.ErrorMessage = exception.ToString();

                ModelState.AddModelError("", exception.Message);
            }

            // If we got this far, something failed, redisplay form            
            return View(model);
        }

        [Authorize]

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
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

        public static string FormatRedirectUrl(string redirectUrl)
        {
            var c=System.Web.HttpContext.Current;
            //Don’t append the forms auth ticket for unauthenticated users 
            //   or
            //for users authenticated with a different mechanism
            if (!c.User.Identity.IsAuthenticated ||
                c.User.Identity.AuthenticationType != "Forms")
                return redirectUrl;
            //Determine if we need to append to an existing query-string or
            //  not
            string qsSpacer;

            if (redirectUrl.IndexOf('?') > 0)
                qsSpacer = "&";
            else
                qsSpacer = "?";
            //Build the new redirect URL. Assuming that currently using
            //forms authentication. Change the below FormsIdentity if required.
            //string newRedirectUrl;
            var fi = (FormsIdentity)c.User.Identity;
            var newRedirectUrl = redirectUrl + qsSpacer +
                                    FormsAuthentication.FormsCookieName + "=" +
                                    FormsAuthentication.Encrypt(fi.Ticket);
            return newRedirectUrl;
        }
    }
}
