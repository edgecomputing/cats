using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.EarlyWarning.Models;
using Cats.Models;
using Cats.Services.EarlyWarning;
using Cats.Models.ViewModels;
using DRMFSS.BLL.Services;

namespace Cats.Areas.Logistics.Controllers
{
    public class HubAllocationController : Controller
    {
        //
        // GET: /Logistics/HubAllocation/
        private IReliefRequistionService _reliefRequistionService;
        public HubAllocationController(IReliefRequistionService reliefRequistionService)
        {
            _reliefRequistionService = reliefRequistionService;
        }
        public List<RequisitionHub> GetAllReliefRequistion()
        {
            //var reliefrequistions = _reliefRequistionService.GetAllReliefRequistion();
            List<RequisitionHub> reliefrequistions = new List<RequisitionHub>();
            RequisitionHub rr1 = 
                new RequisitionHub 
                {  
                    ReliefRequistionId=1, 
                    Region = "Oromia", 
                    RequestedAmount=123455.0,
                    RequestedItem=new Commodity{CommodityID=1,Name="Grain"}, 
                    ReferenceNumber = "123", Remark = "Fake on the fly rr",
                    Input = new RequisitionHub.RequestHubAssignment()
                        {
                            HubID = 0,
                            ReliefRequistionID=1
                        }

                };
            RequisitionHub rr2 = 
                new RequisitionHub 
                {  
                    ReliefRequistionId=2, 
                    Region = "Afar", 
                    RequestedAmount=123455.0,
                    RequestedItem=new Commodity{CommodityID=1,Name="Grain"}, 
                    ReferenceNumber = "123", Remark = "Fake on the fly rr",
                    Input = new RequisitionHub.RequestHubAssignment()
                        {
                            HubID = 0,
                            ReliefRequistionID=2
                        }

                };

            IHubService _hubservices = new HubService();
            ViewBag.AllHubs = _hubservices.GetAllHub();
            rr1.Input.ReliefRequistionID = rr1.ReliefRequistionId; 
            reliefrequistions.Add(rr1);
            reliefrequistions.Add(rr2);
            return reliefrequistions;
        }
        public ActionResult Index()
        {
            var reliefrequistions = _reliefRequistionService.GetAllReliefRequistion();
           // List<RequisitionHub> reliefrequistions = GetAllReliefRequistion();
            return View(reliefrequistions.ToList());//reliefrequistions.ToList());
        }
        [HttpPost]
        public ActionResult Edit(List<RequisitionHub.RequestHubAssignment> input)
        {
           
            return View(input);
        }

        // public ActionResult

    }
}
