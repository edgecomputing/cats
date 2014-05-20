using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LanguageHelpers.Localization;
using Cats.Services.Security;

namespace Cats.Filters
{
    public class LocalizationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Translator.CurrentLanguage = Translator.defaultLanguage ?? "en";
            try
            {
                UserIdentity user = (UserIdentity)HttpContext.Current.User.Identity;
                if (user != null)
                {
                    Translator.CurrentLanguage = user.Profile.LanguageCode;
                }
            }
            catch(Exception e){}
        }
    }
}