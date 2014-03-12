using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Services.EarlyWarning;
using Cats.Services.Procurement;

namespace Cats.Areas.Finance.Controllers
{
    public class TransportOrderPerformanceController : Controller
    {
        private readonly ITransportOrderService _transportOrderService;
        private readonly ITransportOrderDetailService _transportOrderDetailService;
        private readonly IHubService _hubService;

        public TransportOrderPerformanceController(ITransportOrderService transportOrderService, ITransporterService transporterService, ITransportOrderDetailService transportOrderDetailService, IHubService hubService)
        {
            _transportOrderService = transportOrderService;
            _transportOrderDetailService = transportOrderDetailService;
            _hubService = hubService;
        }

        //
        // GET: /Finance/TransportOrderPerformance/

        public ActionResult Index(int id=-1)
        {
            ViewBag.TransportOrderId = id;
            return View();
        }

       
        public JsonResult GetTransporter(int transportOrderId)
        {
            var transporter = _transportOrderService.FindBy(t => t.TransportOrderID == transportOrderId).Select(r => new
                                                                                                           {
                                                                                                               Name =r.Transporter.Name,
                                                                                                               subCity =r.Transporter.SubCity,
                                                                                                               kebele =r.Transporter.Kebele,
                                                                                                               houseNo =r.Transporter.HouseNo,
                                                                                                               TelephoneNo=r.Transporter.TelephoneNo,
                                                                                                               MobileNo=r.Transporter.MobileNo,
                                                                                                               Email =r.Transporter.Email,
                                                                                                               ContratNo=r.ContractNumber,
                                                                                                               bidNo =r.BidDocumentNo,
                                                                                                               TransporOrderStartDate= r.StartDate,
                                                                                                               TransportOrderEndDate= r.EndDate
                                                                                                           });
           
            return Json(transporter, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetDispatches(int transportOrderId)
        {
            var transportOrderDetail =
                _transportOrderDetailService.FindBy(t => t.TransportOrderID == transportOrderId).Select(r => new 
                                                                                                                 {
                                                                                                                     fdp = r.FDP.Name,
                                                                                                                     region = r.FDP.AdminUnit.AdminUnit2.Name,
                                                                                                                     hub =  _hubService.FindById(r.SourceWarehouseID).Name,
                                                                                                                     amount = r.QuantityQtl,
                                                                                                                     requisitionNo = r.ReliefRequisition.RequisitionNo,
                                                                                                                     tariff = r.TariffPerQtl,
                                                                                                                     commodity =r.Commodity.Name,
                                                                                                                     donor = r.Donor,
                                                                                                                     zone = r.AdminUnit.Name
                                                                                                                 });
            return Json(transportOrderDetail,JsonRequestBehavior.AllowGet);
        }
    }
}
