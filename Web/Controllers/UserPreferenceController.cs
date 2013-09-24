using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Settings.Models;
using Cats.Services.Security;

namespace Cats.Controllers
{
    public class UserPreferenceController : Controller
    {

        private readonly IUserAccountService userService;

        public UserPreferenceController(IUserAccountService _userService)
        {
            this.userService = _userService;
        }

        public ActionResult Edit(UserPreferenceEditModel model)
        {
            if (ModelState.IsValid)
            {
                var user = userService.GetUserDetail(HttpContext.User.Identity.Name);
                user.DefaultTheme = model.DefaultTheme;
                user.DatePreference = model.DatePreference;
                user.Keyboard = model.KeyboardLanguage;
                user.LanguageCode = model.Language;
                user.PreferedWeightMeasurment = model.PreferedWeightMeasurement;

                // Edit user preference
                ModelState.AddModelError("Success", "Preference Updated");
                userService.Save(user);
            }
            return RedirectToAction("Preference", "Home");
        }
    }
}
