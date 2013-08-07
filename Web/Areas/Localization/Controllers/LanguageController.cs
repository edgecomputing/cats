using System;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using LanguageHelpers.Localization.Models;
using LanguageHelpers.Localization.Services;
using LanguageHelpers.Localization.Data.UnitOfWork;

namespace Cats.Areas.Localization.Controllers
{
    public class LanguageController : Controller
    {
        //
        // GET: /Localization/Language/
        private ILanguageService _languageService;
        private ILocalizedTextService _localizedTextService;

        public LanguageController()
        {
            _languageService = new LanguageService(new UnitOfWork());
            _localizedTextService=new LocalizedTextService(new UnitOfWork());
        }

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Language_Read([DataSourceRequest] DataSourceRequest request)
        {

            var language = _languageService.GetAllLanguage();
            var languageToDisplay = language.ToList();
            return Json(languageToDisplay.ToDataSourceResult(request));
        }
        public ActionResult Create()
        {
            var language = new Language();
            return View(language);
        }

        [HttpPost]
        public ActionResult Create(Language language)
        {
            
            if(ModelState.IsValid)
            {
                try
                {
                    language.LanguageCode = language.LanguageCode.ToUpper();
                    _languageService.AddLanguage(language);
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ViewBag.Error = "Language Code Must Be Unique Please Try again";
                    
                    
                }
                return View(language);
                
            }
            return View(language);
        }
        public ActionResult Edit(int id)
        {
            var language = _languageService.FindById(id);
            return View(language);
        }
        [HttpPost]
        public ActionResult Edit(Language language)
        {
           if(ModelState.IsValid)
           {
               _languageService.EditLanguage(language);
               return RedirectToAction("Index");
           }
            return View(language);
        }
        public ActionResult Translation_Read([DataSourceRequest] DataSourceRequest request)
        {


            //var hrdDetail = _hrdService.GetHRDDetailByHRDID(id).OrderBy(m => m.AdminUnit.AdminUnit2.Name).OrderBy(m => m.AdminUnit.AdminUnit2.AdminUnit2.Name);
            //var hrd = _hrdService.Get(m => m.HRDID == id, null, "HRDDetails").FirstOrDefault();
            //var language = _languageService.FindById(id);
            var localized = _localizedTextService.FindBy(m => m.LanguageCode =="AM");

            if (localized != null)
            {
                var detailsToDisplay = localized.ToList();
                return Json(detailsToDisplay.ToDataSourceResult(request));
            }
            return RedirectToAction("Index");
        }
         public ActionResult EditTranslation(int id)
        {
            var localizedText = _localizedTextService.FindById(id);
            ViewBag.LanguageCode = new SelectList(_localizedTextService.GetAllLocalizedText(), "LocalizedTextId", "LanguageCode", localizedText.LocalizedTextId);
          
            return View(localizedText);
        }
        [HttpPost]
         public ActionResult EditTranslation(LocalizedText localizedText, string amharicText)
        {
            localizedText.Value = amharicText;
            if(ModelState.IsValid)
            {
                _localizedTextService.UpdateLocalizedText(localizedText);
                return RedirectToAction("Details");
            }
            return View(localizedText);
        }
        public ActionResult Details()
        {
            //var language = _languageService.FindById(id);
            var localized = _localizedTextService.FindBy(m => m.LanguageCode == "AM").FirstOrDefault();
            return View(localized);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Translation_Update([DataSourceRequest] DataSourceRequest request, TranslationViewModel translation)
        {
            if (translation != null && ModelState.IsValid)
            {
                var detail = _localizedTextService.FindById(translation.LocalizedTextId);
                if (detail != null)
                {
                    detail.LocalizedTextId = translation.LocalizedTextId;
                    detail.LanguageCode = translation.LanguageCode;
                    detail.TextKey = translation.TextKey;
                    detail.Value = translation.Value;
                    
                    _localizedTextService.UpdateLocalizedText(detail);
                }

            }
            return Json(new[] { translation }.ToDataSourceResult(request, ModelState));
            //return Json(ModelState.ToDataSourceResult());
        }

    }
}
