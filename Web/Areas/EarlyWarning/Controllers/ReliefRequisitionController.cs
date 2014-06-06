using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.EarlyWarning.Models;
using Cats.Helpers;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Models.ViewModels;
using Cats.Services.Common;
using Cats.Services.EarlyWarning;
using Cats.Services.Security;
using Cats.Services.Transaction;
using Cats.ViewModelBinder;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Areas.EarlyWarning.Controllers
{
    public class ReliefRequisitionController : Controller
    {
        //
        // GET: /EarlyWarning/ReliefRequisition/

        private readonly IReliefRequisitionService _reliefRequisitionService;
        private readonly IWorkflowStatusService _workflowStatusService;
        private readonly IReliefRequisitionDetailService _reliefRequisitionDetailService;
        private readonly IUserAccountService _userAccountService;
        private readonly IRationService  _rationService;
        private readonly IDonorService _donorService;
        private readonly INotificationService _notificationService;
        private readonly IPlanService _planService;
        private readonly ICommonService _commonService;
        private readonly Cats.Services.Transaction.ITransactionService _transactionService;
        public ReliefRequisitionController(IReliefRequisitionService reliefRequisitionService, IWorkflowStatusService workflowStatusService, 
            IReliefRequisitionDetailService reliefRequisitionDetailService,
            IUserAccountService userAccountService,
            IRationService rationService, IDonorService donorService, INotificationService notificationService, IPlanService planService, ITransactionService transactionService, ICommonService commonService)
        {
            this._reliefRequisitionService = reliefRequisitionService;
            this._workflowStatusService = workflowStatusService;
            this._reliefRequisitionDetailService = reliefRequisitionDetailService;
            _userAccountService = userAccountService;
            _rationService = rationService;
            _donorService = donorService;
            _notificationService = notificationService;
            _planService = planService;
            _transactionService = transactionService;
            _commonService = commonService;
        }

        public ViewResult Index()
        {
            ViewBag.Status = 1;
            var filter = new SearchRequistionViewModel();
            ViewBag.Filter = filter;
            Populatelookup();
            //ViewBag.Status = id;
            return View();
        }

        [HttpPost]
        public ActionResult Index(SearchRequistionViewModel filter)
        {
            ViewBag.Filter = filter;
            Populatelookup();
            return View(filter);
        }

        void Populatelookup()
        {
            var user = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name);
            ViewBag.RegionID = user.RegionalUser ? new SelectList(_commonService.GetAminUnits(t => t.AdminUnitTypeID == 2 && t.AdminUnitID == user.RegionID), "AdminUnitID", "Name") : new SelectList(_commonService.GetAminUnits(t => t.AdminUnitTypeID == 2), "AdminUnitID", "Name");

            ViewBag.ProgramId = new SelectList(_commonService.GetPrograms(), "ProgramID", "Name");
            //ViewBag.Month = new SelectList(RequestHelper.GetMonthList(), "ID", "Name");
            //ViewBag.RationID = new SelectList(_commonService.GetRations(), "RationID", "RefrenceNumber");
            //ViewBag.DonorID = new SelectList(_commonService.GetDonors(), "DonorId", "Name");
            //ViewBag.Round = new SelectList(RequestHelper.GetMonthList(), "ID", "ID");
            //ViewBag.PlanID = new SelectList(_commonService.GetPlan(1), "PlanID", "PlanName");
            //ViewBag.PSNPPlanID = new SelectList(_commonService.GetPlan(2), "PlanID", "PlanName");
            //ViewBag.SeasonID = new SelectList(_commonService.GetSeasons(), "SeasonID", "Name");

            var statuslist = new List<RequestStatus>();

            statuslist.Add(new RequestStatus { StatusID = 1, StatusName = "Draft" });
            statuslist.Add(new RequestStatus { StatusID = 2, StatusName = "Approved" });
            statuslist.Add(new RequestStatus { StatusID = 3, StatusName = "Hub Assigned" });
            statuslist.Add(new RequestStatus { StatusID = 4, StatusName = "Project Code Assigned" });
            statuslist.Add(new RequestStatus { StatusID = 5, StatusName = "Transport Requisition Created" });
            statuslist.Add(new RequestStatus { StatusID = 6, StatusName = "Transport Order Created" });

            ViewBag.StatusID = new SelectList(statuslist, "StatusID", "StatusName");
        }

        [HttpGet]
        public ActionResult CreateRequisitionForIDPS(int id,int programId=-1)
        {
            try
            {
                if (programId == (int)Programs.IDPS)
                {
                    var planToBeEdited = _planService.FindBy(p => p.PlanID == programId).Single();
                    if (planToBeEdited != null)
                    {
                        //var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
                        //var planViewModel = new PlanViewModel()
                        //                        {
                        //                            planID = planToBeEdited.PlanID,
                        //                            planName = planToBeEdited.PlanName,
                        //                            StartDate = planToBeEdited.StartDate.ToCTSPreferedDateFormat(datePref),
                        //                            EndDate = planToBeEdited.EndDate.ToCTSPreferedDateFormat(datePref),
                        //                            ProgramID = planToBeEdited.ProgramID,
                        //                            Program = planToBeEdited.Program.Name,
                        //                            StatusID = planToBeEdited.Status,
                        //                        };

                        return PartialView(planToBeEdited);
                    }
                }
                
                return null;
            }
            catch (Exception)
            {
                return null;

            }
            
        }

        [HttpPost]
        public ActionResult CreateRequisitionForIDPS(Plan plan, int id)
        {
            try
            {
                var planToBeEdited = _planService.FindBy(p => p.PlanID == plan.PlanID).Single();

                if (planToBeEdited != null)
                {
                    planToBeEdited.PlanName = plan.PlanName;
                    planToBeEdited.StartDate = plan.StartDate;
                    planToBeEdited.EndDate = plan.EndDate;

                    _planService.EditPlan(planToBeEdited);
                    return RedirectToAction("CreateRequisiton", new { id = id });
                }
                ModelState.AddModelError("Error", errorMessage: "Can not edit Plan");
                return null;
            }
            catch (Exception)
            {

                return null;
            }
            
        }

        [HttpGet]
        public ActionResult CreateRequisiton(int id)
        {
            var input = _reliefRequisitionService.CreateRequisition(id);
            return RedirectToAction("NewRequisiton", "ReliefRequisition", new { id = id });
        }

        [HttpGet]
        public ViewResult NewRequisiton(int id)
        {
            var input = _reliefRequisitionService.GetRequisitionByRequestId(id).ToList();
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            
            foreach (var reliefRequisitionNew in input)
            {
                if (reliefRequisitionNew.RequestedDate.HasValue)
                {
                    reliefRequisitionNew.RequestDatePref = reliefRequisitionNew.RequestedDate.Value.ToCTSPreferedDateFormat(datePref);
                }
            }
            return View(input);
        }

        [HttpPost]
        public ActionResult NewRequisiton(List<DataFromGrid> input)
        {
            var requId = 0;
            if (ModelState.IsValid)
            {
                var requisitionNumbers = input.ToDictionary(t => t.Number, t => t.RequisitionNo);
                _reliefRequisitionService.AssignRequisitonNo(requisitionNumbers);
            }
            return RedirectToAction("Index", "ReliefRequisition");
        }

        [HttpGet]
        public ActionResult Allocation(int id)
        {
            var requisition =
                _reliefRequisitionService.Get(t => t.RequisitionID == id, null, "ReliefRequisitionDetails").
                    FirstOrDefault();
            ViewData["donors"] = _donorService.GetAllDonor();
            //ViewBag.HRDID = new SelectList(_donorService.GetAllDonor(), "HRDID", "Year", donor.HRDID);

            if (requisition == null)
            {
                HttpNotFound();
            }
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            var requisitionViewModel = RequisitionViewModelBinder.BindReliefRequisitionViewModel(requisition, _workflowStatusService.GetStatus(WORKFLOW.RELIEF_REQUISITION),datePref);

            return View(requisitionViewModel);
        }

        public ActionResult Allocation_Read([DataSourceRequest] DataSourceRequest request, int id)
        {
            
            var requisitionDetails = _reliefRequisitionDetailService.Get(t => t.RequisitionID == id, null, "ReliefRequisition.AdminUnit,FDP.AdminUnit,FDP,Donor,Commodity").ToList();
            var commodityID = requisitionDetails.FirstOrDefault().CommodityID;
            var RationAmount = GetCommodityRation(id, commodityID);
            RationAmount = RationAmount.GetPreferedRation();
            
            var requisitionDetailViewModels = RequisitionViewModelBinder.BindReliefRequisitionDetailListViewModel(requisitionDetails,RationAmount);
            return Json(requisitionDetailViewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Allocation_Create([DataSourceRequest] DataSourceRequest request, ReliefRequisitionDetailViewModel reliefRequisitionDetailViewModel)
        {
            if (reliefRequisitionDetailViewModel != null && ModelState.IsValid)
            {
                _reliefRequisitionDetailService.AddReliefRequisitionDetail(RequisitionViewModelBinder.BindReliefRequisitionDetail(reliefRequisitionDetailViewModel));
            }

            return Json(new[] { reliefRequisitionDetailViewModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Allocation_Update([DataSourceRequest] DataSourceRequest request, ReliefRequisitionDetailViewModel reliefRequisitionDetailViewModel)
        {
            if (reliefRequisitionDetailViewModel != null && ModelState.IsValid)
            {
                var target = _reliefRequisitionDetailService.FindById(reliefRequisitionDetailViewModel.RequisitionDetailID);
                if (target != null)
                {
                    target.Amount = reliefRequisitionDetailViewModel.Amount;
                    target.BenficiaryNo = reliefRequisitionDetailViewModel.BenficiaryNo;
                    if(reliefRequisitionDetailViewModel.DonorID.HasValue)
                    target.DonorID = reliefRequisitionDetailViewModel.DonorID.Value;
                    _reliefRequisitionDetailService.EditReliefRequisitionDetail(target);
                }
            }

            return Json(new[] { reliefRequisitionDetailViewModel }.ToDataSourceResult(request, ModelState));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Allocation_Destroy([DataSourceRequest] DataSourceRequest request,
                                                  ReliefRequisitionDetail reliefRequisitionDetail)
        {
            if (reliefRequisitionDetail != null)
            {
                _reliefRequisitionDetailService.DeleteById(reliefRequisitionDetail.RequisitionDetailID);
            }

            return Json(ModelState.ToDataSourceResult());
        }


        [HttpPost]
        public ActionResult RequistionDetailEdit(IEnumerable<ReleifRequisitionDetailEdit.ReleifRequisitionDetailEditInput> input)
        {
            // var requId = 0;
            if (ModelState.IsValid)
            {
                var allocaitons = input.ToDictionary(t => t.Number, t => t.Amount);

                _reliefRequisitionService.EditAllocatedAmount(allocaitons);

            }
            return RedirectToAction("Index", "ReliefRequisition");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var relifRequisition = _reliefRequisitionService.FindById(id);
            if (relifRequisition == null)
            {
                HttpNotFound();
            }
         return View(relifRequisition);
        }

        [HttpPost]
        public ActionResult Edit(ReliefRequisition reliefrequisition)
        {
            if (ModelState.IsValid)
            {
                _reliefRequisitionService.EditReliefRequisition(reliefrequisition);
                return RedirectToAction("Index", "ReliefRequisition");
            }
            return View(reliefrequisition);
        }

        [HttpGet]
        public ActionResult SendToLogistics(int id)
        {
            //var requistion = _reliefRequisitionService.FindById(id);
            //if (requistion == null)
            //{
            //    HttpNotFound();
            //}
            var requisition =
                _reliefRequisitionService.Get(t => t.RequisitionID == id, null, "ReliefRequisitionDetails").
                    FirstOrDefault();
            if (requisition == null)
            {
                HttpNotFound();
            }
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            var requisitionViewModel = RequisitionViewModelBinder.BindReliefRequisitionViewModel(requisition, _workflowStatusService.GetStatus(WORKFLOW.RELIEF_REQUISITION), datePref);

          

            return View(requisitionViewModel);
        }


       

        [HttpPost]
        public ActionResult ConfirmSendToLogistics(int requisitionid)
        {
            var requisition = _reliefRequisitionService.FindById(requisitionid);
            requisition.Status = (int)ReliefRequisitionStatus.Approved;
            _reliefRequisitionService.EditReliefRequisition(requisition);
            //send notification
            SendNotification(requisition);
            _transactionService.PostRequestAllocation(requisitionid);
            return RedirectToAction("Index", "ReliefRequisition");
        }

        private void SendNotification(ReliefRequisition requisition)
        {
            try
            {
                string destinationURl;
                if (Request.Url.Host != null)
                {
                    if (Request.Url.Host == "localhost")
                    {
                        destinationURl = "http://" + Request.Url.Authority +
                                         "/Logistics/DispatchAllocation/IndexFromNotification?paramRegionId=" +
                                         requisition.RegionID +
                                         "&recordId=" + requisition.RequisitionID;
                    }
                    else
                    {
                        destinationURl = "http://" + Request.Url.Authority +
                                        Request.ApplicationPath +
                                         "/Logistics/DispatchAllocation/IndexFromNotification?paramRegionId=" +
                                         requisition.RegionID +
                                         "&recordId=" + requisition.RequisitionID;
                    }

                    _notificationService.AddNotificationForLogistcisFromEarlyWaring(destinationURl,
                                                                                    requisition.RequisitionID,
                                                                                    (int) requisition.RegionID,
                                                                                    requisition.RequisitionNo);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public ActionResult Requisition_Read([DataSourceRequest] DataSourceRequest request, int id = 0)
        {
            var requests = _reliefRequisitionService.Get(t => t.Status == id);
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            var requestViewModels = RequisitionViewModelBinder.BindReliefRequisitionListViewModel(requests,
                                                                                                  _workflowStatusService
                                                                                                      .GetStatus(
                                                                                                          WORKFLOW.
                                                                                                              RELIEF_REQUISITION),datePref).OrderByDescending(m=>m.RequisitionID);
            return Json(requestViewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Requisition_Search([DataSourceRequest] DataSourceRequest request, int regionID, int programID, int id)// SearchRequsetViewModel filter)
        {
            var requests = _reliefRequisitionService.Get(t => t.Status == id && t.RegionID==regionID && t.ProgramID==programID);
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            var requestViewModels = RequisitionViewModelBinder.BindReliefRequisitionListViewModel(requests,_workflowStatusService
                                                                                                      .GetStatus(
                                                                                                          WORKFLOW.
                                                                                                              RELIEF_REQUISITION), datePref).OrderByDescending(m => m.RequisitionID);
            return Json(requestViewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Requisition_Update([DataSourceRequest] DataSourceRequest request, ReliefRequisitionViewModel reliefRequisitionViewModel)
        {
            if (reliefRequisitionViewModel != null && ModelState.IsValid)
            {
                var target = _reliefRequisitionService.FindById(reliefRequisitionViewModel.RequisitionID);
                if (target != null)
                {

                    target.RequisitionNo = reliefRequisitionViewModel.RequisitionNo;

                    _reliefRequisitionService.EditReliefRequisition(target);
                }
            }

            return Json(new[] { reliefRequisitionViewModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Requisition_Destroy([DataSourceRequest] DataSourceRequest request,
                                                  ReliefRequisition reliefRequisition)
        {
            if (reliefRequisition != null)
            {
                _reliefRequisitionDetailService.DeleteById(reliefRequisition.RequisitionID);
            }

            return Json(ModelState.ToDataSourceResult());
        }

        public ActionResult Details(int id)
        {
            var requisition = _reliefRequisitionService.FindById(id);
            if (requisition == null)
            {
                return HttpNotFound();
            }
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            var requisitionViewModel = RequisitionViewModelBinder.BindReliefRequisitionViewModel(requisition, _workflowStatusService.GetStatus(WORKFLOW.RELIEF_REQUISITION), datePref);
            return View(requisitionViewModel);
        }

        public decimal GetCommodityRation(int requisitionID, int commodityID)
        {
            var reliefRequisition = _reliefRequisitionService.FindById(requisitionID);
                var ration = _rationService.FindById(reliefRequisition.RegionalRequest.RationID);
                var rationModel = ration.RationDetails.FirstOrDefault(m => m.CommodityID == commodityID);

             return rationModel!=null?rationModel.Amount:0;

        }


        //public  JsonResult CancelChanges(List<DataFromGrid> input)
        //{
        //    List<int> ids = new List<int>();
        //    if (input!=null)
        //    {
        //        foreach (var id in ids)
        //        {
                   
        //        }
                
        //    }
        //    return Json(ids, JsonRequestBehavior.AllowGet);
        //}
    }
}