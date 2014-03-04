using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Procurement.Models;
using Cats.Models;
using Cats.Services.Dashboard;
using Cats.Services.Procurement;
using Kendo.Mvc.UI;

namespace Cats.Areas.Procurement.Controllers
{
    public class FetchDataController : Controller
    {

        private readonly IPaymentRequestService _paymentRequestService;
        private readonly IBidService _bidService;
        //
        // GET: /Procurement/FetchData/

        public FetchDataController(IPaymentRequestService paymentRequestService, IBidService bidService)
        {
            _paymentRequestService = paymentRequestService;
            _bidService = bidService;
        }

        public JsonResult ReadSummarizedNumbers([DataSourceRequest]DataSourceRequest request)
        {
            var paymentRequests = _paymentRequestService.GetAll().Count();
            var paymentRequestsFromTransporters = _paymentRequestService.Get(t => t.BusinessProcess.CurrentState.BaseStateTemplate.Name == "Payment Requested").Count();
            var paymentRequestsAtLogistics = _paymentRequestService.Get(t => t.BusinessProcess.CurrentState.BaseStateTemplate.Name == "Submitted for Approval").Count();
            var approvedPaymentRequests = _paymentRequestService.Get(t => t.BusinessProcess.CurrentState.BaseStateTemplate.Name == "Approved for Payment").Count();
            var rejectedPaymentRequests = _paymentRequestService.Get(t => t.BusinessProcess.CurrentState.BaseStateTemplate.Name == "Rejected").Count();
            var checkIssuedPaymentRequests = _paymentRequestService.Get(t => t.BusinessProcess.CurrentState.BaseStateTemplate.Name == "Check Issued").Count();
            var checkCashedPaymentRequests = _paymentRequestService.Get(t => t.BusinessProcess.CurrentState.BaseStateTemplate.Name == "Check Cashed").Count();
            var summarizedNumbersViewModel = new SummarizedNumbersViewModel()
                                                 {
                                                     ApprovedPaymentRequests = approvedPaymentRequests,
                                                     CheckCashedPaymentRequests = checkCashedPaymentRequests,
                                                     CheckIssuedPaymentRequests = checkIssuedPaymentRequests,
                                                     PaymentRequests = paymentRequests,
                                                     PaymentRequestsAtLogistics = paymentRequestsAtLogistics,
                                                     PaymentRequestsFromTransporters = paymentRequestsFromTransporters,
                                                     RejectedPaymentRequests = rejectedPaymentRequests
                                                 };
            return Json(summarizedNumbersViewModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PaymentRequestsStatus([DataSourceRequest]DataSourceRequest request)
        {
            var paymentRequestStatus = _paymentRequestService.GetAll().Take(10);
            return Json(paymentRequestStatus, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PaymentRequestsPercentage([DataSourceRequest]DataSourceRequest request)
        {
            var totalPaymentRequests = _paymentRequestService.GetAll().Count();
            var paymentRequestsFromTransporters =
                (_paymentRequestService.Get(t => t.BusinessProcess.CurrentState.BaseStateTemplate.Name == "Payment Requested").Count() / totalPaymentRequests) * 100;
            var paymentRequestsAtLogistics =
                (_paymentRequestService.Get(t => t.BusinessProcess.CurrentState.BaseStateTemplate.Name == "Submitted for Approval").Count() / totalPaymentRequests) * 100;
            var approvedPaymentRequests = 
                (_paymentRequestService.Get(t => t.BusinessProcess.CurrentState.BaseStateTemplate.Name == "Approved for Payment").Count() / totalPaymentRequests) * 100;
            var rejectedPaymentRequests = 
                (_paymentRequestService.Get(t => t.BusinessProcess.CurrentState.BaseStateTemplate.Name == "Rejected").Count() / totalPaymentRequests) * 100;
            var checkIssuedPaymentRequests = 
                (_paymentRequestService.Get(t => t.BusinessProcess.CurrentState.BaseStateTemplate.Name == "Check Issued").Count() / totalPaymentRequests) * 100;
            var checkCashedPaymentRequests = 
                (_paymentRequestService.Get(t => t.BusinessProcess.CurrentState.BaseStateTemplate.Name == "Check Cashed").Count() / totalPaymentRequests) * 100;
            var paymentRequestPercentage = new PaymentRequestPercentageViewModel()
                                               {
                                                   Requested = paymentRequestsFromTransporters,
                                                   Submitted = paymentRequestsAtLogistics,
                                                   Approved = approvedPaymentRequests,
                                                   Rejected = rejectedPaymentRequests,
                                                   CheckIssued = checkIssuedPaymentRequests,
                                                   CheckCashed = checkCashedPaymentRequests
                                               };
            return Json(paymentRequestPercentage, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PaymentRequest([DataSourceRequest]DataSourceRequest request)
        {
            var paymentRequest =
                _paymentRequestService.Get(
                    t => t.BusinessProcess.CurrentState.BaseStateTemplate.Name == "Payment Requested").ToList();
            return Json(paymentRequest, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PaymentRequestAtLogistics([DataSourceRequest]DataSourceRequest request)
        {
            var paymentRequestAtLogistics =
                _paymentRequestService.Get(
                    t => t.BusinessProcess.CurrentState.BaseStateTemplate.Name == "Submitted for Approval").ToList();
            return Json(paymentRequestAtLogistics, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VerifiedPaymentRequest([DataSourceRequest]DataSourceRequest request)
        {
            var verifiedPaymentRequest =
                _paymentRequestService.Get(
                    t => t.BusinessProcess.CurrentState.BaseStateTemplate.Name == "Approved for Payment").ToList();
            return Json(verifiedPaymentRequest, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckCashedPaymentRequest([DataSourceRequest]DataSourceRequest request)
        {
            var checkCashedPaymentRequest =
                _paymentRequestService.Get(
                    t => t.BusinessProcess.CurrentState.BaseStateTemplate.Name == "Check Cashed").ToList();
            return Json(checkCashedPaymentRequest, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RecentBids([DataSourceRequest]DataSourceRequest request)
        {
            var recentBids =
                _bidService.GetAllBid().OrderByDescending(t=>t.OpeningDate).Take(10).ToList();
            return Json(recentBids, JsonRequestBehavior.AllowGet);
        }
    }
}
