using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Services.Finance;
using Cats.Services.Procurement;
using Cats.Helpers;
namespace Cats.Areas.Finance.Controllers
{

    public class HomeController : Controller
    {
        private readonly ITransporterChequeService _transporterChequeService;
        private readonly IPaymentRequestService _paymentRequestServvice;


        public HomeController(ITransporterChequeService transporterChequeService, IPaymentRequestService paymentRequestServvice)
        {
            _transporterChequeService = transporterChequeService;
            _paymentRequestServvice = paymentRequestServvice;
        }

        //
        // GET: /Finance/Home/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ReadPaymentRequest()
        {
            
            var requests = _paymentRequestServvice.GetAll().Select(p => new
                                                                           {
                                                                               Transporter = p.TransportOrder.Transporter.Name,
                                                                               RequestedAmount = p.RequestedAmount,
                                                                               AditionalLabourCost = p.LabourCost,
                                                                               RejectedAmount = p.RejectedAmount,
                                                                               Date  = _transporterChequeService.FindBy(t=>t.PaymentRequestID == p.PaymentRequestID).Select(d=>d.AppovedDate).FirstOrDefault().ToCTSPreferedDateFormat(UserAccountHelper.UserCalendarPreference()),
                                                                               ChequeNo = _transporterChequeService.FindBy(t=>t.PaymentRequestID == p.PaymentRequestID).Select(d=>d.CheckNo).FirstOrDefault(),
                                                                               PVNo = _transporterChequeService.FindBy(t=>t.PaymentRequestID == p.PaymentRequestID).Select(d=>d.PaymentVoucherNo).FirstOrDefault(),
                                                                               Status = p.BusinessProcess.CurrentState.BaseStateTemplate.Name,
                                                                               Performer = p.BusinessProcess.CurrentState.PerformedBy
                                                                           });
            return Json(requests.OrderBy(t=>t.Date).Take(10), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadCheques()
        {
            var cheques = _transporterChequeService.GetAllTransporterCheque().Where(t=>t.Status < 4).Select(c => new
                                                                                             {
                                                                                                 chequeNo = c.CheckNo,
                                                                                                 Transporter = c.PaymentRequest.TransportOrder.Transporter.Name,
                                                                                                 Amount = c.Amount,
                                                                                                 PreparedBy = c.UserProfile.FirstName + " " + c.UserProfile.LastName,
                                                                                                 ApprovedBy = c.UserProfile1.FirstName + " " + c.UserProfile1.LastName,
                                                                                                 DateApproved = c.AppovedDate.Date.ToCTSPreferedDateFormat(UserAccountHelper.UserCalendarPreference()),
                                                                                                 transporterChequeId = c.TransporterChequeId,
                                                                                                 State = c.Status,
                                                                                                 Status = status((int) c.Status),
                                                                                                 ButtonStatus = _status((int)c.Status)
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