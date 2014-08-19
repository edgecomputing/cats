using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Services.Administration;
using Cats.Web.Adminstration.Models.ViewModels;
using Cats.Web.Adminstration.ViewModelBinder;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Web.Adminstration.Controllers
{
     [Authorize]
    public class CommodityGradeController : Controller
    {
        private readonly ICommodityGradeService _commodityGradeService;
        //
        // GET: /CommodityGrade/
        public CommodityGradeController(ICommodityGradeService commodityGradeService)
        {
            _commodityGradeService = commodityGradeService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CommodityGrade_Read([DataSourceRequest]DataSourceRequest request)
        {
            var commodityGrades = _commodityGradeService.GetAllCommodityGrade();
            var commodityGradesViewModel = CommodityGradeViewModelBinder.BindListCommodityGradeViewModel(commodityGrades);
            return Json(commodityGradesViewModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CommodityGrade_Create([DataSourceRequest] DataSourceRequest request,
                                                  CommodityGradeViewModel commodityGradeViewModel)
        {
            if (commodityGradeViewModel != null && ModelState.IsValid)
            {
                var commodityGrade = CommodityGradeViewModelBinder.BindCommodityGrade(commodityGradeViewModel);
                _commodityGradeService.AddCommodityGrade(commodityGrade);
            }
            return Json(new[] {commodityGradeViewModel}.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CommodityGrade_Update([DataSourceRequest] DataSourceRequest request,
                                                  CommodityGradeViewModel commodityGradeViewModel)
        {
            if (commodityGradeViewModel != null && ModelState.IsValid)
            {
                var target = _commodityGradeService.FindById(commodityGradeViewModel.CommodityGradeID);
                var commodityGrade = CommodityGradeViewModelBinder.BindCommodityGrade(commodityGradeViewModel, target);
                _commodityGradeService.EditCommodityGrade(commodityGrade);
            }

            return Json(new[] {commodityGradeViewModel}.ToDataSourceResult(request, ModelState));
        }


        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult CommodityGrade_Destroy([DataSourceRequest] DataSourceRequest request,
        //                                           CommodityGradeViewModel commodityGradeViewModel)
        //{
        //    if (commodityGradeViewModel != null)
        //    {
        //        _commodityGradeService.DeleteById(commodityGradeViewModel.CommodityGradeID);
        //    }

        //    return Json(ModelState.ToDataSourceResult());
        //}


        public ActionResult CommodityGrade_Destroy(int id)
        {
            var commodityGrade = _commodityGradeService.FindById(id);
            try
            {
                _commodityGradeService.DeleteCommodityGrade(commodityGrade);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Errors", @"Unable to delete CommodityGrade");
            }
            return RedirectToAction("Index");
        }

    }

}