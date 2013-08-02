using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models;
using Cats.Data;
using Cats.Services.PSNP;
using Cats.Services.EarlyWarning;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Areas.PSNP.Controllers
{
    public class RegionalPSNPPlanDetailController : Controller
    {
        private readonly IRegionalPSNPPlanDetailService _regionalPSNPPlanDetailService;
        private readonly IRegionalPSNPPlanService _regionalPSNPPlanService;
        private readonly IFDPService _FDPService;

        public RegionalPSNPPlanDetailController(
                            IRegionalPSNPPlanDetailService regionalPSNPPlanDetailServiceParam,
                            IRegionalPSNPPlanService regionalPSNPPlanServiceParam,
                            IFDPService FDPServiceParam)
        {
            this._regionalPSNPPlanDetailService = regionalPSNPPlanDetailServiceParam;
            this._regionalPSNPPlanService = regionalPSNPPlanServiceParam;
            this._FDPService = FDPServiceParam;
        }
        public void loadLookups()
        {
            ViewBag.RegionalPSNPPlanID = new SelectList(_regionalPSNPPlanService.GetAllRegionalPSNPPlan(), "RegionalPSNPPlanID", "RegionalPSNPPlanID");
            ViewBag.PlanedFDPID = new SelectList(_FDPService.GetAllFDP(), "FDPID", "Name");

        }
        //
        // GET: /PSNP/RegionalPSNPPlanDetail/

        public ActionResult Index()
        {
            IEnumerable<Cats.Models.RegionalPSNPPlanDetail> list = (IEnumerable<Cats.Models.RegionalPSNPPlanDetail>)_regionalPSNPPlanDetailService.GetAllRegionalPSNPPlanDetail();

            return View(list);
        }

        //
        // GET: /PSNP/RegionalPSNPPlanDetail/Details/5

        public ActionResult Details(int id = 0)
        {
            RegionalPSNPPlanDetail regionalpsnpplandetail = _regionalPSNPPlanDetailService.FindById(id);
            if (regionalpsnpplandetail == null)
            {
                return HttpNotFound();
            }
            return View(regionalpsnpplandetail);
        }

        //
        // GET: /PSNP/RegionalPSNPPlanDetail/Create

        public ActionResult Create()
        {
            loadLookups();
            return View();
        }

        //
        // POST: /PSNP/RegionalPSNPPlanDetail/Create

        [HttpPost]
        public ActionResult Create(RegionalPSNPPlanDetail regionalpsnpplandetail)
        {
            if (ModelState.IsValid)
            {
                _regionalPSNPPlanDetailService.AddRegionalPSNPPlanDetail(regionalpsnpplandetail);
                return RedirectToAction("Index");
            }

            loadLookups();
            return View(regionalpsnpplandetail);
        }

        //
        // GET: /PSNP/RegionalPSNPPlanDetail/Edit/5

        public ActionResult Edit(int id = 0)
        {
            RegionalPSNPPlanDetail regionalpsnpplandetail  = _regionalPSNPPlanDetailService.FindById(id);
            if (regionalpsnpplandetail == null)
            {
                return HttpNotFound();
            }
            loadLookups();
            return View(regionalpsnpplandetail);
        }

        //
        // POST: /PSNP/RegionalPSNPPlanDetail/Edit/5

        [HttpPost]
        public ActionResult Edit(RegionalPSNPPlanDetail regionalpsnpplandetail)
        {
            if (ModelState.IsValid)
            {

                _regionalPSNPPlanDetailService.UpdateRegionalPSNPPlanDetail(regionalpsnpplandetail);
                return RedirectToAction("Index");
            }
            loadLookups();
             return View();
        }

        //
        // GET: /PSNP/RegionalPSNPPlanDetail/Delete/5

        public ActionResult Delete(int id = 0)
        {
            RegionalPSNPPlanDetail regionalpsnpplandetail  = _regionalPSNPPlanDetailService.FindById(id);
            if (regionalpsnpplandetail == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        //
        // POST: /PSNP/RegionalPSNPPlanDetail/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _regionalPSNPPlanDetailService.DeleteById(id);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
           
            base.Dispose(disposing);
        }
    }
}