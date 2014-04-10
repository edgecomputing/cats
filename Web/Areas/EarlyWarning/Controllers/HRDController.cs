using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Cats.Helpers;
using Cats.Infrastructure;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Models.ViewModels;
using Cats.Models.ViewModels.HRD;
using Cats.Services.EarlyWarning;
using Cats.Services.Security;
using Cats.ViewModelBinder;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using log4net;
using Cats.Security;

namespace Cats.Areas.EarlyWarning.Controllers
{
    public class HRDController : Controller
    {
        //
        // GET: /EarlyWarning/HRD/
        private IAdminUnitService _adminUnitService;
        private IHRDService _hrdService;
        private IRationService _rationService;
        private IHRDDetailService _hrdDetailService;
        private ICommodityService _commodityService;
        private IRationDetailService _rationDetailService;
        private INeedAssessmentDetailService _needAssessmentDetailService;
        private INeedAssessmentHeaderService _needAssessmentService;
        private IWorkflowStatusService _workflowStatusService;
        private ISeasonService _seasonService;
        private IUserAccountService _userAccountService;
        private ILog _log;
        private IPlanService _planService;

        public HRDController(IAdminUnitService adminUnitService, IHRDService hrdService,
                             IRationService rationservice, IRationDetailService rationDetailService,
                             IHRDDetailService hrdDetailService, ICommodityService commodityService,
                             INeedAssessmentDetailService needAssessmentDetailService, INeedAssessmentHeaderService needAssessmentService,
                             IWorkflowStatusService workflowStatusService, ISeasonService seasonService, 
                             IUserAccountService userAccountService, ILog log,IPlanService planService)
        {
            _adminUnitService = adminUnitService;
            _hrdService = hrdService;
            _hrdDetailService = hrdDetailService;
            _commodityService = commodityService;
            _rationService = rationservice;
            _rationDetailService = rationDetailService;
            _needAssessmentDetailService = needAssessmentDetailService;
            _needAssessmentService = needAssessmentService;
            _workflowStatusService = workflowStatusService;
            _seasonService = seasonService;
            _userAccountService = userAccountService;
            _log = log;
            _planService = planService;
        }

        [EarlyWarningAuthorize(operation = EarlyWarningConstants.Operation.View_HRD_list)]
        public ActionResult Index()
        {
            var hrd = _hrdService.GetAllHRD();
            //ViewBag.Status = _workflowStatusService.GetStatusName();
            return View(hrd);
        }

        [EarlyWarningAuthorize(operation = EarlyWarningConstants.Operation.Print_HRD)]
        public ActionResult HRDPrintOut()
        {
            return View("HRDPrintOut");
        }

        [EarlyWarningAuthorize(operation = EarlyWarningConstants.Operation.View_HRD_Detail)]
        public ActionResult HRDDetail(int id = 0)
        {
            ViewData["Month"] = RequestHelper.GetMonthList();
            var hrd = _hrdService.Get(m => m.HRDID == id, null, "HRDDetails").FirstOrDefault();
            ViewBag.SeasonID = hrd.Season.Name;
            ViewBag.Year = hrd.Year;
            ViewBag.HRDID = id;
            if (hrd != null)
            {
                return View(hrd);
            }
            return RedirectToAction("Index");
        }

        [EarlyWarningAuthorize(operation = EarlyWarningConstants.Operation.Approve_HRD)]
        public ActionResult ApprovedHRDs()
        {
            return View();
        }

        [EarlyWarningAuthorize(operation = EarlyWarningConstants.Operation.View_Current_HRD)]
        public ActionResult CurrentHRDs()
        {
            return View();
        }

        [EarlyWarningAuthorize(operation = EarlyWarningConstants.Operation.View_HRD_list)]
        public ActionResult HRD_Read([DataSourceRequest] DataSourceRequest request)
        {
            var hrds = _hrdService.Get(m => m.Status == 1).OrderByDescending(m => m.HRDID);
            var hrdsToDisplay = GetHrds(hrds).ToList();
            return Json(hrdsToDisplay.ToDataSourceResult(request));
        }

