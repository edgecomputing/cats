using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Finance.Models;
using Cats.Infrastructure;
using Cats.Models;
using Cats.Services.EarlyWarning;
using Cats.Services.Finance;
using Cats.Services.Hub;
using Cats.Services.Logistics;
using Cats.Services.Procurement;
using Cats.Helpers;
using IUserProfileService = Cats.Services.Administration.IUserProfileService;

namespace Cats.Areas.Finance.Controllers
{

    public class HomeController : Controller
    {
        private readonly ITransporterChequeService _transporterChequeService;
        private readonly ITransporterPaymentRequestService _transporterPaymentRequestService;
        private readonly IUserProfileService _userProfileService;
        private readonly IDispatchService _dispatchService;
        private readonly ITransportOrderDetailService _transportOrderDetailService;
        private readonly IBusinessProcessService _businessProcessService;

        public HomeController(ITransporterChequeService transporterChequeService, IUserProfileService userProfileService, ITransporterPaymentRequestService transporterPaymentRequestService, IDispatchService dispatchService, ITransportOrderDetailService transportOrderDetailService, IBusinessProcessService businessProcessService)
        {
            _transporterChequeService = transporterChequeService;
            _userProfileService = userProfileService;
            _transporterPaymentRequestService = transporterPaymentRequestService;
            _dispatchService = dispatchService;
            _transportOrderDetailService = transportOrderDetailService;
            _businessProcessService = businessProcessService;
        }

