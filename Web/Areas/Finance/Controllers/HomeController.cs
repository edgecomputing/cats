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
            var requests = _paymentRequestServvice.GetAll().Select(p=> new 
                                                                           {
                                                                               Transporter = p.TransportOrder.Transporter.Name,
                                                                               Amount =p.RequestedAmount,
                                                                               Transport_Order = p.TransportOrder.TransportOrderNo,
                                                                               Reference_No = p.ReferenceNo,
                                                                               AmountTransported = p.TransportOrder.TransportOrderDetails.Sum(y => y.QuantityQtl)
                                                                           });
            return Json(requests, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadCheques()
        {
            var cheques = _transporterChequeService.GetAllTransporterCheque().Select(c=> new
                                                                                             {
                                                                                                 chequeNo = c.CheckNo,
                                                                                                 Transporter = c.Transporter.Name,
                                                                                                 Amount = c.Amount,
                                                                                                 PreparedBy = c.UserProfile.FirstName + " " + c.UserProfile.LastName,
                                                                                                 ApprovedBy = c.UserProfile1.FirstName + " " + c.UserProfile1.LastName,
                                                                                                 DateApproved = c.AppovedDate.Date.ToCTSPreferedDateFormat(UserAccountHelper.UserCalendarPreference())
                                                                                                 
                                                                                             });
            return Json(cheques, JsonRequestBehavior.AllowGet);
        }
    }
}
