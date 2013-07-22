﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Cats.Areas.Procurement.Models;
using Cats.Services.EarlyWarning;
using Cats.Services.Procurement;
using System;
using Cats.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Areas.Procurement.Controllers
{
    public class DispatchLocationsController : Controller
    {
        //
        // GET: /Procurement/DispatchLocations/

        private IBidWinnerService _bidWinnerService;

        private ITransportOrderService _transportOrderService;
    
        public DispatchLocationsController(ITransportOrderService transportOrderService)
        {
            this._transportOrderService = transportOrderService;
        }
       
        public ActionResult Index(string transporter="")
        {
           // var bidWinner = _bidWinnerService.Get(m => m.Transporter.Name.StartsWith(transporter));
            var transporterOrder = _transportOrderService.Get(m => m.Transporter.Name.StartsWith(transporter));
            ViewData["Transporters"] = transporterOrder;
            return View(transporterOrder);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DispatchLocation_Read([DataSourceRequest] DataSourceRequest request,int id=1)
        {
            //var TransportOrder = _transportOrderService.Get(m => m.TransporterID == id);
            var locations = _transportOrderService.GetTransportOrderDetailByTransportId(id);
            var locationsToDisplay = GetDispatchLocations(locations).ToList();
            return Json(locationsToDisplay.ToDataSourceResult(request));
        }
     private IEnumerable<DispatchLocationViewModel> GetDispatchLocations(IEnumerable<TransportOrderDetail> dispatchLocations)
        {
            return (from location in dispatchLocations
                    select new DispatchLocationViewModel()
                    {
                        TransportOrerDetailID = location.TransportOrderDetailID,
                        RequisitionNumber = location.ReliefRequisition.RequisitionNo,
                        Warehouse = location.Hub.Name,
                        Zone = location.FDP.AdminUnit.AdminUnit2.Name,
                        Woreda = location.FDP.AdminUnit.Name,
                        Destination = location.FDP.AdminUnit.Name,
                        Item = location.Commodity.Name,
                        Quantity = location.QuantityQtl,
                        Tariff = location.TariffPerQtl
                        
                    });
        }
        public ActionResult Details(int id=0)
        {
            TransportOrder transportOrder = _transportOrderService.Get(t => t.TransportOrderID == id, null, "TransportOrderDetails,TransportOrderDetails.FDP.AdminUnit.AdminUnit2,Transporter").FirstOrDefault();
            //var bidWinner = _bidWinnerService.Get(m => m.TransporterID == transportOrder.TransporterID).FirstOrDefault();
            if (transportOrder != null)
            {
                var totalAmount = transportOrder.TransportOrderDetails.Sum(m => m.QuantityQtl);
                var region = transportOrder.TransportOrderDetails.FirstOrDefault().FDP.AdminUnit.AdminUnit2.AdminUnit2.Name;
                ViewBag.Transporter = transportOrder.Transporter.Name;
                ViewBag.TotalAmount = totalAmount;
                ViewBag.BidNumber = transportOrder.BidDocumentNo;
                ViewBag.Region = region;
                return View(transportOrder);
            }
            return RedirectToAction("Index");
            
        }

    }
}
