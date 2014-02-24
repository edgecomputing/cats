using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Cats.Areas.EarlyWarning.Models;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Models.ViewModels;
using Cats.Services.Common;
using Cats.Services.EarlyWarning;
using Cats.Services.PSNP;
using Cats.Helpers;
using Cats.Services.Security;
using Cats.ViewModelBinder;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using log4net;
using Cats.Services.Administration;
using IAdminUnitService = Cats.Services.EarlyWarning.IAdminUnitService;
using IFDPService = Cats.Services.EarlyWarning.IFDPService;
using Workflow = Cats.Models.Constant.WORKFLOW;


namespace Cats.Areas.EarlyWarning.Controllers
{
    public class RequestController : Controller
    {
        //
        // GET: /EarlyWarning/RegionalRequest/

        private IRegionalRequestService _regionalRequestService;
        private IFDPService _fdpService;
        private IUserAccountService _userAccountService;
        private IRegionalRequestDetailService _regionalRequestDetailService;
        private ICommonService _commonService;
        private IHRDService _hrdService;
        private IHRDDetailService _HRDDetailService;
        private IApplicationSettingService _applicationSettingService;
        private readonly ILog _log;
        private readonly IRegionalPSNPPlanDetailService _RegionalPSNPPlanDetailService;
        private readonly IRegionalPSNPPlanService _RegionalPSNPPlanService;
        private readonly IAdminUnitService _adminUnitService;
        private readonly IPlanService _planService;
        private readonly IIDPSReasonTypeServices _idpsReasonTypeServices;
        public RequestController(IRegionalRequestService reliefRequistionService,
                                IFDPService fdpService,
                                IRegionalRequestDetailService reliefRequisitionDetailService,
                                ICommonService commonService,
                                IHRDService hrdService,
                                IApplicationSettingService ApplicationSettingService,
                                IUserAccountService userAccountService,
                                ILog log,
                                IHRDDetailService hrdDetailService,
                                IRegionalPSNPPlanDetailService regionalPSNPPlanDetailService,
                                IRegionalPSNPPlanService RegionalPSNPPlanService, 
            IAdminUnitService adminUnitService, 
            IPlanService planService, 
            IIDPSReasonTypeServices idpsReasonTypeServices)
        {
            _regionalRequestService = reliefRequistionService;
            _fdpService = fdpService;
            _regionalRequestDetailService = reliefRequisitionDetailService;
            _commonService = commonService;
            _hrdService = hrdService;
            _applicationSettingService = ApplicationSettingService;
            _userAccountService = userAccountService;
            _log = log;
            _HRDDetailService = hrdDetailService;
            _RegionalPSNPPlanDetailService = regionalPSNPPlanDetailService;
            _RegionalPSNPPlanService = RegionalPSNPPlanService;
            _adminUnitService = adminUnitService;
            _planService = planService;
            _idpsReasonTypeServices = idpsReasonTypeServices;
           
        }
        public  ActionResult RegionalRequestsPieChart()
        {
            return View();
        }

        public ViewResult SubmittedRequest(int id)
        {
            ViewBag.Months = new SelectList(RequestHelper.GetMonthList(), "Id", "Name");
            ViewBag.RegionID = new SelectList(_commonService.GetAminUnits(t => t.AdminUnitTypeID == 2), "AdminUnitID", "Name");
            ViewBag.Status = new SelectList(_commonService.GetStatus(Workflow.REGIONAL_REQUEST), "StatusID",
                                            "Description");

            var requests = _regionalRequestService.Get(t => t.Status == id, null, "AdminUnit,Program");
            var statuses = _commonService.GetStatus(WORKFLOW.REGIONAL_REQUEST);
            var userPref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            return View(RequestViewModelBinder.BindRegionalRequestListViewModel(requests, statuses, userPref));
        }


        [HttpGet]
        public ActionResult New()
        {
            PopulateLookup();
            
            ViewBag.SeasonID = new SelectList(_commonService.GetSeasons(), "SeasonID", "Name");
            return View();
        }

        private RegionalRequest CretaeRegionalRequest(HRDPSNPPlanInfo hrdpsnpPlanInfo)
        {
            var regionalRequest = new RegionalRequest();

            regionalRequest.Status = (int)RegionalRequestStatus.Draft;
            regionalRequest.RequistionDate = DateTime.Today;
            regionalRequest.Year = hrdpsnpPlanInfo.HRDPSNPPlan.Year;

            regionalRequest.PlanID = hrdpsnpPlanInfo.HRDPSNPPlan.PlanID;
            
            if (hrdpsnpPlanInfo.HRDPSNPPlan.ProgramID == 2)
            {
                regionalRequest.PlanID = hrdpsnpPlanInfo.HRDPSNPPlan.PSNPPlanID;
            }

            if (hrdpsnpPlanInfo.HRDPSNPPlan.SeasonID.HasValue)
                regionalRequest.Season = hrdpsnpPlanInfo.HRDPSNPPlan.SeasonID.Value;
            regionalRequest.Month = hrdpsnpPlanInfo.HRDPSNPPlan.Month;
            regionalRequest.RegionID = hrdpsnpPlanInfo.HRDPSNPPlan.RegionID;
            regionalRequest.ProgramId = hrdpsnpPlanInfo.HRDPSNPPlan.ProgramID;
            regionalRequest.DonorID = hrdpsnpPlanInfo.HRDPSNPPlan.DonorID;
            regionalRequest.RationID = hrdpsnpPlanInfo.HRDPSNPPlan.RationID.HasValue ? hrdpsnpPlanInfo.HRDPSNPPlan.RationID.Value : _applicationSettingService.getDefaultRation();
            regionalRequest.Round = hrdpsnpPlanInfo.HRDPSNPPlan.Round;
            regionalRequest.RegionalRequestDetails = (from item in hrdpsnpPlanInfo.BeneficiaryInfos
                                                      where item.Selected == false
                                                      select new RegionalRequestDetail()
                                                                 {
                                                                     Beneficiaries = item.Beneficiaries,
                                                                     Fdpid = item.FDPID
                                                                 }).ToList();
            _regionalRequestService.AddRegionalRequest(regionalRequest);

            return regionalRequest;
        }

