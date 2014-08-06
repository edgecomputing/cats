using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Procurement.Models;
using Cats.Helpers;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Models.ViewModels;
using Cats.Services.EarlyWarning;
using Cats.Services.Procurement;
using Cats.Services.Security;
using Cats.ViewModelBinder;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Areas.Hub.Controllers
{
    public class TransportOrderController : Controller
    {
        //
        // GET: /Hub/TransportOrder/
        private readonly ITransportOrderService _transportOrderService;
        private readonly IWorkflowStatusService _workflowStatusService;
        private readonly IUserAccountService _userAccountService;
        public TransportOrderController(ITransportOrderService transportOrderService, ITransReqWithoutTransporterService transReqWithoutTransporterService, IWorkflowStatusService workflowStatusService, IUserAccountService userAccountService)
        {
            _transportOrderService = transportOrderService;
            _workflowStatusService = workflowStatusService;
            _userAccountService = userAccountService;
        }


        public ActionResult NotificationIndex(int recordId)
        {

            NotificationHelper.MakeNotificationRead(recordId);
            return RedirectToAction("Index", new { id = (int) TransportOrderStatus.Approved });//get approved transport orders

        }

        public ActionResult ReturnListOfApprovedListFromMainMenu()
        {


            return RedirectToAction("Index", new { id = (int)TransportOrderStatus.Approved });//get approved transport orders

        }

        public ViewResult Index(int id = 0)
        {
            ViewBag.TransportOrdrStatus = id;
            ViewBag.TransportOrderTitle = id == 0
                                              ? "Draft"
                                              : _workflowStatusService.GetStatusName(WORKFLOW.TRANSPORT_ORDER, id);
            return View();
        }

        public ActionResult TransportOrder_Read([DataSourceRequest] DataSourceRequest request, int id = 0)
        {
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            var hubId = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DefaultHub.HasValue ? _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DefaultHub.Value : 0 ;
            var transportOrders = id == 0 ? _transportOrderService.GetByHub(t => t.StatusID == (int)TransportOrderStatus.Draft, includeProperties: "TransportOrderDetails", hubId: hubId, statusId: (int)TransportOrderStatus.Draft)
                .OrderByDescending(m => m.TransportOrderID)
                .ToList() : _transportOrderService.GetByHub(t => t.StatusID == id, includeProperties: "TransportOrderDetails", hubId: hubId, statusId: id).ToList();

            //var transportOrders = id == 0 ? _transportOrderService.Get(t => t.StatusID == (int)TransportOrderStatus.Draft, includeProperties: "TransportOrderDetails")
            //    .OrderByDescending(m => m.TransportOrderID)
            //    .ToList() : _transportOrderService.Get(t => t.StatusID == id).ToList();
            
            var statuses = _workflowStatusService.GetStatus(WORKFLOW.TRANSPORT_ORDER);
            var transportOrderViewModels = TransportOrderViewModelBinder.BindListTransportOrderViewModel(
                transportOrders, datePref, statuses);
            return Json(transportOrderViewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }


        public ActionResult TransportContract(int id)
        {
            var transportOrder = _transportOrderService.FindById(id);
            ViewBag.HubID = _transportOrderService.GetHubs();
            ViewBag.TransportOrderID = id;
            var transportContract = GetTransportOrder(transportOrder);
            return View(transportContract);
        }
        public ActionResult Contract_Read([DataSourceRequest] DataSourceRequest request, int id = 0)
        {
            var transportOrder = _transportOrderService.Get(m => m.TransportOrderID == id, null, "TransportOrderDetails").OrderByDescending(m => m.TransportOrderID).FirstOrDefault();
            if (transportOrder != null)
            {
                var detailToDisplay = GetTransportContract(transportOrder).ToList();
                return Json(detailToDisplay.ToDataSourceResult(request));
            }
            return RedirectToAction("Index");
        }


        private IEnumerable<TransportOrderDetailViewModel> GetTransportContract(TransportOrder transportOrder)
        {
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            var hubId = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DefaultHub.HasValue ? _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DefaultHub.Value : 0;
            var transportContractDetail = transportOrder.TransportOrderDetails.Where(m=>m.SourceWarehouseID==hubId);
            return (from detail in transportContractDetail
                    select new TransportOrderDetailViewModel()
                    {
                        TransportOrderID = detail.TransportOrderID,
                        CommodityID = detail.CommodityID,
                        SourceWarehouseID = detail.SourceWarehouseID,
                        QuantityQtl = detail.QuantityQtl.ToPreferedWeightUnit(),
                        RequisitionID = detail.RequisitionID,
                        TariffPerQtl = detail.TariffPerQtl,
                        Commodity = detail.Commodity.Name,
                        OriginWarehouse = detail.Hub.Name,
                        HubID = detail.Hub.HubID,
                        Woreda = detail.FDP.AdminUnit.Name,
                        FDP = detail.FDP.Name,
                        RequisitionNo = detail.ReliefRequisition.RequisitionNo

                    });

            // return transportContractDetail;
        }
        private TransportContractViewModel GetTransportOrder(TransportOrder transportOrder)
        {
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            var transportContract = new TransportContractViewModel()
            {
                TransportOrderID = transportOrder.TransportOrderID,
                TransportOrderNo = transportOrder.TransportOrderNo,
                TransporterID = transportOrder.TransporterID,
                RequisitionNo = transportOrder.TransportOrderDetails.First().ReliefRequisition.RequisitionNo,
                Transporter = transportOrder.Transporter.Name,
                BidDocumentNo = transportOrder.BidDocumentNo,
                ConsignerName = transportOrder.ConsignerName,
                ContractNumber = transportOrder.ContractNumber,
                OrderDate = transportOrder.OrderDate.ToCTSPreferedDateFormat(datePref),
                OrderExpiryDate = transportOrder.OrderExpiryDate.ToCTSPreferedDateFormat(datePref),
                RequestedDispatchDate = transportOrder.RequestedDispatchDate.ToCTSPreferedDateFormat(datePref),
                ConsignerDate = transportOrder.ConsignerDate.ToCTSPreferedDateFormat(datePref),
                PerformanceBondReceiptNo = transportOrder.PerformanceBondReceiptNo,
                TransporterSignedDate = transportOrder.TransporterSignedDate.ToCTSPreferedDateFormat(datePref),
                TransporterSignedName = transportOrder.TransporterSignedName,
                StatusID = transportOrder.StatusID,
                Zone = transportOrder.TransportOrderDetails.First().FDP.AdminUnit.AdminUnit2.Name,
                Region = transportOrder.TransportOrderDetails.First().FDP.AdminUnit.AdminUnit2.AdminUnit2.Name

            };
            return transportContract;
        }

    }
}
