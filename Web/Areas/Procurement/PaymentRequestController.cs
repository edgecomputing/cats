using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Cats.Areas.Procurement.Models;
using Cats.Helpers;
using Cats.Models.ViewModels.Bid;
using Cats.Services.EarlyWarning;
using Cats.Services.Procurement;
using Cats.Services.Common;
using System;
using Cats.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Areas.Procurement
{
    public class PaymentRequestController:Controller
    {
        private readonly IBusinessProcessService _BusinessProcessService;
        private readonly IBusinessProcessStateService _BusinessProcessStateService;
        private readonly IApplicationSettingService _ApplicationSettingService;
        private readonly IPaymentRequestService _PaymentRequestservice;
        private readonly ITransportOrderService _TransportOrderService;

        public PaymentRequestController(IBusinessProcessService _paramBusinessProcessService
                                        , IBusinessProcessStateService _paramBusinessProcessStateService
                                        , IApplicationSettingService _paramApplicationSettingService
                                        , IPaymentRequestService _paramPaymentRequestservice
                                        , ITransportOrderService _paramTransportOrderService
                                        )
            {

                _BusinessProcessService=_paramBusinessProcessService;
                _BusinessProcessStateService=_paramBusinessProcessStateService;
                _ApplicationSettingService=_paramApplicationSettingService;
                _PaymentRequestservice =_paramPaymentRequestservice;
                _TransportOrderService = _paramTransportOrderService;
                 
            }
        public void LoadLookups()
        {

            ViewBag.TransportOrderID = new SelectList(_TransportOrderService.GetAllTransportOrder(), "AdminUnitID", "Name");

        }
        //
        // GET: /Procurement/PaymentRequest/

        public ActionResult Index()
        {
            LoadLookups();
            IEnumerable<Cats.Models.PaymentRequest> list = (IEnumerable<Cats.Models.PaymentRequest>)_PaymentRequestservice.GetAll();

            return View(list);

        }

    
    }
}

