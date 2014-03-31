using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models.Security;
using Cats.Models.ViewModels;
using Cats.Services.Security;
using Cats.Helpers;

namespace Cats.Controllers
{
    public class UserPreferenceController : Controller
    {

        private readonly IUserAccountService _userAccountService;

        public UserPreferenceController(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        public ActionResult Edit(UserPreferenceEditModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userAccountService.GetUserDetail(HttpContext.User.Identity.Name);
                var users = UserAccountHelper.GetUser(HttpContext.User.Identity.Name);
                user.DefaultTheme = model.ThemePreference;
                user.DatePreference = model.DateFormatPreference;
                user.Keyboard = model.KeyboardLanguage;
                user.LanguageCode = model.Languages;
                user.PreferedWeightMeasurment = model.WeightPrefernce;

                // Edit user preference
                TempData["PreferenceUpdateSuccessMsg"] = "Success: General preference updated";
               _userAccountService.UpdateUser(user);
               
                var userInfo = new UserInfo
                                   {
                                       UserName = user.UserName,
                                       FirstName = user.FirstName,
                                       LastName = user.LastName,
                                       ActiveInd = user.ActiveInd,
                                       CaseTeam = user.CaseTeam,
                                       DatePreference = user.DatePreference,
                                       DefaultTheme = user.DefaultTheme,
                                       Email = user.Email,
                                       FailedAttempts = user.FailedAttempts,
                                       GrandFatherName = user.GrandFatherName,
                                       Keyboard = user.Keyboard,
                                       LanguageCode = user.LanguageCode,
                                       LockedInInd = user.LockedInInd,
                                       LogOutDate = user.LogOutDate,
                                       LogginDate = user.LogginDate,
                                       NumberOfLogins = user.NumberOfLogins,
                                       PreferedWeightMeasurment = user.PreferedWeightMeasurment
                                   };
                Session["USER_INFO"] = userInfo;
                
            }
            else
            {
                TempData["PreferenceUpdateErrorMsg"] = "Error: General preference not updated";
            }
            return RedirectToAction("Preference", "Home");
        }
    }
}