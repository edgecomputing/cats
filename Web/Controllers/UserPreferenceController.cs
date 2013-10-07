using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models.ViewModels;
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
                user.DefaultTheme = model.ThemePreference;
                user.DatePreference = model.DateFormatPreference;
                user.Keyboard = model.KeyboardLanguage;
                user.LanguageCode = model.Languages;
                user.PreferedWeightMeasurment = model.WeightPrefernce;

                // Edit user preference
                TempData["PreferenceUpdateSuccessMsg"] = "Success: General preference updated";
                
                userService.Save(user);
            }
            else
            {
                TempData["PreferenceUpdateErrorMsg"] = "Error: General preference not updated";
            }
            return RedirectToAction("Preference", "Home");
        }
    }
}
