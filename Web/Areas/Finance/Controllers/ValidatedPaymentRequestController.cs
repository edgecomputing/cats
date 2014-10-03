using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Finance.Models;
using Cats.Areas.Procurement.Models;
using Cats.Helpers;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Services.Common;
using Cats.Services.EarlyWarning;
using Cats.Services.Finance;
using Cats.Services.Hub;
using Cats.Services.Logistics;
using Cats.Services.Procurement;
using Cats.Services.Security;
using Cats.ViewModelBinder;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ITransporterService = Cats.Services.Procurement.ITransporterService;
using IUserProfileService = Cats.Services.Administration.IUserProfileService;

namespace Cats.Areas.Finance.Controllers
{
    public class ValidatedPaymentRequestController : Controller
    {
        private readonly IBusinessProcessService _businessProcessService;
        private readonly IBusinessProcessStateService _businessProcessStateService;
        private readonly IApplicationSettingService _applicationSettingService;
        private readonly ITransportOrderService _transportOrderService;
        private readonly ITransporterAgreementVersionService _transporterAgreementVersionService;
        private readonly IWorkflowStatusService _workflowStatusService;
        private readonly ITransporterService _transporterService;
        private readonly ITransporterChequeService _transporterChequeService;
        private readonly IUserProfileService _userProfileService;
        private readonly IBidWinnerService _bidWinnerService;
        private readonly ITransporterPaymentRequestService _transporterPaymentRequestService;
        private readonly IUserAccountService _userAccountService;
        private readonly IDispatchService _dispatchService;
        private readonly ITransporterChequeDetailService _transporterChequeDetailService;

        public ValidatedPaymentRequestController(IBusinessProcessService paramBusinessProcessService
                                        , IBusinessProcessStateService paramBusinessProcessStateService
                                        , IApplicationSettingService paramApplicationSettingService
                                        , ITransportOrderService paramTransportOrderService
                                        , ITransporterAgreementVersionService transporterAgreementVersionService
                                        , IWorkflowStatusService workflowStatusService, ITransporterService transporterService
                                        , ITransporterChequeService transporterChequeService, IUserProfileService userProfileService, ITransporterPaymentRequestService transporterPaymentRequestService, IBidWinnerService bidWinnerService, IUserAccountService userAccountService, IDispatchService dispatchService, ITransporterChequeDetailService transporterChequeDetailService)
            {

                _businessProcessService = paramBusinessProcessService;
                _businessProcessStateService = paramBusinessProcessStateService;
                _applicationSettingService = paramApplicationSettingService;
                _transportOrderService = paramTransportOrderService;
                _transporterAgreementVersionService = transporterAgreementVersionService;
                _workflowStatusService = workflowStatusService;
                _transporterService = transporterService;
                _transporterChequeService = transporterChequeService;
                 _userProfileService = userProfileService;
                _transporterPaymentRequestService = transporterPaymentRequestService;
                _bidWinnerService = bidWinnerService;
                _userAccountService = userAccountService;
                _dispatchService = dispatchService;
                _transporterChequeDetailService = transporterChequeDetailService;
            }
        //
        // GET: /Procurement/ValidatedPaymentRequest/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OLDBidWinningTransporters_read([DataSourceRequest] DataSourceRequest request)
        {
            var winningTransprters = _bidWinnerService.Get(t => t.Position == 1 && t.Status == 1).Select(t => t.Transporter).Distinct();
            var winningTransprterViewModels = TransporterListViewModelBinder(winningTransprters.ToList());
            return Json(winningTransprterViewModels.ToDataSourceResult(request));
        }

