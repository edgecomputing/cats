using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Procurement.Models;
using Cats.Models;
using Cats.Services.Logistics;

namespace Cats.Areas.Logistics.Controllers
{
    public class TransportRequisitionController : Controller
    {
        private readonly ITransportRequisitionService _transportRequisitionService;
        
        public TransportRequisitionController(ITransportRequisitionService transportRequisitionService)
        {
            this._transportRequisitionService = transportRequisitionService;
        }
        //
        // GET: /Logistics/TransportRequisition/

        public ActionResult Index()
        {
            var transportRequisitions = _transportRequisitionService.GetAllTransportRequisition();
            return View(transportRequisitions);
        }
        [HttpGet]
        public ActionResult Requisitions()
        {
            var assignedRequisitions = _transportRequisitionService.GetRequisitionToDispatch();
            var transportReqInput = (from item in assignedRequisitions
                                     select new RequisitionToDispatchSelect()
                                     {
                                         CommodityName = item.CommodityName,
                                         CommodityID = item.CommodityID,
                                         HubID = item.HubID,
                                         OrignWarehouse = item.OrignWarehouse,
                                         QuanityInQtl = item.QuanityInQtl,
                                         RegionID = item.RegionID,
                                         RegionName = item.RegionName,
                                         RequisitionID = item.RequisitionID,
                                         RequisitionNo = item.RequisitionNo,
                                         ZoneID = item.ZoneID,
                                         Zone = item.Zone,
                                         RequisitionStatusName = item.RequisitionStatusName,
                                         RequisitionStatus = item.RequisitionStatus,
                                         Input = new RequisitionToDispatchSelect.RequisitionToDispatchSelectInput
                                         {
                                             Number = item.RequisitionID,
                                             IsSelected = false
                                         }


                                     });

            var  result1 = new List<RequisitionToDispatchSelect>();
            try
            {
                result1=transportReqInput.ToList();
            }
            catch (Exception)
            {
                
                
            }
            
            return View(result1);


          
        }

        [HttpPost]
        public ActionResult Requisitions(IList<RequisitionToDispatchSelect.RequisitionToDispatchSelectInput> input)
        {

            var requisionIds = (from item in input where item.IsSelected select item.Number).ToList();
            return CreateTransportRequisition(requisionIds);

        }

        public ActionResult CreateTransportRequisition(List<int> requisitionToDispatches)
        {
           var transportRequisition=  _transportRequisitionService.CreateTransportRequisition(requisitionToDispatches);
            return RedirectToAction("Edit", "TransportRequisition",new {id=transportRequisition.TransportRequisitionID});
        }

        public ActionResult Edit(int id)
        {
            var transportRequisition = _transportRequisitionService.FindById(id);
            if(transportRequisition ==null)
            {
                return HttpNotFound();
            }
            return View(transportRequisition);
        }
        [HttpPost]
        public ActionResult Edit(TransportRequisition transportRequisition)
        {
          
            if (!ModelState.IsValid)
            {
                return View(transportRequisition);
            }
            return RedirectToAction("Index","TransportRequisition");
        }


       
    }
}
