using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

        private IReliefRequisitionService _reliefRequistionService;
        public HubAllocationController(IReliefRequisitionService reliefRequistionService)
        {
            _reliefRequistionService = reliefRequistionService;
        }


        public ActionResult ApprovedRequesitions()
        {

            RequisitionViewModel vm = new RequisitionViewModel();
            vm._reliefRequisition = header().ToList();
            ViewBag.Detail = d().ToList();
            return View("View1", vm);
           

        }

        [HttpPost]
        public ActionResult SelectHub(RequisitionViewModel requisition)
        {
            return View("hubAllocation",requisition);
        }


        [HttpPost]
        public ActionResult Edit(List<RequisitionHub.RequestHubAssignment> input)
        {

            return View(input);

        }

        private List<ReliefRequisition> header()
        {
            List<ReliefRequisition> r = new List<ReliefRequisition>();
           
               r.Add(new ReliefRequisition() { RequisitionNo="002",RequestedDate=DateTime.Now,ApprovedDate=DateTime.Now });
               r.Add(new ReliefRequisition() { RequisitionNo="003",RequestedDate=DateTime.Now,ApprovedDate=DateTime.Now});
               r.Add(new ReliefRequisition() { RequisitionNo="004", RequestedDate=DateTime.Now,ApprovedDate=DateTime.Now});
               r.Add(new  ReliefRequisition(){ RequisitionNo="005", RequestedDate=DateTime.Now,ApprovedDate=DateTime.Now});
                    
               
            return r;
        }

        private List<ReliefRequisitionDetail> d()
        {
            List<ReliefRequisitionDetail> detail = new List<ReliefRequisitionDetail>();
            detail.Add(new ReliefRequisitionDetail()  {Amount = 2000, Beneficiaries=5678});
            detail.Add(new ReliefRequisitionDetail() {Amount = 2000,Beneficiaries=567});
            detail.Add( new ReliefRequisitionDetail() {Amount = 2000,Beneficiaries=56});
            detail.Add( new ReliefRequisitionDetail() {Amount = 2000,Beneficiaries=5});
         
            return detail;
        }
    }
}
