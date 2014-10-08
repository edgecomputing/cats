using System;
using System.Linq;
using System.Web.Mvc;
using Cats.Helpers;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using LanguageHelpers.Localization.Models;
using LanguageHelpers.Localization.Services;
using LanguageHelpers.Localization.Data;
using log4net;


namespace Cats.Areas.Localization.Controllers
{
    public class LanguageController : Controller
    {
        //
        // GET: /Localization/Language/
        private ILanguageService _languageService;
        //private ILocalizedTextService _localizedTextService;
       // private  ILog _Log;
        //public LanguageController() { }

        public LanguageController(ILanguageService languageService)
        {
            _languageService = languageService;
            //_localizedTextService = localizedTextService;
            //_Log = log;
        }
        //public LanguageController() 
        //{ 

        //}

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
                catch (Exception )
                {
                    //var log = new Logger();
                    //log.LogAllErrorsMesseges(exception,_Log);
                    //ModelState.AddModelError("Errors", "Language Code Must Be Unique.");
                }
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