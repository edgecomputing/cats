using Cats.Localization.Services;
using Cats.Localization.Exceptions;
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
        const string LOCALIZATION_TEXT = "LOCALIZATION_TEXT";

        public static string Translate(this HtmlHelper html, string phrase, string language = "EN")
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
            if (System.String.Compare(currentLanguage, "EN", System.StringComparison.OrdinalIgnoreCase)==0)
                return phrase;

            // Check if we already have a session variable to hold list of translations terms
            if (HttpContext.Current.Session != null && HttpContext.Current.Session[LOCALIZATION_TEXT] != null)
            {

            }

            // For other languages try to get the corresponding translation
            var service = (ILocalizedTextService)DependencyResolver.Current.GetService(typeof(ILocalizedTextService));

            return service.Translate(phrase, currentLanguage);
        }

        public static string Translate2(this HtmlHelper html, string pageName, string phrase, string language = "EN")
        {
            var translations = new Dictionary<string, string>();
            var translatedPhrase = phrase;

            if (null != HttpContext.Current.Session[pageName])
            {
                translations = (Dictionary<string, string>)HttpContext.Current.Session[pageName];
            }
            else
            {
                translations = TranslatePage(pageName, language);
            }

            try
            {
                translatedPhrase = translations.ContainsKey(phrase) ? translations[phrase] : phrase;
            }
            catch (Exception ex)
            {
                // return the incoming text in the default language
                translatedPhrase = phrase;
                //TODO: log exception                                
            }
            return translatedPhrase;

        }

        public static Dictionary<string, string> TranslatePage(this HtmlHelper html, string pageName, string language = "EN")
        {
            return TranslatePage(pageName, language);
        }

        private static Dictionary<string, string> TranslatePage(string pageName, string language = "EN")
        {
            // Check to see if we already have translation text for the current page in a cache.
            var translations = new Dictionary<string, string>();

            // Return cached copy if we have one
            if (null != HttpContext.Current.Session[pageName])
            {
                return (Dictionary<string, string>)translations;
            }

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

            // Get a reference to an instance of LocalizationService
            var service = (ILocalizationService)DependencyResolver.Current.GetService(typeof(ILocalizationService));

            try
            {
                translations = service.GetLocalizedTextDictionaryForPage(pageName, language);
            }
            catch (PageNotFoundException pnfe)
            {
                // If the requested page is not found in the database then add associated string requstes for this page
                // to the database with the user's preference language
                // TODO: Add code to add text requests for this page.
            }

            catch (Exception)
            {

            }

            // Add the resulting dictionary to the cache to avoid trip to the database for consequent requests
            HttpContext.Current.Session.Add(pageName, translations);

            return translations;
        }
    }
}