        public ActionResult BidWinningTransporters_read([DataSourceRequest] DataSourceRequest request)
        {
            var transprtersWithActiveTO = _transportOrderService.Get(t => t.StatusID > 3, null, "Transporter").Select(t => t.Transporter).Distinct();
            var winningTransprterViewModels = TransporterListViewModelBinder(transprtersWithActiveTO.ToList());
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
            ViewBag.TargetController = "ValidatedPaymentRequest";
            var list = (IEnumerable<Cats.Models.TransporterPaymentRequest>)_transporterPaymentRequestService
                        .Get(t => t.BusinessProcess.CurrentState.BaseStateTemplate.StateNo >= 2 && t.TransportOrder.TransporterID == transporterID, null, "BusinessProcess")
                        .OrderByDescending(t => t.TransporterPaymentRequestID);
            var transporterPaymentRequests = TransporterPaymentRequestViewModelBinder(list.ToList());
            var transportOrder = _transportOrderService.Get(t => t.TransporterID == transporterID && t.StatusID >= 3, null, "Transporter").FirstOrDefault();
            var transportOrderViewModel = TransportOrderViewModelBinder.BindTransportOrderViewModel(transportOrder, datePref, statuses);
            ViewBag.TransportOrderViewModel = transportOrderViewModel;
            ViewBag.TransporterID = transportOrderViewModel.TransporterID;
            ViewBag.ApprovedPRCount = 
                _transporterPaymentRequestService.Get(t =>t.BusinessProcess.CurrentState.BaseStateTemplate.StateNo == 3, null,"BusinessProcess").Count();
            return View(transporterPaymentRequests);
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
                    var businessProcess = _businessProcessService.FindById(request.BusinessProcessID);
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

        //public ActionResult Promote(BusinessProcessState st, int PaymentRequestID, int transporterID)
        //{
        //    _businessProcessService.PromotWorkflow(st);
        //    var transporterChequeObj =
        //        _transporterChequeService.Get(t => t.TransporterPaymentRequestID == PaymentRequestID).FirstOrDefault();
        //    if (transporterChequeObj != null)
        //    {
        //        if (st.StateID == (int) Cats.Models.Constant.StateTemplate.ChequeApproved)
        //        {
        //            transporterChequeObj.AppovedBy = UserAccountHelper.GetUser(User.Identity.Name).UserProfileID;
        //            transporterChequeObj.AppovedDate = DateTime.Now;
        //            transporterChequeObj.Status = (int)Cats.Models.Constant.ChequeStatus.ChequeApproved;
        //            _transporterChequeService.EditTransporterCheque(transporterChequeObj);
        //        }
        //        if (st.StateID == (int)Cats.Models.Constant.StateTemplate.ChequeCollected)
        //        {
        //            transporterChequeObj.Status = (int)Cats.Models.Constant.ChequeStatus.ChequeCollected;
        //            _transporterChequeService.EditTransporterCheque(transporterChequeObj);
        //        }
        //    }
        //    return RedirectToAction("PaymentRequests", "ValidatedPaymentRequest", new {Area = "Finance", transporterID});

        //}

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

        public ActionResult TransporterDetail(int transporterID)
        {
            var transporter = _transporterService.FindById(transporterID);
            if (transporter == null)
            {
                return HttpNotFound();
            }
            return View(transporter);
        }

        public ActionResult PaymentDeductionsPartialView(int paymentRequestID)
        {
            var paymentRequestObj = _transporterPaymentRequestService.Get(t => t.TransporterPaymentRequestID == paymentRequestID, null, "TransportOrder, TransportOrder.Transporter").FirstOrDefault();
            var paymentRequestViewModel = new Models.PaymentRequestViewModel();
            var firstOrDefault = paymentRequestObj.Delivery.DeliveryDetails.FirstOrDefault();
            if (paymentRequestObj!=null && firstOrDefault != null)
            {
                paymentRequestViewModel.PaymentRequestID = paymentRequestObj.TransporterPaymentRequestID;
                paymentRequestViewModel.TransportOrderID = paymentRequestObj.TransportOrderID;
                paymentRequestViewModel.TransportOrderNo = paymentRequestObj.TransportOrder.TransportOrderNo;
                paymentRequestViewModel.TransporterID = paymentRequestObj.TransportOrder.TransporterID;
                paymentRequestViewModel.Transporter = paymentRequestObj.TransportOrder.Transporter.Name;
                paymentRequestViewModel.RequestedAmount = firstOrDefault.SentQuantity;
                paymentRequestViewModel.TransportedQuantityInQtl = firstOrDefault.ReceivedQuantity;
                paymentRequestViewModel.ReferenceNo = paymentRequestObj.ReferenceNo;
                paymentRequestViewModel.BusinessProcessID = paymentRequestObj.BusinessProcessID;
                paymentRequestViewModel.LabourCostRate = paymentRequestObj.LabourCostRate;
                paymentRequestViewModel.LabourCost = paymentRequestObj.LabourCost;
                paymentRequestViewModel.RejectedAmount = paymentRequestObj.RejectedAmount;
                paymentRequestViewModel.RejectionReason = paymentRequestObj.RejectionReason;
            }
            return View(paymentRequestViewModel);
        }

        public ActionResult LoadLabourCost(int transporterPaymentRequestID)
        {
            var paymentRequestObj = _transporterPaymentRequestService.Get(t => t.TransporterPaymentRequestID == transporterPaymentRequestID, requests => null, "TransportOrder, TransportOrder.Transporter").FirstOrDefault();
            var paymentRequestViewModel = new Models.PaymentRequestViewModel();
            
            if (paymentRequestObj!=null)
            {
                var firstOrDefault = paymentRequestObj.Delivery.DeliveryDetails.FirstOrDefault();
                paymentRequestViewModel.PaymentRequestID = paymentRequestObj.TransporterPaymentRequestID;
                paymentRequestViewModel.TransportOrderID = paymentRequestObj.TransportOrderID;
                paymentRequestViewModel.TransportOrderNo = paymentRequestObj.TransportOrder.TransportOrderNo;
                paymentRequestViewModel.TransporterID = paymentRequestObj.TransportOrder.TransporterID;
                paymentRequestViewModel.Transporter = paymentRequestObj.TransportOrder.Transporter.Name;
                paymentRequestViewModel.RequestedAmount = firstOrDefault != null? firstOrDefault.SentQuantity: (decimal) 0.00;
                paymentRequestViewModel.TransportedQuantityInQtl = firstOrDefault != null ? firstOrDefault.ReceivedQuantity : (decimal)0.00;
                paymentRequestViewModel.ReferenceNo = paymentRequestObj.ReferenceNo;
                paymentRequestViewModel.BusinessProcessID = paymentRequestObj.BusinessProcessID;
                paymentRequestViewModel.LabourCostRate = paymentRequestObj.LabourCostRate;
                paymentRequestViewModel.LabourCost = paymentRequestObj.LabourCost;
                paymentRequestViewModel.RejectedAmount = paymentRequestObj.RejectedAmount;
                paymentRequestViewModel.RejectionReason = paymentRequestObj.RejectionReason;
            }
            return Json(paymentRequestViewModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadPaymentRequest()
        {
            var paymentRequests = _transporterPaymentRequestService.Get(null, null, "TransportOrder, TransportOrder.Transporter, BusinessProcess.CurrentState.BaseStateTemplate");
            var paymentRequestViewModels = new List<Models.PaymentRequestViewModel>();
            
            if (paymentRequests != null)
            {
                foreach (var paymentRequest in paymentRequests)
                {
                    var firstOrDefault = paymentRequest.Delivery.DeliveryDetails.FirstOrDefault();
                    var paymentRequestViewModel = new Models.PaymentRequestViewModel();
                    paymentRequestViewModel.PaymentRequestID = paymentRequest.TransporterPaymentRequestID;
                    paymentRequestViewModel.TransportOrderID = paymentRequest.TransportOrderID;
                    paymentRequestViewModel.TransportOrderNo = paymentRequest.TransportOrder.TransportOrderNo;
                    paymentRequestViewModel.TransporterID = paymentRequest.TransportOrder.TransporterID;
                    paymentRequestViewModel.Transporter = paymentRequest.TransportOrder.Transporter.Name;
                    paymentRequestViewModel.RequestedAmount = firstOrDefault != null ? firstOrDefault.SentQuantity : (decimal)0.00;
                    paymentRequestViewModel.TransportedQuantityInQtl = firstOrDefault != null ? firstOrDefault.ReceivedQuantity : (decimal)0.00;
                    paymentRequestViewModel.ReferenceNo = paymentRequest.ReferenceNo;
                    paymentRequestViewModel.BusinessProcessID = paymentRequest.BusinessProcessID;
                    paymentRequestViewModel.StateNo = paymentRequest.BusinessProcess.CurrentState.BaseStateTemplate.StateNo;
                    paymentRequestViewModel.LabourCostRate = paymentRequest.LabourCostRate;
                    paymentRequestViewModel.LabourCost = paymentRequest.LabourCost;
                    paymentRequestViewModel.RejectedAmount = paymentRequest.RejectedAmount;
                    paymentRequestViewModel.RejectionReason = paymentRequest.RejectionReason;
                    paymentRequestViewModels.Add(paymentRequestViewModel);
                }
                
            }
            return Json(paymentRequestViewModels, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditLabourCostInfo(Models.PaymentRequestViewModel paymentRequestViewModel)
        {
            var paymentRequestObj = _transporterPaymentRequestService.FindById(paymentRequestViewModel.PaymentRequestID);
            paymentRequestObj.LabourCostRate = paymentRequestViewModel.LabourCostRate;
            paymentRequestObj.LabourCost = paymentRequestViewModel.LabourCost;
            paymentRequestObj.RejectedAmount = paymentRequestViewModel.RejectedAmount;
            paymentRequestObj.RejectionReason = paymentRequestViewModel.RejectionReason;
            _transporterPaymentRequestService.EditTransporterPaymentRequest(paymentRequestObj);
            return RedirectToAction("PaymentRequests", "ValidatedPaymentRequest", new { Area = "Finance", transporterID = paymentRequestObj.TransportOrder.TransporterID });
        }

        public ActionResult LoadCheque()
        {
            var user = UserAccountHelper.GetUser(User.Identity.Name);
            var approvedPaymentRequests = _transporterPaymentRequestService.Get(t => t.BusinessProcess.CurrentState.BaseStateTemplate.StateNo == 3);
            //var transporterChequeObj = _transporterChequeService.Get(t => t.TransporterPaymentRequestID == paymentRequestID, null, "UserProfile").FirstOrDefault();
            var transporterChequeViewModel = new Models.TransporterChequeViewModel();
            foreach (var approvedPaymentRequest in approvedPaymentRequests)
            {
                var request = approvedPaymentRequest;
                var dispatch = _dispatchService.Get(t => t.DispatchID == request.Delivery.DispatchID, null, "Hub, FDP").FirstOrDefault();
                var firstOrDefault = _bidWinnerService.Get(t => t.SourceID == dispatch.HubID && t.DestinationID == dispatch.FDPID
                    && t.TransporterID == request.TransportOrder.TransporterID && t.Bid.BidNumber == dispatch.BidNumber).FirstOrDefault();
                var tarrif = (decimal)0.00;
                var deliveryDetail = request.Delivery.DeliveryDetails.FirstOrDefault() ?? new DeliveryDetail();
                if (request.LabourCost == null)
                    request.LabourCost = (decimal)0.00;
                if (request.RejectedAmount == null)
                    request.RejectedAmount = (decimal)0.00;
                if (firstOrDefault != null)
                {
                    tarrif = firstOrDefault.Tariff != null ? (decimal)firstOrDefault.Tariff : (decimal)0.00;
                }
                transporterChequeViewModel.Transporter = request.TransportOrder.Transporter.Name;
                transporterChequeViewModel.PaymentRequestsList = transporterChequeViewModel.PaymentRequestsList + " [" + approvedPaymentRequest.ReferenceNo + "] ";
                transporterChequeViewModel.Amount = transporterChequeViewModel.Amount +
                                                    (decimal)
                                                    (approvedPaymentRequest.ShortageBirr != null
                                                         ? (deliveryDetail.ReceivedQuantity*tarrif)
                                                           - approvedPaymentRequest.ShortageBirr + approvedPaymentRequest.LabourCost -
                                                           approvedPaymentRequest.RejectedAmount
                                                         : (deliveryDetail.ReceivedQuantity*tarrif) +
                                                           approvedPaymentRequest.LabourCost - approvedPaymentRequest.RejectedAmount);
            }
            transporterChequeViewModel.PreparedByID = user.UserProfileID;
            transporterChequeViewModel.PreparedBy = user.FullName;
            return Json(transporterChequeViewModel, JsonRequestBehavior.AllowGet);
        }

        

        private TransporterChequeViewModel BindTransporterChequeViewModel(TransporterCheque transporterCheque)
        {
            TransporterChequeViewModel transporterChequeViewModel = null;
            if (transporterCheque != null)
            {
                var transporterChequeDetailObj = transporterCheque.TransporterChequeDetails.FirstOrDefault();
                if (transporterChequeDetailObj != null)
                {
                    var transporterObj = transporterChequeDetailObj.TransporterPaymentRequest.TransportOrder.Transporter;

                    transporterChequeViewModel = new TransporterChequeViewModel
                                                     {
                                                         TransporterChequeId = transporterCheque.TransporterChequeId,
                                                         CheckNo = transporterCheque.CheckNo,
                                                         PaymentVoucherNo = transporterCheque.PaymentVoucherNo,
                                                         BankName = transporterCheque.BankName,
                                                         Amount = transporterCheque.Amount,
                                                         TransporterId = transporterObj.TransporterID,
                                                         PreparedBy =
                                                             _userProfileService.FindById(
                                                                 (int) transporterCheque.PreparedBy).FirstName + " " +
                                                             _userProfileService.FindById(
                                                                 (int) transporterCheque.PreparedBy).LastName,
                                                         AppovedBy = transporterCheque.AppovedBy != null
                                                                         ? _userProfileService.FindById(
                                                                             (int) transporterCheque.AppovedBy).
                                                                               FirstName + " " +
                                                                           _userProfileService.FindById(
                                                                               (int) transporterCheque.AppovedBy).
                                                                               LastName
                                                                         : string.Empty,
                                                         AppovedDate = transporterCheque.AppovedDate,
                                                         Status = (int) transporterCheque.Status
                                                     };
                }
            }
            return transporterChequeViewModel;
        }

        private IEnumerable<TransporterChequeViewModel> BindTransporterChequeViewModel(IEnumerable<TransporterCheque> transporterCheques)
        {
            
            var transporterChequeViewModels = new List<TransporterChequeViewModel>();
            foreach (var transporterCheque in transporterCheques)
            {
                var transporterChequeDetailObj = transporterCheque.TransporterChequeDetails.FirstOrDefault();
                if (transporterChequeDetailObj != null)
                {
                    var transporterObj = transporterChequeDetailObj.TransporterPaymentRequest.TransportOrder.Transporter;
                    var transporterChequeViewModel = new TransporterChequeViewModel
                                                         {
                                                             TransporterChequeId = transporterCheque.TransporterChequeId,
                                                             CheckNo = transporterCheque.CheckNo,
                                                             PaymentVoucherNo = transporterCheque.PaymentVoucherNo,
                                                             BankName = transporterCheque.BankName,
                                                             Amount = transporterCheque.Amount,
                                                             TransporterId = transporterObj.TransporterID,
                                                             PreparedBy = _userProfileService.FindById((int)transporterCheque.PreparedBy).FirstName + " " + _userProfileService.FindById((int)transporterCheque.PreparedBy).LastName,
                                                             AppovedBy = transporterCheque.AppovedBy != null? _userProfileService.FindById((int)transporterCheque.AppovedBy).FirstName + " " + _userProfileService.FindById((int)transporterCheque.AppovedBy).LastName: string.Empty,
                                                             AppovedDate = transporterCheque.AppovedDate,
                                                             Status = (int)transporterCheque.Status
                                                         };
                    transporterChequeViewModels.Add(transporterChequeViewModel);
                }
            }
            return transporterChequeViewModels;
        }

        public ActionResult ApproveChequeInfo(int transporterChequeID)
        {
            var user = UserAccountHelper.GetUser(User.Identity.Name);
            var transporterChequeObj = _transporterChequeService.FindById(transporterChequeID);
            transporterChequeObj.AppovedBy = user.UserProfileID;
            transporterChequeObj.AppovedDate = DateTime.Now;
            transporterChequeObj.Status = 2;
            _transporterChequeService.EditTransporterCheque(transporterChequeObj);
            //var transporterChequeViewModel = BindTransporterChequeViewModel(transporterChequeObj);
            return Json(transporterChequeID, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditCollectiveChequeInfo(Models.TransporterChequeViewModel transporterChequeViewModel, int transporterID)
        {
            var paymentRequestList = (IEnumerable<Cats.Models.TransporterPaymentRequest>)_transporterPaymentRequestService
                        .Get(t => t.BusinessProcess.CurrentState.BaseStateTemplate.StateNo == 3, null, "BusinessProcess")
                        .OrderByDescending(t => t.TransporterPaymentRequestID);
            //var transporterChequeObj = _transporterChequeService.Get(t => t.TransporterChequeId == transporterChequeViewModel.TransporterChequeId).FirstOrDefault();
            if (paymentRequestList.Any())
            {
                var transporterChequeDetails = new List<TransporterChequeDetail>();
                foreach(var paymentRequest in paymentRequestList)
                {
                    var transporterChequeDetail = new TransporterChequeDetail
                                                      {
                                                        
                                                          TransporterPaymentRequestID = paymentRequest.TransporterPaymentRequestID
                                                      };
                    transporterChequeDetails.Add(transporterChequeDetail);
                }
                var user = UserAccountHelper.GetUser(User.Identity.Name);
                var transporterChequeObj = new TransporterCheque()
                                                {
                                                    CheckNo = transporterChequeViewModel.CheckNo,
                                                    PaymentVoucherNo = transporterChequeViewModel.PaymentVoucherNo,
                                                    BankName = transporterChequeViewModel.BankName,
                                                    Amount = transporterChequeViewModel.Amount,
                                                    PreparedBy = user.UserProfileID,
                                                    Status = 1,
                                                    TransporterChequeDetails = transporterChequeDetails
                                                };

                int BP_PR = _applicationSettingService.getTransporterChequeWorkflow();
                if (BP_PR != 0)
                {
                    BusinessProcessState createdstate = new BusinessProcessState
                    {
                        DatePerformed = DateTime.Now,
                        PerformedBy = "System",
                        Comment = "Created workflow for Transporter Cheque"

                    };
                    //_PaymentRequestservice.Create(request);

                    BusinessProcess bp = _businessProcessService.CreateBusinessProcess(BP_PR, transporterChequeObj.TransporterChequeId,
                                                                                    "ValidatedPaymentRequest", createdstate);
                    if (bp != null) transporterChequeObj.BusinessProcessID = bp.BusinessProcessID;
                    transporterChequeObj.IssueDate = DateTime.Now;
                    _transporterChequeService.AddTransporterCheque(transporterChequeObj);
                    foreach (var paymentRequest in paymentRequestList)
                    {
                        var currFlowTemplate = paymentRequest.BusinessProcess.CurrentState.BaseStateTemplate.InitialStateFlowTemplates.FirstOrDefault();
                        if (currFlowTemplate != null)
                        {
                            var state = new BusinessProcessState()
                                            {
                                                StateID = currFlowTemplate.FinalStateID,
                                                PerformedBy = user.FullName,
                                                Comment = "Finance: Batch generated cheque for the payment request",
                                                DatePerformed = DateTime.Now,
                                                ParentBusinessProcessID = paymentRequest.BusinessProcess.CurrentState.ParentBusinessProcessID
                                            };
                            var item = _businessProcessService.FindById(state.ParentBusinessProcessID);
                            _businessProcessStateService.Add(state);
                            item.CurrentStateID = state.BusinessProcessStateID;
                            _businessProcessService.Update(item);
                        }
                    }
                }
            }
            return RedirectToAction("Cheques", "Cheque", new { Area = "Finance", transporterID });
        }

        public ActionResult EditChequeInfo(Models.TransporterChequeViewModel transporterChequeViewModel, int transporterID)
        {
            var transporterChequeObj = _transporterChequeService.FindById(transporterChequeViewModel.TransporterChequeId);
            transporterChequeObj.CheckNo = transporterChequeViewModel.CheckNo;
            transporterChequeObj.PaymentVoucherNo = transporterChequeViewModel.PaymentVoucherNo;
            transporterChequeObj.BankName = transporterChequeViewModel.BankName;
            _transporterChequeService.EditTransporterCheque(transporterChequeObj);
            return RedirectToAction("Cheques", "Cheque", new { Area = "Finance", transporterID });
        }

        public ActionResult Promote(BusinessProcessState st, int transporterID)
        {
            _businessProcessService.PromotWorkflow(st);
            return RedirectToAction("PaymentRequests", "ValidatedPaymentRequest", new { Area = "Finance", transporterID });
        }

        private IEnumerable<TransportOrderDetailViewModel> GetDetail(IEnumerable<TransportOrderDetail> transportOrderDetails)
        {
            var transportOrderDetailViewModels = (from itm in transportOrderDetails select BindTransportOrderDetailViewModel(itm));
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

        public void ViewContractAgreement(int transporterID)
        {
            // TODO: Make sure to use DI to get the template generator instance

            var transportAgreementVersionObj = _transporterAgreementVersionService.Get(t=>t.TransporterID == transporterID && t.Current).FirstOrDefault();
            //var filePath = template.GenerateTemplate(transporterID, 7, "FrameworkPucrhaseContract"); //here you have to send the name of the tempalte and the id of the TransporterID

            if (transportAgreementVersionObj != null)
            {
                var data = (byte[])transportAgreementVersionObj.AgreementDocxFile;
                var guid = new Guid();
                var documentPath =
                    System.Web.HttpContext.Current.Server.MapPath(string.Format("~/Templates/{0}.docx", Guid.NewGuid().ToString()));
                using (var stream = new FileStream(documentPath, FileMode.Create))
                {
                    stream.Write(data, 0, data.Length);
                };

                Response.Clear();
                Response.ContentType = "application/text";
                Response.AddHeader("Content-Disposition", @"filename= FrameworkPurchaseContract.docx");
                Response.TransmitFile(documentPath);
            }
            Response.End();
        }

    }
}
