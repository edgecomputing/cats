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
        private readonly IHRDService _hrdService;

        public DashboardController(IRegionalRequestService regionalRequestService,
            IRegionalRequestDetailService reliefRequisitionDetailService,
            IReliefRequisitionService reliefRequisitionService,
            IHRDService hrdService)
        {
            _regionalRequestService = regionalRequestService;
            _regionalRequestDetailService = reliefRequisitionDetailService;
            _reliefRequisitionService = reliefRequisitionService;
            _hrdService = hrdService;
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
                f.RequestId = regionalRequest.RegionalRequestID;
                r.Add(f);
            }
            return Json(r, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RequestPie()
        {
            var currentPlan = _hrdService.FindBy(t => t.Status == 3).FirstOrDefault().PlanID;
            var requests = _regionalRequestService.FindBy(t=>t.PlanID==currentPlan);

            var r = (from request in requests
                     group request by request.AdminUnit.AdminUnitID into g
                     select new
                     {
                         Region = g.First().AdminUnit.Name,
                         Count = g.Count()
                     });
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
                f.RequisitionId = regionalRequsition.RequisitionID;
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