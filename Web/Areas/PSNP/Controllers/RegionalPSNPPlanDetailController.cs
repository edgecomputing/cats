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
using Cats.Models.PSNP;
namespace Cats.Areas.PSNP.Controllers
{
    public class RegionalPSNPPlanDetailController : Controller
    {
        private readonly IRegionalPSNPPlanDetailService _regionalPSNPPlanDetailService;
        private readonly IRegionalPSNPPlanService _regionalPSNPPlanService;
        private readonly IFDPService _FDPService;
        private readonly IRegionalRequestService _reqService;

        public RegionalPSNPPlanDetailController(
                            IRegionalPSNPPlanDetailService regionalPSNPPlanDetailServiceParam,
                            IRegionalPSNPPlanService regionalPSNPPlanServiceParam,
                            IRegionalRequestService regionalRequestServiceParam,
                            IFDPService FDPServiceParam)
        {
            this._regionalPSNPPlanDetailService = regionalPSNPPlanDetailServiceParam;
            this._regionalPSNPPlanService = regionalPSNPPlanServiceParam;
            this._FDPService = FDPServiceParam;
            this._reqService = regionalRequestServiceParam;
        }
        public void loadLookups()
        {
            ViewBag.RegionalPSNPPlanID = new SelectList(_regionalPSNPPlanService.GetAllRegionalPSNPPlan(), "RegionalPSNPPlanID", "RegionalPSNPPlanID");
            ViewBag.PlanedFDPID = new SelectList(_FDPService.GetAllFDP(), "FDPID", "Name");

        }
        public IEnumerable<PSNPPlanDetailView> toViewModel(IEnumerable<Cats.Models.RegionalPSNPPlanDetail> list, IEnumerable<PSNPPlanDetailView> allFDPs)
        {
            List<PSNPPlanDetailView> ret = new List<PSNPPlanDetailView>();
            foreach (PSNPPlanDetailView fdp in allFDPs)
            {
                foreach (RegionalPSNPPlanDetail pd in list)
                {

                    //   fdp.FoodRatio = 1;
                    if (fdp.FDPID == pd.PlanedFDPID)
                    {
                        fdp.RegionalPSNPPlanDetailID = pd.RegionalPSNPPlanDetailID;
                        fdp.BeneficiaryCount = pd.BeneficiaryCount;
                        fdp.CashRatio = pd.CashRatio;
                        fdp.FoodRatio = pd.FoodRatio;
                        fdp.Item3Ratio = pd.Item3Ratio;
                        fdp.Item4Ratio = pd.Item4Ratio;
                    }
                }
                ret.Add(fdp);
            }
            return ret;
        }
        public IEnumerable<PSNPPlanDetailView> getRegionFDPs(int regionID, int planID)
        {
            IEnumerable<FDP> list = _FDPService.GetAllFDP();
            return (from fdp in list
                    where (fdp.AdminUnit.AdminUnit2.AdminUnit2.AdminUnitID == regionID)
                    select new PSNPPlanDetailView
                    {
                        FDPID = fdp.FDPID,
                        FDPName = fdp.Name,
                        WoredaID = fdp.AdminUnit.AdminUnitID,
                        WoredaName = fdp.AdminUnit.Name,
                        ZoneID = fdp.AdminUnit.AdminUnit2.AdminUnitID,
                        ZoneName = fdp.AdminUnit.AdminUnit2.Name,
                        BeneficiaryCount = 0,
                        RegionalPSNPPlanID = planID
                    }
                    );


        }
        public IEnumerable<RegionalPSNPPlanDetail> toBussinessModel(IEnumerable<PSNPPlanDetailView> view)
        {
            return (from pd in view

                    select new RegionalPSNPPlanDetail
                    {
                        RegionalPSNPPlanID = pd.RegionalPSNPPlanID,
                        PlanedFDPID = pd.FDPID,
                        RegionalPSNPPlanDetailID = pd.RegionalPSNPPlanDetailID,
                        BeneficiaryCount = (int)pd.BeneficiaryCount,
                        CashRatio = (int)pd.CashRatio,
                        FoodRatio = (int)pd.FoodRatio,
                        Item3Ratio = (int)pd.Item3Ratio,
                        Item4Ratio = (int)pd.Item4Ratio
                    });
        }
        public ActionResult Index(int id = 0)
        {
            if (id == 0)
            {
                return RedirectToAction("Index", "RegionalPSNPPlan");
            }
            IEnumerable<Cats.Models.RegionalPSNPPlanDetail> filledData = new List<RegionalPSNPPlanDetail>();
            IEnumerable<PSNPPlanDetailView> allFDPData = new List<PSNPPlanDetailView>();
            RegionalPSNPPlan plan = _regionalPSNPPlanService.FindById(id);
            
            if (plan != null)
            {
                ViewBag.PsnpPlan = plan;
                filledData = plan.RegionalPSNPPlanDetails;
                IEnumerable<PSNPPlanDetailView> allFDPs = getRegionFDPs(plan.Region.AdminUnitID, id);
                allFDPData = toViewModel(filledData, allFDPs);
            }
            return View(allFDPData);
        }
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
            {
                return RedirectToAction("Index", "RegionalPSNPPlan");
            }
            IEnumerable<Cats.Models.RegionalPSNPPlanDetail> filledData = new List<RegionalPSNPPlanDetail>();
            IEnumerable<PSNPPlanDetailView> allFDPData = new List<PSNPPlanDetailView>();
            RegionalPSNPPlan plan = _regionalPSNPPlanService.FindById(id);

