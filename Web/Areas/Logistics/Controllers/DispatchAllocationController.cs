using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Cats.Helpers;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Services.Common;
using Cats.Services.EarlyWarning;
using Cats.Services.Security;
using Cats.ViewModelBinder;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNet.SignalR;
using log4net;
using Cats.Helpers;
namespace Cats.Areas.Logistics.Controllers
{
    public class DispatchAllocationController : Controller
    {
        //
        // GET: /Logistics/DispatchAllocation/

        private readonly IReliefRequisitionDetailService _reliefRequisitionDetailService;
        private readonly IReliefRequisitionService _reliefRequisitionService;
        private readonly IHubService _hubService;
        private readonly IHubAllocationService _HubAllocationService;
        private readonly IAdminUnitService _adminUnitService;
        private readonly INeedAssessmentService _needAssessmentService;
        private readonly IAllocationByRegionService _AllocationByRegionService;
        private readonly INotificationService _notificationService;
        private readonly ILog _log;
        private readonly IUserAccountService _userAccountService;
        public DispatchAllocationController(IReliefRequisitionService reliefRequisitionService, IReliefRequisitionDetailService reliefRequisitionDetailService, IHubService hubService, IAdminUnitService adminUnitService, INeedAssessmentService needAssessmentService, IHubAllocationService hubAllocationService, IUserAccountService userAccountService, ILog log, IAllocationByRegionService allocationByRegionService, INotificationService notification)
        {
            _reliefRequisitionService = reliefRequisitionService;
            _reliefRequisitionDetailService = reliefRequisitionDetailService;
            _hubService = hubService;
            _adminUnitService = adminUnitService;
            _needAssessmentService = needAssessmentService;
            _HubAllocationService = hubAllocationService;
            _userAccountService = userAccountService;
            _log = log;
            _AllocationByRegionService = allocationByRegionService;
            _notificationService = notification;
        }


       

        public ActionResult Index(int regionId=-1)
        {

           
            //var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            //hubContext.Clients.All.receiveNotification("this is a sample data");

            ViewBag.regionId = regionId;
            ViewBag.Region = new SelectList(_adminUnitService.GetRegions(), "AdminUnitID", "Name");
            return View();
        }

        //#region "test"

        //public ActionResult Main()
        //{
        //    return View();
        //}

        //[HttpGet]
        //public JsonResult HubAllocationByRegion(int regionId = -1)
        //{
        //    List<AllocationByRegion> requisititions = null;
        //    requisititions = regionId != -1 ? _AllocationByRegionService.FindBy(r => r.Status == (int)ReliefRequisitionStatus.HubAssigned && r.RegionID == regionId) : _AllocationByRegionService.FindBy(r => r.Status == (int)ReliefRequisitionStatus.HubAssigned);

        //    var requisitionViewModel = BindAllocation(requisititions);// HubAllocationViewModelBinder.ReturnRequisitionGroupByReuisitionNo(requisititions);

        //    return Json(requisitionViewModel,JsonRequestBehavior.AllowGet);
        //}


        //public JsonResult AllocatedProjectCode(int regionId = -1,int status=-1)
        //{
        //    if (regionId < 0 || status < 0) return Json(new List<RequisitionViewModel>(), JsonRequestBehavior.AllowGet);
        //    var requisititions = new List<ReliefRequisition>();
        //    requisititions = _reliefRequisitionService.FindBy(r => r.Status == status && r.RegionID == regionId);

        //    var requisitionViewModel = HubAllocationViewModelBinder.ReturnRequisitionGroupByReuisitionNo(requisititions);
        //    return Json(requisitionViewModel,JsonRequestBehavior.AllowGet);
        //}


        //#endregion

        public ActionResult GetRegions()
        {
            IOrderedEnumerable<RegionsViewModel> regions = _needAssessmentService.GetRegions();
            return Json(regions, JsonRequestBehavior.AllowGet);
        }
        public ActionResult HubAllocation([DataSourceRequest]DataSourceRequest request,int regionId)
        {
            List<AllocationByRegion> requisititions = null;
            requisititions = regionId != -1
                                 ? _AllocationByRegionService.FindBy(
                                     r =>
                                     r.Status == (int) ReliefRequisitionStatus.HubAssigned && r.RegionID == regionId)
                                 : null;// _AllocationByRegionService.FindBy(r => r.Status == (int)ReliefRequisitionStatus.HubAssigned);

            var requisitionViewModel = BindAllocation(requisititions);// HubAllocationViewModelBinder.ReturnRequisitionGroupByReuisitionNo(requisititions);
            
            return Json(requisitionViewModel.ToDataSourceResult(request));
        }

