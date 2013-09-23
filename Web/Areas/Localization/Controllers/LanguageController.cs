using System;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using LanguageHelpers.Localization.Models;
using LanguageHelpers.Localization.Services;
using LanguageHelpers.Localization.Data;


namespace Cats.Areas.Localization.Controllers
{
    public class LanguageController : Controller
    {
        //
        // GET: /Localization/Language/
        private ILanguageService _languageService;
        //private ILocalizedTextService _localizedTextService;

        //public LanguageController() { }

        public LanguageController(ILanguageService languageService)
        {
            _languageService = languageService;
            //_localizedTextService = localizedTextService;
        }

        public ActionResult Index()
        {
             return View();
        }
        
        public ActionResult Language_Read([DataSourceRequest] DataSourceRequest request)
        {
            var language = _languageService.GetAllLanguage();
            var languageToDisplay = language;
           // return Json(languageToDisplay.ToDataSourceResult(request));

            //if (language != null)
            //{
                var detailsToDisplay = language.ToList();
                return Json(detailsToDisplay.ToDataSourceResult(request));
            //}
            //return RedirectToAction("Index");

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
                    //ModelState.AddModelError("Error", "Language Code Must Be Unique.");
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
        
    }
}