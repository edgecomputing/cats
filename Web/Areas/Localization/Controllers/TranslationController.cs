using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using LanguageHelpers.Localization.Models;
using LanguageHelpers.Localization.Services;
using LanguageHelpers.Localization.Data;


namespace Cats.Areas.Localization.Controllers
{
    public class TranslationController : Controller
    {
        //
        // GET: /Localization/Translation/
        private ILocalizedTextService _localizedTextService;

        public TranslationController(ILocalizedTextService localizedTextService)
        {
            _localizedTextService = localizedTextService;
        }

        public ActionResult Localization_Read([DataSourceRequest] DataSourceRequest request)
        {
            //var loccalization = _localizedTextService.FindBy(m=>m.LanguageCode=="EN");
            var loccalization = _localizedTextService.GetAllLocalizedText();
            var localToDisplay = loccalization.ToList();
            return Json(localToDisplay.ToDataSourceResult(request));
        }
        public ActionResult Index()
        {
            var localization = _localizedTextService.GetAllLocalizedText();
            return View(localization);
        }

        public ActionResult Create()
        {
            var localizedText = new LocalizedText();
            ViewBag.LanguageCode = new SelectList(_localizedTextService.GetAllLocalizedText(), "LanguageCode", "LanguageCode").Distinct().ToList();
            return View(localizedText);
        }

         [HttpPost]
        public ActionResult Create (LocalizedText localizedText)
        {
            if (ModelState.IsValid)
            {
                _localizedTextService.AddLocalizedText(localizedText);
                return RedirectToAction("Index");
            }
            return View(localizedText);
        }

        public ActionResult Edit(int id)
        {
            var localizedText = _localizedTextService.FindById(id);
            ViewBag.LanguageCode = new SelectList(_localizedTextService.GetAllLocalizedText(), "LocalizedTextId", "LanguageCode", localizedText.LocalizedTextId);
          
            return View(localizedText);
        }
        [HttpPost]
        public ActionResult Edit(LocalizedText localizedText)
        {
            if(ModelState.IsValid)
            {
                _localizedTextService.UpdateLocalizedText(localizedText);
                return RedirectToAction("Index");
            }
            return View(localizedText);
        }

        public ActionResult Details()
        {
            //var language = _languageService.FindById(id);
            var localized = _localizedTextService.FindBy(m => m.LanguageCode == "AM").FirstOrDefault();
            return View(localized);
        }




    }
}