        private RegionalRequest CreateRegionalRequest(HRDPSNPPlanInfo hrdpsnpPlanInfo, FormCollection collection, int planid, int reasonTypeID)
        {

            int regionId = Convert.ToInt32(collection["RegionId"].ToString(CultureInfo.InvariantCulture));
            var programId = 3;
           
            
            var regionalRequest = new RegionalRequest
                                      {
                                          Status = (int) RegionalRequestStatus.Draft,
                                          RequistionDate = DateTime.Today,
                                          Year = DateTime.Now.Year,
                                          PlanID = planid,
                                          Season = 1,
                                          Month = DateTime.Now.Month,
                                          RegionID = regionId,
                                          ProgramId = programId,
                                          DonorID = null,
                                          RationID = hrdpsnpPlanInfo.HRDPSNPPlan.RationID.HasValue ? hrdpsnpPlanInfo.HRDPSNPPlan.RationID.Value : _applicationSettingService.getDefaultRation(),
                                          Round = null,
                                          IDPSReasonType = reasonTypeID,
                                          RegionalRequestDetails = (from item in hrdpsnpPlanInfo.BeneficiaryInfos
                                                                    where item.Selected == false
                                                                    select new RegionalRequestDetail()
                                                                               {
                                                                                   Beneficiaries = item.Beneficiaries,
                                                                                   Fdpid = item.FDPID
                                                                               }).ToList()
                                      };

            _regionalRequestService.AddRegionalRequest(regionalRequest);

            return regionalRequest;
        }
        private void PopulateLookup()
        {
            ViewBag.RegionID = new SelectList(_commonService.GetAminUnits(t => t.AdminUnitTypeID == 2), "AdminUnitID", "Name");
            ViewBag.ProgramId = new SelectList(_commonService.GetPrograms().Take(2), "ProgramID", "Name");
            ViewBag.Month = new SelectList(RequestHelper.GetMonthList(), "ID", "Name");
            ViewBag.RationID = new SelectList(_commonService.GetRations(), "RationID", "RefrenceNumber");
            ViewBag.DonorID = new SelectList(_commonService.GetDonors(), "DonorId", "Name");
            ViewBag.Round = new SelectList(RequestHelper.GetMonthList(), "ID", "ID");
            ViewBag.PlanID = new SelectList(_commonService.GetPlan(1), "PlanID", "PlanName");
            ViewBag.PSNPPlanID = new SelectList(_commonService.GetPlan(2), "PlanID", "PlanName");
            ViewBag.SeasonID = new SelectList(_commonService.GetSeasons(), "SeasonID", "Name");

            List<RequestStatus> statuslist = new List<RequestStatus>();

            statuslist.Add(new RequestStatus { StatusID = 1, StatusName="Draft" });
            statuslist.Add(new RequestStatus { StatusID = 2, StatusName = "Approved" });
            statuslist.Add(new RequestStatus { StatusID = 3, StatusName = "Closed" });
            statuslist.Add(new RequestStatus { StatusID = 4, StatusName = "FederalApproved" });

            ViewBag.StatusID = new SelectList(statuslist, "StatusID", "StatusName");

        }
        private void PopulateLookup(RegionalRequest regionalRequest)
        {
            ViewBag.RegionID = new SelectList(_commonService.GetAminUnits(t => t.AdminUnitTypeID == 2), "AdminUnitID", "Name", regionalRequest.RegionID);
            ViewBag.ProgramId = new SelectList(_commonService.GetPrograms(), "ProgramID", "Name", regionalRequest.ProgramId);
            ViewBag.Month = new SelectList(RequestHelper.GetMonthList(), "ID", "Name", regionalRequest.Month);
            ViewBag.RationID = new SelectList(_commonService.GetRations(), "RationID", "RefrenceNumber", regionalRequest.RationID);
            //ViewBag.PlanID = new SelectList(_commonService.GetPlan(), "PlanID", "PlanName", regionalRequest.PlanID);
        }
        //
        // GET: /ReliefRequisitoin/Details/5


