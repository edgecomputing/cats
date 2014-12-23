using Cats.Areas.Logistics.Models;
using Cats.Areas.Procurement.Models;
using Cats.Helpers;
using Cats.Infrastructure;
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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using ICommonService = Cats.Services.Common.ICommonService;

namespace Cats.Areas.Logistics.Controllers
{
    [Authorize]
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
        private readonly ITransportOrderDetailService _transportOrderDetailService;
        private readonly ICommonService _commodityService;
        private readonly IFlowTemplateService _flowTemplateService;

        public TransporterPaymentRequestController(IBusinessProcessService _paramBusinessProcessService
                                                   , IBusinessProcessStateService _paramBusinessProcessStateService
                                                   , IApplicationSettingService _paramApplicationSettingService
                                                   , ITransporterPaymentRequestService transporterPaymentRequestService
                                                   , ITransportOrderService _paramTransportOrderService
                                                   , IBidWinnerService bidWinnerService
                                                   , IDispatchService dispatchService
                                                   , IWorkflowStatusService workflowStatusService
                                                   , IUserAccountService userAccountService
                                                   , Services.Procurement.ITransporterService transporterService,
                                                   ITransportOrderService transportOrderService,
                                                   ITransportOrderDetailService transportOrderDetailService, ICommonService commodityService
                                                   , IFlowTemplateService flowTemplateService)
        {
            _BusinessProcessService = _paramBusinessProcessService;
            _BusinessProcessStateService = _paramBusinessProcessStateService;
            _ApplicationSettingService = _paramApplicationSettingService;
            _transporterPaymentRequestService = transporterPaymentRequestService;
            _TransportOrderService = _paramTransportOrderService;
            _bidWinnerService = bidWinnerService;
            _dispatchService = dispatchService;
            _workflowStatusService = workflowStatusService;
            _userAccountService = userAccountService;
            _transporterService = transporterService;
            _transportOrderService = transportOrderService;
            _transportOrderDetailService = transportOrderDetailService;
            _commodityService = commodityService;
            _flowTemplateService = flowTemplateService;
        }

        //
        // GET: /Logistics/TransporterPaymentRequest/

        public ActionResult Index()
        {
            var list =
                (IEnumerable<TransporterPaymentRequest>)
                _transporterPaymentRequestService.Get(t => t.BusinessProcess.CurrentState.BaseStateTemplate.StateNo <= 2)
                    .OrderByDescending(t => t.BusinessProcess.CurrentState.DatePerformed);
            return View(list);
        }

        public ActionResult PromoteList(List<ProcessTemplateViewModel> requests, int transporterId, string actionType)
        {
            if (requests != null)
            {
                var type = typeof(ActionType);
                MemberInfo[] memInfo = null;

                if (String.Compare(actionType, Enum.GetName(typeof(ActionType), ActionType.Finance), true) == 0)
                    memInfo = type.GetMember(ActionType.Finance.ToString());

                if (String.Compare(actionType, Enum.GetName(typeof(ActionType), ActionType.Approve), true) == 0)
                    memInfo = type.GetMember(ActionType.Approve.ToString());

                var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                var description = ((DescriptionAttribute)attributes[0]).Description;
                var templateModel = _flowTemplateService.FindBy(o => o.Name == description).First();

                if (templateModel != null)
                    requests.ForEach(o => o.StateID = templateModel.FinalStateID);

                var processTemplates = ProcessTemplateViewModelBinder.BindProcessTemplateViewModel(requests);

                foreach (var oTemplate in processTemplates)
                {
                    _BusinessProcessService.PromotWorkflow(oTemplate);
                }
            }

            return RedirectToAction("PaymentRequests", "TransporterPaymentRequest",
                                    new { Area = "Logistics", transporterId });
        }

        public ActionResult Promote(BusinessProcessState st, int transporterID)
        {
            _BusinessProcessService.PromotWorkflow(st);
            return RedirectToAction("PaymentRequests", "TransporterPaymentRequest",
                                    new { Area = "Logistics", transporterID });
        }

