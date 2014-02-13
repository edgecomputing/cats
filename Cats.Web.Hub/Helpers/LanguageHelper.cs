using System.Web.Mvc;
using Cats.Services.Hub;


namespace Cats.Web.Hub.Helpers
{
    public static class LanguageHelper
    {
        public static string Translate(this HtmlHelper helper, string text)
        {
            text = text.Trim();
           // IUnitOfWork repository = new UnitOfWork();

            var translationService = (ITranslationService)DependencyResolver.Current.GetService(typeof(ITranslationService));
            if (helper.GetCurrentUser() != null)
            {
                var langauge = helper.GetCurrentUser().LanguageCode;
                return translationService.GetForText(text, langauge);
            }
            return translationService.GetForText(text, "en");
        }

        public static string Translate(this string str)
        {
            return str;
        }
    }
}