        [HttpGet]
        public ActionResult Edit(int id)
        {

            var regionalRequest =
                _regionalRequestService.Get(t => t.RegionalRequestID == id, null,
                                            "RegionalRequestDetails,RegionalRequestDetails.Fdp," +
                                            "RegionalRequestDetails.Fdp.AdminUnit,RegionalRequestDetails.Fdp.AdminUnit.AdminUnit2")
                    .
                    FirstOrDefault();
            if (regionalRequest == null)
            {
                return HttpNotFound();
            }
            PopulateLookup(regionalRequest);
            return View(regionalRequest);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(RegionalRequest regionalRequest)
        {
            var requId = 0;
            if (regionalRequest != null && ModelState.IsValid)
            {
                try
                {
                    var target = _regionalRequestService.Get(t => t.RegionalRequestID == regionalRequest.RegionalRequestID, null, "AdminUnit,Program").FirstOrDefault();
                    RequestViewModelBinder.BindRegionalRequest(regionalRequest, target);

                    _regionalRequestService.EditRegionalRequest(target);
                    ModelState.AddModelError("Success", "Regional Request updated successfully.");
                    return RedirectToAction("Details", "Request", new { id = regionalRequest.RegionalRequestID });
                }
                catch (Exception ex)
                {
                    _log.Error(ex);

                }

            }

            PopulateLookup(regionalRequest);
            return View(regionalRequest);
        }

        public ActionResult ApproveRequest(int id)
        {
            _regionalRequestService.ApproveRequest(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult New(HRDPSNPPlan hrdpsnpPlan, FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                HRDPSNPPlanInfo psnphrdPlanInfo = _regionalRequestService.PlanToRequest(hrdpsnpPlan);
                if (psnphrdPlanInfo != null)
                {
                    var exisiting = _regionalRequestService.FindBy(r => r.PlanID == psnphrdPlanInfo.HRDPSNPPlan.PSNPPlanID
                                                                        &&
                                                                        r.ProgramId ==
                                                                        psnphrdPlanInfo.HRDPSNPPlan.ProgramID && r.RegionID==psnphrdPlanInfo.HRDPSNPPlan.RegionID
                                                                        && r.Year == psnphrdPlanInfo.HRDPSNPPlan.Year
                                                                        && r.Month == psnphrdPlanInfo.HRDPSNPPlan.Month)
                        .Count;

                    if (exisiting == 0)
                    {
                        RegionalRequest req = CretaeRegionalRequest(psnphrdPlanInfo);
                        var model = getRequestDetai(req.RegionalRequestID);
                        ViewBag.message = "Request Created";
                        //RedirectToAction(@)
                        return RedirectToAction("Details" + "/" + req.RegionalRequestID);
                    }
                    else
                    {
                        ModelState.AddModelError("Errors", @"A request with the same parameters has already been made");
                    }

                }
                else
                {
                    ModelState.AddModelError("Errors", @"Can Not Create Request! Duration of Assistance for this region is Completed ");
                }
               
            }
            ViewBag.SeasonID = new SelectList(_commonService.GetSeasons(), "SeasonID", "Name");
            PopulateLookup();
            return View(hrdpsnpPlan);
        }


        [HttpGet]
        public ActionResult NewIdps()
        {
            ViewBag.RationID = new SelectList(_commonService.GetRations(), "RationID", "RefrenceNumber");
            ViewBag.RegionID = new SelectList(_adminUnitService.GetRegions(), "AdminUnitID", "Name",0);
            ViewBag.IDPSReasonType = new SelectList(_idpsReasonTypeServices.GetAllIDPSReasonType(), "IDPSId", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult  NewIdps(HRDPSNPPlan hrdpsnpPlan,FormCollection collection)
        {

            Plan plan = null;

            var reasonTypeID = int.Parse(collection["ReasonType"].ToString(CultureInfo.InvariantCulture));

            DateTime startDate = DateTime.Now.Date;
            DateTime endDate = DateTime.Now.Add(new TimeSpan(DateTime.Now.Year));
            string planName = string.Format("IDPS - {0}", startDate.Date.Year.ToString(CultureInfo.InvariantCulture) + endDate.Date.Year.ToString(CultureInfo.InvariantCulture));
            try
            {
                _planService.AddPlan(planName, startDate, endDate);
                plan = _planService.Get(p => p.PlanName == planName).Single();
            }
            catch (Exception)
            {
            }

            HRDPSNPPlanInfo psnphrdPlanInfo = _regionalRequestService.PlanToRequest(hrdpsnpPlan);
            RegionalRequest req = CreateRegionalRequest(psnphrdPlanInfo, collection, plan.PlanID, reasonTypeID);

            return RedirectToAction("Allocation", new { id = req.RegionalRequestID, programid = 3 });

        }

        /*
        [HttpPost]
        public ActionResult New(HRDPSNPPlan hrdpsnpPlan)
        {
            if (ModelState.IsValid)
            {
                var psnphrdPlanInfo = _regionalRequestService.PlanToRequest(hrdpsnpPlan);
                return View("PreparePlan", psnphrdPlanInfo);
            }
            ViewBag.SeasonID = new SelectList(_commonService.GetSeasons(), "SeasonID", "Name");
            PopulateLookup();
            return View(hrdpsnpPlan);
        }
        */
        public ActionResult PreparePlan(HRDPSNPPlanInfo psnphrdPlanInfo)
        {
           
            return View(psnphrdPlanInfo);
        }
        [HttpGet]
        public ActionResult RequestFromPlan()
        {
            return RedirectToAction("New");
        }
        [HttpPost]
        public ActionResult RequestFromPlan(HRDPSNPPlanInfo psnphrdPlanInfo)
        {
            RegionalRequest req=CretaeRegionalRequest(psnphrdPlanInfo);
            var model = getRequestDetai(req.RegionalRequestID);
            ViewBag.message = "Request Created";
            return View("Details",model);
        }

        #region Regional Request Detail


        public ActionResult Allocation(int id,int programId = -1)
        {
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            ViewBag.programId = programId;
            ViewBag.RequestID = id;
            var request =
                _regionalRequestService.Get(t => t.RegionalRequestID == id, null, "AdminUnit,Program,Ration").FirstOrDefault();
            var statuses = _commonService.GetStatus(WORKFLOW.REGIONAL_REQUEST);
            var requestModelView = RequestViewModelBinder.BindRegionalRequestViewModel(request, statuses, datePref);
            var requestDetails = _regionalRequestDetailService.Get(t => t.RegionalRequestID == id);
            var requestDetailCommodities = (from item in requestDetails select item.RequestDetailCommodities).FirstOrDefault();
            if (requestDetailCommodities != null)
                ViewData["AllocatedCommodities"] = (from itm in requestDetailCommodities select new Commodity() { CommodityID = itm.CommodityID });
            ViewData["AvailableCommodities"] = _commonService.GetCommodities();

            return View(requestModelView);
        }

        public object getRequestDetai(int id)
        {
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            //  datePref = "gc";
            var request =
               _regionalRequestService.Get(t => t.RegionalRequestID == id, null, "AdminUnit,Program,Ration").FirstOrDefault();

            if (request == null)
            {
                return HttpNotFound();
            }
            var statuses = _commonService.GetStatus(WORKFLOW.REGIONAL_REQUEST);
            var requestModelView = RequestViewModelBinder.BindRegionalRequestViewModel(request, statuses, datePref);

            var requestDetails = _regionalRequestDetailService.Get(t => t.RegionalRequestID == id, null, "RequestDetailCommodities,RequestDetailCommodities.Commodity").ToList();
            var dt = RequestViewModelBinder.TransposeData(requestDetails);
            ViewData["Request_main_data"] = requestModelView;
            return dt;
        }

        public ActionResult Details(int id)
        {
            ViewBag.RequestID = id;
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            var request =
               _regionalRequestService.Get(t => t.RegionalRequestID == id, null, "AdminUnit,Program,Ration").FirstOrDefault();

            if (request == null)
            {
                return HttpNotFound();
            }
            var statuses = _commonService.GetStatus(WORKFLOW.REGIONAL_REQUEST);
            var requestModelView = RequestViewModelBinder.BindRegionalRequestViewModel(request, statuses, datePref);
            
            //var requestDetails = _regionalRequestDetailService.Get(t => t.RegionalRequestID == id, null, "RequestDetailCommodities,RequestDetailCommodities.Commodity").ToList();

            var result = GetRequestWithPlan(request);
            //var dt = RequestViewModelBinder.TransposeData(requestDetails);
            var dt = RequestViewModelBinder.TransposeDataNew(result,request.ProgramId);
            ViewData["Request_main_data"] = requestModelView;
            return View(dt);
        }

       public ActionResult Details_Read([DataSourceRequest] DataSourceRequest request, int id)
        {
            ViewBag.RequestID = id;
            var requestDetails = _regionalRequestDetailService.Get(t => t.RegionalRequestID == id, null, "FDP,FDP.AdminUnit,FDP.AdminUnit.AdminUnit2,RequestDetailCommodities,RequestDetailCommodities.Commodity").ToList();
            var dt = RequestViewModelBinder.TransposeData(requestDetails);
            return Json(dt.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        private void Details_Read_IDPS([DataSourceRequest] DataSourceRequest request, int regionId)
        {
            ViewBag.regionId = regionId;
            var zones = _adminUnitService.GetZones(regionId);
            var woredas = new List<AdminUnit>();
            var fdps = new List<FDP>();

            foreach (var zone in zones)
            {
                woredas.AddRange(_adminUnitService.GetWoreda(zone.AdminUnitID));
            }
            foreach (var woreda in woredas)
            {
                fdps.AddRange(_fdpService.GetAllFDP().Where(f=>f.AdminUnitID ==  woreda.AdminUnitID));
            }
            

        }
       
        public ActionResult Allocation_Read([DataSourceRequest] DataSourceRequest request, int id)
        {

            var requestDetails = _regionalRequestDetailService.FindBy(t => t.RegionalRequestID == id);
            var requestDetailViewModels = (from dtl in requestDetails select BindRegionalRequestDetailViewModel(dtl));
            return Json(requestDetailViewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        
        private RegionalRequestDetailViewModel BindRegionalRequestDetailViewModel(RegionalRequestDetail regionalRequestDetail)
        {
            if (regionalRequestDetail.RegionalRequest.Program.Name == "Relief")
            {
                return new RegionalRequestDetailViewModel()
                {
                    Beneficiaries = regionalRequestDetail.Beneficiaries,
                    FDP = regionalRequestDetail.Fdp.Name,
                    Fdpid = regionalRequestDetail.Fdpid,
                    RegionalRequestID = regionalRequestDetail.RegionalRequestID,
                    RegionalRequestDetailID = regionalRequestDetail.RegionalRequestDetailID,
                    Woreda = regionalRequestDetail.Fdp.AdminUnit.Name,
                    WoredaId = regionalRequestDetail.Fdp.AdminUnit.AdminUnitID,
                    Zone = regionalRequestDetail.Fdp.AdminUnit.AdminUnit2.Name,
                    //PlannedBeneficiaries = GetPlanned(regionalRequestDetail.RegionalRequest.Year,
                        //(int)regionalRequestDetail.RegionalRequest.Season,
                        //regionalRequestDetail.Fdp.AdminUnit.AdminUnitID)
                };
            }
            else
            {
                return new RegionalRequestDetailViewModel()
                {
                    Beneficiaries = regionalRequestDetail.Beneficiaries,
                    FDP = regionalRequestDetail.Fdp.Name,
                    Fdpid = regionalRequestDetail.Fdpid,
                    RegionalRequestID = regionalRequestDetail.RegionalRequestID,
                    RegionalRequestDetailID = regionalRequestDetail.RegionalRequestDetailID,
                    Woreda = regionalRequestDetail.Fdp.AdminUnit.Name,
                    Zone = regionalRequestDetail.Fdp.AdminUnit.AdminUnit2.Name,
                    PlannedBeneficiaries = GetPlannedForPSNP(regionalRequestDetail.RegionalRequest.Year,
                        regionalRequestDetail.RegionalRequest.RegionID,
                        regionalRequestDetail.Fdpid)
                };
            }
        }

        private int GetPlanned(int year, int season, int woreda)
        {
            var HRD = _HRDDetailService.Get(t => t.HRD.Season.SeasonID == season && t.HRD.Year == year && t.WoredaID == woreda).SingleOrDefault();
            if (HRD != null)
            {
                return HRD.NumberOfBeneficiaries;
            }
            else return 0;
        }
        
        private int GetPlannedForPSNP(int year, int regionId, int fdpId)
        {
            RegionalPSNPPlanDetail psnp = null;
            try
            {

                psnp = _RegionalPSNPPlanDetailService.Get(
                    p =>
                    p.RegionalPSNPPlan.Year == year  && p.PlanedFDPID == fdpId)
                    .SingleOrDefault();

            }catch (Exception)
            {
                
            }
            

            if (psnp != null)
            {
                return psnp.BeneficiaryCount;
            }
            else return 0;
        }
        private RegionalRequestDetail BindRegionalRequestDetail(RegionalRequestDetailViewModel regionalRequestDetailViewModel)
        {
            return new RegionalRequestDetail()
                               {
                                   Beneficiaries = regionalRequestDetailViewModel.Beneficiaries,
                                   Fdpid = regionalRequestDetailViewModel.Fdpid,
                                   RegionalRequestID = regionalRequestDetailViewModel.RegionalRequestID,
                                   RegionalRequestDetailID = regionalRequestDetailViewModel.RegionalRequestDetailID
                               };
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Allocation_Create([DataSourceRequest] DataSourceRequest request, RegionalRequestDetailViewModel regionalRequestDetailViewModel)
        {
            if (regionalRequestDetailViewModel != null && ModelState.IsValid)
            {
                _regionalRequestDetailService.AddRegionalRequestDetail(BindRegionalRequestDetail(regionalRequestDetailViewModel));
            }

            return Json(new[] { regionalRequestDetailViewModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Allocation_Update([DataSourceRequest] DataSourceRequest request, RegionalRequestDetailViewModel regionalRequestDetail)
        {
            if (regionalRequestDetail != null && ModelState.IsValid)
            {
                var target = _regionalRequestDetailService.FindById(regionalRequestDetail.RegionalRequestDetailID);
                if (target != null)
                {
                    target.Beneficiaries = regionalRequestDetail.Beneficiaries;
                    _regionalRequestDetailService.EditRegionalRequestDetail(target);
                }
            }

            return Json(new[] { regionalRequestDetail }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Allocation_Destroy([DataSourceRequest] DataSourceRequest request,
                                                  RegionalRequestDetail regionalRequestDetail)
        {
            if (regionalRequestDetail != null)
            {
                _regionalRequestDetailService.DeleteById(regionalRequestDetail.RegionalRequestDetailID);
            }

            return Json(ModelState.ToDataSourceResult());
        }

        public ActionResult Commodity_Read([DataSourceRequest] DataSourceRequest request, int id)
        {
            IEnumerable<RequestDetailCommodityViewModel> commodities = new List<RequestDetailCommodityViewModel>();
            var requestDetails = _regionalRequestDetailService.Get(t => t.RegionalRequestID == id);
            var requestDetailCommodities = (from item in requestDetails select item.RequestDetailCommodities).FirstOrDefault();
            
            if (requestDetailCommodities!=null)
                commodities = (from itm in requestDetailCommodities select new RequestDetailCommodityViewModel() { CommodityID = itm.CommodityID,Commodity = itm.Commodity.Name, RequestDetailCommodityID = itm.RequestCommodityID });
            ViewData["AvailableCommodities"] = _commonService.GetCommodities();

            return Json(commodities.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Commodity_Create([DataSourceRequest] DataSourceRequest request, RequestDetailCommodityViewModel commodity, int id)
        {
            if (commodity != null && ModelState.IsValid)
            {
                var user = UserAccountHelper.GetCurrentUser();
                var currentUnit = user.PreferedWeightMeasurment;

                //try
                //{
                _regionalRequestDetailService.AddRequestDetailCommodity(commodity.CommodityID, id, currentUnit);
                //}
                //catch(Exception ex)
                //{

                //}
            }

            return Json(new[] { commodity }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Commodity_Update([DataSourceRequest] DataSourceRequest request, RequestDetailCommodityViewModel commodity)
        {
            if (commodity != null && ModelState.IsValid)
            {
                var target = _regionalRequestDetailService.UpdateRequestDetailCommodity(commodity.CommodityID,
                                                                                      commodity.RequestDetailCommodityID);
            }

            return Json(new[] { commodity }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Commodity_Destroy([DataSourceRequest] DataSourceRequest request,
                                                  RequestDetailCommodityViewModel commodity, int id)
        {
            if (commodity != null)
            {
                _regionalRequestDetailService.DeleteRequestDetailCommodity(commodity.CommodityID, id);
            }

            return Json(ModelState.ToDataSourceResult());
        }

        #endregion


        #region Reguest

        /*
        public ActionResult Index(int id = -1)
        {
            var regions = _commonService.GetAminUnits(t => t.AdminUnitTypeID == 2);
            ViewBag.RegionID = new SelectList(regions, "AdminUnitID", "Name");
            //ViewData["adminunits"] = regions;
            var programs = _commonService.GetPrograms();
            ViewBag.ProgramID = new SelectList(programs, "ProgramID", "Name");
            //ViewData["programs"] = programs;
            var statuses = _commonService.GetStatus(WORKFLOW.REGIONAL_REQUEST);
            //ViewBag.StatusID = id;
            ViewBag.StatusID = new SelectList(statuses, "StatusID", "Description");
            return View();
        }
        */
        [HttpGet]
        public ActionResult Index()
        {
            SearchRequsetViewModel filter = new SearchRequsetViewModel();
            ViewBag.Filter = filter;
            PopulateLookup();
            ViewBag.ProgramId = new SelectList(_commonService.GetPrograms(), "ProgramID", "Name");
            return View(filter);
        }

        [HttpPost]
        public ActionResult Index(SearchRequsetViewModel filter)
        {
            ViewBag.Filter = filter;
            PopulateLookup();
            ViewBag.ProgramId = new SelectList(_commonService.GetPrograms(), "ProgramID", "Name");
            return View(filter);
        }

        public ActionResult Request_Read([DataSourceRequest] DataSourceRequest request, int id = -1)
        {

            var requests = id == -1 ? _regionalRequestService.GetAllRegionalRequest().OrderByDescending(m => m.RegionalRequestID) : _regionalRequestService.Get(t => t.Status == id);
            var statuses = _commonService.GetStatus(WORKFLOW.REGIONAL_REQUEST);
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            var requestViewModels = RequestViewModelBinder.BindRegionalRequestListViewModel(requests, statuses, datePref);
            return Json(requestViewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Request_Search([DataSourceRequest] DataSourceRequest request, int RegionID, int ProgramID, int StatusID, DateTime DateFrom, DateTime DateTo)// SearchRequsetViewModel filter)
        {
            var requests = _regionalRequestService.FindBy(t => t.RegionID == RegionID
                                                        && t.ProgramId == ProgramID
                                                        && t.Status == StatusID
                                                        && t.RequistionDate <= DateTo
                                                        && t.RequistionDate >= DateFrom
                                                        ).OrderByDescending(m => m.RegionalRequestID);

///            var requests = id == -1 ? _regionalRequestService.GetAllRegionalRequest().OrderByDescending(m => m.RegionalRequestID) : _regionalRequestService.Get(t => t.Status == id);
            var statuses = _commonService.GetStatus(WORKFLOW.REGIONAL_REQUEST);
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            var requestViewModels = RequestViewModelBinder.BindRegionalRequestListViewModel(requests, statuses, datePref);
            return Json(requestViewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }




        #endregion

        public ActionResult ReconcileRequest_Read([DataSourceRequest] DataSourceRequest request, int id = 0)
        {
            var regionalRequest = _regionalRequestService.Get(m => m.RegionalRequestID == id, null, "RegionalRequestDetails").FirstOrDefault();

            if (regionalRequest != null)
            {
                var detailsToDisplay = GetRequestWithPlan(regionalRequest).ToList();
                return Json(detailsToDisplay.ToDataSourceResult(request));
            }
            return RedirectToAction("Index");
        }

        public ActionResult ReconcileRequest(int id)
        {
            var regionalRequest = _regionalRequestService.FindById(id);
            ViewBag.RegionID = regionalRequest.AdminUnit.Name;
            return View(regionalRequest);
        }

        private List<PLANWithRegionalRequestViewModel> GetRequestWithPlan(RegionalRequest regionalRequest)
        {
           var result = new List<PLANWithRegionalRequestViewModel>();

           if(regionalRequest.ProgramId==1)
           {
            
            var details = regionalRequest.RegionalRequestDetails;
            var hrd = _hrdService.FindBy(m => m.PlanID == regionalRequest.PlanID);
            
            //var woredaGrouped = (from detail in details
            //                     group detail by detail.Fdp.AdminUnit
            //                     into woredaDetail
            //                     select new
            //                    {
            //                        Woreda = woredaDetail.Key,
            //                        NoOfBeneficiaries = woredaDetail.Sum(m => m.Beneficiaries),
            //                        hrdBeneficiary = hrd != null ? hrd.First().HRDDetails.First(m => m.AdminUnit.AdminUnitID == woredaDetail.Key.AdminUnitID).NumberOfBeneficiaries : 0,
            //                        //PsnpBeneficiary = psnp != null ? psnp.First().RegionalPSNPPlanDetails.First(m => m.PlanedFDP.AdminUnit.AdminUnitID == woredaDetail.Key.AdminUnitID).BeneficiaryCount : 0,
            //                        detailsf = woredaDetail,
            //                    });

            var woredaG = (from detail in details
                                 group detail by detail.Fdp.AdminUnit
                                 into woredaDetail 
                                 select woredaDetail);

            //var res =  (from woredaDetail in woredaGrouped
            //           let regionalRequestDetail = woredaDetail.detailsf.FirstOrDefault()
            //           where regionalRequestDetail != null
            //           select new PLANWithRegionalRequestViewModel
            //            {
            //                zone = regionalRequestDetail.Fdp.AdminUnit.AdminUnit2.Name,
            //                Woreda = woredaDetail.Woreda.Name,
            //                RequestedBeneficiaryNo = woredaDetail.NoOfBeneficiaries,
            //                PlannedBeneficaryNo = woredaDetail.hrdBeneficiary,
            //                Difference = woredaDetail.hrdBeneficiary - woredaDetail.NoOfBeneficiaries,
            //            }).ToList();

               result.AddRange(from sw in woredaG
                               let oneWoreda = sw.ToList()
                               let regionalRequestDetail = oneWoreda.FirstOrDefault()
                               where regionalRequestDetail != null
                               select new PLANWithRegionalRequestViewModel()
                                   {
                                       zone = regionalRequestDetail.Fdp.AdminUnit.AdminUnit2.Name,
                                       Woreda = sw.Key.Name, 
                                       RequestedBeneficiaryNo = sw.Sum(m => m.Beneficiaries),
                                       PlannedBeneficaryNo = hrd != null ? hrd.First().HRDDetails.First(m => m.AdminUnit.AdminUnitID == sw.Key.AdminUnitID).NumberOfBeneficiaries : 0,
                                       Difference = ((hrd != null ? hrd.First().HRDDetails.First(m => m.AdminUnit.AdminUnitID == sw.Key.AdminUnitID).NumberOfBeneficiaries : 0) - (sw.Sum(m => m.Beneficiaries))),
                                       RegionalRequestDetails = oneWoreda
                                   });
           }

           if(regionalRequest.ProgramId==2)
           {
               var details = regionalRequest.RegionalRequestDetails;
               var psnp = _RegionalPSNPPlanService.FindBy(m => m.PlanId == regionalRequest.PlanID);
              
               //var psnpBeneficiary = psnp != null
               //                          ? psnp.First().RegionalPSNPPlanDetails.First(
               //                              m => m.PlanedFDPID == 16).BeneficiaryCount
               //                          : 0;

               //var woredaGrouped = (from detail in details
               //                     group detail by detail.Fdp.AdminUnit
               //                         into woredaDetail
               //                         select new
               //                         {
               //                             Woreda = woredaDetail.Key,
               //                             NoOfBeneficiaries = woredaDetail.Sum(m => m.Beneficiaries),
               //                             psnpBeneficiary = psnp != null ? psnp.First().RegionalPSNPPlanDetails.First(m => m.PlanedFDPID == woredaDetail.Key.AdminUnitID).BeneficiaryCount : 0,
               //                             detailsf = woredaDetail
               //                         });

               var woredaG = (from detail in details
                              group detail by detail.Fdp.AdminUnit
                                  into woredaDetail
                                  select woredaDetail);

               result.AddRange(from sw in woredaG
                               let oneWoreda = sw.ToList()
                               let regionalRequestDetail = oneWoreda.FirstOrDefault()
                               where regionalRequestDetail != null
                               select new PLANWithRegionalRequestViewModel()
                               {
                                   zone = regionalRequestDetail.Fdp.AdminUnit.AdminUnit2.Name,
                                   Woreda = sw.Key.Name,
                                   RequestedBeneficiaryNo = sw.Sum(m => m.Beneficiaries),
                                   PlannedBeneficaryNo = psnp != null ? psnp.First().RegionalPSNPPlanDetails.TakeWhile(d=>d.PlanedFDP.AdminUnitID==sw.Key.AdminUnitID).Sum(a=>a.BeneficiaryCount) : 0,
                                   Difference = ((psnp != null ? psnp.First().RegionalPSNPPlanDetails.TakeWhile(d => d.PlanedFDP.AdminUnitID == sw.Key.AdminUnitID).Sum(a => a.BeneficiaryCount) : 0) - (sw.Sum(m => m.Beneficiaries))),
                                   RegionalRequestDetails = oneWoreda
                               });

              //result = (from woredaDetail in woredaGrouped
              //         select new PLANWithRegionalRequestViewModel
              //         {
              //             zone = woredaDetail.detailsf.FirstOrDefault().Fdp.AdminUnit.AdminUnit2.Name,
              //             Woreda = woredaDetail.Woreda.Name,
              //             RequestedBeneficiaryNo = woredaDetail.NoOfBeneficiaries,
              //             PlannedBeneficaryNo = woredaDetail.psnpBeneficiary,
              //             //PlannedBeneficaryNo = 52,
              //             Difference = woredaDetail.psnpBeneficiary - woredaDetail.NoOfBeneficiaries
              //             //Difference =  woredaDetail.NoOfBeneficiaries
              //         }).ToList();
                }

           else if (regionalRequest.ProgramId == 3)
           {
               var details = regionalRequest.RegionalRequestDetails;

               var woredaG = (from detail in details
                              group detail by detail.Fdp.AdminUnit
                                  into woredaDetail
                                  select woredaDetail);


               result.AddRange(from sw in woredaG
                               let oneWoreda = sw.ToList()
                               let regionalRequestDetail = oneWoreda.FirstOrDefault()
                               where regionalRequestDetail != null
                               select new PLANWithRegionalRequestViewModel()
                               {
                                   zone = regionalRequestDetail.Fdp.AdminUnit.AdminUnit2.Name,
                                   Woreda = sw.Key.Name,
                                   RequestedBeneficiaryNo = sw.Sum(m => m.Beneficiaries),
                                   //PlannedBeneficaryNo = 0,
                                   //Difference = 0 - sw.Sum(m => m.Beneficiaries),
                                   RegionalRequestDetails = oneWoreda
                               });
            }
            return result;
        }

        public JsonResult GetPlan(int programID)
        {
            var plan = _commonService.GetPlan(programID);
            var planID = new SelectList(_commonService.GetPlan(programID), "PlanID", "PlanName");
            return Json(planID, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddBeneficary(int id,int programId = -1)
        {
            var request = _regionalRequestService.FindById(id);
            ViewBag.RegionID = new SelectList(_commonService.GetAminUnits(t => t.AdminUnitTypeID == 2), "AdminUnitID", "Name");
            ViewBag.programId = programId;
            ViewBag.ZoneID = programId != -1 ? new SelectList(_commonService.GetAminUnits(t => t.AdminUnitTypeID == 3), "AdminUnitID", "Name") : new SelectList(_commonService.GetAminUnits(t => t.AdminUnitTypeID == 3 && t.ParentID == request.RegionID), "AdminUnitID", "Name");
           
            ViewBag.WoredaID = new SelectList(_commonService.GetAminUnits(t => t.AdminUnitTypeID == 4), "AdminUnitID", "Name");
            ViewBag.FDPID = new SelectList(_commonService.GetAminUnits(t => t.AdminUnitTypeID == 4), "AdminUnitID", "Name");
            var addFDPWithBeneficary = new AddFDPViewModel();
            addFDPWithBeneficary.RegionalRequestID = request.RegionalRequestID;
            addFDPWithBeneficary.RegionID = request.RegionID;
            ViewBag.CurrentRegion = request.RegionID;
            return PartialView(addFDPWithBeneficary);
        }

        private RegionalRequestDetail GetRequestDetail(AddFDPViewModel addFdpViewModel)
        {
            var requestdetail = new RegionalRequestDetail()
                {
                    RegionalRequestID = addFdpViewModel.RegionalRequestID,
                    Fdpid = addFdpViewModel.FDPID,
                    Beneficiaries = addFdpViewModel.Beneficiaries,
                };
            return requestdetail;
        }

        [HttpPost]
        public ActionResult AddBeneficary(AddFDPViewModel requestDetail,int _programId = -1)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ViewBag.programId = _programId;
                    var detail = GetRequestDetail(requestDetail);
                    _regionalRequestDetailService.AddCommodityFdp(detail);
                    return RedirectToAction("Allocation", new { id = requestDetail.RegionalRequestID, programId = _programId });
                }

                catch (Exception ex)
                {
                    ModelState.AddModelError("Errors", "Unable to Add new fpd");
                    ViewBag.ZoneID = new SelectList(_commonService.GetAminUnits(t => t.AdminUnitTypeID == 3), "AdminUnitID", "Name");
                    ViewBag.WoredaID = new SelectList(_commonService.GetAminUnits(t => t.AdminUnitTypeID == 4), "AdminUnitID", "Name");
                    ViewBag.FDPID = new SelectList(_commonService.GetFDPs(2), "FDPID", "FDPName");
                    return RedirectToAction("Allocation", new { id = requestDetail.RegionalRequestID, programId = _programId });
                }

            }
            return PartialView(requestDetail);
        }

        public ActionResult DeleteFDP(int id)
        {
            var requestDetail = _regionalRequestDetailService.FindById(id);
            if (requestDetail != null)
            {
                _regionalRequestDetailService.DeleteRegionalRequestDetail(requestDetail);
                return RedirectToAction("Allocation", new { id = requestDetail.RegionalRequestID });
            }
            ModelState.AddModelError("Errors", "unable to delete fdp");
            return RedirectToAction("Index");
        }

        public JsonResult GetCascadedAdminUnits(int regionID,int programId = -1)
        {
            
            if (programId!=-1)
            {
                var cascadeAdminUnitAllRegions = (from region in _commonService.GetAminUnits(m => m.AdminUnitTypeID == 2)

                                                  select new
                                                  {
                                                      RegionID = region.AdminUnitID,
                                                      RegionName = region.Name,

                                                      zones = from zone in _commonService.GetAminUnits(z => z.ParentID == region.AdminUnitID)
                                                              select new
                                                              {
                                                                  ZoneID = zone.AdminUnitID,
                                                                  ZoneName = zone.Name,


                                                                  Woredas = from woreda in _commonService.GetAminUnits(m => m.ParentID == zone.AdminUnitID)
                                                                            select new
                                                                            {
                                                                                WoredaID = woreda.AdminUnitID,
                                                                                WoredaName = woreda.Name,
                                                                                fdps = from fdp in _commonService.GetFDPs(woreda.AdminUnitID)
                                                                                       select new
                                                                                       {
                                                                                           FDPID = fdp.FDPID,
                                                                                           FDPName = fdp.Name
                                                                                       }
                                                                            }

                                                              }
                                                  }


                  );
                return Json(cascadeAdminUnitAllRegions, JsonRequestBehavior.AllowGet);
            }
            else
            {
               var cascadeAdminUnit = (from zone in _commonService.GetAminUnits(m => m.AdminUnitTypeID == 3 && m.ParentID == regionID)

                                        select new
                                        {
                                            ZoneID = zone.AdminUnitID,
                                            ZoneName = zone.Name,
                                            Woredas = from woreda in _commonService.GetAminUnits(m => m.ParentID == zone.AdminUnitID)
                                                      select new
                                                      {
                                                          WoredaID = woreda.AdminUnitID,
                                                          WoredaName = woreda.Name,
                                                          fdps = from fdp in _commonService.GetFDPs(woreda.AdminUnitID)
                                                                 select new
                                                                 {
                                                                     FDPID = fdp.FDPID,
                                                                     FDPName = fdp.Name
                                                                 }
                                                      }

                                        }

                  );
               return Json(cascadeAdminUnit, JsonRequestBehavior.AllowGet);
            }
          
           
        }
        public ActionResult AddCommodity(int id )
        {
            var request = _regionalRequestService.FindById(id);
            ViewBag.CommodityID = new SelectList(_commonService.GetRationCommodity(request.RationID), "CommodityID", "Name");
            var addCommodityViewModel = new AddCommodityViewModel();
            addCommodityViewModel.RegionalRequestID = request.RegionalRequestID;
            return PartialView(addCommodityViewModel);
        }

        [HttpPost]
        public ActionResult AddCommodity(AddCommodityViewModel addCommodity)
        {
            if (ModelState.IsValid)
            {
                var user = UserAccountHelper.GetCurrentUser();
                var currentUnit = user.PreferedWeightMeasurment;

                _regionalRequestDetailService.AddRequestDetailCommodity(addCommodity.CommodityID, addCommodity.RegionalRequestID, currentUnit);
                return RedirectToAction("Allocation", new { id = addCommodity.RegionalRequestID });
            }
            ModelState.AddModelError("Errors",@"Unable to add Commodity");
            return RedirectToAction("Allocation", new {id = addCommodity.RegionalRequestID});
        }
        public ActionResult AddAllCommodity(int? id)
        {
            if (id != null)
            {
                _regionalRequestDetailService.AddAllCommodity((int)id);
                return RedirectToAction("Allocation", new { id = id });
            }
            ModelState.AddModelError("Errors", @"unable to Add All Commodities");
            return RedirectToAction("Allocation", new { id = id });
        }
        public ActionResult DeleteCommodity(int? commodityID, int requestID)
        {
            if (commodityID != null)
            {
                _regionalRequestDetailService.DeleteRequestDetailCommodity((int) commodityID, requestID);
                return RedirectToAction("Allocation", new {id = requestID});
            }
            return RedirectToAction("Allocation", new { id = requestID });
        }
        public ActionResult ChangeRation(int requestID,int rationID )
        {
            if(rationID!=null)
            {
                var request = _regionalRequestService.FindById(requestID);
                request.RationID = rationID;
                _regionalRequestService.EditRegionalRequest(request);
                return RedirectToAction("Allocation", new { id = requestID });
            }
            return RedirectToAction("Allocation", new { id = requestID });
        }
    }
}