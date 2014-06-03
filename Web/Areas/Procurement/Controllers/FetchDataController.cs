using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Procurement.Models;
using Cats.Helpers;
using Cats.Models;
using Cats.Services.Dashboard;
using Cats.Services.Procurement;
using Cats.Services.Security;
using Kendo.Mvc.UI;

namespace Cats.Areas.Procurement.Controllers
{
    public class FetchDataController : Controller
    {
        private readonly IPaymentRequestService _paymentRequestService;
        private readonly IBidService _bidService;
        private readonly IUserAccountService _userAccountService;
        private readonly IBidWinnerService _bidWinnerService;
        //
        // GET: /Procurement/FetchData/

        public FetchDataController(IPaymentRequestService paymentRequestService, IBidService bidService, IUserAccountService userAccountService, IBidWinnerService bidWinnerService)
        {
            _paymentRequestService = paymentRequestService;
            _bidService = bidService;
            _userAccountService = userAccountService;
            _bidWinnerService = bidWinnerService;
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
            var paymentRequestStatusViewModels = BindPaymentRequestViewModel(paymentRequestStatus);
            return Json(paymentRequestStatusViewModels, JsonRequestBehavior.AllowGet);
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
            var paymentRequestViewModels = BindPaymentRequestViewModel(paymentRequest);
            return Json(paymentRequestViewModels, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PaymentRequestAtLogistics([DataSourceRequest]DataSourceRequest request)
        {
            var paymentRequestAtLogistics =
                _paymentRequestService.Get(
                    t => t.BusinessProcess.CurrentState.BaseStateTemplate.Name == "Submitted for Approval").ToList();
            var paymentRequestAtLogisticViewModels = BindPaymentRequestViewModel(paymentRequestAtLogistics);
            return Json(paymentRequestAtLogisticViewModels, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VerifiedPaymentRequest([DataSourceRequest]DataSourceRequest request)
        {
            var verifiedPaymentRequest =
                _paymentRequestService.Get(
                    t => t.BusinessProcess.CurrentState.BaseStateTemplate.Name == "Approved for Payment").ToList();
            var verifiedPaymentRequestViewModels = BindPaymentRequestViewModel(verifiedPaymentRequest);
            return Json(verifiedPaymentRequestViewModels, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckCashedPaymentRequest([DataSourceRequest]DataSourceRequest request)
        {
            var checkCashedPaymentRequest =
                _paymentRequestService.Get(
                    t => t.BusinessProcess.CurrentState.BaseStateTemplate.Name == "Check Cashed").ToList();
            var checkCashedPaymentRequestViewModels = BindPaymentRequestViewModel(checkCashedPaymentRequest);
            return Json(checkCashedPaymentRequestViewModels, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RecentBids([DataSourceRequest]DataSourceRequest request)
        {
            var recentBids =
                _bidService.FindBy(t => t.StatusID == 5).OrderByDescending(t => t.OpeningDate).Take(10).ToList();
            var recentBidViewModels = BindBidViewModels(recentBids);
            return Json(recentBidViewModels, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PriceQoutation([DataSourceRequest]DataSourceRequest request)
        {
            var recentBids =
                _bidService.FindBy(t => t.StatusID == 5).OrderByDescending(t => t.OpeningDate).Take(10).ToList();
            var recentBidViewModels = BindBidViewModels(recentBids);
            return Json(recentBidViewModels, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GroupedWinners(int bidid , int rank)
        {
            var winners = _bidWinnerService.FindBy(t => t.BidID == bidid && t.Position == rank);
            //var recentBidViewModels = BindBidViewModels(recentBids);
            var list = winners.Select(winner => new
                {
                    transporter = winner.Transporter.Name,
                    //offers = firstwinners.Count
                }).ToList();

            var grouped = (
                            from r in winners
                            group r by new
                            {
                                r.BidID,
                                r.TransporterID
                            }
                                into g
                                select g
                            );
            var groupedwinners = new List<Object>();

            foreach (var transporter in grouped)
            {
                var detail = new
                    {
                        Name = transporter.First().Transporter.Name,
                        Count = transporter.Count(),
                        minoffer = transporter.Min(t=>t.Tariff),
                        maxoffer = transporter.Max(t=>t.Tariff)
                    };

                groupedwinners.Add(detail);
            }

            return Json(groupedwinners, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SecondWinners([DataSourceRequest]DataSourceRequest request)
        {
            var recentBids =
                _bidService.FindBy(t => t.StatusID == 5).OrderByDescending(t => t.OpeningDate).Take(10).ToList();
            var recentBidViewModels = BindBidViewModels(recentBids);
            return Json(recentBidViewModels, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Woredaswithoutoffer([DataSourceRequest]DataSourceRequest request)
        {
            var recentBids =
                _bidService.FindBy(t => t.StatusID == 5).OrderByDescending(t => t.OpeningDate).Take(10).ToList();
            var recentBidViewModels = BindBidViewModels(recentBids);
            return Json(recentBidViewModels, JsonRequestBehavior.AllowGet);
        }

        public List<PaymentRequestViewModel> BindPaymentRequestViewModel(IEnumerable<PaymentRequest> paymentRequests)
        {
            return paymentRequests.Select(paymentRequest => new PaymentRequestViewModel()
                                                                {
                                                                    BusinessProcessID = paymentRequest.BusinessProcessID,
                                                                    PaymentRequestID = paymentRequest.PaymentRequestID,
                                                                    ReferenceNo = paymentRequest.ReferenceNo,
                                                                    RequestedAmount = paymentRequest.RequestedAmount,
                                                                    TransportOrderID = paymentRequest.TransportOrderID,
                                                                    TransportOrderNo = paymentRequest.TransportOrder.TransportOrderNo,
                                                                    TransporterName = paymentRequest.TransportOrder.Transporter.Name
                                                                }).ToList();
        }

        public List<BidsViewModel> BindBidViewModels(IEnumerable<Bid> bids)
        {
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            return bids.Select(bid => new BidsViewModel()
                                        {
                                            BidID = bid.BidID,
                                            BidNumber = bid.BidNumber,
                                            EndDate = bid.EndDate,
                                            OpeningDate = bid.OpeningDate.ToCTSPreferedDateFormat(datePref),
                                            StartDate = bid.StartDate,
                                            StatusID = bid.StatusID
                                        }).ToList();
        }
    }
}
