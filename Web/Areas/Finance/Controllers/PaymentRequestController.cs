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

namespace Cats.Areas.Logistics.Controllers
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

            ViewBag.TransportOrderID = new SelectList(_TransportOrderService.GetAllTransportOrder(), "TransportOrderID", "TransportOrderNo");

        }
        //
        // GET: /Procurement/PaymentRequest/

        public ActionResult Index()
        {
            LoadLookups();
            //var list = (IEnumerable<PaymentRequest>)_PaymentRequestservice.GetAll();
            var list = (IEnumerable<PaymentRequest>)_PaymentRequestservice.FindBy(t=>t.BusinessProcess.CurrentState.BaseStateTemplate.StateNo<=2);
            return View(list);
        }

        public ActionResult Create()
        {
            LoadLookups();
            return View();
        }

        //
        // POST: /PSNP/RegionalPSNPPlan/Create

        [HttpPost]
        public ActionResult Create(PaymentRequest request)
        {


            if (ModelState.IsValid)
            {
                   int BP_PR = _ApplicationSettingService.getPaymentRequestWorkflow();
                   
                   if (BP_PR != 0)
                    {
                        BusinessProcessState createdstate = new BusinessProcessState
                        {
                            DatePerformed = DateTime.Now,
                            PerformedBy = "System",
                            Comment = "Created workflow for Payment Request"

                        };
                        //_PaymentRequestservice.Create(request);

                        BusinessProcess bp = _BusinessProcessService.CreateBusinessProcess(BP_PR,request.PaymentRequestID,
                                                                                           "PaymentRequest", createdstate);
                        request.BusinessProcessID = bp.BusinessProcessID;
                        _PaymentRequestservice.Create(request);
                        //_PaymentRequestservice.Update(request);
                        return RedirectToAction("Index");
                    }
                    ViewBag.ErrorMessage1 = "The workflow assosiated with Payment Request doesnot exist.";
                    ViewBag.ErrorMessage2 = "Please make sure the workflow is created and configured.";
                }
                LoadLookups();
                ModelState.AddModelError("Errors", "Could not create Request Plan.");
                return View(request);
            }

        public ActionResult Promote(BusinessProcessState st)
        {
            _BusinessProcessService.PromotWorkflow(st);
            return RedirectToAction("Index");
        }
    }
}