using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Finance.Models;
using Cats.Areas.Procurement.Models;
using Cats.Models;
using Cats.Services.Common;
using Cats.Services.EarlyWarning;
using Cats.Services.Finance;
using Cats.Services.Hub;
using Cats.Services.Logistics;
using Cats.Services.Procurement;
using Cats.Services.Security;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ITransporterService = Cats.Services.Procurement.ITransporterService;
using IUserProfileService = Cats.Services.Administration.IUserProfileService;

namespace Cats.Areas.Finance.Controllers
{
    public class ChequeController : Controller
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

        public ChequeController(IBusinessProcessService paramBusinessProcessService
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

        public ActionResult Cheques(int transporterID)
        {
            ViewBag.TargetController = "Cheque";
            ViewBag.TransporterID = transporterID;
            var transporterCheques = _transporterChequeService.Get(t=>t.TransporterChequeDetails.FirstOrDefault().TransporterPaymentRequest.TransportOrder.TransporterID == transporterID).OrderByDescending(t => t.IssueDate);
            //var transporterChequeViewModels = BindTransporterChequeViewModel(transporterCheques);
            return View(transporterCheques);
        }

        public ActionResult Promote(BusinessProcessState st, int transporterID)
        {
            _businessProcessService.PromotWorkflow(st);
            return RedirectToAction("Cheques", "Cheque", new { Area = "Finance", transporterID });
        }

        //public ActionResult ChequesRead([DataSourceRequest] DataSourceRequest request)
        //{
        //    var transporterCheques = _transporterChequeService.GetAllTransporterCheque().OrderByDescending(t=>t.IssueDate);
        //    var transporterChequeViewModels = BindTransporterChequeViewModel(transporterCheques);
        //    return Json(transporterChequeViewModels.ToDataSourceResult(request));
        //}

        public ActionResult LoadChequeOne(int id)
        {
            var transporterChequeObj = _transporterChequeService.FindById(id);
            var transporterChequeViewModel = BindTransporterChequeViewModel(transporterChequeObj);
            var transporterChequeDetail = transporterChequeObj.TransporterChequeDetails.FirstOrDefault();
            if (transporterChequeDetail != null)
            {
                transporterChequeViewModel.Transporter = transporterChequeDetail.TransporterPaymentRequest.TransportOrder.Transporter.Name;
                transporterChequeViewModel.PaymentRequestsList = transporterChequeViewModel.PaymentRequestsList + " [" + transporterChequeDetail.TransporterPaymentRequest.ReferenceNo + "] ";
            }
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

    }
}
