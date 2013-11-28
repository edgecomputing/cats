using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Services.EarlyWarning;
using Cats.Services.Procurement;
using Cats.Services.Security;
using hub = Cats.Services.Hub;

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
       
        public HomeController(IReliefRequisitionService reliefRequisitionService,
            hub.IDispatchAllocationService dispatchAllocationService,
            IUserAccountService userAccountService,
            ITransportOrderService transportOrderService,
            ITransportOrderDetailService transportOrderDetailService,
            hub.DispatchService dispatchService,
            hub.DispatchDetailService dispatchDetailService)
        {
            this._reliefRequisitionService = reliefRequisitionService;
            _dispatchAllocationService = dispatchAllocationService;
            _userAccountService = userAccountService;
            _transportOrderService = transportOrderService;
            _transportOrderDetailService = transportOrderDetailService;
            _dispatchService = dispatchService;
            _dispatchDetailService = dispatchDetailService; 
        }

        public ActionResult Index()
        {
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
                                daysLeft = (int)(contract.EndDate - DateTime.Now).TotalDays,
                                daysToStart = (int)(contract.StartDate - DateTime.Now).TotalDays,
                                daysElapsed = (int)(DateTime.Now - contract.StartDate).TotalDays,
                                //percentage = 50
                                duration = (int)(contract.EndDate - contract.StartDate).TotalDays,
                                percentage = ((contract.EndDate - DateTime.Now).TotalDays / (contract.EndDate - contract.StartDate).TotalDays) * 100
                            }
                       );
            return Json(info,JsonRequestBehavior.AllowGet);
        }
    }
}