        [EarlyWarningAuthorize(operation = EarlyWarningConstants.Operation.View_HRD_Detail)]
        public ActionResult HRDDetail_Read([DataSourceRequest] DataSourceRequest request, int id = 0)
        {


            //var hrdDetail = _hrdService.GetHRDDetailByHRDID(id).OrderBy(m => m.AdminUnit.AdminUnit2.Name).OrderBy(m => m.AdminUnit.AdminUnit2.AdminUnit2.Name);
            var hrd = _hrdService.Get(m => m.HRDID == id, null, "HRDDetails").FirstOrDefault();

            if (hrd != null)
            {
                var detailsToDisplay = GetHRDDetails(hrd).ToList();
                return Json(detailsToDisplay.ToDataSourceResult(request));
            }
            return RedirectToAction("Index");
        }

        [EarlyWarningAuthorize(operation = EarlyWarningConstants.Operation.View_Approved_HRD)]
        public ActionResult ApprovedHRD_Read([DataSourceRequest] DataSourceRequest request)
        {

            var hrds = _hrdService.Get(m => m.Status == 2).OrderByDescending(m => m.HRDID);
            var hrdsToDisplay = GetHrds(hrds).ToList();
            return Json(hrdsToDisplay.ToDataSourceResult(request));
        }

        //get published hrds information
        public ActionResult CurrentHRD_Read([DataSourceRequest] DataSourceRequest request)
        {
            DateTime latestDate = _hrdService.Get(m => m.Status == 3).Max(m => m.PublishedDate);
            var hrds = _hrdService.FindBy(m => m.Status == 3 && m.PublishedDate == latestDate);
            //.OrderBy(m => m.PublishedDate);
            var hrdsToDisplay = GetHrds(hrds).ToList();
            return Json(hrdsToDisplay.ToDataSourceResult(request));
        }

        //gets hrd information
        private IEnumerable<HRDViewModel> GetHrds(IEnumerable<HRD> hrds)
        {
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            return (from hrd in hrds
                    select new HRDViewModel
                        {
                            HRDID = hrd.HRDID,
                            Season = hrd.Season.Name,
                            Year = hrd.Year,
                            Ration = hrd.Ration.RefrenceNumber,
                            CreatedDate = hrd.CreatedDate,
                            CreatedBy = hrd.UserProfile.FirstName + " " + hrd.UserProfile.LastName,
                            PublishedDate = hrd.PublishedDate,
                            StatusID = hrd.Status,
                            Status = _workflowStatusService.GetStatusName(WORKFLOW.HRD, hrd.Status.Value),
                            CreatedDatePref = hrd.CreatedDate.ToCTSPreferedDateFormat(datePref),
                            PublishedDatePref = hrd.PublishedDate.ToCTSPreferedDateFormat(datePref),
                            Plan = hrd.Plan.PlanName

                        });
        }

        //public ActionResult RegionalSummary_Read([DataSourceRequest] DataSourceRequest request, int id = 0)
        //{
        //    var hrd = _hrdService.Get(m => m.HRDID == id, null, "HRDDetails").FirstOrDefault();

        //    if (hrd != null)
        //    {
        //        var detailsToDisplay = GetSummary(hrd).ToList();
        //        return Json(detailsToDisplay.ToDataSourceResult(request));
        //    }
        //    return RedirectToAction("Index");
        //}
        [EarlyWarningAuthorize(operation = EarlyWarningConstants.Operation.HRD_Summary)]
        public ActionResult RegionalSummary(int id = 0)
        {
            var hrd = _hrdService.Get(m => m.HRDID == id).FirstOrDefault();
            ViewBag.SeasonID = hrd.Season.Name;
            ViewBag.Year = hrd.Year;
            ViewBag.HRDID = id;
            var dt = GetHRDSummary(id);

            return View(dt);
        }

