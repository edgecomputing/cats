using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LanguageHelpers.Localization.DataAnnotations
{
    
    public class Required_Attribute : System.ComponentModel.DataAnnotations.RequiredAttribute
    {

        private string _displayName;
        public Required_Attribute()
        {
            ErrorMessageResourceName = "Validation.Required";
        }
        protected override ValidationResult IsValid
        (object value, ValidationContext validationContext)
        {
            _displayName = validationContext.DisplayName;
            return base.IsValid(value, validationContext);
        }
        public override string FormatErrorMessage(string name)
        {
            var msg =  Translator.Translate(ErrorMessageResourceName);
           // var msg = LanguageService.Instance.Translate(ErrorMessageResourceName);
            return string.Format(msg, _displayName);
        }
    }

}
