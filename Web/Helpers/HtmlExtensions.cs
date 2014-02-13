using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cats.Helpers
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString CustomValidationSummary(this HtmlHelper<dynamic> helper)
        {            
            var html = string.Empty;
            foreach (var item in helper.ViewData.ModelState)
            {                
                if (item.Value.Errors.Any())
                {                    
                    foreach (ModelError e in item.Value.Errors)
                    {
                        if (item.Key == "Errors")
                        {
                            html
                                += "<div class='cats_error'>"
                                + LanguageHelpers.Localization.Translator.Translate(e.ErrorMessage)
                                + "</div>";
                        }
                        else if (item.Key == "Warning")
                        {
                            html
                                += "<div class='cats_warning'>"
                                + LanguageHelpers.Localization.Translator.Translate(e.ErrorMessage)
                                + "</div>";
                        }
                        else if (item.Key == "Info")
                        {
                            html
                                += "<div class='cats_info'>"
                                + LanguageHelpers.Localization.Translator.Translate(e.ErrorMessage)
                                + "</div>";
                        }
                        else if (item.Key == "Success")
                        {
                            html
                                += "<div class='cats_success'>"
                                + LanguageHelpers.Localization.Translator.Translate(e.ErrorMessage)
                                + "</div>";
                        }
                    }                    
                }     
            }
            return MvcHtmlString.Create(html);
        }

    }
}