        private DataTable GetHRDSummary(int id)
        {
            var weightPref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).PreferedWeightMeasurment;
            var hrd = _hrdService.FindById(id);
            var hrdDetails = _hrdDetailService.Get(t => t.HRDID == id, null, "AdminUnit,AdminUnit.AdminUnit2,AdminUnit.AdminUnit2.AdminUnit2").ToList();
            var rationDetails = _rationDetailService.Get(t => t.RationID == hrd.RationID, null, "Commodity");
            var dt = HRDViewModelBinder.TransposeDataSummary(hrdDetails, rationDetails,weightPref);
            return dt;
        }

        private IEnumerable<RegionalSummaryViewModel> GetSummary(HRD hrd)
        {
            var details = hrd.HRDDetails;
            //var hrd = _hrdService.FindById(id);
            //details.First().HRD;
            var cerealCoefficient = hrd.Ration.RationDetails.First(m => m.Commodity.CommodityID == 1).Amount;
            var blendFoodCoefficient = hrd.Ration.RationDetails.First(m => m.Commodity.CommodityID == 2).Amount;
            var pulseCoefficient = hrd.Ration.RationDetails.First(m => m.Commodity.CommodityID == 3).Amount;
            var oilCoefficient = hrd.Ration.RationDetails.First(m => m.Commodity.CommodityID == 4).Amount;
            ViewBag.SeasonID = hrd.Season.Name;
            ViewBag.Year = hrd.Year;

            var groupedTotal = from detail in details
                               group detail by detail.AdminUnit.AdminUnit2.AdminUnit2 into regionalDetail
                               select new
                               {
                                   Region = regionalDetail.Key,
                                   NumberOfBeneficiaries = regionalDetail.Sum(m => m.NumberOfBeneficiaries),
                                   Duration = regionalDetail.Sum(m => (m.NumberOfBeneficiaries * m.DurationOfAssistance))
                               };

            return (from total in groupedTotal
                    select new RegionalSummaryViewModel
                        {
                            RegionName = total.Region.Name,
                            NumberOfBeneficiaries = total.NumberOfBeneficiaries,
                            Cereal = cerealCoefficient * total.Duration,
                            BlededFood = blendFoodCoefficient * total.Duration,
                            Oil = oilCoefficient * total.Duration,
                            Pulse = pulseCoefficient * total.Duration
                        });
        }
        [EarlyWarningAuthorize(operation = EarlyWarningConstants.Operation.View_HRD_Detail)]
        public ActionResult Detail(int id)
        {
          var preferedweight =  _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).PreferedWeightMeasurment;
            
