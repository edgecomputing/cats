using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Cats.Areas.EarlyWarning.Models;
using Cats.Services.EarlyWarning;
using Cats.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Areas.EarlyWarning.Controllers
{
    public class CurrencyController : Controller
    {
        //
        // GET: /EarlyWarning/Currency/
        private ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }
        public ActionResult Index()
        {
            var currency = _currencyService.GetAllCurrency();
            return View(currency);
        }
        public ActionResult Currency_Read([DataSourceRequest] DataSourceRequest request)
        {

            var currency = _currencyService.GetAllCurrency();
            var curencyToDisplay = GetCurrency(currency).ToList();
            return Json(curencyToDisplay.ToDataSourceResult(request));
        }


        private IEnumerable<CurrencyViewModel> GetCurrency(IEnumerable<Currency> Currency)
        {
            return (from currencies in Currency
                    select new CurrencyViewModel()
                    {
                        CurrencyID = currencies.CurrencyID,
                        CurrencyCode = currencies.Code,
                        CurrencyName = currencies.Name
                       
                    });
        }
        public ActionResult Create()
        {
            var currency = new Currency();
            return View(currency);
        }

        [HttpPost]
        public ActionResult Create(Currency currency)
        {
            if(ModelState.IsValid)
            {
                _currencyService.AddCurrency(currency);
                return RedirectToAction("Index");
            }
            return View(currency);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Currency_Create([DataSourceRequest] DataSourceRequest request, CurrencyViewModel currency)
        {
            if (currency != null && ModelState.IsValid)
            {


                _currencyService.AddCurrency(BindCurrency(currency));
            }

            return Json(new[] { currency }.ToDataSourceResult(request, ModelState));
        }

        private Currency BindCurrency(CurrencyViewModel currencyViewModel)
        {
            if (currencyViewModel == null) return null;
            var currency = new Currency()
            {
                CurrencyID = currencyViewModel.CurrencyID,
                Code = currencyViewModel.CurrencyCode,
                //CommodityID = contributionDetailViewModel.CommodityID,
                Name = currencyViewModel.CurrencyName
            };
            return currency;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Currency_Update([DataSourceRequest] DataSourceRequest request, CurrencyViewModel currencyViewModel)
        {
            if (currencyViewModel != null && ModelState.IsValid)
            {
                var origin = _currencyService.FindById(currencyViewModel.CurrencyID);
                origin.Code = currencyViewModel.CurrencyCode;
                origin.Name = currencyViewModel.CurrencyName;
                _currencyService.EditCurrency(origin);
            }
            return Json(new[] { currencyViewModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Currency_Destroy([DataSourceRequest] DataSourceRequest request, CurrencyViewModel currencyViewModel)
        {
            if (currencyViewModel != null && ModelState.IsValid)
            {
                var currency = _currencyService.FindById(currencyViewModel.CurrencyID);
                _currencyService.DeleteCurrency(currency);
            }
            return Json(ModelState.ToDataSourceResult());
        }
        public ActionResult Delete(int id)
        {
            var currency = _currencyService.FindById(id);
            if(currency!=null)
            {
                _currencyService.DeleteCurrency(currency);
                return RedirectToAction("Index");
            }
          return RedirectToAction("Index");
        }
    }
}
