using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Services.EarlyWarning;
using Cats.Services.Logistics;
using Cats.Services.Procurement;
using Cats.Services.Security;
using hub = Cats.Services.Hub;
using Cats.Models;

namespace Cats.Areas.Logistics.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Logistics/Home/
        private readonly IReliefRequisitionService _reliefRequisitionService;
        private readonly hub.IDispatchAllocationService _dispatchAllocationService;
        private readonly IUserAccountService _userAccountService;
        private readonly ITransportOrderService _transportOrderService;
        private readonly ITransportOrderDetailService _transportOrderDetailService;
        private readonly hub.IDispatchService _dispatchService;
        private readonly hub.IDispatchDetailService _dispatchDetailService;
        private readonly Cats.Services.Logistics.ISIPCAllocationService _sipcAllocationService;
        private readonly IAdminUnitService _adminUnitService;

        public HomeController(IReliefRequisitionService reliefRequisitionService,
            hub.IDispatchAllocationService dispatchAllocationService,
            IUserAccountService userAccountService,
            ITransportOrderService transportOrderService,
            ITransportOrderDetailService transportOrderDetailService,
            hub.DispatchService dispatchService,
            hub.DispatchDetailService dispatchDetailService, ISIPCAllocationService sipcAllocationService, IAdminUnitService adminUnitService)
        {
            this._reliefRequisitionService = reliefRequisitionService;
            _dispatchAllocationService = dispatchAllocationService;
            _userAccountService = userAccountService;
            _transportOrderService = transportOrderService;
            _transportOrderDetailService = transportOrderDetailService;
            _dispatchService = dispatchService;
            _dispatchDetailService = dispatchDetailService;
            _sipcAllocationService = sipcAllocationService;
            _adminUnitService = adminUnitService;
        }

        public ActionResult Index()
        {
            ViewBag.RegionID = 2;
            ViewBag.RegionName = "Afar";
            return View();
        }

        public JsonResult GetRecievedRequisitions()
        {
            return Json(_reliefRequisitionService.GetRequisitionsSentToLogistics(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDispatchAllocationByHub(int hubId)
        {
            var user = _userAccountService.GetUserInfo(User.Identity.Name);
           
            var fdpAllocations = _dispatchAllocationService.GetCommitedAllocationsByHubDetached(hubId, user.PreferedWeightMeasurment);

            return Json(fdpAllocations, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllocationSummary()
        {
            return new JsonResult();
        }

        public JsonResult GetTransportContractExecution()
        {
            var contracts = _transportOrderService.FindBy(t => t.StatusID >= 3);
            var requisitions = from contract in contracts
                               select new
                               {
                                       contract.TransportOrderNo,
                                       details = from detail in
                                                     _transportOrderDetailService.FindBy(
                                                         d => d.TransportOrderID == contract.TransportOrderID)
                                                 select
                                                     new {detail.RequisitionID, detail.FDP.Name,
                                                          sum = from dispatch in _dispatchService.FindBy(r => r.RequisitionNo == detail.ReliefRequisition.RequisitionNo)
                                                            group dispatch by dispatch.RequisitionNo into d
                                                            select new { 
                                                                d.Key,
                                                                d                      
                                                            }
                                                     }
                               };
            //from requisition in requisitions 
            //select requisition.details.
            return Json(requisitions, JsonRequestBehavior.AllowGet);
        }

        public  JsonResult GetTransportContractInfo()
        {
            var contracts = _transportOrderService.FindBy(t => t.StatusID >= 3);
            var info = (
                        from contract in contracts 
                        select new
                            {
                                contract = contract.ContractNumber,
                                transporter = contract.Transporter.Name ,
                                owner = contract.Transporter.OwnerName,
                                //daysLeft = (int)(contract.EndDate - DateTime.Now).TotalDays,
                                daysLeft = DaysLeft(contract),
                                //daysToStart = (int)(contract.StartDate - DateTime.Now).TotalDays,
                                daysToStart = DaysToStart(contract),
                                //daysElapsed = (int)(DateTime.Now - contract.StartDate).TotalDays,
                                daysElapsed = DaysElapsed(contract),
                                //percentage = 50
                                duration = (int)(contract.EndDate - contract.StartDate).TotalDays,
                                percentage = ((contract.EndDate - DateTime.Now).TotalDays / (contract.EndDate - contract.StartDate).TotalDays) * 100
                            }
                       );
            return Json(info,JsonRequestBehavior.AllowGet);
        }

        public int DaysLeft( TransportOrder transportOrder)
        {
            var days = -1;

            if ((int)(transportOrder.StartDate - DateTime.Now).TotalDays>0)
            {
                days = (int) (transportOrder.EndDate - DateTime.Now).TotalDays;
            }
            return days;
        }

        public int DaysToStart(TransportOrder transportOrder)
        {
            var days = -1;

            if ((int)(transportOrder.StartDate - DateTime.Now).TotalDays < 0)
            {
                days = (int)(DateTime.Now - transportOrder.StartDate).TotalDays;
            }
            return days;
        }

        public int DaysElapsed(TransportOrder transportOrder)
        {
            var days = -1;

            if ((int)(transportOrder.StartDate - DateTime.Now).TotalDays > 0)
            {
                days = (int)(DateTime.Now - transportOrder.StartDate).TotalDays;
            }
            return days;
        }



        #region "Dashboard"

        public JsonResult GetRegions()
        {
            var regions = _adminUnitService.GetRegions().Select(r=> new
                                                                        {
                                                                            name = r.Name,
                                                                            id=r.AdminUnitID
                                                                        });
            return Json(regions, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ImportantNumbers ()
        {
            var requests =
                _reliefRequisitionService.GetAllReliefRequisition().GroupBy(s => s.Status).Select(c=> new
                                                                                                          {
                                                                                                              Status = c.Key,
                                                                                                              Count =c.Count(),
                                                                                                              regionId = c.Select(d=>d.RegionID),
                                                                                                              RegionName = c.Select(d=>d.AdminUnit.Name)
                                                                                                          });
            return Json(requests, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetRequiasitions(int id)
        {
            var requestes = _reliefRequisitionService.FindBy(s => s.Status == id).Select(r => new
                                                                                                  {
                                                                                                      reqNo= r.RequisitionNo,
                                                                                                      zone= r.AdminUnit1.Name,
                                                                                                      beneficiaries = r.ReliefRequisitionDetails.Sum(d=>d.BenficiaryNo),
                                                                                                      amount  = r.ReliefRequisitionDetails.Sum(d=>d.Amount),
                                                                                                      commodity = r.Commodity.Name,
                                                                                                      regionId  = r.RegionalRequest.RegionID,
                                                                                                      RegionName = r.RegionalRequest.AdminUnit.Name
                                                                                                  });
            return Json(requestes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSiPcAllocation()
        {
            var siPcAllocated = _sipcAllocationService.GetAll().Select(s => new
                                                                                {
                                                                                    reqNo = s.ReliefRequisitionDetail.ReliefRequisition.RequisitionNo,
                                                                                    beneficiaries = s.ReliefRequisitionDetail.BenficiaryNo,
                                                                                    amount = s.AllocatedAmount,
                                                                                    commodity = s.ReliefRequisitionDetail.ReliefRequisition.Commodity.Name,
                                                                                    allocationType = s.AllocationType,
                                                                                    regionId = s.ReliefRequisitionDetail.ReliefRequisition.RegionID,
                                                                                    RegionName =s.ReliefRequisitionDetail.ReliefRequisition.AdminUnit.Name
                                                                                });
            return Json(siPcAllocated, JsonRequestBehavior.AllowGet);
        }


        
        #endregion
    }
}