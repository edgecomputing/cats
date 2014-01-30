using Cats.Localization.Services;
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
        public static string Translate(this HtmlHelper html, string phrase, string language = "EN")
        {
            //TODO: By pass phrase translation to see the impact of localization module on performance
            return phrase;

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
            var service = (ILocalizedTextService)DependencyResolver.Current.GetService(typeof(ILocalizedTextService));

            return service.Translate(phrase, language);
        }

        public static string Translate2(this HtmlHelper html, string pageName, string phrase, string language = "EN")
        {
            var translations = new Dictionary<string, string>();
            var translatedPhrase = phrase;

            if (null != HttpContext.Current.Cache[pageName])
            {
                translations = (Dictionary<string, string>)HttpContext.Current.Cache[pageName];
            }
            else
            {
                translations = TranslatePage(pageName, language);
            }

            try
            {
                translatedPhrase = null != translations[phrase] ? translations[phrase] : phrase;
            }
            catch (Exception ex)
            {
                //Add the requested term to LocalizedText together with Language and Page

            }
            return translatedPhrase;

        }

        public static Dictionary<string, string> TranslatePage(this HtmlHelper html, string pageName, string language = "EN")
        {
            // Check to see if we already have translation text for the current page in a cache.
            var translations = new Dictionary<string, string>();

            // Return cached copy if we have one
            if (null != HttpContext.Current.Cache[pageName])
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
            translations = service.GetLocalizedTextDictionaryForPage(pageName, language);

            // Add the resulting dictionary to the cache to reduce trip to the database for consequent requests
            HttpContext.Current.Cache.Insert(pageName, translations);

            return translations;
        }

        private static Dictionary<string, string> TranslatePage(string pageName, string language = "EN")
        {
            // Check to see if we already have translation text for the current page in a cache.
            var translations = new Dictionary<string, string>();

            // Return cached copy if we have one
            if (null != HttpContext.Current.Cache[pageName])
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
            translations = service.GetLocalizedTextDictionaryForPage(pageName, language);

            // Add the resulting dictionary to the cache to reduce trip to the database for consequent requests
            HttpContext.Current.Cache.Insert(pageName, translations);

            return translations;
        }
    }
}