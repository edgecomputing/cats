using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Logistics.Models;
using Cats.Areas.Procurement.Models;
using Cats.Helpers;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Services.Common;
using Cats.Services.EarlyWarning;
using Cats.Services.Hub;
using Cats.Services.Logistics;
using Cats.Services.Procurement;
using Cats.Services.Security;
using Cats.ViewModelBinder;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Areas.Logistics.Controllers
{
    public class TransporterPaymentRequestController : Controller
    {
        private readonly IBusinessProcessService _BusinessProcessService;
        private readonly IBusinessProcessStateService _BusinessProcessStateService;
        private readonly IApplicationSettingService _ApplicationSettingService;
        private readonly ITransporterPaymentRequestService _transporterPaymentRequestService;
        private readonly ITransportOrderService _TransportOrderService;
        private readonly IBidWinnerService _bidWinnerService;
        private readonly IDispatchService _dispatchService;
        private readonly IWorkflowStatusService _workflowStatusService;
        private readonly IUserAccountService _userAccountService;
        private readonly Services.Procurement.ITransporterService _transporterService;
        private readonly ITransportOrderService _transportOrderService;

        public TransporterPaymentRequestController(IBusinessProcessService _paramBusinessProcessService
                                        , IBusinessProcessStateService _paramBusinessProcessStateService
                                        , IApplicationSettingService _paramApplicationSettingService
                                        , ITransporterPaymentRequestService transporterPaymentRequestService
                                        , ITransportOrderService _paramTransportOrderService
                                        , IBidWinnerService bidWinnerService
                                        , IDispatchService dispatchService
                                        , IWorkflowStatusService workflowStatusService
                                        , IUserAccountService userAccountService
                                        , Services.Procurement.ITransporterService transporterService, ITransportOrderService transportOrderService)
            {
                _BusinessProcessService=_paramBusinessProcessService;
                _BusinessProcessStateService=_paramBusinessProcessStateService;
                _ApplicationSettingService=_paramApplicationSettingService;
                _transporterPaymentRequestService = transporterPaymentRequestService;
                _TransportOrderService = _paramTransportOrderService;
                _bidWinnerService = bidWinnerService;
                _dispatchService = dispatchService;
                _workflowStatusService = workflowStatusService;
                _userAccountService = userAccountService;
                _transporterService = transporterService;
            _transportOrderService = transportOrderService;
            }
        //
        // GET: /Logistics/TransporterPaymentRequest/

        public ActionResult Index()
        {
            var list = (IEnumerable<TransporterPaymentRequest>)_transporterPaymentRequestService.Get(t => t.BusinessProcess.CurrentState.BaseStateTemplate.StateNo <= 2).OrderByDescending(t => t.BusinessProcess.CurrentState.DatePerformed);
            return View(list);
        }

        public ActionResult Promote(BusinessProcessState st, int transporterID)
        {
            _BusinessProcessService.PromotWorkflow(st);
            return RedirectToAction("PaymentRequests", "TransporterPaymentRequest", new { Area = "Logistics", transporterID });
        }

        public ActionResult BidWinningTransporters_read([DataSourceRequest] DataSourceRequest request)
        {
            var winningTransprters = _bidWinnerService.Get(t => t.Position == 1 && t.Status == 1).Select(t => t.Transporter).Distinct();
            var winningTransprterViewModels = TransporterListViewModelBinder(winningTransprters.ToList());
            return Json(winningTransprterViewModels.ToDataSourceResult(request));
        }

        public List<TransporterViewModel> TransporterListViewModelBinder(List<Transporter> transporters)
        {
            return transporters.Select(transporter =>
            {
                var firstOrDefault = _bidWinnerService.Get(t => t.TransporterID == transporter.TransporterID, null, "Bid").FirstOrDefault();
                return firstOrDefault != null ? new TransporterViewModel
                {
                    TransporterID = transporter.TransporterID,
                    TransporterName = transporter.Name,
                    BidContract = firstOrDefault.Bid.BidNumber
                } : null;
            }).ToList();
        }

        public ActionResult PaymentRequests(int transporterID)
        {
            var statuses = _workflowStatusService.GetStatus(WORKFLOW.TRANSPORT_ORDER);
            var currentUser = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name);

            var datePref = currentUser.DatePreference;
            ViewBag.TargetController = "TransporterPaymentRequest";
            var paymentRequests = _transporterPaymentRequestService
                .Get(t => t.TransportOrder.TransporterID == transporterID 
                    && t.BusinessProcess.CurrentState.BaseStateTemplate.StateNo < 2, null, "Delivery,Delivery.DeliveryDetails,TransportOrder").ToList();
            var transporterPaymentRequests = TransporterPaymentRequestViewModelBinder(paymentRequests);
            var transportOrder = _TransportOrderService.Get(t => t.TransporterID == transporterID && t.StatusID == 3, null, "Transporter").FirstOrDefault();
            var transportOrderViewModel = TransportOrderViewModelBinder.BindTransportOrderViewModel(transportOrder, datePref, statuses);
            ViewBag.TransportOrderViewModel = transportOrderViewModel;
            ViewBag.TransporterID = transportOrderViewModel.TransporterID;
            return View(transporterPaymentRequests);
        }

        public ActionResult TransporterDetail(int transporterID)
        {
            var transporter = _transporterService.FindById(transporterID);
            if (transporter == null)
            {
                return HttpNotFound();
            }
            return View(transporter);
        }

        public ActionResult TransportOrderDetail(int transportOrderID)
        {
            var transportOrder = _transportOrderService.Get(t => t.TransportOrderID == transportOrderID, null, "TransportOrderDetails.FDP,TransportOrderDetails.FDP.AdminUnit,TransportOrderDetails.Commodity,TransportOrderDetails.Hub,TransportOrderDetails.ReliefRequisition").FirstOrDefault();
            var datePref = UserAccountHelper.GetUser(User.Identity.Name).DatePreference;
            var statuses = _workflowStatusService.GetStatus(WORKFLOW.TRANSPORT_ORDER);
            var transportOrderViewModel = TransportOrderViewModelBinder.BindTransportOrderViewModel(transportOrder, datePref, statuses);
            ViewData["Transport.order.detail.ViewModel"] = transportOrder == null ? null :
                GetDetail(transportOrder.TransportOrderDetails);
            return View(transportOrderViewModel);
        }

        private IEnumerable<TransportOrderDetailViewModel> GetDetail(IEnumerable<TransportOrderDetail> transportOrderDetails)
        {

            var transportOrderDetailViewModels =
                (from itm in transportOrderDetails select BindTransportOrderDetailViewModel(itm));
            return transportOrderDetailViewModels;
        }

        private TransportOrderDetailViewModel BindTransportOrderDetailViewModel(TransportOrderDetail transportOrderDetail)
        {
            TransportOrderDetailViewModel transportOrderDetailViewModel = null;
            if (transportOrderDetail != null)
            {
                transportOrderDetailViewModel = new TransportOrderDetailViewModel
                {
                    FdpID = transportOrderDetail.FdpID,
                    FDP = transportOrderDetail.FDP.Name,
                    CommodityID = transportOrderDetail.CommodityID,
                    Commodity = transportOrderDetail.Commodity.Name,
                    DonorID = transportOrderDetail.DonorID,
                    OriginWarehouse = transportOrderDetail.Hub.Name,
                    QuantityQtl = transportOrderDetail.QuantityQtl,
                    RequisitionID = transportOrderDetail.RequisitionID,
                    RequisitionNo =
                        transportOrderDetail.ReliefRequisition.RequisitionNo,
                    SourceWarehouseID = transportOrderDetail.SourceWarehouseID,
                    TariffPerQtl = transportOrderDetail.TariffPerQtl,
                    Woreda = transportOrderDetail.FDP.AdminUnit.Name
                };
            }
            return transportOrderDetailViewModel;
        }

        public ActionResult EditCommodityTarrif([DataSourceRequest] DataSourceRequest request, int transporterPaymentRequestID, decimal CommodityTarrif = (decimal)0.00)
        {
            var transporterPaymentRequest = _transporterPaymentRequestService.Get(t=>t.TransporterPaymentRequestID == transporterPaymentRequestID, null, "TransportOrder").FirstOrDefault();
            if (transporterPaymentRequest != null)
            {
                var deliveryDetail = transporterPaymentRequest.Delivery.DeliveryDetails.FirstOrDefault();
                if (deliveryDetail != null)
                    transporterPaymentRequest.ShortageBirr = (deliveryDetail.SentQuantity - deliveryDetail.ReceivedQuantity) * CommodityTarrif;
                _transporterPaymentRequestService.EditTransporterPaymentRequest(transporterPaymentRequest);
                return RedirectToAction("PaymentRequests", "TransporterPaymentRequest", new { Area = "Logistics", transporterID = transporterPaymentRequest.TransportOrder.TransporterID });
            }
            else
            {
                return RedirectToAction("Index");
            }
            //return Json(new[] { true }.ToDataSourceResult(request, ModelState));
        }

        public List<TransporterPaymentRequestViewModel> TransporterPaymentRequestViewModelBinder(List<TransporterPaymentRequest> transporterPaymentRequests)
        {
            var transporterPaymentRequestViewModels = new List<TransporterPaymentRequestViewModel>();
            foreach (var transporterPaymentRequest in transporterPaymentRequests)
            {
                var request = transporterPaymentRequest;
                var dispatch = _dispatchService.Get(t => t.DispatchID == request.Delivery.DispatchID, null, "Hub, FDP").FirstOrDefault();
                var firstOrDefault = _bidWinnerService.Get(t => t.SourceID == dispatch.HubID && t.DestinationID == dispatch.FDPID
                    && t.TransporterID == request.TransportOrder.TransporterID && t.Bid.BidNumber == dispatch.BidNumber).FirstOrDefault();
                var tarrif = (decimal)0.00;
                if (firstOrDefault != null)
                {
                    tarrif = firstOrDefault.Tariff != null ? (decimal)firstOrDefault.Tariff : (decimal)0.00;
                }
                if (dispatch != null && request.Delivery.DeliveryDetails.FirstOrDefault() != null)
                {
                    var deliveryDetail = request.Delivery.DeliveryDetails.FirstOrDefault();
                    var businessProcess = _BusinessProcessService.FindById(request.BusinessProcessID);
                    if (request.LabourCost == null)
                        request.LabourCost = (decimal)0.00;
                    if (request.RejectedAmount == null)
                        request.RejectedAmount = (decimal)0.00;
                    if (deliveryDetail != null)
                    {
                        var transporterPaymentRequestViewModel = new TransporterPaymentRequestViewModel()
                        {
                            RequisitionNo = dispatch.RequisitionNo,
                            GIN = request.Delivery.InvoiceNo,
                            GRN = request.Delivery.ReceivingNumber,
                            Commodity = deliveryDetail.Commodity.Name,
                            Source = dispatch.Hub.Name,
                            Destination = dispatch.FDP.Name,
                            ReceivedQty = deliveryDetail.ReceivedQuantity,
                            Tarrif = tarrif,
                            ShortageQty = deliveryDetail.SentQuantity - deliveryDetail.ReceivedQuantity,
                            ShortageBirr = request.ShortageBirr,
                            SentQty = deliveryDetail.SentQuantity,
                            BusinessProcessID = request.BusinessProcessID,
                            DeliveryID = request.DeliveryID,
                            ReferenceNo = request.ReferenceNo,
                            TransportOrderID = request.TransportOrderID,
                            TransporterPaymentRequestID = request.TransporterPaymentRequestID,
                            FreightCharge = (decimal)(request.ShortageBirr != null ? (deliveryDetail.ReceivedQuantity * tarrif) - request.ShortageBirr + request.LabourCost - request.RejectedAmount : (deliveryDetail.ReceivedQuantity * tarrif) + request.LabourCost - request.RejectedAmount),
                            BusinessProcess = businessProcess,
                            LabourCost = request.LabourCost,
                            LabourCostRate = request.LabourCostRate,
                            RejectedAmount = request.RejectedAmount,
                            RejectionReason = request.RejectionReason,
                            RequestedDate = request.RequestedDate
                        };
                        transporterPaymentRequestViewModels.Add(transporterPaymentRequestViewModel);
                    }
                }
            }
            return transporterPaymentRequestViewModels;
        }


    }
}
