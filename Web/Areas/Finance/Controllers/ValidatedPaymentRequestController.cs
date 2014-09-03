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

        public ValidatedPaymentRequestController(IBusinessProcessService paramBusinessProcessService
                                        , IBusinessProcessStateService paramBusinessProcessStateService
                                        , IApplicationSettingService paramApplicationSettingService
                                        , ITransportOrderService paramTransportOrderService
                                        , ITransporterAgreementVersionService transporterAgreementVersionService
                                        , IWorkflowStatusService workflowStatusService, ITransporterService transporterService
                                        , ITransporterChequeService transporterChequeService, IUserProfileService userProfileService, ITransporterPaymentRequestService transporterPaymentRequestService, IBidWinnerService bidWinnerService, IUserAccountService userAccountService, IDispatchService dispatchService)
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
            }
        //
        // GET: /Procurement/ValidatedPaymentRequest/

        public ActionResult Index()
        {
            return View();
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
            ViewBag.TargetController = "ValidatedPaymentRequest";
            var list = (IEnumerable<Cats.Models.TransporterPaymentRequest>)_transporterPaymentRequestService
                        .Get(t => t.BusinessProcess.CurrentState.BaseStateTemplate.StateNo >= 2, null, "BusinessProcess")
                        .OrderByDescending(t => t.TransporterPaymentRequestID);
            var transporterPaymentRequests = TransporterPaymentRequestViewModelBinder(list.ToList());
            var transportOrder = _transportOrderService.Get(t => t.TransporterID == transporterID && t.StatusID == 3, null, "Transporter").FirstOrDefault();
            var transportOrderViewModel = TransportOrderViewModelBinder.BindTransportOrderViewModel(transportOrder, datePref, statuses);
            ViewBag.TransportOrderViewModel = transportOrderViewModel;
            ViewBag.TransporterID = transportOrderViewModel.TransporterID;
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
                            FreightCharge = (decimal)(request.ShortageBirr != null ? (deliveryDetail.ReceivedQuantity * tarrif) - request.ShortageBirr - request.LabourCost - request.RejectedAmount : (deliveryDetail.ReceivedQuantity * tarrif) - request.LabourCost - request.RejectedAmount),
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

        public ActionResult Promote(BusinessProcessState st, int PaymentRequestID, int transporterID)
        {
            _businessProcessService.PromotWorkflow(st);
            var transporterChequeObj =
                _transporterChequeService.Get(t => t.TransporterPaymentRequestID == PaymentRequestID).FirstOrDefault();
            if (transporterChequeObj != null)
            {
                if (st.StateID == (int) Cats.Models.Constant.StateTemplate.ChequeApproved)
                {
                    transporterChequeObj.AppovedBy = UserAccountHelper.GetUser(User.Identity.Name).UserProfileID;
                    transporterChequeObj.AppovedDate = DateTime.Now;
                    transporterChequeObj.Status = (int)Cats.Models.Constant.ChequeStatus.ChequeApproved;
                    _transporterChequeService.EditTransporterCheque(transporterChequeObj);
                }
                if (st.StateID == (int)Cats.Models.Constant.StateTemplate.ChequeCollected)
                {
                    transporterChequeObj.Status = (int)Cats.Models.Constant.ChequeStatus.ChequeCollected;
                    _transporterChequeService.EditTransporterCheque(transporterChequeObj);
                }
            }
            return RedirectToAction("PaymentRequests", "ValidatedPaymentRequest", new {Area = "Finance", transporterID});

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

        public ActionResult LoadCheque(int paymentRequestID)
        {
            var user = UserAccountHelper.GetUser(User.Identity.Name);
            var paymentRequestObj = _transporterPaymentRequestService.FindById(paymentRequestID);
            var transporterChequeObj = _transporterChequeService.Get(t => t.TransporterPaymentRequestID == paymentRequestID, null, "UserProfile").FirstOrDefault();
            var transporterChequeViewModel = new Models.TransporterChequeViewModel();
            if (paymentRequestObj != null)
            {
                var deliveryDetail = paymentRequestObj.Delivery.DeliveryDetails.FirstOrDefault();
                var dispatch = _dispatchService.Get(t => t.DispatchID == paymentRequestObj.Delivery.DispatchID, null, "Hub, FDP").FirstOrDefault();
                var firstOrDefault = _bidWinnerService.Get(t => t.SourceID == dispatch.HubID && t.DestinationID == dispatch.FDPID
                    && t.TransporterID == paymentRequestObj.TransportOrder.TransporterID && t.Bid.BidNumber == dispatch.BidNumber).FirstOrDefault();
                var tarrif = (decimal)0.00;
                if (firstOrDefault != null)
                {
                    tarrif = firstOrDefault.Tariff != null ? (decimal)firstOrDefault.Tariff : (decimal)0.00;
                }
                if (transporterChequeObj != null)
                {
                    transporterChequeViewModel.TransporterChequeId = transporterChequeObj.TransporterChequeId;
                    transporterChequeViewModel.TransporterPaymentRequestID = transporterChequeObj.TransporterPaymentRequestID;
                    transporterChequeViewModel.PaymentRequestRefNo = transporterChequeObj.TransporterPaymentRequest.ReferenceNo;
                    transporterChequeViewModel.CheckNo = transporterChequeObj.CheckNo;
                    transporterChequeViewModel.PaymentVoucherNo = transporterChequeObj.PaymentVoucherNo;
                    transporterChequeViewModel.BankName = transporterChequeObj.BankName;
                    transporterChequeViewModel.Amount = transporterChequeObj.Amount;
                    transporterChequeViewModel.TransporterId = paymentRequestObj.TransportOrder.Transporter.TransporterID;
                    transporterChequeViewModel.Transporter = paymentRequestObj.TransportOrder.Transporter.Name;

                    transporterChequeViewModel.PreparedByID = transporterChequeObj.PreparedBy;
                    transporterChequeViewModel.PreparedBy = _userProfileService.FindById((int)transporterChequeObj.PreparedBy).FirstName + " " +
                                                            _userProfileService.FindById((int)transporterChequeObj.PreparedBy).LastName;
                    transporterChequeViewModel.AppovedByID = transporterChequeObj.AppovedBy??0;
                    transporterChequeViewModel.AppovedBy = transporterChequeObj.AppovedBy != null ? 
                        _userProfileService.FindById((int)transporterChequeObj.AppovedBy).FirstName + " " +
                        _userProfileService.FindById((int)transporterChequeObj.AppovedBy).LastName : null;
                    transporterChequeViewModel.AppovedDateString = transporterChequeObj.AppovedDate.ToCTSPreferedDateFormat(UserAccountHelper.UserCalendarPreference());
                    transporterChequeViewModel.Status = (int) transporterChequeObj.Status;
                }
                else
                {
                    transporterChequeViewModel.TransporterPaymentRequestID = paymentRequestID;
                    transporterChequeViewModel.PaymentRequestRefNo = paymentRequestObj.ReferenceNo;
                    transporterChequeViewModel.PreparedBy = user.FirstName + " " + user.LastName;
                    transporterChequeViewModel.PreparedByID = user.UserProfileID;
                    if (paymentRequestObj.LabourCost != null && paymentRequestObj.RejectedAmount!=null)
                    {
                        transporterChequeViewModel.Amount = (decimal)(paymentRequestObj.ShortageBirr != null ? (deliveryDetail.ReceivedQuantity * tarrif) - paymentRequestObj.ShortageBirr : (deliveryDetail.ReceivedQuantity * tarrif));
                    }
                    
                    //transporterChequeViewModel.AppovedDate = "MM/DD/YYYY";
                    //transporterChequeViewModel.AppovedDate = DateTime.Parse("mm-dd-yyyy");
                }
            }
            return Json(transporterChequeViewModel, JsonRequestBehavior.AllowGet);
        }

        private TransporterChequeViewModel BindTransporterChequeViewModel(TransporterCheque transporterCheque)
        {
            TransporterChequeViewModel transporterChequeViewModel = null;
            if (transporterCheque != null)
            {
                transporterChequeViewModel = new TransporterChequeViewModel
                {
                    TransporterChequeId = transporterCheque.TransporterChequeId,
                    TransporterPaymentRequestID = transporterCheque.TransporterPaymentRequestID,
                    CheckNo = transporterCheque.CheckNo,
                    PaymentVoucherNo = transporterCheque.PaymentVoucherNo,
                    BankName = transporterCheque.BankName,
                    Amount = transporterCheque.Amount,
                    TransporterId = _transporterPaymentRequestService.FindById(transporterCheque.TransporterPaymentRequestID).TransportOrder.TransporterID,
                    PreparedBy = _userProfileService.FindById((int)transporterCheque.PreparedBy).FirstName + " " +
                        _userProfileService.FindById((int)transporterCheque.PreparedBy).LastName,
                    AppovedBy = transporterCheque.AppovedBy != null ? 
                        _userProfileService.FindById((int)transporterCheque.AppovedBy).FirstName + " " +
                        _userProfileService.FindById((int)transporterCheque.AppovedBy).LastName : string.Empty,
                    AppovedDate = transporterCheque.AppovedDate,
                    Status = (int) transporterCheque.Status
                };
            }
            return transporterChequeViewModel;
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

        public ActionResult EditChequeInfo(Models.TransporterChequeViewModel transporterChequeViewModel)
        {
            var paymentRequestObj = _transporterPaymentRequestService.FindById(transporterChequeViewModel.TransporterPaymentRequestID);
            var transporterChequeObj = _transporterChequeService.Get(t => t.TransporterPaymentRequestID == transporterChequeViewModel.TransporterPaymentRequestID).FirstOrDefault();
            var user = UserAccountHelper.GetUser(User.Identity.Name);
            if(paymentRequestObj != null)
            {
                if(transporterChequeObj != null)
                {
                    transporterChequeObj.CheckNo = transporterChequeViewModel.CheckNo;
                    transporterChequeObj.PaymentVoucherNo = transporterChequeViewModel.PaymentVoucherNo;
                    transporterChequeObj.BankName = transporterChequeViewModel.BankName;
                    transporterChequeObj.Amount = transporterChequeViewModel.Amount;
                    //transporterChequeObj.PreparedBy = user.UserProfileID;
                    //transporterChequeObj.AppovedBy = transporterChequeViewModel.AppovedByID;
                    //transporterChequeObj.AppovedDate = transporterChequeViewModel.AppovedDate;
                    _transporterChequeService.EditTransporterCheque(transporterChequeObj);
                }
                else
                {
                    transporterChequeObj = new TransporterCheque()
                                                   {
                                                       TransporterChequeId = Guid.NewGuid(),
                                                       TransporterPaymentRequestID = transporterChequeViewModel.TransporterPaymentRequestID,
                                                       CheckNo = transporterChequeViewModel.CheckNo,
                                                       PaymentVoucherNo = transporterChequeViewModel.PaymentVoucherNo,
                                                       BankName = transporterChequeViewModel.BankName,
                                                       Amount = transporterChequeViewModel.Amount,
                                                       PreparedBy = user.UserProfileID,
                                                       Status = 1
                                                       //AppovedBy = transporterChequeViewModel.AppovedByID,
                                                       //AppovedDate = transporterChequeViewModel.AppovedDate,
                                                   };
                    _transporterChequeService.AddTransporterCheque(transporterChequeObj);
                }
            }
            return Json(BindTransporterChequeViewModel(transporterChequeObj), JsonRequestBehavior.AllowGet);
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
