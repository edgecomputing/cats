using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Cats.Web.Administration.Models.ViewModels;
using Cats.Services.Security;



namespace Cats.Web.AdministrationControllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private IUserAccountService service;
        
        public AccountController(IUserAccountService userAccountService)
        {
            service = userAccountService;
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
                    // TODO: Review user permission code
                    //string[] authorization = service.GetUserPermissions(service.GetUserInfo(model.UserName).UserAccountId, "Administrator", "Manage User Account");                   
                    return RedirectToLocal(returnUrl);
                }
            }

            catch (Exception exception)
            {
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
            return RedirectToAction("Index","Home");
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