            var hrd = _hrdService.Get(m => m.HRDID == id).FirstOrDefault();
            ViewBag.SeasonID = hrd.Season.Name;
            ViewBag.Year = hrd.Year;
            ViewBag.HRDID = id;
            var dt = GetTransposedHRD(id, preferedweight);
            return View(dt);
        }
        private DataTable GetTransposedHRD(int id, string preferedweight)
        {
           

            var hrd = _hrdService.FindById(id);
            var hrdDetails = _hrdDetailService.Get(t => t.HRDID == id, null, "AdminUnit,AdminUnit.AdminUnit2,AdminUnit.AdminUnit2.AdminUnit2").ToList();
            var rationDetails = _rationDetailService.Get(t => t.RationID == hrd.RationID, null, "Commodity");
            var dt = HRDViewModelBinder.TransposeData(hrdDetails, rationDetails,preferedweight);
            return dt;
        }


        private IEnumerable<HRDDetailViewModel> GetHRDDetails(HRD hrd)
        {
            var hrdDetails = hrd.HRDDetails;
            var rationDetails = _rationService.FindById(hrd.RationID).RationDetails;
            return (from hrdDetail in hrdDetails
                    select new HRDDetailViewModel()
                    {
                        HRDDetailID = hrdDetail.HRDDetailID,
                        HRDID = hrdDetail.HRDID,
                        WoredaID = hrdDetail.WoredaID,
                        Zone = hrdDetail.AdminUnit.AdminUnit2.Name,
                        Region = hrdDetail.AdminUnit.AdminUnit2.AdminUnit2.Name,
                        Woreda = hrdDetail.AdminUnit.Name,
                        NumberOfBeneficiaries = hrdDetail.NumberOfBeneficiaries,
                        //(int)GetTotalBeneficiaries(hrdDetail.HRDID, hrdDetail.AdminUnit.AdminUnit2.AdminUnit2.AdminUnitID),
                        StartingMonth = hrdDetail.StartingMonth,
                        DurationOfAssistance = hrdDetail.DurationOfAssistance,
                        //Cereal = (hrdDetail.DurationOfAssistance) * (hrdDetail.NumberOfBeneficiaries) * (rationDetails.Single(m => m.CommodityID == 1).Amount),
                        //Pulse = (hrdDetail.DurationOfAssistance) * (hrdDetail.NumberOfBeneficiaries) * (rationDetails.Single(m => m.CommodityID == 2).Amount),
                        //BlendedFood = (hrdDetail.DurationOfAssistance) * (hrdDetail.NumberOfBeneficiaries) * (rationDetails.Single(m => m.CommodityID == 3).Amount),
                        //Oil = (hrdDetail.DurationOfAssistance) * (hrdDetail.NumberOfBeneficiaries) * (rationDetails.Single(m => m.CommodityID == 4).Amount)


                    });
        }
        [EarlyWarningAuthorize(operation = EarlyWarningConstants.Operation.New_HRD)]
        public ActionResult Create()
        {
            var hrd = new HRD();
            // hrd.HRDDetails = new List<HRDDetail>();
            ViewBag.Year = DateTime.Today.Year;
            ViewBag.RationID = new SelectList(_rationService.GetAllRation(), "RationID", "RefrenceNumber", hrd.RationID = 1);
            ViewBag.NeedAssessmentID = new SelectList(_needAssessmentService.GetAllNeedAssessmentHeader().Where(m => m.NeedAssessment.NeedAApproved == true), "NAHeaderId",
                                                      "NeedACreatedDate");
            ViewBag.PlanID = new SelectList(_hrdService.GetPlans(), "PlanID", "PlanName");
            ViewBag.SeasonID = new SelectList(_seasonService.GetAllSeason(), "SeasonID", "Name");
            return View(hrd);
        }
        [EarlyWarningAuthorize(operation = EarlyWarningConstants.Operation.View_Ration_List)]
        public JsonResult GetRation()
        {


            var ration = _rationService.Get(t => t.IsDefaultRation, null, "RationDetails").FirstOrDefault();
            var rationViewModel = (from item in ration.RationDetails
                                   select new
                                              {
                                                  _commodityService.FindById(item.CommodityID).Name,
                                                  Value = item.Amount
                                              });
            return Json(rationViewModel, JsonRequestBehavior.AllowGet);
        }

        //update HRD detail information
        [AcceptVerbs(HttpVerbs.Post)]
        [EarlyWarningAuthorize(operation = EarlyWarningConstants.Operation.Modify_HRD)]
        public ActionResult HRDDetail_Update([DataSourceRequest] DataSourceRequest request, HRDDetailViewModel hrdDetails)
        {
            if (hrdDetails != null && ModelState.IsValid)
            {
                var detail = _hrdDetailService.FindById(hrdDetails.HRDDetailID);
                if (detail != null)
                {
                    detail.HRDID = hrdDetails.HRDID;
                    detail.DurationOfAssistance = hrdDetails.DurationOfAssistance;
                    detail.NumberOfBeneficiaries = hrdDetails.NumberOfBeneficiaries;
                    detail.StartingMonth = hrdDetails.StartingMonth;
                    detail.WoredaID = hrdDetails.WoredaID;

                    _hrdDetailService.EditHRDDetail(detail);
                }

            }
            return Json(new[] { hrdDetails }.ToDataSourceResult(request, ModelState));
            //return Json(ModelState.ToDataSourceResult());
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

        [HttpPost]
        [EarlyWarningAuthorize(operation = EarlyWarningConstants.Operation.New_HRD)]
        public ActionResult Create(HRD hrd)
        {
            //DateTime dateCreated = DateTime.Now;
            //DateTime DatePublished = DateTime.Now;

            //hrd.CreatedDate = dateCreated;
            //hrd.PublishedDate = DatePublished;
            //hrd.Status = 1;

            var year = hrd.Year;
            var userID = _needAssessmentService.GetUserProfileId(HttpContext.User.Identity.Name);
            var seasonID = hrd.SeasonID.HasValue ? hrd.SeasonID.Value:0;
            var rationID = hrd.RationID;
            

            var planName = hrd.Plan.PlanName;
            var startDate = hrd.Plan.StartDate;
            var endDate = hrd.Plan.EndDate;

            if (ModelState.IsValid)
            {
                if (startDate >= endDate)
                {
                    ModelState.AddModelError("Errors", @"Start Date Can't be greater than OR Equal to End Date!");
                }
                else
                {
                    try
                    {
                        _planService.AddHRDPlan(planName, startDate, endDate);
                        var plan = _planService.FindBy(m => m.PlanName == planName).FirstOrDefault();
                        var planID = plan.PlanID;
                        _hrdService.AddHRD(year, userID, seasonID, rationID, planID);
                        return RedirectToAction("Index");
                    }
                    catch (Exception exception)
                    {
                        var log = new Logger();
                        log.LogAllErrorsMesseges(exception, _log);
                        ModelState.AddModelError("Errors", "Unable To Create New HRD");
                        //ViewBag.Error = "HRD for this Season and Year already Exists";
                    }
                }

            }

            ViewBag.Year = hrd.Year;
            ViewBag.RationID = new SelectList(_rationService.GetAllRation(), "RationID", "RefrenceNumber", hrd.RationID = 1);
            ViewBag.SeasonID = new SelectList(_seasonService.GetAllSeason(), "SeasonID", "Name");
            return View(hrd);
        }
        //HRD/Edit/2
        [EarlyWarningAuthorize(operation = EarlyWarningConstants.Operation.Modify_HRD)]
        public ActionResult Edit(int id)
        {
            var hrd = _hrdService.Get(m => m.HRDID == id, null, "HRDDetails").FirstOrDefault();
            ViewBag.SeasonID = new SelectList(_seasonService.GetAllSeason(), "SeasonID", "Name", hrd.SeasonID);
            ViewBag.RationID = new SelectList(_rationService.GetAllRation(), "RationID", "RefrenceNumber", hrd.RationID);
            //ViewBag.NeedAssessmentID = new SelectList(_needAssessmentService.GetAllNeedAssessmentHeader(), "NAHeaderId", "NeedACreatedDate", hrd.NeedAssessmentID);


            return View(hrd);
        }

        [HttpPost]
        [EarlyWarningAuthorize(operation = EarlyWarningConstants.Operation.Modify_HRD)]
        public ActionResult Edit(HRD hrd)
        {
            var userid = UserAccountHelper.GetUser(HttpContext.User.Identity.Name).UserProfileID;
            hrd.CreatedBY = userid;
            if (ModelState.IsValid)
            {
                _hrdService.EditHRD(hrd);
                return RedirectToAction("Index");
            }

            return View(hrd);
        }
        [EarlyWarningAuthorize(operation = EarlyWarningConstants.Operation.Print_HRD)]
        public ActionResult Print()
        {
            var allHrd = _hrdService.GetAllHRD();
            var hrdViewModel = GetHrds(allHrd);
            return View();//ViewPdf("HRD report", "Print", hrdViewModel);
        }
        [EarlyWarningAuthorize(operation = EarlyWarningConstants.Operation.Print_HRD)]
        public ActionResult PrintSummary(int? id)
        {
            if (id == null)
            {
                RedirectToAction("Index");
            }
            var hrd = _hrdService.Get(m => m.HRDID == id, null, "HRDDetails,Season").FirstOrDefault();

            if (hrd == null) RedirectToAction("Index");
            var season = hrd.Season.Name;
            var year = hrd.Year;

            var reportPath = Server.MapPath("~/Report/HRD/HRDSummaryByRegion.rdlc");
            var reportData = (from item in GetSummary(hrd).ToList() 
                              select new
                                  {
                                      item.BlededFood,
                                      item.Cereal, 
                                      item.DurationOfAssistance,
                                      item.HRDID,
                                      item.NumberOfBeneficiaries,
                                      item.Oil, 
                                      item.Pulse, 
                                      item.RegionName,
                                      item.Total,
                                      Season = season,
                                      Year = year
                                  });

            var dataSourceName = "hrdsummarybyregion";
            var result = ReportHelper.PrintReport(reportPath, reportData, dataSourceName);
            return File(result.RenderBytes, result.MimeType);
        }
        [EarlyWarningAuthorize(operation = EarlyWarningConstants.Operation.View_Approved_HRD)]
        public ActionResult ApproveHRD(int id)
        {
            var hrd = _hrdService.FindById(id);
            hrd.Status = 2;
            _hrdService.EditHRD(hrd);
            return RedirectToAction("Index");
        }
        
        public ActionResult PublishHRD(int id)
        {
            _hrdService.PublishHrd(id);
            return RedirectToAction("ApprovedHRDs");
        }
        [EarlyWarningAuthorize(operation = EarlyWarningConstants.Operation.Compare_HRD)]
        public ActionResult Compare()
        {
            var hrds = _hrdService.Get(null, null, "Season");
            var hrds1 =
                (from item in hrds
                 select new { item.HRDID, Name = string.Format("{0}-{1}", item.Season.Name, item.Year) }).ToList();
            ViewBag.firstHrd = new SelectList(hrds1, "HRDID", "Name");
            ViewBag.secondHrd = new SelectList(hrds1, "HRDID", "Name");
            ViewBag.regionId = new SelectList(_adminUnitService.GetRegions(), "AdminUnitID", "Name");

            return View();
        }


        [HttpPost]
        [EarlyWarningAuthorize(operation = EarlyWarningConstants.Operation.Compare_HRD)]
        public ActionResult Compare_HRD([DataSourceRequest] DataSourceRequest request, int? firstHrd, int? secondHrd, int? regionId)
        {
            int hrd1Id = firstHrd ?? 0;
            int hrd2Id = secondHrd ?? 0;
            int regionid = regionId ?? 0;

            var hrdFirst = _hrdService.Get(t => t.HRDID == hrd1Id, null,
                                      "Season,HRDDetails,HRDDetails.AdminUnit,HRDDetails.AdminUnit.AdminUnit2,HRDDetails.AdminUnit.AdminUnit2.AdminUnit2").FirstOrDefault();
            var hrdSecond = _hrdService.Get(t => t.HRDID == hrd2Id).FirstOrDefault();
            var hrdsViewModel = HRDViewModelBinder.BindHRDCompareViewModel(hrdFirst, hrdSecond, regionid).OrderBy(t => t.Zone);


            var hrds = _hrdService.Get(null, null, "Season");
            var hrds1 =
                (from item in hrds
                 select new { item.HRDID, Name = string.Format("{0}-{1}", item.Season.Name, item.Year) }).ToList();
            ViewBag.firstHrd = new SelectList(hrds1, "HRDID", "Name");
            ViewBag.secondHrd = new SelectList(hrds1, "HRDID", "Name");
            ViewBag.redionId = new SelectList(_adminUnitService.GetRegions(), "AdminUnitID", "Name");
            return Json(hrdsViewModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

        }
        [EarlyWarningAuthorize(operation = EarlyWarningConstants.Operation.New_HRD)]
        public ActionResult CreateHRD(int id)
        {
            var plan = _hrdService.GetPlan(id);
            var hrd = new HRD();
            ViewBag.Year = DateTime.Today.Year;
            ViewBag.RationID = new SelectList(_rationService.GetAllRation(), "RationID", "RefrenceNumber");
            ViewBag.SeasonID = new SelectList(_seasonService.GetAllSeason(), "SeasonID", "Name");
            hrd.PlanID = plan.PlanID;
            return View(hrd);
        }
        [HttpPost]
        [EarlyWarningAuthorize(operation = EarlyWarningConstants.Operation.New_HRD)]
        public ActionResult CreateHRD(HRD hrd)
        {
            DateTime dateCreated = DateTime.Now;
            DateTime DatePublished = DateTime.Now;

            hrd.CreatedDate = dateCreated;
            hrd.PublishedDate = DatePublished;
            hrd.Status = 1;

            if (ModelState.IsValid)
            {
                try
                {
                    var userid = _needAssessmentService.GetUserProfileId(HttpContext.User.Identity.Name);
                    var woredas = _adminUnitService.FindBy(m => m.AdminUnitTypeID == 4);
                    hrd.CreatedBY = userid;
                    var hrdDetails = (from detail in woredas
                                      select new HRDDetail
                                          {
                                              WoredaID = detail.AdminUnitID,
                                              StartingMonth = 1,
                                              NumberOfBeneficiaries =
                                                  _needAssessmentDetailService.GetNeedAssessmentBeneficiaryNoFromPlan(
                                                      hrd.PlanID, detail.AdminUnitID),
                                              DurationOfAssistance =
                                                  _needAssessmentDetailService.GetNeedAssessmentMonthsFromPlan(
                                                      hrd.PlanID, detail.AdminUnitID)

                                          }).ToList();

                    hrd.HRDDetails = hrdDetails;
                    _hrdService.AddHRDFromAssessment(hrd);
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    var log = new Logger();
                    log.LogAllErrorsMesseges(exception, _log);
                    ModelState.AddModelError("Errors", @"Unable to create hrd form the given need Assessment");
                    //ViewBag.Error = "HRD for this Season and Year already Exists";
                }
            }
            ViewBag.Year = hrd.Year;
            ViewBag.RationID = new SelectList(_rationService.GetAllRation(), "RationID", "RefrenceNumber");
            ViewBag.SeasonID = new SelectList(_seasonService.GetAllSeason(), "SeasonID", "Name");
            return View(hrd);
            }
        public ActionResult HRDComaparison_Read([DataSourceRequest] DataSourceRequest request, int? firstHrd)
        {
            var hrdid = firstHrd ?? 0;
            var hrd = _hrdService.FindById(hrdid);
            var hrdToDisplay = CompareHRDSummary(hrd);
            if (hrdToDisplay == null)
            {
                var compareHrdViewModel = new List<CompareHrdViewModel>();
                return Json(compareHrdViewModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            return Json(hrdToDisplay.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult HRDComaparisonTo_Read([DataSourceRequest] DataSourceRequest request, int? secondHrd)
        {
            var hrdid = secondHrd ?? 0;
            var hrd = _hrdService.FindById(hrdid);
            var hrdToDisplay = CompareHRDSummary(hrd);
            if (hrdToDisplay==null)
            {
                var compareHrdViewModel = new List<CompareHrdViewModel>();
                return Json(compareHrdViewModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            return Json(hrdToDisplay.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        private IEnumerable<CompareHrdViewModel>  CompareHRDSummary(HRD hrd)
        {
            if (hrd!=null)
            {
                var totalBeneficary =1;
                if ( hrd.HRDDetails.Sum(m => m.NumberOfBeneficiaries)>0)
                {
                    totalBeneficary = hrd.HRDDetails.Sum(m => m.NumberOfBeneficiaries);
                }
                  var groupedTotal = from detail in hrd.HRDDetails
                               group detail by detail.AdminUnit.AdminUnit2.AdminUnit2 into regionalDetail
                               select new
                               {
                                   Region = regionalDetail.Key,
                                   NumberOfBeneficiaries = regionalDetail.Sum(m => m.NumberOfBeneficiaries)
                               };
                   return (from total in groupedTotal
                    select new CompareHrdViewModel
                        {
                            Region = total.Region.Name,
                            BeneficiaryNumber = total.NumberOfBeneficiaries,
                            Percentage = (total.NumberOfBeneficiaries/totalBeneficary) *100
                        });
                
            }
            return null;
        }
    }
}
