using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
        public ActionResult Index()
        {
            return View();
        }
        public ViewResult TransportRequisitions()
        {
            var transportRequisitions = _transportOrderService.GetRequisitionToDispatch();
            return View(transportRequisitions.ToList());
        }

    }
}
