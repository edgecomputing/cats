using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Services.EarlyWarning;
using Cats.Areas.PSNP.Models;

namespace Cats.Areas.PSNP.Controllers
{
    public class DashboardController : Controller
    {
        //
        // GET: /PSNP/Dashboard/
        private readonly IRegionalRequestService _regionalRequestService;
        private readonly IRegionalRequestDetailService _regionalRequestDetailService;
        private readonly IReliefRequisitionService _reliefRequisitionService;

        public DashboardController(IRegionalRequestService regionalRequestService,
            IRegionalRequestDetailService reliefRequisitionDetailService,
            IReliefRequisitionService reliefRequisitionService)
        {
            _regionalRequestService = regionalRequestService;
            _regionalRequestDetailService = reliefRequisitionDetailService;
            _reliefRequisitionService = reliefRequisitionService;
        }
        public JsonResult GetPsnpRequests()
        {
            var requests = _regionalRequestService.GetAllRegionalRequest().OrderByDescending(t=>t.RequistionDate).Take(5);
            var r = new List<PSNPRequetViewModel>();
            foreach (var regionalRequest in requests)
            {
                var f = new PSNPRequetViewModel();
                f.Number = regionalRequest.ReferenceNumber;
                f.fdps = regionalRequest.RegionalRequestDetails.Count;
                f.beneficiaries = regionalRequest.RegionalRequestDetails.Sum(t => t.Beneficiaries);
                f.status = regionalRequest.Status;
                r.Add(f);
            }
            return Json(r, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult GetPsnpRequisitions()
        {
            var requests = _reliefRequisitionService.GetAllReliefRequisition().OrderByDescending(t => t.RequestedDate).Take(5);
            var r = new List<PSNPRequisitionViewModel>();
            foreach (var regionalRequsition in requests)
            {
                var f = new PSNPRequisitionViewModel();
                f.Number = regionalRequsition.RequisitionNo;
                f.Commodity = regionalRequsition.Commodity.Name;
                f.Beneficicaries = regionalRequsition.ReliefRequisitionDetails.Sum(t => t.BenficiaryNo);
                f.Amount = regionalRequsition.ReliefRequisitionDetails.Sum(t => t.Amount);
                f.Status = regionalRequsition.Status;
                r.Add(f);
            }
            return Json(r, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUtilizationReport()
        {
            var requisitions = _regionalRequestService.FindBy(t => t.Status == 2 && t.ProgramId == 2);
            return Json(requisitions, JsonRequestBehavior.AllowGet);
        }
    }
}