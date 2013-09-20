using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models;
using Cats.Services.Administration;
using Cats.Web.Adminstration.Models.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Web.Adminstration.Controllers
{
    public class UnitController : Controller
    {
        private IUnitService _unitService;

        public UnitController(IUnitService unitService)
        {
            _unitService = unitService;
        }
        // GET: /Unit/

        public ActionResult Index()
        {
            var unit = _unitService.GetAllUnit();
            return View(unit);
        }

        public ActionResult Unit_Read([DataSourceRequest] DataSourceRequest request)
        {

            var unit = _unitService.GetAllUnit();
            var unitToDisplay = Getunits(unit).ToList();
            return Json(unitToDisplay.ToDataSourceResult(request));
        }


        private IEnumerable<UnitViewModel> Getunits(IEnumerable<Unit> unit)
        {
            return (from units in unit
                    select new UnitViewModel()
                    {
                        UnitID = units.UnitID,
                        UnitName = units.Name
                    });
        }

        private Unit BindUnit(UnitViewModel model)
        {
            if (model == null) return null;
            var unit = new Unit()
                {
                    UnitID = model.UnitID,
                    Name = model.UnitName
                };
            return unit;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Unit_Create([DataSourceRequest] DataSourceRequest request, UnitViewModel unit)
        {
            if (unit != null && ModelState.IsValid)
            {


                _unitService.AddUnit(BindUnit(unit));
            }

            return Json(new[] { unit }.ToDataSourceResult(request, ModelState));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Unit_Update([DataSourceRequest] DataSourceRequest request, UnitViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var origin = _unitService.FindById(model.UnitID);
                origin.Name = model.UnitName;
                _unitService.EditUnit(origin);
            }
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Unit_Destroy([DataSourceRequest] DataSourceRequest request, UnitViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var unit = _unitService.FindById(model.UnitID);
                _unitService.DeleteUnit(unit);
            }
            return Json(ModelState.ToDataSourceResult());
        }

        public ActionResult Delete(int id)
        {
            var unit = _unitService.FindById(id);
            if (unit!=null)
            {
                _unitService.DeleteUnit(unit);
               return  RedirectToAction("Index");
            }
            return View();
        }
    }
}
