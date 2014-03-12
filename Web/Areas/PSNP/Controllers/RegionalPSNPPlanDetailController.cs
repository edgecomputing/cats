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
        public IEnumerable<PSNPPlanDetailView> getRegionFDPs(int planID)
        {
            IEnumerable<FDP> list = _FDPService.GetAllFDP();
            return (from fdp in list
                   select new PSNPPlanDetailView
                    {
                        ZoneID = fdp.AdminUnit.AdminUnit2.AdminUnitID,
                        ZoneName = fdp.AdminUnit.AdminUnit2.Name,
                        WoredaID = fdp.AdminUnit.AdminUnitID,
                        WoredaName = fdp.AdminUnit.Name,
                        FDPID = fdp.FDPID,
                        FDPName = fdp.Name,
                        BeneficiaryCount = 0,
                        RegionalPSNPPlanID = planID
                    } 
                    ).OrderBy(t=>t.ZoneName).ThenBy(t=>t.WoredaName);


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
            RegionalPSNPPlan plan = _regionalPSNPPlanService.FindById(id);
            if (plan == null)
            {
                return RedirectToAction("Index", "RegionalPSNPPlan");
            }

            IEnumerable<PSNPPlanDetailView> allFDPData = new List<PSNPPlanDetailView>();

            ViewBag.PsnpPlan = plan;
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
                IEnumerable<PSNPPlanDetailView> allFDPs = getRegionFDPs(id);
                allFDPData = toViewModel(filledData, allFDPs);
            }
            return View(allFDPData);
        }

        public ActionResult GetDataListAjax([DataSourceRequest] DataSourceRequest request, int id = 0)
        {
            IEnumerable<Cats.Models.RegionalPSNPPlanDetail> filledData = new List<RegionalPSNPPlanDetail>();
            IEnumerable<PSNPPlanDetailView> allFDPData = new List<PSNPPlanDetailView>();
            RegionalPSNPPlan plan = _regionalPSNPPlanService.FindById(id);
            if (plan != null)
            {
                IEnumerable<FDP> allFDPs = _FDPService.GetAllFDP();
                filledData = plan.RegionalPSNPPlanDetails;
                allFDPData = from fdp in allFDPs
                             join plandetail in filledData on fdp.FDPID equals plandetail.PlanedFDPID

                             select new PSNPPlanDetailView
                             {
                                 FDPID = fdp.FDPID,
                                 FDPName = fdp.Name,
                                 WoredaID = fdp.AdminUnit.AdminUnitID,
                                 WoredaName = fdp.AdminUnit.Name,
                                 ZoneID = fdp.AdminUnit.AdminUnit2.AdminUnitID,
                                 ZoneName = fdp.AdminUnit.AdminUnit2.Name,
                                 RegionName = fdp.AdminUnit.AdminUnit2.AdminUnit2.Name,
                                 RegionalPSNPPlanDetailID = plandetail.RegionalPSNPPlanDetailID,
                                 BeneficiaryCount = plandetail.BeneficiaryCount,
                                 RegionalPSNPPlanID = plan.RegionalPSNPPlanID,
                                 FoodRatio = plandetail.FoodRatio,
                                 CashRatio = plandetail.CashRatio
                             };
            }
            return Json(allFDPData.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        private IEnumerable<PSNPPlanDetailView> getDetailView(int id=0)
        {
            IEnumerable<Cats.Models.RegionalPSNPPlanDetail> filledData = new List<RegionalPSNPPlanDetail>();
            IEnumerable<PSNPPlanDetailView> allFDPData = new List<PSNPPlanDetailView>();
            RegionalPSNPPlan plan = _regionalPSNPPlanService.FindById(id);
            if (plan != null)
            {

                ViewBag.PsnpPlan = plan;
                filledData = plan.RegionalPSNPPlanDetails;
                IEnumerable<FDP> allFDPs = _FDPService.GetAllFDP();
                allFDPData = from fdp in allFDPs
                             join plandetail in filledData on fdp.FDPID equals plandetail.PlanedFDPID
                             into fdpBeneficiary
                             from fdb in fdpBeneficiary.DefaultIfEmpty(new RegionalPSNPPlanDetail { RegionalPSNPPlanID = plan.RegionalPSNPPlanID })
                             select new PSNPPlanDetailView
                             {
                                 FDPID = fdp.FDPID,
                                 FDPName = fdp.Name,
                                 WoredaID = fdp.AdminUnit.AdminUnitID,
                                 WoredaName = fdp.AdminUnit.Name,
                                 ZoneID = fdp.AdminUnit.AdminUnit2.AdminUnitID,
                                 ZoneName = fdp.AdminUnit.AdminUnit2.Name,
                                 RegionalPSNPPlanDetailID = fdb.RegionalPSNPPlanDetailID,
                                 BeneficiaryCount = fdb.BeneficiaryCount,
                                 RegionalPSNPPlanID = fdb.RegionalPSNPPlanID,
                                 FoodRatio = fdb.FoodRatio,
                                 CashRatio = fdb.CashRatio
                             };
            }

            return allFDPData;
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
                IEnumerable<FDP> allFDPs = _FDPService.GetAllFDP();
                allFDPData = from fdp in allFDPs
                             join plandetail in filledData on fdp.FDPID equals plandetail.PlanedFDPID
                             into fdpBeneficiary
                             from fdb in fdpBeneficiary.DefaultIfEmpty(new RegionalPSNPPlanDetail { RegionalPSNPPlanID = plan.RegionalPSNPPlanID })
                             select new PSNPPlanDetailView
                             {
                                 FDPID = fdp.FDPID,
                                 FDPName = fdp.Name,
                                 WoredaID = fdp.AdminUnit.AdminUnitID,
                                 WoredaName = fdp.AdminUnit.Name,
                                 ZoneID = fdp.AdminUnit.AdminUnit2.AdminUnitID,
                                 ZoneName = fdp.AdminUnit.AdminUnit2.Name,
                                 RegionalPSNPPlanDetailID = fdb.RegionalPSNPPlanDetailID,
                                 BeneficiaryCount = fdb.BeneficiaryCount,
                                 RegionalPSNPPlanID = fdb.RegionalPSNPPlanID,
                                 FoodRatio = fdb.FoodRatio,
                                 CashRatio = fdb.CashRatio,
                                 RegionName = fdp.AdminUnit.AdminUnit2.AdminUnit2.Name
                             };
            }
            return Json(allFDPData.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }


        public ActionResult EditAjax([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<PSNPPlanDetailView> items)
        {
            int planId = 0;
            List<PSNPPlanDetailView> updated = new List<PSNPPlanDetailView>();
            foreach (PSNPPlanDetailView item in items)
            {
                updated.Add(item);
                planId = item.RegionalPSNPPlanID;
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
                        var psnpPlanExist =
                            _regionalPSNPPlanDetailService.FindBy(
                                m => m.RegionalPSNPPlanID == item.RegionalPSNPPlanID && m.PlanedFDPID == item.FDPID).
                                FirstOrDefault();
                        if (psnpPlanExist==null)
                        {
                            _regionalPSNPPlanDetailService.AddRegionalPSNPPlanDetail(bm);
                        }
                        
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
            /*var allFDPData = getDetailView(planId);
           return Json(new[] { regionalRequestDetail }.ToDataSourceResult(request, ModelState)); */
            return Json(updated.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

           // return Json(ModelState.ToDataSourceResult(request, ModelState));

        }
        public ActionResult DeleteAjax(int id = 0)
        {
            _regionalPSNPPlanDetailService.DeleteById(id);
            return Json("{}");
        }

        protected override void Dispose(bool disposing)
        {

            base.Dispose(disposing);
        }
    }
}