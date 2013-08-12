using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models;
using Cats.Models.PSNP;
using Cats.Data;
using Cats.Services.PSNP;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Areas.PSNP
{
    public class RegionalPSNPPlanController : Controller
    {
        private readonly IRegionalPSNPPlanService _regionalPSNPPlanService;

        public RegionalPSNPPlanController(IRegionalPSNPPlanService regionalPSNPPlanServiceParam)
        {
            this._regionalPSNPPlanService = regionalPSNPPlanServiceParam;
        }

        public IEnumerable<RegionalPSNPPlanViewModel> toViewModel(IEnumerable<Cats.Models.RegionalPSNPPlan> list)
        {
            return (from plan in list
                    select new RegionalPSNPPlanViewModel
                    {
                        RegionalPSNPPlanID = plan.RegionalPSNPPlanID,
                        Duration = plan.Duration,
                        RegionID = plan.RegionID,
                        Year = plan.Year,
                        RegionName = plan.Region.Name

                    });
        }
        //
        // GET: /PSNP/RegionalPSNPPlan/

        public ActionResult Index()
        {
            IEnumerable<Cats.Models.RegionalPSNPPlan> list = (IEnumerable<Cats.Models.RegionalPSNPPlan>)_regionalPSNPPlanService.GetAllRegionalPSNPPlan();

            return View(toViewModel(list));

        }
        public ActionResult GetListAjax([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<Cats.Models.RegionalPSNPPlan> list = (IEnumerable<Cats.Models.RegionalPSNPPlan>)_regionalPSNPPlanService.GetAllRegionalPSNPPlan();
            return Json(toViewModel(list).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /PSNP/RegionalPSNPPlan/Details/5

        public ActionResult Details(int id = 0)
        {
            RegionalPSNPPlan regionalpsnpplan = _regionalPSNPPlanService.FindById(id);
            if (regionalpsnpplan == null)
            {
                return HttpNotFound();
            }
            return View(regionalpsnpplan);
        }

        //
        // GET: /PSNP/RegionalPSNPPlan/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /PSNP/RegionalPSNPPlan/Create

        [HttpPost]
        public ActionResult Create(RegionalPSNPPlan regionalpsnpplan)
        {
            if (ModelState.IsValid)
            {
                _regionalPSNPPlanService.AddRegionalPSNPPlan(regionalpsnpplan);

                return RedirectToAction("Index");
            }

            return View(regionalpsnpplan);
        }

        //
        // GET: /PSNP/RegionalPSNPPlan/Edit/5

        public ActionResult Edit(int id = 0)
        {
            RegionalPSNPPlan regionalpsnpplan = _regionalPSNPPlanService.FindById(id);
            if (regionalpsnpplan == null)
            {
                return HttpNotFound();
            }
            return View(regionalpsnpplan);
        }

        //
        // POST: /PSNP/RegionalPSNPPlan/Edit/5

        [HttpPost]
        public ActionResult Edit(RegionalPSNPPlan regionalpsnpplan)
        {
            if (ModelState.IsValid)
            {
                _regionalPSNPPlanService.UpdateRegionalPSNPPlan(regionalpsnpplan);
                return RedirectToAction("Index");
            }
            return View(regionalpsnpplan);
        }

        //
        // GET: /PSNP/RegionalPSNPPlan/Delete/5

        public ActionResult Delete(int id = 0)
        {
            RegionalPSNPPlan regionalpsnpplan = _regionalPSNPPlanService.FindById(id);
            if (regionalpsnpplan == null)
            {
                return HttpNotFound();
            }
            return View(regionalpsnpplan);
        }

        //
        // POST: /PSNP/RegionalPSNPPlan/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _regionalPSNPPlanService.DeleteById(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {

            base.Dispose(disposing);
        }
    }
}