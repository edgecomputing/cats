using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Procurement.Models;
using Cats.Helpers;
using Cats.Infrastructure;
using Cats.Models;
using Cats.Models.ViewModels;
using Cats.Services.Logistics;
using Cats.Services.Procurement;
using Cats.Services.EarlyWarning;

namespace Cats.Areas.Procurement.Controllers
{
    public class TransportOrderController : Controller
    {
        //
        // GET: /Procurement/TransportOrder/
        private readonly ITransportOrderService _transportOrderService;

        private readonly ITransportRequisitionService _transportRequisitionService;


        public TransportOrderController(ITransportOrderService transportOrderService,ITransportRequisitionService transportRequisitionService)
        {
            this._transportOrderService = transportOrderService;
            this._transportRequisitionService = transportRequisitionService;
           
        } 

            
        

       [HttpGet]
        public ViewResult TransportRequisitions()
        {
            var transportRequisitions = _transportRequisitionService.GetAllTransportRequisition();
            var transportReqInput = (from item in transportRequisitions
                                     select new TransportRequisitionSelect
                                                {
                                                    CertifiedBy= item.CertifiedBy,
                                                    CertifiedDate= item.CertifiedDate,
                                                    RequestedBy= item.RequestedBy ,
                                                    RequestedDate= item.RequestedDate,
                                                    Status= item.Status ,
                                                    TransportRequisitionID= item.TransportRequisitionID,
                                                    TransportRequisitionNo= item.TransportRequisitionNo,
                                                    Input= new TransportRequisitionSelect.TransportRequisitionSelectInput
                                                              {
                                                                  Number=item.TransportRequisitionID ,
                                                                  IsSelected = false
                                                              }


                                                });


            return View(transportReqInput.ToList());
        }
        public FileResult Print(int id)
        {
            var reportPath = Server.MapPath("~/Report/Procurment/TransportOrder.rdlc");
            var reportData = _transportOrderService.GeTransportOrderRpt(id);
            var dataSourceName = "TransportOrders";
            var result = ReportHelper.PrintReport(reportPath, reportData, dataSourceName);

            return File(result.RenderBytes ,result.MimeType);
        }
        [HttpPost]
        public ActionResult TransportRequisitions(IList<TransportRequisitionSelect.TransportRequisitionSelectInput> input)
        {
           
                var requisionIds = (from item in input where item.IsSelected select item.Number).ToList();
                return CreateTransportOrder(requisionIds);
           
        }

        public ActionResult CreateTransportOrder(IEnumerable<int> requisitionToDispatches)
        {
            _transportOrderService.CreateTransportOrder(requisitionToDispatches);
            return RedirectToAction("Index","TransportOrder");
        }

        public ViewResult Index()
        {
            var transportOrders = _transportOrderService.Get(null,null,"TransportOrderDetails,Transporter");
           
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

        public ActionResult TransportOrder()
        {
            ViewBag.Months = new SelectList(RequestHelper.GetMonthList(), "Id", "Name");
            var reliefRequisitions = _transportOrderService.GetTransportOrderReleifRequisition(6);
            var transOrderDetailList = new List<TransportOrderDetail>();
            var reqItems = reliefRequisitions as ReliefRequisition[] ?? reliefRequisitions.ToArray();
            foreach (var reqItem in reqItems)
            {
                var result = _transportOrderService.GetTransportOrderDetail(reqItem.RequisitionID).ToList();
                transOrderDetailList.AddRange(result);
            }
            return View(transOrderDetailList);
           

        }

        public ActionResult TransportOrderDetail(int id)
        {
            var detailTransportOrders = _transportOrderService.GetTransportOrderDetailByTransportId(id);
            if (detailTransportOrders == null)
            {
                 return HttpNotFound();
            }
            return View(detailTransportOrders.ToList());
        }
    }
}
