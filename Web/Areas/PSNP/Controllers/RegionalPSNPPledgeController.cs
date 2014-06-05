using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Helpers;
using Cats.Models;
using Cats.Models.PSNP;
using Cats.Models.ViewModels;
using Cats.Models.ViewModels.Bid;
using Cats.Services.EarlyWarning;
using Cats.Services.PSNP;
using Cats.Services.Security;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Areas.PSNP.Controllers
{
    public class RegionalPSNPPledgeController : Controller
    {
        private readonly IDonorService _donorService;
        private readonly IFDPService _fdpService;
        private readonly IRegionalPSNPPlanDetailService _regionalPSNPPlanDetailService;
        private readonly IRegionalPSNPPlanService _regionalPSNPPlanService;
        private readonly IRegionalPSNPPledgeService _regionalPSNPPledgeService;
        private readonly IAdminUnitService _adminUnitService;
        private readonly ICommodityService _commodityService;
        private readonly IRationDetailService _rationDetailService;
        private readonly IUnitService _unitService;

        public RegionalPSNPPledgeController(IDonorService donorService, IFDPService fdpService,
            IRegionalPSNPPlanDetailService regionalPSNPPlanDetailService,IRegionalPSNPPlanService regionalPSNPPlanService,
            IRegionalPSNPPledgeService regionalPSNPPledgeService, IAdminUnitService adminUnitService,
            ICommodityService commodityService, IRationDetailService rationDetailService, IUnitService unitService)
        {
            _donorService = donorService;
            _fdpService = fdpService;
            _regionalPSNPPlanDetailService = regionalPSNPPlanDetailService;
            _regionalPSNPPlanService = regionalPSNPPlanService;
            _regionalPSNPPledgeService = regionalPSNPPledgeService;
            _adminUnitService = adminUnitService;
            _commodityService = commodityService;
            _rationDetailService = rationDetailService;
            _unitService = unitService;
        }
        //
        // GET: /PSNP/RegionalPSNPPledge/

        public ActionResult Index()
        {
            ViewBag.DonorID = new SelectList(_donorService.GetAllDonor(), "DonorID", "Name");
            ViewBag.CommodityID = new SelectList(_commodityService.GetAllCommodity(), "CommodityID", "Name");
            ViewBag.UnitID = new SelectList(_unitService.GetAllUnit(), "UnitID", "Name");
            return View();
        }

        public ActionResult RegionalPSNPPlan_Read([DataSourceRequest] DataSourceRequest request)
        {

            var regionalPSNPPlans = _regionalPSNPPlanService.GetAllRegionalPSNPPlan();
            var regionalPSNPPlansToDisplay = GetRegionalPSNPPlans(regionalPSNPPlans).ToList();
            return Json(regionalPSNPPlansToDisplay.ToDataSourceResult(request));
        }

        private IEnumerable<Cats.Models.ViewModels.PSNP.RegionalPSNPPlanViewModel> GetRegionalPSNPPlans(IEnumerable<RegionalPSNPPlan> regionalPSNPPlans)
        {
            return (from regionalPSNPPlan in regionalPSNPPlans
                  
                    select new Cats.Models.ViewModels.PSNP.RegionalPSNPPlanViewModel()
                        {
                            RegionalPSNPPlanID = regionalPSNPPlan.RegionalPSNPPlanID,
                            Year = regionalPSNPPlan.Year,
                            Duration = regionalPSNPPlan.Duration,

                            RationID = regionalPSNPPlan.RationID,
                            StatusID = regionalPSNPPlan.StatusID,
                        });
        }

        public ActionResult RegionalPSNPPlanDetail_Read(int regionalPSNPPlanID, [DataSourceRequest] DataSourceRequest request)
        {
            var regionalPSNPPlanDetails = _regionalPSNPPlanDetailService.GetAllRegionalPSNPPlanDetail();
            var regionalPSNPPlanDetailsToDisplay = GetregionalPSNPPlanDetails(regionalPSNPPlanDetails).ToList();
            return Json(regionalPSNPPlanDetailsToDisplay.Where(p => p.RegionalPSNPPlanID == regionalPSNPPlanID).ToDataSourceResult(request));
        }

        private IEnumerable<Cats.Models.ViewModels.PSNP.RegionalPSNPPlanDetailViewModel> GetregionalPSNPPlanDetails(IEnumerable<RegionalPSNPPlanDetail> regionalPSNPPlanDetails)
        {
            return (from regionalPSNPPlanDetail in regionalPSNPPlanDetails
                    select new Cats.Models.ViewModels.PSNP.RegionalPSNPPlanDetailViewModel()
                    {
                        RegionalPSNPPlanDetailID = regionalPSNPPlanDetail.RegionalPSNPPlanDetailID,
                        RegionalPSNPPlanID = regionalPSNPPlanDetail.RegionalPSNPPlanID,
                        PlanedWoredaID = regionalPSNPPlanDetail.PlanedWoredaID,
                        PlanedWoreda = regionalPSNPPlanDetail.PlanedWoreda.Name,
                        BeneficiaryCount = regionalPSNPPlanDetail.BeneficiaryCount,
                        FoodRatio = regionalPSNPPlanDetail.FoodRatio,
                        CashRatio = regionalPSNPPlanDetail.CashRatio,
                        Item3Ratio = regionalPSNPPlanDetail.Item3Ratio,
                        Item4Ratio = regionalPSNPPlanDetail.Item4Ratio
                    });
        }

        public ActionResult Issue()
        {
            var regionalPSNPPledge = new RegionalPSNPPledge();
            var regionalPSNPPlanList = new List<String>();
            var alreadyReadIDs = new List<int>();
            foreach (var regionalPSNPPlanDetail in _regionalPSNPPlanDetailService.GetAllRegionalPSNPPlanDetail())
            {
                if(alreadyReadIDs.Contains(regionalPSNPPlanDetail.RegionalPSNPPlanID))
                    continue;
                var regionalPSNPPlan = _regionalPSNPPlanService.FindById(regionalPSNPPlanDetail.RegionalPSNPPlanID);
                //var region = _adminUnitService.FindById(regionalPSNPPlan.RegionID);
                var regionalPSNPPlanName = regionalPSNPPlan.Year;// +" - " + region.Name;
                regionalPSNPPlanList.Add(regionalPSNPPlanName.ToString());
                alreadyReadIDs.Add(regionalPSNPPlanDetail.RegionalPSNPPlanID);
            }
            ViewBag.RegionalPSNPPlan = new SelectList(regionalPSNPPlanList, "", "", regionalPSNPPledge.RegionalPSNPPlanID = 1);
            ViewBag.DonorID = new SelectList(_donorService.GetAllDonor(), "DonorID", "Name");
            ViewBag.CommodityID = new SelectList(_commodityService.GetAllCommodity(), "CommodityID", "Name");
            ViewBag.UnitID = new SelectList(_unitService.GetAllUnit(), "UnitID", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Issue(RegionalPSNPPledge regionalPSNPPledge, string pledgeDate)
        {
            regionalPSNPPledge.PledgeDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                _regionalPSNPPledgeService.AddRegionalPSNPPledge(regionalPSNPPledge);
            }

            return RedirectToAction("Index");
        }

        public ActionResult FDPsCoveredByDonorsPostBack([DataSourceRequest]DataSourceRequest request)
        {
            var coveredFDPsList = new List<FDPsCoveredByDonors>();
            var regionalPSNPPledges = _regionalPSNPPledgeService.GetAllRegionalPSNPPledge();
            foreach (var regionalPSNPPledge in regionalPSNPPledges)
            {
                var coveredFDPs = new FDPsCoveredByDonors();
                
                coveredFDPs.Donor = regionalPSNPPledge.Donor.Name;
                //var fdpObj = _fdpService.FindById(regionalPSNPPlanDetail.PlanedFDPID);
                //var woredaAdminUnit = _adminUnitService.FindById(fdpObj.AdminUnitID);
                //coveredFDPs.FDP = fdpObj.Name;
                //coveredFDPs.Woreda = woredaAdminUnit.Name;
                //coveredFDPs.Zone = woredaAdminUnit.AdminUnit2.Name;
                //coveredFDPs.Region = woredaAdminUnit.AdminUnit2.AdminUnit2.Name;
                coveredFDPs.Commodity = _commodityService.FindById(regionalPSNPPledge.Commodity.CommodityID).Name;
                var regionalPSNPPlan =
                    _regionalPSNPPlanService.FindById(regionalPSNPPledge.RegionalPSNPPlanID);
                //var rationDetails = _rationDetailService.Get(t => t.RationID == regionalPSNPPlan.RationID);
                //var pledge = regionalPSNPPledge;
                //decimal neededQty = 0;
                //const string neededQtyUnit = "";
                //foreach (var rationDetail in rationDetails.Where(rationDetail => rationDetail.CommodityID == pledge.Commodity.CommodityID))
                //{
                //    neededQty = rationDetail.Amount;
                //}
                //coveredFDPs.NeededQty = neededQty.ToString(CultureInfo.InvariantCulture);
                //coveredFDPs.NeededQtyUnit = neededQtyUnit;
                coveredFDPs.PledgedQty = regionalPSNPPledge.Quantity.ToString(CultureInfo.InvariantCulture);
                coveredFDPs.PledgedQtyUnit = regionalPSNPPledge.Unit.Name;
                coveredFDPs.PledgeDate = regionalPSNPPledge.PledgeDate.ToString(CultureInfo.InvariantCulture);

                coveredFDPsList.Add(coveredFDPs);
            }

            return Json(coveredFDPsList.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        private DateTime GetGregorianDate(string ethiopianDate)
        {
            DateTime convertedGregorianDate;
            try
            {
                convertedGregorianDate = DateTime.Parse(ethiopianDate);
            }
            catch (Exception ex)
            {
                var strEth = new getGregorianDate();
                convertedGregorianDate = strEth.ReturnGregorianDate(ethiopianDate);
            }
            return convertedGregorianDate;
        }
    }
}
