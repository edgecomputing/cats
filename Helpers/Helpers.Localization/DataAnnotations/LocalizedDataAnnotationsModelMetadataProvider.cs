using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LanguageHelpers.Localization.DataAnnotations
{
    public class LocalizedDataAnnotationsModelMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes,
 Type containerType,
 Func<Object> modelAccessor,
 Type modelType,
 String propertyName)
        {
            var metadata = base.CreateMetadata(
            attributes,
            containerType,
            modelAccessor,
            modelType,
            propertyName);
            //TODO:Comented out because of error  
            //metadata.DisplayName = Translator.Translate(metadata.GetDisplayName());
            //BekaMvcTests.Services.LocalizationTextService.Instance.Translate(metadata.GetDisplayName());
            return metadata;
        }
    }
}
