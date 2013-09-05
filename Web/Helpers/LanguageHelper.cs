using Cats.Services.Security;
using LanguageHelpers.Localization.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cats.Helpers
{
    public static class LanguageHelper
    {
        public static string Translate(this HtmlHelper html,string phrase, string language="EN")
        {
            var currentLanguage = language;

            // Get current language setting for the user.
            // NOTE: Since we might call this method from public views where we might not have a signed-in
            //       user, we must check for possible errors.
            try
            {
                var user = (UserIdentity)HttpContext.Current.User.Identity;                
                currentLanguage = user.Profile.LanguageCode;
            }
            catch (Exception)
            {
                currentLanguage = language;
            }

            // If the current language is 'English' then return the default value (the passed value)            
            if (currentLanguage == "EN")
                return phrase;

            // For other languages try to get the corresponding translation
            var service = DependencyResolver.Current.GetService<ILocalizedTextService>();
            return service.Translate(phrase, language);
        }

    }
}