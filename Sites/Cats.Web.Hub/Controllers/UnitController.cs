using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models.Hubs;
using Cats.Services.Hub;

namespace Cats.Web.Hub.Controllers
{
    [Authorize]
    public partial class UnitController : BaseController
    {

        private readonly IUnitService _unitService;

        public UnitController(IUnitService unitService, IUserProfileService userProfileService)
            : base(userProfileService)
        {
            this._unitService = unitService;
        }
        //
        // GET: /Unit/

        public virtual ViewResult Index()
        {
            var units = _unitService.GetAllUnit();
            return View(units);
        }

        public virtual ActionResult Update()
        {
            var units = _unitService.GetAllUnit();
            return PartialView(units);
        }

        //
        // GET: /Unit/Details/5

        public virtual ViewResult Details(int id)
        {
            Unit unit = _unitService.FindById(id);
            return View(unit);
        }

        //
        // GET: /Unit/Create

        public virtual ActionResult Create()
        {
            return PartialView();
        }

        //
        // POST: /Unit/Create

        [HttpPost]
        public virtual ActionResult Create(Unit unit)
        {
            if (ModelState.IsValid)
            {
                _unitService.AddUnit(unit);
                return Json(new { success = true });
            }

            return PartialView(unit);
        }

        //
        // GET: /Unit/Edit/5

        public virtual ActionResult Edit(int id)
        {
            Unit unit = _unitService.FindById(id);
            var units = _unitService.GetAllUnit();
            ViewBag.UnitID = new SelectList(units, "UnitID", "Name", unit.UnitID);
            return PartialView(unit);
        }

        //
        // POST: /Unit/Edit/5

        [HttpPost]
        public virtual ActionResult Edit(Unit unit)
        {
            if (ModelState.IsValid)
            {
                var origin = _unitService.FindById(unit.UnitID);
                origin.Name = unit.Name;

                _unitService.EditUnit(origin);


                //return RedirectToAction("Index");
                return Json(new { success = true });
            }

            ViewBag.UnitID = new SelectList(_unitService.GetAllUnit(), "UnitID", "Name", unit.UnitID);
            return PartialView(unit);
        }

        //
        // GET: /Unit/Delete/5

        public virtual ActionResult Delete(int id)
        {
            var unit = _unitService.FindById(id);
            return View(unit);
        }

        //
        // POST: /Unit/Delete/5

        [HttpPost, ActionName("Delete")]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            _unitService.DeleteById(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _unitService.Dispose();
            base.Dispose(disposing);
        }
    }
}