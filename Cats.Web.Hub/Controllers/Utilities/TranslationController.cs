using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models.Hub;
using Cats.Services.Hub;

namespace Cats.Web.Hub.Controllers.Utilities
{
    public class TranslationController : BaseController
    {
        //
        // GET: /Translation/

        private readonly ITranslationService _translationService;
        public TranslationController(ITranslationService translationService, IUserProfileService userProfileService)
            : base(userProfileService)
        {
            _translationService = translationService;
        }
        public ActionResult Index()
        {
           
            return View(_translationService.GetAll("am"));
        }

        public ActionResult Edit(int id)
        {
            return PartialView(_translationService.FindById(id));
        }

        [HttpPost]
        public ActionResult Save( Translation model)
        {
            Translation translation = _translationService.FindById(model.TranslationID);
            translation.Phrase = translation.Phrase.Trim();
            translation.TranslatedText = model.TranslatedText.Trim();
            _translationService.AddTranslation(translation);
            return RedirectToAction("Index");
        }
    }
}
