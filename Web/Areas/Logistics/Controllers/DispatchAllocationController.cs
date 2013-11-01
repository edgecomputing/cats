using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Cats.Helpers;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Services.EarlyWarning;
using Cats.Services.Security;
using Cats.ViewModelBinder;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using log4net;

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
      
        private readonly ILog _log;
        private readonly IUserAccountService _userAccountService;
        public DispatchAllocationController(IReliefRequisitionService reliefRequisitionService, IReliefRequisitionDetailService reliefRequisitionDetailService, IHubService hubService, IAdminUnitService adminUnitService, INeedAssessmentService needAssessmentService, IHubAllocationService hubAllocationService, IUserAccountService userAccountService, ILog log)
        {
            _reliefRequisitionService = reliefRequisitionService;
            _reliefRequisitionDetailService = reliefRequisitionDetailService;
            _hubService = hubService;
            _adminUnitService = adminUnitService;
            _needAssessmentService = needAssessmentService;
            _HubAllocationService = hubAllocationService;
            _userAccountService = userAccountService;
            _log = log;
        }


        public ActionResult Index(int regionId=-1)
        {
            
            ViewBag.regionId = regionId;
            ViewBag.Region = new SelectList(_adminUnitService.GetRegions(), "AdminUnitID", "Name");
            return View();
        }
        public ActionResult GetRegions()
        {
            IOrderedEnumerable<RegionsViewModel> regions = _needAssessmentService.GetRegions();
            return Json(regions, JsonRequestBehavior.AllowGet);
        }
        public ActionResult HubAllocation([DataSourceRequest]DataSourceRequest request,int regionId)
        {
            List<ReliefRequisition> requisititions = null;
            requisititions = regionId!=-1 ? _reliefRequisitionService.FindBy(r => r.Status == (int)ReliefRequisitionStatus.HubAssigned && r.RegionID == regionId) : _reliefRequisitionService.FindBy(r => r.Status == (int)ReliefRequisitionStatus.HubAssigned);

            var requisitionViewModel = BindAllocation(requisititions);// HubAllocationViewModelBinder.ReturnRequisitionGroupByReuisitionNo(requisititions);
            
            return Json(requisitionViewModel.ToDataSourceResult(request));
        }

        public ActionResult AllocateProjectCode([DataSourceRequest]DataSourceRequest request, int regionId)
        {
            List<ReliefRequisition> requisititions = null;
            requisititions = regionId != -1 ? _reliefRequisitionService.FindBy(r => r.Status == (int)ReliefRequisitionStatus.HubAssigned && r.RegionID == regionId) : _reliefRequisitionService.FindBy(r => r.Status == (int)ReliefRequisitionStatus.HubAssigned);
            
            var requisitionViewModel = HubAllocationViewModelBinder.ReturnRequisitionGroupByReuisitionNo(requisititions);
            return Json(requisitionViewModel.ToDataSourceResult(request));
        }

        public ActionResult Hub(int regionId)
        {
            if (regionId !=-1)
            {
                ViewBag.regionId = regionId;
                ViewBag.RegionName =
                    _adminUnitService.GetRegions().Where(r => r.AdminUnitID == regionId).Select(r => r.Name).Single();
                ViewData["Hubs"] = _hubService.GetAllHub().Where(h => h.HubOwnerID == 1);//get DRMFSS stores
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
                 }
                
                 return Json(new { success = true });
             }
             catch (Exception ex)
             {

                 return Json(new { success = false, errorMessage = ex.Message });
             }
           
        }
        public ActionResult RegionId(int id)
        {
           return RedirectToAction("Index", new {regionId = id});
        }


         public   List<HubAllocationByRegionViewModel> GroupByRegion(List<HubAllocationByRegionViewModel> listToBeGrouped)
         {
             var result = (from req in listToBeGrouped
                           group req by req.RegionId
                           into region
                           select new HubAllocationByRegionViewModel
                                      {
                                          Region = region.First().Region,
                                          RegionId =  region.First().RegionId,
                                          Hub = region.First().Hub,
                                          AllocatedAmount = region.Sum(a => a.AllocatedAmount)
                                      });
             return Enumerable.Cast<HubAllocationByRegionViewModel>(result).ToList();
         }
        public   List<HubAllocationByRegionViewModel> BindAllocation(List<ReliefRequisition> reliefRequisitions)
        {
            var result = (from req in reliefRequisitions
                          select new HubAllocationByRegionViewModel()
                          {

                              Region = req.AdminUnit.Name,
                              RegionId = (int)req.RegionID,
                              Hub = GetHubName(req.RequisitionID),
                              AllocatedAmount = req.ReliefRequisitionDetails.Sum(a => a.Amount)
                              
                          });



            return Enumerable.Cast<HubAllocationByRegionViewModel>(result).ToList();
        }
        public string GetHubName(int requisitionId)
        {
            var allocated = _HubAllocationService.FindBy(r => r.RequisitionID == requisitionId).SingleOrDefault();
            if (allocated != null)
                return allocated.Hub.Name;
            else return null;
        }
    }
}
