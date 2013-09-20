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
            //var localized = _localizedTextService.FindBy(m => m.LanguageCode == "AM").FirstOrDefault();
            //return View(localized);
            return View();
        }

        public ActionResult Create()
        {
            var localizedText = new LocalizedText();
            ViewBag.LanguageCode = new SelectList(_localizedTextService.GetAllLocalizedText(), "LanguageCode", "LanguageCode").Distinct().ToList();
            return View(localizedText);
        }

        [HttpPost]
        public ActionResult Create(LocalizedText localizedText)
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
            if (ModelState.IsValid)
            {
                _localizedTextService.UpdateLocalizedText(localizedText);
                return RedirectToAction("Index");
            }
            return View(localizedText);
        }

        public ActionResult Details()
        {
            var localized = _localizedTextService.FindBy(m => m.LanguageCode == "AM").FirstOrDefault();
            return View(localized);
        }


        public ActionResult EditTranslation(int id)
        {
            var localizedText = _localizedTextService.FindById(id);
            ViewBag.LanguageCode = new SelectList(_localizedTextService.GetAllLocalizedText(), "LocalizedTextId", "LanguageCode", localizedText.LocalizedTextId);
            return View(localizedText);
        }

        [HttpPost]
        public ActionResult EditTranslation(LocalizedText localizedText)
        {
            //localizedText.Value = amharicText;
            if (ModelState.IsValid)
            {
                _localizedTextService.UpdateLocalizedText(localizedText);
                return RedirectToAction("Details");
            }
            return View(localizedText);
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
                    // detail.value = translation.TranslatedText;
                    _localizedTextService.UpdateLocalizedText(detail);
                }
            }
            return Json(new[] { translation }.ToDataSourceResult(request, ModelState));
            //return Json(ModelState.ToDataSourceResult());
        }

        public ActionResult Translation_Read([DataSourceRequest] DataSourceRequest request)
        {
            //var hrdDetail = _hrdService.GetHRDDetailByHRDID(id).OrderBy(m => m.AdminUnit.AdminUnit2.Name).OrderBy(m => m.AdminUnit.AdminUnit2.AdminUnit2.Name);
            //var hrd = _hrdService.Get(m => m.HRDID == id, null, "HRDDetails").FirstOrDefault();
            //var language = _languageService.FindById(id);
            var localized = _localizedTextService.FindBy(m => m.LanguageCode == "AM");

            if (localized != null)
            {
                var detailsToDisplay = localized.ToList();
                return Json(detailsToDisplay.ToDataSourceResult(request));
            }
            return RedirectToAction("Index");
        }
    }
}