        public ActionResult AllocateProjectCode([DataSourceRequest]DataSourceRequest request, int regionId,int status)
        {
            List<ReliefRequisition> requisititions = null;
            if (regionId == -1 || status == -1) return Json((new List<RequisitionViewModel>()).ToDataSourceResult(request));
            requisititions = _reliefRequisitionService.FindBy(
                r =>
                r.Status == status && r.RegionID == regionId);
                                 
            
            var requisitionViewModel = HubAllocationViewModelBinder.ReturnRequisitionGroupByReuisitionNo(requisititions);
            return Json(requisitionViewModel.ToDataSourceResult(request));
        }

        public ActionResult Hub(int regionId)
        {
            if (regionId !=-1)
            {
                ViewBag.regionId = regionId;
                ViewBag.RegionName =_adminUnitService.GetRegions().Where(r => r.AdminUnitID == regionId).Select(r => r.Name).Single();
                ViewData["Hubs"] = _hubService.GetAllHub();//.Where(h => h.HubOwnerID == 1);//get DRMFSS stores
                return View();
            }
            return View();
        }



         [HttpGet]
        public JsonResult ReadRequisitions(int regionId)
        {
            var requisititions = _reliefRequisitionService.FindBy(r => r.Status == (int)ReliefRequisitionStatus.Approved && r.RegionID == regionId);
            var requisitionViewModel = HubAllocationViewModelBinder.ReturnRequisitionGroupByReuisitionNo(requisititions);
            return Json(requisitionViewModel, JsonRequestBehavior.AllowGet);
        }


         [System.Web.Http.HttpPost]
        public JsonResult Save( List<Allocation> allocation )
        {
            var userName = HttpContext.User.Identity.Name;
            var user = _userAccountService.GetUserDetail(userName);

             try
             {
                 foreach (Allocation appRequisition in allocation)
                 {

                     var newHubAllocation = new HubAllocation();

                     newHubAllocation.AllocatedBy = user.UserProfileID;
                     newHubAllocation.RequisitionID = appRequisition.ReqId;
                     newHubAllocation.AllocationDate = DateTime.Now.Date;
                     newHubAllocation.ReferenceNo = "001";
                     newHubAllocation.HubID = appRequisition.HubId;
                     

                     _HubAllocationService.AddHubAllocation(newHubAllocation);
                     AddNotification(newHubAllocation.HubAllocationID);
                 }
                
                 return Json(new { success = true });
             }
             catch (Exception ex)
             {

                 return Json(new { success = false, errorMessage = ex.Message });
             }
           
        }

        private void AddNotification(int hubAllocationId)
        {
            if (Request.Url != null)
            {
                var notification = new Notification
                                       {
                                           Text = "Hub Allocation",
                                           CreatedDate = DateTime.Now.Date,
                                           IsRead = false,
                                           Role = 2,
                                           RecordId = hubAllocationId,
                                           Url = Request.Url.AbsoluteUri,
                                           TypeOfNotification = "Hub Allocation"
                                       };

                _notificationService.AddNotification(notification);

            }


        }

        public ActionResult RegionId(int id)
        {
           return RedirectToAction("Index", new {regionId = id});
        }


        
         public List<HubAllocationByRegionViewModel> BindAllocation(List<AllocationByRegion> reliefRequisitions)
        {

             try
             {
                 if (reliefRequisitions == null)
                     return new List<HubAllocationByRegionViewModel>();
                 var result = (reliefRequisitions.Select(req => new HubAllocationByRegionViewModel()
                 {
                     Region = req.Name,
                     RegionId = (int)req.RegionID,
                     AdminUnitID = (int)req.RegionID,
                     Hub = req.Hub,
                     AllocatedAmount = ((decimal)req.Amount).ToPreferedWeightUnit()
                 }));



                 return Enumerable.Cast<HubAllocationByRegionViewModel>(result).ToList();
             }
             catch 
             {
                 
                 return new List<HubAllocationByRegionViewModel>();
             }

             
        }
       
    }
}
