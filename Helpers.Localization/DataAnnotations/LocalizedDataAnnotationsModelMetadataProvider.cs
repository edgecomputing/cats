using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using Helpers.Localization;
using LanguageHelpers.Localization;

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

            metadata.DisplayName = Translator.Translate(metadata.GetDisplayName());
            //BekaMvcTests.Services.LocalizationTextService.Instance.Translate(metadata.GetDisplayName());
            return metadata;
        }
    }
}