            if (plan != null)
            {
                ViewBag.PsnpPlan = plan;
                filledData = plan.RegionalPSNPPlanDetails;
                IEnumerable<PSNPPlanDetailView> allFDPs = getRegionFDPs(plan.Region.AdminUnitID, id);
                allFDPData = toViewModel(filledData, allFDPs);
            }
            return View(allFDPData);
        }

        public ActionResult GetListAjax([DataSourceRequest] DataSourceRequest request, int id = 0)
        {
            IEnumerable<Cats.Models.RegionalPSNPPlanDetail> filledData = new List<RegionalPSNPPlanDetail>();
            IEnumerable<PSNPPlanDetailView> allFDPData = new List<PSNPPlanDetailView>();
            RegionalPSNPPlan plan = _regionalPSNPPlanService.FindById(id);
            if (plan != null)
            {

                ViewBag.PsnpPlan = plan;
                filledData = plan.RegionalPSNPPlanDetails;
                IEnumerable<PSNPPlanDetailView> allFDPs = getRegionFDPs(plan.Region.AdminUnitID, id);
                allFDPData = toViewModel(filledData, allFDPs);
            }
            return Json(allFDPData.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            //return Json(toViewModel(list).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditAjax([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<PSNPPlanDetailView> items)
        {
            
            foreach (PSNPPlanDetailView item in items)
            {
                if (item.BeneficiaryCount > 0)
                {
                    RegionalPSNPPlanDetail bm;
                    if (item.RegionalPSNPPlanDetailID >= 1)
                    {
                        bm = _regionalPSNPPlanDetailService.FindById(item.RegionalPSNPPlanDetailID);
                        bm.BeneficiaryCount = (int)item.BeneficiaryCount;
                        bm.FoodRatio = (int)item.FoodRatio;
                        bm.CashRatio = (int)item.CashRatio;
                        _regionalPSNPPlanDetailService.UpdateRegionalPSNPPlanDetail(bm);
                    }
                    else
                    {
                        bm = new RegionalPSNPPlanDetail();
                        bm.RegionalPSNPPlanID = item.RegionalPSNPPlanID;
                        bm.PlanedFDPID = item.FDPID;
                        bm.BeneficiaryCount = (int)item.BeneficiaryCount;
                        bm.FoodRatio = (int)item.FoodRatio;
                        bm.CashRatio = (int)item.CashRatio;

                        _regionalPSNPPlanDetailService.AddRegionalPSNPPlanDetail(bm);
                    }
                }
                else
                {
                    if (item.RegionalPSNPPlanDetailID >= 0)
                    {
                        _regionalPSNPPlanDetailService.DeleteById(item.RegionalPSNPPlanDetailID);
                    }
                }
            }
            return Json(ModelState.ToDataSourceResult());

        }
        public ActionResult DeleteAjax(int id = 0)
        {
            _regionalPSNPPlanDetailService.DeleteById(id);
            return Json("{}");
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
        /*
        public ActionResult Edit(int id = 0)
        {
            RegionalPSNPPlanDetail regionalpsnpplandetail = _regionalPSNPPlanDetailService.FindById(id);
            if (regionalpsnpplandetail == null)
            {
                return HttpNotFound();
            }
            loadLookups();
            return View(regionalpsnpplandetail);
        }
        */
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
            RegionalPSNPPlanDetail regionalpsnpplandetail = _regionalPSNPPlanDetailService.FindById(id);
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