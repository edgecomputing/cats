using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Procurement.Models;
using Cats.Infrastructure;
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
       [HttpGet]
        public ViewResult TransportRequisitions()
        {
            var transportRequisitions = _transportOrderService.GetRequisitionToDispatch();
            var transportReqInput = (from item in transportRequisitions
                                     select new RequisitionToDispatchSelect()
                                                {
                                                    CommodityName=item.CommodityName,
                                                    CommodityID=item.CommodityID,
                                                    HubID=item.HubID ,
                                                    OrignWarehouse=item.OrignWarehouse,
                                                    QuanityInQtl=item.QuanityInQtl ,
                                                    RegionID=item.RegionID,
                                                    RegionName=item.RegionName,
                                                    RequisitionID=item.RequisitionID ,
                                                    RequisitionNo=item.RequisitionNo,
                                                    ZoneID=item.ZoneID,
                                                    Zone=item.Zone,
                                                    RequisitionStatusName=item.RequisitionStatusName,
                                                    RequisitionStatus=item.RequisitionStatus,
                                                    Input=new RequisitionToDispatchSelect.RequisitionToDispatchSelectInput
                                                              {
                                                                  Number=item.RequisitionID ,
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
        public ActionResult TransportRequisitions(IList<RequisitionToDispatchSelect.RequisitionToDispatchSelectInput> input )
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

    }
}
