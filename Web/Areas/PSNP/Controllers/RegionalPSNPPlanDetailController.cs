using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Helpers;
using Cats.Models;
using Cats.Data;
using Cats.Services.Administration;
using Cats.Services.PSNP;
using Cats.Services.EarlyWarning;
using Cats.Services.Security;
using Cats.ViewModelBinder;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Cats.Models.PSNP;
using IAdminUnitService = Cats.Services.EarlyWarning.IAdminUnitService;
using IFDPService = Cats.Services.EarlyWarning.IFDPService;

namespace Cats.Areas.PSNP.Controllers
{
    public class RegionalPSNPPlanDetailController : Controller
    {
        private readonly IRegionalPSNPPlanDetailService _regionalPSNPPlanDetailService;
        private readonly IRegionalPSNPPlanService _regionalPSNPPlanService;
        private readonly IFDPService _FDPService;
        private readonly IRegionalRequestService _reqService;
        private readonly IAdminUnitService _adminUnitService;
        private readonly IRationDetailService _rationDetailService;
        private readonly IUserAccountService _userAccountService;
        private readonly IUserProfileService _userProfileService;
        public RegionalPSNPPlanDetailController(
                            IRegionalPSNPPlanDetailService regionalPSNPPlanDetailServiceParam,
                            IRegionalPSNPPlanService regionalPSNPPlanServiceParam,
                            IRegionalRequestService regionalRequestServiceParam,
                            IFDPService FDPServiceParam,
                            IAdminUnitService adminUnitService,
                            IRationDetailService rationDetailService,
                            IUserAccountService userAccountService, IUserProfileService userProfileService)
        {
            this._regionalPSNPPlanDetailService = regionalPSNPPlanDetailServiceParam;
            this._regionalPSNPPlanService = regionalPSNPPlanServiceParam;
            this._FDPService = FDPServiceParam;
            this._reqService = regionalRequestServiceParam;
            this._adminUnitService = adminUnitService;
            this._rationDetailService = rationDetailService;
            this._userAccountService = userAccountService;
            this._userProfileService = userProfileService;
        }
        public void loadLookups()
        {
            ViewBag.RegionalPSNPPlanID = new SelectList(_regionalPSNPPlanService.GetAllRegionalPSNPPlan(), "RegionalPSNPPlanID", "RegionalPSNPPlanID");
            ViewBag.PlanedFDPID = new SelectList(_FDPService.GetAllFDP(), "FDPID", "Name");

        }
        public IEnumerable<PSNPPlanDetailView> toViewModel(IEnumerable<Cats.Models.RegionalPSNPPlanDetail> list, IEnumerable<PSNPPlanDetailView> allWoredas)
        {
            List<PSNPPlanDetailView> ret = new List<PSNPPlanDetailView>();
            foreach (PSNPPlanDetailView woreda in allWoredas)
            {
                foreach (RegionalPSNPPlanDetail pd in list)
                {

                    //   fdp.FoodRatio = 1;
                    if (woreda.WoredaID == pd.PlanedWoredaID)
                    {
                        woreda.RegionalPSNPPlanDetailID = pd.RegionalPSNPPlanDetailID;
                        woreda.BeneficiaryCount = pd.BeneficiaryCount;
                        woreda.CashRatio = pd.CashRatio;
                        woreda.FoodRatio = pd.FoodRatio;
                        woreda.Item3Ratio = pd.Item3Ratio;
                        woreda.Item4Ratio = pd.Item4Ratio;
                        woreda.Contingency = pd.Contingency;
                    }
                }
                ret.Add(woreda);
            }
            return ret;
        }
        public IEnumerable<PSNPPlanDetailView> getRegionFDPs(int planID)
        {
            IEnumerable<AdminUnit> list = _adminUnitService.FindBy(m=>m.ParentID==4);
            return (from woreda in list
                   select new PSNPPlanDetailView
                    {
                        ZoneID = woreda.AdminUnit2.AdminUnitID,
                        ZoneName = woreda.AdminUnit2.Name,
                        WoredaID = woreda.AdminUnitID,
                        WoredaName = woreda.Name,
                        //FDPID = fdp.FDPID,
                        //FDPName = fdp.Name,
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
                        PlanedWoredaID = pd.WoredaID,
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

           // IEnumerable<PSNPPlanDetailView> allFDPData = new List<PSNPPlanDetailView>();
            var preferedweight = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).PreferedWeightMeasurment;
            var dt = GetTransposedPSNPPlan(id, preferedweight);
            ViewBag.PsnpPlan = plan;
            UserProfile user = _userProfileService.GetUser(User.Identity.Name);
            ViewBag.plan_user = plan.User;
            ViewBag.current_user = user.UserProfileID;
            ViewBag.IsRequestCreated = IsRequestCreatedFromThisPlan(plan.PlanId);
            return View(dt);
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
            ViewData["Month"] = RequestHelper.GetMonthList();
            UserProfile user = _userProfileService.GetUser(User.Identity.Name);
            if (plan != null)
            {
                ViewBag.PsnpPlan = plan;
                filledData = plan.RegionalPSNPPlanDetails;
                IEnumerable<PSNPPlanDetailView> allFDPs = getRegionFDPs(id);
                allFDPData = toViewModel(filledData, allFDPs);
                ViewBag.plan_user = plan.User;
                ViewBag.current_user = user.UserProfileID;
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
                IEnumerable<AdminUnit> allWoredas = _adminUnitService.FindBy(m=>m.AdminUnitTypeID==4);
                filledData = plan.RegionalPSNPPlanDetails;
                allFDPData = from woreda in allWoredas
                             join plandetail in filledData on woreda.AdminUnitID equals plandetail.PlanedWoredaID

                             select new PSNPPlanDetailView
                             {
                                 //FDPID = fdp.FDPID,
                                 //FDPName = fdp.Name,
                                 WoredaID = woreda.AdminUnitID,
                                 WoredaName = woreda.Name,
                                 ZoneID = woreda.AdminUnit2.AdminUnitID,
                                 ZoneName = woreda.AdminUnit2.Name,
                                 RegionName = woreda.AdminUnit2.AdminUnit2.Name,
                                 RegionalPSNPPlanDetailID = plandetail.RegionalPSNPPlanDetailID,
                                 BeneficiaryCount = plandetail.BeneficiaryCount,
                                 RegionalPSNPPlanID = plan.RegionalPSNPPlanID,
                                 FoodRatio = plandetail.FoodRatio,
                                 CashRatio = plandetail.CashRatio,
                                 StartingMonthName =  RequestHelper.MonthName(plandetail.StartingMonth)
                                
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
                IEnumerable<AdminUnit> allWoredas = _adminUnitService.FindBy(m=>m.AdminUnitTypeID==4);
                allFDPData = from woreda in allWoredas
                             join plandetail in filledData on woreda.AdminUnitID equals plandetail.PlanedWoredaID
                             into fdpBeneficiary
                             from fdb in fdpBeneficiary.DefaultIfEmpty(new RegionalPSNPPlanDetail { RegionalPSNPPlanID = plan.RegionalPSNPPlanID })
                             select new PSNPPlanDetailView
                             {
                                 //FDPID = fdp.FDPID,
                                 //FDPName = fdp.Name,
                                 WoredaID = woreda.AdminUnitID,
                                 WoredaName = woreda.Name,
                                 ZoneID =woreda.AdminUnit2.AdminUnitID,
                                 ZoneName = woreda.AdminUnit2.Name,
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
                var woredas = _adminUnitService.FindBy(m => m.AdminUnitTypeID == 4);
                //IEnumerable<FDP> allFDPs = _FDPService.GetAllFDP();
                allFDPData = from woreda in woredas
                             join plandetail in filledData on woreda.AdminUnitID equals plandetail.PlanedWoredaID
                             into fdpBeneficiary
                             from fdb in fdpBeneficiary.DefaultIfEmpty(new RegionalPSNPPlanDetail { RegionalPSNPPlanID = plan.RegionalPSNPPlanID })
                             select new PSNPPlanDetailView
                             {
                                 //FDPID = fdp.FDPID,
                                 //FDPName = fdp.Name,
                                 WoredaID = woreda.AdminUnitID,
                                 WoredaName = woreda.Name,
                                 ZoneID = woreda.AdminUnit2.AdminUnitID,
                                 ZoneName = woreda.AdminUnit2.Name,
                                 RegionalPSNPPlanDetailID = fdb.RegionalPSNPPlanDetailID,
                                 BeneficiaryCount = fdb.BeneficiaryCount,
                                 RegionalPSNPPlanID = fdb.RegionalPSNPPlanID,
                                 FoodRatio = fdb.FoodRatio,
                                 CashRatio = fdb.CashRatio,
                                 RegionName = woreda.AdminUnit2.AdminUnit2.Name,
                                 SartingMonth = fdb.StartingMonth,
                                 Contingency = fdb.Contingency
                             };
            }
            return Json(allFDPData.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditAjax([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<PSNPPlanDetailView> items)
        {
            int planId = 0;
            //var psnpPlan =
            //    _regionalPSNPPlanService.FindBy(m => m.RegionalPSNPPlanID == items.FirstOrDefault().RegionalPSNPPlanID).
            //        FirstOrDefault();
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
                        bm.StartingMonth = 1;  //set default month January
                        if (item.SartingMonth!=0)
                        {
                            bm.StartingMonth = item.SartingMonth;
                        }
                        
                        bm.BeneficiaryCount = (int)item.BeneficiaryCount;
                        bm.FoodRatio = (int)item.FoodRatio;
                        bm.CashRatio = (int)item.CashRatio;
                        bm.Contingency = item.Contingency;
                        _regionalPSNPPlanDetailService.UpdateRegionalPSNPPlanDetail(bm);
                    }
                    else
                    {
                       
                        bm = new RegionalPSNPPlanDetail();
                        bm.RegionalPSNPPlanID = item.RegionalPSNPPlanID;
                        bm.PlanedWoredaID = item.WoredaID;
                        bm.BeneficiaryCount = (int)item.BeneficiaryCount;
                        bm.FoodRatio = (int)item.FoodRatio;
                        bm.CashRatio = (int)item.CashRatio;
                        bm.Contingency = item.Contingency;
                        bm.StartingMonth = 1;  //set default month January
                        if (item.SartingMonth!=0)
                        {
                            bm.StartingMonth = item.SartingMonth;
                        }
                        var psnpPlanExist =
                            _regionalPSNPPlanDetailService.FindBy(
                                m => m.RegionalPSNPPlanID == item.RegionalPSNPPlanID && m.PlanedWoredaID == item.WoredaID).
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
        private DataTable GetTransposedPSNPPlan(int id, string preferedweight)
        {

            var psnpPlan = _regionalPSNPPlanService.FindById(id);
            var psnpPlanDetails = _regionalPSNPPlanDetailService.FindBy(t => t.RegionalPSNPPlanID == id).ToList();
            var rationDetails = _rationDetailService.Get(t => t.RationID == psnpPlan.RationID, null, "Commodity");
            var dt = PSNPPlanViewModelBinder.TransposeData(psnpPlanDetails, rationDetails, preferedweight);
            return dt;
        }

        private bool IsRequestCreatedFromThisPlan(int id)
        {
            var regionalRequest = _reqService.FindBy(p => p.PlanID == id).FirstOrDefault();
            return regionalRequest != null;
        }
    }
}