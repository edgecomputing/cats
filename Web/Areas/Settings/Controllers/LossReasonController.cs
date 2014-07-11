using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Settings.Models.ViewModels;
using Cats.Models;
using Cats.Services.Administration;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Areas.Settings.Controllers
{
    public class LossReasonController : Controller
    {
        //
        // GET: /Settings/LossReason/
        private readonly ILossReasonService _lossReasonService;

        public LossReasonController(ILossReasonService lossReasonService)
        {
            _lossReasonService = lossReasonService;

        }

        public ActionResult Index()
        {
            var lossReasons = _lossReasonService.GetAllLossReason();
            return View(lossReasons);
        }
        public ActionResult LossReason_Read([DataSourceRequest] DataSourceRequest request)
        {

            var lossReason = _lossReasonService.GetAllLossReason().OrderByDescending(m => m.LossReasonId);
            var lossToDisplay = GetLossReason(lossReason).ToList();
            return Json(lossToDisplay.ToDataSourceResult(request));
        }
        private IEnumerable<LossReasonModel> GetLossReason(IEnumerable<LossReason> lossReasons)
        {
            return (from lossReason in lossReasons
                    select new LossReasonModel()
                        {
                            LossReasonId = lossReason.LossReasonId,
                            LossReasonEg = lossReason.LossReasonEg,
                            LossReasonAm = lossReason.LossReasonAm,
                            LossReasonCodeAm = lossReason.LossReasonCodeAm,
                            LossReasonCodeEg = lossReason.LossReasonCodeEg,
                            Description = lossReason.Description

                        });

        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult LossReason_Create([DataSourceRequest] DataSourceRequest request, LossReasonModel lossReason)
        {
            if (lossReason != null && ModelState.IsValid)
            {
                _lossReasonService.AddLossReason(BindLossReason(lossReason));
            }

            return Json(new[] { lossReason }.ToDataSourceResult(request, ModelState));
        }
        private LossReason BindLossReason(LossReasonModel model)
        {
            if (model == null) return null;
            var lossReason = new LossReason()
            {
                LossReasonId = model.LossReasonId,
                LossReasonAm = model.LossReasonAm,
                LossReasonEg = model.LossReasonEg,
                LossReasonCodeAm = model.LossReasonCodeAm,
                LossReasonCodeEg = model.LossReasonCodeEg,
                Description = model.Description
            };
            return lossReason;
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult LossReason_Update([DataSourceRequest] DataSourceRequest request, LossReasonModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var origin = _lossReasonService.FindById(model.LossReasonId);
                origin.LossReasonEg = model.LossReasonEg;
                origin.LossReasonAm = model.LossReasonAm;
                origin.LossReasonCodeEg = model.LossReasonCodeEg;
                origin.LossReasonCodeAm = model.LossReasonCodeAm;
                origin.Description = model.Description;
                
                _lossReasonService.EditLossReason(origin);
            }
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

    }
}