        public ActionResult OLDBidWinningTransporters_read([DataSourceRequest] DataSourceRequest request)
        {
            var winningTransprters =
                _bidWinnerService.Get(t => t.Position == 1 && t.Status == 1).Select(t => t.Transporter).Distinct();
            var winningTransprterViewModels = TransporterListViewModelBinder(winningTransprters.ToList());
            return Json(winningTransprterViewModels.ToDataSourceResult(request));
        }

        public ActionResult BidWinningTransporters_read([DataSourceRequest] DataSourceRequest request)
        {
            var transprtersWithActiveTO =
                _transportOrderService.Get(t => t.StatusID >= 3, null, "Transporter").Select(t => t.Transporter).
                    Distinct();
            var winningTransprterViewModels = TransporterListViewModelBinder(transprtersWithActiveTO.ToList());
            return Json(winningTransprterViewModels.ToDataSourceResult(request));
        }

        public List<TransporterViewModel> TransporterListViewModelBinder(List<Transporter> transporters)
        {
            return transporters.Select(transporter =>
                                           {
                                               var firstOrDefault =
                                                   _bidWinnerService.Get(
                                                       t => t.TransporterID == transporter.TransporterID, null, "Bid").
                                                       FirstOrDefault();
                                               return firstOrDefault != null
                                                          ? new TransporterViewModel
                                                                {
                                                                    TransporterID = transporter.TransporterID,
                                                                    TransporterName = transporter.Name,
                                                                    BidContract = firstOrDefault.Bid.BidNumber
                                                                }
                                                          : null;
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
                          && t.BusinessProcess.CurrentState.BaseStateTemplate.StateNo < 2, null,
                     "Delivery,Delivery.DeliveryDetails,TransportOrder").ToList();
            var transporterPaymentRequests = TransporterPaymentRequestViewModelBinder(paymentRequests);
            var transportOrder =
                _TransportOrderService.Get(t => t.TransporterID == transporterID && t.StatusID >= 3, null, "Transporter")
                    .FirstOrDefault();
            var transportOrderViewModel = TransportOrderViewModelBinder.BindTransportOrderViewModel(transportOrder,
                                                                                                    datePref, statuses);
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
            var transportOrder =
                _transportOrderService.Get(t => t.TransportOrderID == transportOrderID, null,
                                           "TransportOrderDetails.FDP,TransportOrderDetails.FDP.AdminUnit,TransportOrderDetails.Commodity,TransportOrderDetails.Hub,TransportOrderDetails.ReliefRequisition")
                    .FirstOrDefault();
            var datePref = UserAccountHelper.GetUser(User.Identity.Name).DatePreference;
            var statuses = _workflowStatusService.GetStatus(WORKFLOW.TRANSPORT_ORDER);
            var transportOrderViewModel = TransportOrderViewModelBinder.BindTransportOrderViewModel(transportOrder,
                                                                                                    datePref, statuses);
            ViewData["Transport.order.detail.ViewModel"] = transportOrder == null
                                                               ? null
                                                               : GetDetail(transportOrder.TransportOrderDetails);
            return View(transportOrderViewModel);
        }

        private IEnumerable<TransportOrderDetailViewModel> GetDetail(
            IEnumerable<TransportOrderDetail> transportOrderDetails)
        {
            var transportOrderDetailViewModels =
                (from itm in transportOrderDetails select BindTransportOrderDetailViewModel(itm));
            return transportOrderDetailViewModels;
        }

        private TransportOrderDetailViewModel BindTransportOrderDetailViewModel(
            TransportOrderDetail transportOrderDetail)
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

        public ActionResult EditCommodityTarrif([DataSourceRequest] DataSourceRequest request,
                                                int transporterPaymentRequestID,
                                                decimal CommodityTarrif = (decimal) 0.00, decimal tariff = (decimal)0.00)
        {
            var transporterPaymentRequest =
                _transporterPaymentRequestService.Get(
                    t => t.TransporterPaymentRequestID == transporterPaymentRequestID, null, "TransportOrder").
                    FirstOrDefault();
            if (transporterPaymentRequest != null)
            {
                var deliveryDetail = transporterPaymentRequest.Delivery.DeliveryDetails.FirstOrDefault();
                if (deliveryDetail != null)
                {
                    if (transporterPaymentRequest.ShortageQty <= 0)
                    {
                        transporterPaymentRequest.ShortageBirr = (0 * CommodityTarrif);
                    }
                    else
                    {
                        transporterPaymentRequest.ShortageBirr = (transporterPaymentRequest.ShortageQty) * CommodityTarrif * tariff;
                    }
                }
                _transporterPaymentRequestService.EditTransporterPaymentRequest(transporterPaymentRequest);
                return RedirectToAction("PaymentRequests", "TransporterPaymentRequest",
                                        new
                                            {
                                                Area = "Logistics",
                                                transporterID = transporterPaymentRequest.TransportOrder.TransporterID
                                            });
            }
            else
            {
                return RedirectToAction("Index");
            }
            //return Json(new[] { true }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult RejectPaymentRequest([DataSourceRequest] DataSourceRequest request,
                                                 int transporterPaymentRequestID)
        {
            var transporterPaymentRequest =
                _transporterPaymentRequestService.Get(
                    t => t.TransporterPaymentRequestID == transporterPaymentRequestID, null, "Delivery").FirstOrDefault();
            if (transporterPaymentRequest != null)
            {
                _transporterPaymentRequestService.Reject(transporterPaymentRequest);
                return Json(new[] { true }.ToDataSourceResult(request, ModelState));
            }
            else
            {
                return Json(new[] { false }.ToDataSourceResult(request, ModelState));
            }
            //return Json(new[] { true }.ToDataSourceResult(request, ModelState));
        }

        public List<TransporterPaymentRequestViewModel> TransporterPaymentRequestViewModelBinder(
            List<TransporterPaymentRequest> transporterPaymentRequests)
        {
            var currentUser = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name);

            var datePref = currentUser.DatePreference;

            var transporterPaymentRequestViewModels = new List<TransporterPaymentRequestViewModel>();
            foreach (var transporterPaymentRequest in transporterPaymentRequests)
            {
                var request = transporterPaymentRequest;

                var dispatch =
                    _dispatchService.Get(t => t.DispatchID == request.Delivery.DispatchID, null, "Hub, FDP").
                        FirstOrDefault();
                var transportOrderdetail =
                    _transportOrderDetailService.FindBy(
                        m =>
                        m.TransportOrderID == request.TransportOrderID && m.SourceWarehouseID == dispatch.HubID &&
                        m.FdpID == dispatch.FDPID).FirstOrDefault();
                //var firstOrDefault = _bidWinnerService.Get(t => t.SourceID == dispatch.HubID && t.DestinationID == dispatch.FDPID
                //    && t.TransporterID == request.TransportOrder.TransporterID && t.Bid.BidNumber == dispatch.BidNumber).FirstOrDefault();
                var tarrif = (decimal)0.00;
                if (transportOrderdetail != null)
                {
                    tarrif = (decimal)transportOrderdetail.TariffPerQtl;
                }
                if (dispatch != null && request.Delivery.DeliveryDetails.FirstOrDefault() != null)
                {
                    {
                        var dispathedAmount = (decimal)0.0;
                        var childCommodity = string.Empty;
                        var firstOrDefault = dispatch.DispatchDetails.FirstOrDefault();
                        if (firstOrDefault != null)
                        {
                            dispathedAmount = firstOrDefault.DispatchedQuantityInMT.ToQuintal();
                        }

                        var dispatchedDate = dispatch.DispatchDate.Date;
                        var dispatchDetail = dispatch.DispatchDetails.FirstOrDefault();
                        if (dispatchDetail != null)
                        {
                            var childCommodityId = dispatchDetail.CommodityChildID;

                            var orDefault = _commodityService.GetCommodities(c => c.CommodityID == childCommodityId).FirstOrDefault();
                            if (orDefault != null)
                            {
                                childCommodity = orDefault.Name;
                            }
                        }
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
                                                                             ReceivedQty =
                                                                                 deliveryDetail.ReceivedQuantity.
                                                                                 ToQuintal(),
                                                                             Tarrif = tarrif,
                                                                             ShortageQty = request.ShortageQty != null ? (decimal)(request.ShortageQty) : (deliveryDetail.SentQuantity.ToQuintal()) -
                                                                                                                                                          (deliveryDetail.ReceivedQuantity.
                                                                                                                                                              ToQuintal()),
                                                                             ShortageBirr = request.ShortageBirr,
                                                                             SentQty = deliveryDetail.SentQuantity,
                                                                             BusinessProcessID =
                                                                                 request.BusinessProcessID,
                                                                             DeliveryID = request.DeliveryID,
                                                                             ReferenceNo = request.ReferenceNo,
                                                                             TransportOrderID = request.TransportOrderID,
                                                                             TransporterPaymentRequestID =
                                                                                 request.TransporterPaymentRequestID,
                                                                             FreightCharge =
                                                                                 (decimal)
                                                                                 (request.ShortageBirr != null
                                                                                      ? (Math.Min(deliveryDetail.ReceivedQuantity
                                                                                             .ToQuintal(), dispathedAmount) * tarrif) - (
                                                                                        request.ShortageBirr +
                                                                                        request.LabourCost -
                                                                                        request.RejectedAmount)
                                                                                      : (deliveryDetail.ReceivedQuantity
                                                                                             .ToQuintal() * tarrif) +
                                                                                        request.LabourCost -
                                                                                        request.RejectedAmount),
                                                                             BusinessProcess = businessProcess,
                                                                             LabourCost = request.LabourCost,
                                                                             LabourCostRate = request.LabourCostRate,
                                                                             RejectedAmount = request.RejectedAmount,
                                                                             RejectionReason = request.RejectionReason,
                                                                             RequestedDate = request.RequestedDate,
                                                                             Program = dispatch.DispatchAllocation.Program,
                                                                             Transporter = dispatch.Transporter,
                                                                             ChildCommodity = childCommodity,
                                                                             DispatchDate = dispatchedDate.ToCTSPreferedDateFormat(datePref),
                                                                             DispatchedAmount = dispathedAmount
                                                                         };
                            transporterPaymentRequestViewModels.Add(transporterPaymentRequestViewModel);
                        }
                    }
                }
            }
            return transporterPaymentRequestViewModels;
        }

        public ActionResult LossAmount([DataSourceRequest] DataSourceRequest request, int transporterPaymentRequestForLossID,
                                       float lossQty = 0, string lossReason = "")
        {
            var transporterPaymentRequest =
                _transporterPaymentRequestService.Get(
                    t => t.TransporterPaymentRequestID == transporterPaymentRequestForLossID, null).FirstOrDefault();
            if (transporterPaymentRequest != null)
            {
                var deliveryDetail = transporterPaymentRequest.Delivery.DeliveryDetails.FirstOrDefault();
                if (deliveryDetail != null)
                {
                    transporterPaymentRequest.ShortageQty = (int?)lossQty;
                    transporterPaymentRequest.LossReason = lossReason;

                    if (transporterPaymentRequest.ShortageQty <= 0)
                    {
                        transporterPaymentRequest.ShortageBirr = 0;
                    }

                    _transporterPaymentRequestService.EditTransporterPaymentRequest(transporterPaymentRequest);
                    return RedirectToAction("PaymentRequests", "TransporterPaymentRequest",
                                            new
                                                {
                                                    Area = "Logistics",
                                                    transporterID = transporterPaymentRequest.TransportOrder.TransporterID
                                                });
                }
                return RedirectToAction("PaymentRequests", "TransporterPaymentRequest",
                                        new
                                            {
                                                Area = "Logistics",
                                                transporterID = transporterPaymentRequest.TransportOrder.TransporterID
                                            });
            }
            return RedirectToAction("PaymentRequests", "TransporterPaymentRequest",
                                    new
                                        {
                                            Area = "Logistics",
                                            transporterID = transporterPaymentRequest.TransportOrder.TransporterID
                                        });
        }

        public ActionResult PrintPaymentRequest([DataSourceRequest] DataSourceRequest request, int transporterId, string refno = "", string programname = "All")
        {
            var statuses = _workflowStatusService.GetStatus(WORKFLOW.TRANSPORT_ORDER);
            var currentUser = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name);

            var datePref = currentUser.DatePreference;
            var paymentRequests = _transporterPaymentRequestService
                .Get(t => t.TransportOrder.TransporterID == transporterId
                          && t.BusinessProcess.CurrentState.BaseStateTemplate.StateNo < 2, null,
                     "Delivery,Delivery.DeliveryDetails,TransportOrder").ToList();
            List<TransporterPaymentRequestViewModel> transporterPaymentRequests;
            if (refno == "" && programname == "All")
            {
                transporterPaymentRequests = TransporterPaymentRequestViewModelBinder(paymentRequests);
            }
            else
            {
                transporterPaymentRequests = (programname == "All") ? TransporterPaymentRequestViewModelBinder(paymentRequests).Where(t => t.ReferenceNo.Contains(refno)).ToList() : TransporterPaymentRequestViewModelBinder(paymentRequests).Where(t => t.ReferenceNo.Contains(refno) && t.Program.Name == programname).ToList();
            }
            var transportOrder = _transportOrderService.Get(t => t.TransporterID == transporterId && t.StatusID >= 3, null, "Transporter").FirstOrDefault();
            var transportOrderViewModel = TransportOrderViewModelBinder.BindTransportOrderViewModel(transportOrder, datePref, statuses);
            var TransportOrderViewModel = transportOrderViewModel;

            var reportPath = Server.MapPath("~/Report/Finance/TransporterPaymentRequest.rdlc");
            var reportDataArray = new object[2];
            var dsNameArray = new string[2];
            var tprvm = (from data in transporterPaymentRequests
                         where (data.BusinessProcess.CurrentState.BaseStateTemplate.Name == "Request Verified")
                         select new
                         {
                             RequisitionNo = data.RequisitionNo,
                             GIN = data.GIN,
                             GRN = data.GRN,
                             CommodityName = data.Commodity,
                             childCommodity = data.ChildCommodity,
                             Source = data.Source,
                             Destination = data.Destination,
                             ReceivedQuantity = data.ReceivedQty,
                             Tariff = data.Tarrif,
                             ShortageQty = data.ShortageQty,
                             ShortageBirr = data.ShortageBirr,
                             FreightCharge = data.FreightCharge
                         });

            var tovm = new List<TransportOrderViewModel> { TransportOrderViewModel };
            reportDataArray[0] = tprvm;
            reportDataArray[1] = tovm;

            dsNameArray[0] = "TransporterPayReq";
            dsNameArray[1] = "TOVM";
            var result = ReportHelper.PrintReport(reportPath, reportDataArray, dsNameArray, "PDF", false);
            return File(result.RenderBytes, result.MimeType);
        }

        public ActionResult PrintLetter(int id = 0)
        {
            var reportPath = Server.MapPath("~/Report/Finance/TransportPaymentRequestLetter.rdlc");
            var reportData = PaymentRequestForPrint(id);
            var dataSourceName = "TPRL";
            var result = ReportHelper.PrintReport(reportPath, reportData, dataSourceName);
            return File(result.RenderBytes, result.MimeType);
        }

        public System.Collections.IEnumerable PaymentRequestForPrint(int transporterId)
        {
            var list = (IEnumerable<Cats.Models.TransporterPaymentRequest>)_transporterPaymentRequestService
                        .Get(t => t.BusinessProcess.CurrentState.BaseStateTemplate.StateNo < 2, null, "BusinessProcess")
                        .OrderByDescending(t => t.TransporterPaymentRequestID);
            var transporterPaymentRequests = TransporterPaymentRequestViewModelBinder(list.ToList()).Where(t => t.BusinessProcess.CurrentState.BaseStateTemplate.Name == "Request Verified");

            var requests = transporterPaymentRequests.GroupBy(ac => new { ac.Transporter.Name, ac.Commodity, ac.Source }).Select(ac => new
            {
                TransporterName = ac.Key.Name,
                TransporterId = ac.FirstOrDefault().Transporter.TransporterID,
                CommodityName = ac.Key.Commodity,
                SourceName = ac.Key.Source,
                ReceivedQuantity = ac.Sum(s => s.ReceivedQty),
                ShortageQuantity = ac.Sum(s => s.ShortageQty),
                ShortageBirr = ac.Sum(s => s.ShortageBirr),
                FreightCharge = ac.Sum(s => s.FreightCharge),
                ShortageBirrInWords = ac.Sum(s => s.ShortageBirr).ToNumWordsWrapper(),
                FreightChargeInWords = Math.Round(ac.Sum(s => s.FreightCharge),2).ToNumWordsWrapper()
            });
            return requests.Where(m => m.TransporterId == transporterId).ToArray();
        }
    }
}