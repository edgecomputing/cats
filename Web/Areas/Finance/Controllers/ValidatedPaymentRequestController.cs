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
using Cats.Services.Administration;
using Cats.Services.Common;
using Cats.Services.EarlyWarning;
using Cats.Services.Finance;
using Cats.Services.Procurement;
using Cats.ViewModelBinder;

namespace Cats.Areas.Finance.Controllers
{
    public class ValidatedPaymentRequestController : Controller
    {
        private readonly IBusinessProcessService _businessProcessService;
        private readonly IBusinessProcessStateService _businessProcessStateService;
        private readonly IApplicationSettingService _applicationSettingService;
        private readonly IPaymentRequestService _paymentRequestservice;
        private readonly ITransportOrderService _transportOrderService;
        private readonly ITransporterAgreementVersionService _transporterAgreementVersionService;
        private readonly IWorkflowStatusService _workflowStatusService;
        private readonly ITransporterService _transporterService;
        private readonly ITransporterChequeService _transporterChequeService;
        private readonly IUserProfileService _userProfileService;

        public ValidatedPaymentRequestController(IBusinessProcessService paramBusinessProcessService
                                        , IBusinessProcessStateService paramBusinessProcessStateService
                                        , IApplicationSettingService paramApplicationSettingService
                                        , IPaymentRequestService paramPaymentRequestservice
                                        , ITransportOrderService paramTransportOrderService
                                        , ITransporterAgreementVersionService transporterAgreementVersionService
                                        , IWorkflowStatusService workflowStatusService, ITransporterService transporterService
                                        , ITransporterChequeService transporterChequeService, IUserProfileService userProfileService)
            {

                _businessProcessService = paramBusinessProcessService;
                _businessProcessStateService = paramBusinessProcessStateService;
                _applicationSettingService = paramApplicationSettingService;
                _paymentRequestservice = paramPaymentRequestservice;
                _transportOrderService = paramTransportOrderService;
                _transporterAgreementVersionService = transporterAgreementVersionService;
                _workflowStatusService = workflowStatusService;
                _transporterService = transporterService;
                _transporterChequeService = transporterChequeService;
                 _userProfileService = userProfileService;
            }
        //
        // GET: /Procurement/ValidatedPaymentRequest/

        public ActionResult Index()
        {
            ViewBag.TargetController = "ValidatedPaymentRequest";
            var list = (IEnumerable<Cats.Models.PaymentRequest>)_paymentRequestservice.Get(t => t.BusinessProcess.CurrentState.BaseStateTemplate.StateNo >= 2, null, "BusinessProcess").OrderByDescending(t => t.PaymentRequestID);
            return View(list);
        }

        public ActionResult Promote(BusinessProcessState st, int PaymentRequestID)
        {
            _businessProcessService.PromotWorkflow(st);
            var transporterChequeObj = _transporterChequeService.Get(t => t.PaymentRequestID == PaymentRequestID).FirstOrDefault();
            if (transporterChequeObj != null)
            {
                if (st.StateID == (int)Cats.Models.Constant.StateTemplate.ChequeApproved)
                {
                    transporterChequeObj.AppovedBy = UserAccountHelper.GetUser(User.Identity.Name).UserProfileID;
                    transporterChequeObj.AppovedDate = DateTime.Now;
                }
            }
            return RedirectToAction("Index");

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
            var paymentRequestObj = _paymentRequestservice.Get(t => t.PaymentRequestID == paymentRequestID, null, "TransportOrder, TransportOrder.Transporter").FirstOrDefault();
            var paymentRequestViewModel = new Models.PaymentRequestViewModel();
            if (paymentRequestObj!=null)
            {
                paymentRequestViewModel.PaymentRequestID = paymentRequestObj.PaymentRequestID;
                paymentRequestViewModel.TransportOrderID = paymentRequestObj.TransportOrderID;
                paymentRequestViewModel.TransportOrderNo = paymentRequestObj.TransportOrder.TransportOrderNo;
                paymentRequestViewModel.TransporterID = paymentRequestObj.TransportOrder.TransporterID;
                paymentRequestViewModel.Transporter = paymentRequestObj.TransportOrder.Transporter.Name;
                paymentRequestViewModel.RequestedAmount = paymentRequestObj.RequestedAmount;
                paymentRequestViewModel.TransportedQuantityInQtl = paymentRequestObj.TransportedQuantityInQTL;
                paymentRequestViewModel.ReferenceNo = paymentRequestObj.ReferenceNo;
                paymentRequestViewModel.BusinessProcessID = paymentRequestObj.BusinessProcessID;
                paymentRequestViewModel.LabourCostRate = paymentRequestObj.LabourCostRate;
                paymentRequestViewModel.LabourCost = paymentRequestObj.LabourCost;
                paymentRequestViewModel.RejectedAmount = paymentRequestObj.RejectedAmount;
                paymentRequestViewModel.RejectionReason = paymentRequestObj.RejectionReason;
            }
            return View(paymentRequestViewModel);
        }