        //
        // GET: /Finance/Home/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ReadPaymentRequest()
        {

            var paymentRequests= _transporterPaymentRequestService.Get(t => t.BusinessProcess.CurrentState.BaseStateTemplate.StateNo == 2, null, "BusinessProcess");
           
            var requests = _transporterPaymentRequestService.Get(t => t.BusinessProcess.CurrentState.BaseStateTemplate.StateNo >= 2, null, "BusinessProcess").Select(p =>
            {
                var firstOrDefault = p.Delivery.DeliveryDetails.FirstOrDefault();
                return firstOrDefault != null ? (_transporterChequeService != null ? new
                {
                    Transporter = p.TransportOrder.Transporter.Name,
                    TransporterId = p.TransportOrder.TransporterID,
                    RequestedAmount = firstOrDefault.SentQuantity,
                   
                           
                    AditionalLabourCost = p.LabourCost,
                    RejectedAmount = p.RejectedAmount,
                    // Date  = _transporterChequeService.FindBy(t=>t.PaymentRequestID == p.PaymentRequestID).Select(d=>d.AppovedDate).FirstOrDefault().ToCTSPreferedDateFormat(UserAccountHelper.UserCalendarPreference()),
                    //        ChequeNo = _transporterChequeService.FindBy(t => t.TransporterChequeDetails.FirstOrDefault().TransporterPaymentRequestID == p.TransporterPaymentRequestID).Select(d => d.CheckNo).FirstOrDefault(),
                    //        PVNo = _transporterChequeService.FindBy(t => t.TransporterChequeDetails.FirstOrDefault().TransporterPaymentRequestID == p.TransporterPaymentRequestID).Select(d => d.PaymentVoucherNo).FirstOrDefault(),
                    //        Status = p.BusinessProcess.CurrentState.BaseStateTemplate.Name,
                    //        Performer = p.BusinessProcess.CurrentState.PerformedBy
                } : null) : null;
            }).GroupBy(ac => new
                   {
                       ac.Transporter,
                   }).Select(ac=> new
                       {
                           Transporter = ac.Key.Transporter,
                           TransporterId = ac.FirstOrDefault().TransporterId,
                            RequestedAmount = ac.Sum(m=>m.RequestedAmount),
                            AditionalLabourCost =ac.Sum(m=>m.AditionalLabourCost),
                            RejectedAmount = ac.Sum(m=>m.RejectedAmount),
                       });

            return Json(requests.Take(10), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadCheques()
        {
            var cheques = _transporterChequeService.GetAllTransporterCheque().Where(t => t.Status < 4).Select(c =>
                                                                                                                  {
                                                                                                                      var transporterChequeDetail = c.TransporterChequeDetails.FirstOrDefault();
                                                                                                                      return transporterChequeDetail != null ? new
                                                                                                                                                                    {
                                                                                                                                                                        chequeNo = c.CheckNo,
                                                                                                                                                                        Transporter = transporterChequeDetail.TransporterPaymentRequest.TransportOrder.Transporter.Name,
                                                                                                                                                                        c.Amount,
                                                                                                                                                                        PreparedBy = c.UserProfile.FirstName + " " + c.UserProfile.LastName,

                                                                                                                                                                        ApprovedBy = c.AppovedBy != null ? _userProfileService.FindById((int)c.AppovedBy).FirstName + " " +
                                                                                                                                                                                                           _userProfileService.FindById((int)c.AppovedBy).LastName : "",
                                                                                                                                                                        DateApproved = c.AppovedDate.Date.ToCTSPreferedDateFormat(UserAccountHelper.UserCalendarPreference()),
                                                                                                                                                                        transporterChequeId = c.TransporterChequeId,
                                                                                                                                                                        State = c.Status,
                                                                                                                                                                        Status = c.BusinessProcess.CurrentState.BaseStateTemplate.Name,
                                                                                                                                                                        ButtonStatus = c.BusinessProcess.CurrentState.BaseStateTemplate.Name,
                                                                                                                                                                        c.BankName
                                                                                                                                                                    } : null;
                                                                                                                  });
            return Json(cheques, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateChequeStatus(int transporterChequeId, int status)
        {
            var cheque = _transporterChequeService.FindById(transporterChequeId);
            if (cheque == null)
                return Json(null);
            cheque.Status = status;
            _transporterChequeService.EditTransporterCheque(cheque);

            return RedirectToAction("Index");



        }

        public JsonResult PaymentRequestBeingProcessed()
        {

            var requests = _transporterPaymentRequestService.Get(t => t.BusinessProcess.CurrentState.BaseStateTemplate.StateNo ==1, null, "BusinessProcess").Select(p =>
            {
                var firstOrDefault = p.Delivery.DeliveryDetails.FirstOrDefault();
                return firstOrDefault != null ? (_transporterChequeService != null ? new
                {
                    Transporter = p.TransportOrder.Transporter.Name,
                    TransporterId = p.TransportOrder.TransporterID,
                    RequestedAmount = firstOrDefault.SentQuantity,
                    AditionalLabourCost = p.LabourCost,
                    RejectedAmount = p.RejectedAmount,// Date  = _transporterChequeService.FindBy(t=>t.PaymentRequestID == p.PaymentRequestID).Select(d=>d.AppovedDate).FirstOrDefault().ToCTSPreferedDateFormat(UserAccountHelper.UserCalendarPreference()),
                    
                } : null) : null;
            }).GroupBy(ac => new
            {
                ac.Transporter,
            }).Select(ac => new
            {
                Transporter = ac.Key.Transporter,
                TransporterId = ac.FirstOrDefault().TransporterId,
                RequestedAmount = ac.Sum(m => m.RequestedAmount),
                AditionalLabourCost = ac.Sum(m => m.AditionalLabourCost),
                RejectedAmount = ac.Sum(m => m.RejectedAmount),
            }); 
            return Json(requests.Take(10), JsonRequestBehavior.AllowGet);
        }

        private string status(int status)
        {
            string statusText = "";

            switch (status)
            {
                case 1:
                    statusText = "Prepared";
                    break;
                case 2:
                    statusText = "Signed";
                    break;
                case 3:
                    statusText = "Paid";
                    break;
                default:
                    statusText = "";
                    break;
            }

            return statusText;
        }
        private string _status(int status)
        {
            if (status <= 1)
                return "Sign";
            if (status == 2)
                return "Pay";
            if (status == 3)
                return "Close";
            return "";
        }

        public JsonResult PaymentRequestForDashboard ()
        {
            var list = (IEnumerable<Cats.Models.TransporterPaymentRequest>)_transporterPaymentRequestService
                        .Get(t => t.BusinessProcess.CurrentState.BaseStateTemplate.StateNo >= 2, null, "BusinessProcess")
                        .OrderByDescending(t => t.TransporterPaymentRequestID);
            var transporterPaymentRequests =  TransporterPaymentRequestViewModelBinder(list.ToList());

            var requests = transporterPaymentRequests.GroupBy(ac => new {ac.Transporter.Name}).Select(ac => new
                                                                                                                                                  {
                                                                                                                                                      TransporterName = ac.Key.Name,
                                                                                                                                                      TransporterId = ac.FirstOrDefault().Transporter.TransporterID,
                                                                                                                                                      ReceivedQuantity = ac.Sum(s => s.ReceivedQty),
                                                                                                                                                      ShortageQuantity = ac.Sum(s => s.ShortageQty),
                                                                                                                                                      ShortageBirr = ac.Sum(s => s.ShortageBirr),
                                                                                                                                                      FreightCharge = ac.Sum(s => s.FreightCharge),
                                                                                                                                                  });
            return Json(requests.Take(10), JsonRequestBehavior.AllowGet);
        }

        public List<TransporterPaymentRequestViewModel> TransporterPaymentRequestViewModelBinder(List<TransporterPaymentRequest> transporterPaymentRequests)
        {
            var transporterPaymentRequestViewModels = new List<TransporterPaymentRequestViewModel>();
            foreach (var transporterPaymentRequest in transporterPaymentRequests)
            {
                var request = transporterPaymentRequest;
                var dispatch = _dispatchService.Get(t => t.DispatchID == request.Delivery.DispatchID, null, "Hub, FDP").FirstOrDefault();
                var transportOrderdetail =
                   _transportOrderDetailService.FindBy(
                       m => m.TransportOrderID == request.TransportOrderID && m.SourceWarehouseID == dispatch.HubID && m.FdpID == dispatch.FDPID).FirstOrDefault();
                //var firstOrDefault = _bidWinnerService.Get(t => t.SourceID == dispatch.HubID && t.DestinationID == dispatch.FDPID
                //    && t.TransporterID == request.TransportOrder.TransporterID && t.Bid.BidNumber == dispatch.BidNumber).FirstOrDefault();
                var tarrif = (decimal)0.00;
                if (transportOrderdetail != null)
                {
                    tarrif = (decimal)transportOrderdetail.TariffPerQtl;
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
                            ReceivedQty = deliveryDetail.ReceivedQuantity.ToQuintal(),
                            Tarrif = tarrif,
                            ShortageQty = deliveryDetail.SentQuantity.ToQuintal() - deliveryDetail.ReceivedQuantity.ToQuintal(),
                            ShortageBirr = request.ShortageBirr,
                            SentQty = deliveryDetail.SentQuantity,
                            BusinessProcessID = request.BusinessProcessID,
                            DeliveryID = request.DeliveryID,
                            ReferenceNo = request.ReferenceNo,
                            TransportOrderID = request.TransportOrderID,
                            TransporterPaymentRequestID = request.TransporterPaymentRequestID,
                            FreightCharge = (decimal)(request.ShortageBirr != null ? (deliveryDetail.ReceivedQuantity.ToQuintal() * tarrif) - request.ShortageBirr + request.LabourCost - request.RejectedAmount : (deliveryDetail.ReceivedQuantity.ToQuintal() * tarrif) + request.LabourCost - request.RejectedAmount),
                            BusinessProcess = businessProcess,
                            LabourCost = request.LabourCost,
                            LabourCostRate = request.LabourCostRate,
                            RejectedAmount = request.RejectedAmount,
                            RejectionReason = request.RejectionReason,
                            RequestedDate = request.RequestedDate,
                            Transporter = dispatch.Transporter
                        };
                        transporterPaymentRequestViewModels.Add(transporterPaymentRequestViewModel);
                    }
                }
            }
            return transporterPaymentRequestViewModels;
        }

        public System.Collections.IEnumerable PaymentRequestForPrint(int transporterId)
        {
            var list = (IEnumerable<Cats.Models.TransporterPaymentRequest>)_transporterPaymentRequestService
                        .Get(t => t.BusinessProcess.CurrentState.BaseStateTemplate.StateNo >= 2, null, "BusinessProcess")
                        .OrderByDescending(t => t.TransporterPaymentRequestID);
            var transporterPaymentRequests = TransporterPaymentRequestViewModelBinder(list.ToList());

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
            });
            return requests.Where(m => m.TransporterId == transporterId).ToArray();
        }

        public ActionResult Print(int id = 0)
        {
            var reportPath = Server.MapPath("~/Report/Finance/TransportPaymentRequestLetter.rdlc");
            var reportData = PaymentRequestForPrint(id);
            var dataSourceName = "TPRL";
            var result = ReportHelper.PrintReport(reportPath, reportData, dataSourceName);
            return File(result.RenderBytes, result.MimeType);
        }
    }
}