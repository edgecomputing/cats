using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Services.Administration;
using Cats.Services.Finance;
using Cats.Services.Procurement;
using Cats.Helpers;
namespace Cats.Areas.Finance.Controllers
{

    public class HomeController : Controller
    {
        private readonly ITransporterChequeService _transporterChequeService;
        private readonly IPaymentRequestService _paymentRequestServvice;
        private readonly IUserProfileService _userProfileService;

        public HomeController(ITransporterChequeService transporterChequeService, IPaymentRequestService paymentRequestServvice, IUserProfileService userProfileService)
        {
            _transporterChequeService = transporterChequeService;
            _paymentRequestServvice = paymentRequestServvice;
            _userProfileService = userProfileService;
        }

        //
        // GET: /Finance/Home/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ReadPaymentRequest()
        {

            var requests = _paymentRequestServvice.Get(t => t.BusinessProcess.CurrentState.BaseStateTemplate.StateNo >= 2, null, "BusinessProcess").Select(p => _transporterChequeService != null ? new
                                                                                                                {
                                                                                                                    Transporter = p.TransportOrder.Transporter.Name,
                                                                                                                    RequestedAmount = p.RequestedAmount,
                                                                                                                    AditionalLabourCost = p.LabourCost,
                                                                                                                    RejectedAmount = p.RejectedAmount,// Date  = _transporterChequeService.FindBy(t=>t.PaymentRequestID == p.PaymentRequestID).Select(d=>d.AppovedDate).FirstOrDefault().ToCTSPreferedDateFormat(UserAccountHelper.UserCalendarPreference()),
                                                                                                                    ChequeNo = _transporterChequeService.FindBy(t=>t.PaymentRequestID == p.PaymentRequestID).Select(d=>d.CheckNo).FirstOrDefault(),
                                                                                                                    PVNo = _transporterChequeService.FindBy(t=>t.PaymentRequestID == p.PaymentRequestID).Select(d=>d.PaymentVoucherNo).FirstOrDefault(),
                                                                                                                    Status = p.BusinessProcess.CurrentState.BaseStateTemplate.Name,
                                                                                                                    Performer = p.BusinessProcess.CurrentState.PerformedBy
                                                                                                                } : null);
            return Json(requests.Take(10), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadCheques()
        {
            var cheques = _transporterChequeService.GetAllTransporterCheque().Where(t=>t.Status < 4).Select(c => new
                                                                                             {
                                                                                                 chequeNo = c.CheckNo,
                                                                                                 Transporter = c.PaymentRequest.TransportOrder.Transporter.Name,
                                                                                                 c.Amount,
                                                                                                 PreparedBy = c.UserProfile.FirstName + " " + c.UserProfile.LastName,

                                                                                                 ApprovedBy = c.AppovedBy != null ? _userProfileService.FindById((int)c.AppovedBy).FirstName + " " +
                                                                                                                _userProfileService.FindById((int)c.AppovedBy).LastName : "",
                                                                                                 DateApproved = c.AppovedDate.Date.ToCTSPreferedDateFormat(UserAccountHelper.UserCalendarPreference()),
                                                                                                 transporterChequeId = c.TransporterChequeId,
                                                                                                 State = c.Status,
                                                                                                 Status = status((int) c.Status),
                                                                                                 ButtonStatus = _status((int)c.Status),
                                                                                                 c.BankName
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

        private string status (int status)
        {
            if (status == 1)
                return "Prepared";
            if (status == 2)
                return "Signed";
            if (status == 3)
                return "Paid";
            return "";
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