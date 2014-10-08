using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Services.Administration;
using Cats.Services.Finance;
using Cats.Services.Logistics;
using Cats.Services.Procurement;
using Cats.Helpers;
namespace Cats.Areas.Finance.Controllers
{

    public class HomeController : Controller
    {
        private readonly ITransporterChequeService _transporterChequeService;
        private readonly ITransporterPaymentRequestService _transporterPaymentRequestService;
        private readonly IUserProfileService _userProfileService;

        public HomeController(ITransporterChequeService transporterChequeService, IUserProfileService userProfileService, ITransporterPaymentRequestService transporterPaymentRequestService)
        {
            _transporterChequeService = transporterChequeService;
            _userProfileService = userProfileService;
            _transporterPaymentRequestService = transporterPaymentRequestService;
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
    }
}