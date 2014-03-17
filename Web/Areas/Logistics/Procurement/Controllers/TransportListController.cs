using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Services.Procurement;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Cats.ViewModelBinder;
using Cats.Areas.Procurement.Models;
namespace Cats.Areas.Procurement.Controllers
{
    public class TransportListController : Controller
    {

        //
        // GET: /Procurement/TransportList/

        private ITransporterService _transporterService;
        private ITransportBidQuotationService _bidQuotationService;

        public TransportListController(ITransporterService transporterService, ITransportBidQuotationService bidQuotationService)
        {
            _transporterService = transporterService;
            _bidQuotationService = bidQuotationService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReadTransporters([DataSourceRequest] DataSourceRequest request)
        {
            var transporters = _transporterService.GetAllTransporter();
            var transportersViewModel = TransporterListViewModelBinder.ReturnViewModel(transporters);
            return Json(transportersViewModel.ToDataSourceResult(request),JsonRequestBehavior.AllowGet);
        }


        public ActionResult TransporterBidDetail(int transporterId)
        {
            var transporter = _transporterService.GetAllTransporter().Single(t => t.TransporterID == transporterId);
            ViewBag.Transporter = transporter.Name;
            ViewBag.transporterId = transporterId;
            return View();
        }
        public ActionResult ShowBidByTransporter([DataSourceRequest] DataSourceRequest request,int transporterId)
        {
            var quotationResutlt =
                _bidQuotationService.GetAllTransportBidQuotation().Where(
                    t => t.TransporterID == transporterId && t.IsWinner == true).ToList();
            var qoutationViewModel =
                TransportBidQuotationBinding.TransportBidQuotationListViewModelBinder(quotationResutlt);
            return Json(qoutationViewModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}