        public ActionResult LoadLabourCost(int paymentRequestID)
        {
            var paymentRequestObj = _paymentRequestservice.Get(t => t.PaymentRequestID == paymentRequestID, null, "TransportOrder, TransportOrder.Transporter").FirstOrDefault();
            var paymentRequestViewModel = new Models.PaymentRequestViewModel();
            if (paymentRequestObj != null)
            {
                paymentRequestViewModel.PaymentRequestID = paymentRequestObj.PaymentRequestID;
                paymentRequestViewModel.TransportOrderID = paymentRequestObj.TransportOrderID;
                paymentRequestViewModel.TransportOrderNo = paymentRequestObj.TransportOrder.TransportOrderNo;
                paymentRequestViewModel.TransporterID = paymentRequestObj.TransportOrder.TransporterID;
                paymentRequestViewModel.Transporter = paymentRequestObj.TransportOrder.Transporter.Name;
                paymentRequestViewModel.RequestedAmount = paymentRequestObj.RequestedAmount;
                paymentRequestViewModel.TransportedQuantityInQtl = paymentRequestObj.TransportedQuantityInQTL;
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
            var paymentRequests = _paymentRequestservice.Get(null, null, "TransportOrder, TransportOrder.Transporter, BusinessProcess.CurrentState.BaseStateTemplate");
            var paymentRequestViewModels = new List<Models.PaymentRequestViewModel>();
            if (paymentRequests != null)
            {
                foreach (var paymentRequest in paymentRequests)
                {
                    var paymentRequestViewModel = new Models.PaymentRequestViewModel();
                    paymentRequestViewModel.PaymentRequestID = paymentRequest.PaymentRequestID;
                    paymentRequestViewModel.TransportOrderID = paymentRequest.TransportOrderID;
                    paymentRequestViewModel.TransportOrderNo = paymentRequest.TransportOrder.TransportOrderNo;
                    paymentRequestViewModel.TransporterID = paymentRequest.TransportOrder.TransporterID;
                    paymentRequestViewModel.Transporter = paymentRequest.TransportOrder.Transporter.Name;
                    paymentRequestViewModel.RequestedAmount = paymentRequest.RequestedAmount;
                    paymentRequestViewModel.TransportedQuantityInQtl = paymentRequest.TransportedQuantityInQTL;
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
            var paymentRequestObj = _paymentRequestservice.FindById(paymentRequestViewModel.PaymentRequestID);
            paymentRequestObj.LabourCostRate = paymentRequestViewModel.LabourCostRate;
            paymentRequestObj.LabourCost = paymentRequestViewModel.LabourCost;
            paymentRequestObj.RejectedAmount = paymentRequestViewModel.RejectedAmount;
            paymentRequestObj.RejectionReason = paymentRequestViewModel.RejectionReason;
            _paymentRequestservice.Update(paymentRequestObj);
            return Json(paymentRequestViewModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadCheque(int paymentRequestID)
        {
            var user = UserAccountHelper.GetUser(User.Identity.Name);
            var paymentRequestObj = _paymentRequestservice.FindById(paymentRequestID);
            var transporterChequeObj = _transporterChequeService.Get(t => t.PaymentRequestID == paymentRequestID, null, "UserProfile").FirstOrDefault();
            var transporterChequeViewModel = new Models.TransporterChequeViewModel();
            if (paymentRequestObj != null)
            {
                if (transporterChequeObj != null)
                {
                    transporterChequeViewModel.TransporterChequeId = transporterChequeObj.TransporterChequeId;
                    transporterChequeViewModel.PaymentRequestID = transporterChequeObj.PaymentRequestID;
                    transporterChequeViewModel.PaymentRequestRefNo = transporterChequeObj.PaymentRequest.ReferenceNo;
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
                    transporterChequeViewModel.AppovedDate = transporterChequeObj.AppovedDate;
                    transporterChequeViewModel.Status = (int) transporterChequeObj.Status;
                }
                else
                {
                    transporterChequeViewModel.PaymentRequestID = paymentRequestID;
                    transporterChequeViewModel.PaymentRequestRefNo = paymentRequestObj.ReferenceNo;
                    transporterChequeViewModel.PreparedBy = user.FirstName + " " + user.LastName;
                    transporterChequeViewModel.PreparedByID = user.UserProfileID;
                    if (paymentRequestObj.LabourCost != null && paymentRequestObj.RejectedAmount!=null)
                    {
                        transporterChequeViewModel.Amount = (decimal) (paymentRequestObj.RequestedAmount + paymentRequestObj.LabourCost -
                                                                      paymentRequestObj.RejectedAmount);
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
                    PaymentRequestID = transporterCheque.PaymentRequestID,
                    CheckNo = transporterCheque.CheckNo,
                    PaymentVoucherNo = transporterCheque.PaymentVoucherNo,
                    BankName = transporterCheque.BankName,
                    Amount = transporterCheque.Amount,
                    TransporterId = _paymentRequestservice.FindById(transporterCheque.PaymentRequestID).TransportOrder.TransporterID,
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
            var paymentRequestObj = _paymentRequestservice.FindById(transporterChequeViewModel.PaymentRequestID);
            var transporterChequeObj = _transporterChequeService.Get(t => t.PaymentRequestID == transporterChequeViewModel.PaymentRequestID).FirstOrDefault();
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
                                                       PaymentRequestID = transporterChequeViewModel.PaymentRequestID,
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
