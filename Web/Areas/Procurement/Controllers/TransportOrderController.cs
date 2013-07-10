using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models;
using Cats.Models.ViewModels;
using Cats.Services.Procurement;

namespace Cats.Areas.Procurement.Controllers
{
    public class TransportOrderController : Controller
    {
        //
        // GET: /Procurement/TransportOrder/
        private readonly ITransportOrderService _transportOrderService;

        public TransportOrderController(ITransportOrderService transportOrderService)
        {
            this._transportOrderService = transportOrderService;
        }
       
        public ViewResult TransportRequisitions()
        {
            var transportRequisitions = _transportOrderService.GetRequisitionToDispatch();
            return View(transportRequisitions.ToList());
        }
        
        public ActionResult CreateTransportOrder(IEnumerable<RequisitionToDispatch> requisitionToDispatches)
        {
            _transportOrderService.CreateTransportOrder(requisitionToDispatches);
            return RedirectToAction("Index","TransportOrder");
        }

        public ViewResult Index()
        {
            var transportOrders = _transportOrderService.GetAllTransportOrder();
            return View(transportOrders.ToList());
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var transportOrder = _transportOrderService.FindById(id);
            if (transportOrder==null)
            {
               return  HttpNotFound();
            }
            return View(transportOrder);

        }
        [HttpPost]
        public ActionResult Edit(TransportOrder transportOrder)
        {
            if (ModelState.IsValid)
            {
                _transportOrderService.EditTransportOrder(transportOrder);
                return RedirectToAction("Index", "TransportOrder");
            }
            return View(transportOrder);
        }
        public ActionResult Details(int id)
        {
            var transportOrder = _transportOrderService.FindById(id);
            return View(transportOrder);
        }

    }
}
