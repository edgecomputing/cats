using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Cats.Models.ViewModels;
using Cats.Services.Security;
using log4net;
using Cats.Helpers;

namespace Cats.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private IUserAccountService service;
        private readonly ILog _log;


        public AccountController(IUserAccountService userAccountService,ILog log)
        {
            service = userAccountService;
            _log = log;
           
        }
        
        [AllowAnonymous]

        public ActionResult Login()
        {
            ViewBag.HasError = false;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            // Check if the supplied credentials are correct.
            try
            {
                if (service.Authenticate(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);

                    // Will be refactored
                    Session["User"] = service.GetUserDetail(model.UserName);
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

    }
}
