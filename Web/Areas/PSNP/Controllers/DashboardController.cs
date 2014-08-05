using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Services.EarlyWarning;
using Cats.Areas.PSNP.Models;
using Cats.Services.PSNP;

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
        private readonly IRegionalPSNPPlanService _regionalPsnpPlanService;

        public DashboardController(IRegionalRequestService regionalRequestService,
            IRegionalRequestDetailService reliefRequisitionDetailService,
            IReliefRequisitionService reliefRequisitionService,
            IHRDService hrdService, IRegionalPSNPPlanService regionalPsnpPlanService)
        {
            _regionalRequestService = regionalRequestService;
            _regionalRequestDetailService = reliefRequisitionDetailService;
            _reliefRequisitionService = reliefRequisitionService;
            _hrdService = hrdService;
            _regionalPsnpPlanService = regionalPsnpPlanService;
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
            var psnpRecentPlan =
                _regionalPsnpPlanService.GetAllRegionalPSNPPlan().OrderByDescending(i => i.RegionalPSNPPlanID).
                    FirstOrDefault();

            var requests = _regionalRequestService.FindBy(t => t.PlanID == 1001);

            var r = (from request in requests
                     group request by request.AdminUnit.AdminUnitID into g
                     select new
                     {
                         Region = g.First().AdminUnit.Name,
                         Count = g.Count()
                     });
            return Json(r, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RequestPieByStatus()
        {
             var psnpRecentPlan =
                _regionalPsnpPlanService.GetAllRegionalPSNPPlan().OrderByDescending(i => i.RegionalPSNPPlanID).
                    FirstOrDefault();

             var requests = _regionalRequestService.FindBy(t => t.PlanID == 1001);

            var r = (from request in requests
                     group request by request.Status into g
                     select new
                     {
                         g.First().Status,
                         Count = g.Count()
                     });

            Dictionary<string, int> _request = new Dictionary<string, int>();
          
            foreach (var req in r )
            {
                if (req.Status == (decimal)Cats.Models.Constant.RegionalRequestStatus.Draft)
                    _request.Add("Draft", req.Count);
                else if (req.Status == (decimal)Cats.Models.Constant.RegionalRequestStatus.Approved)
                    _request.Add("Approved", req.Count);
                else if (req.Status == (decimal)Cats.Models.Constant.RegionalRequestStatus.Closed)
                    _request.Add("Closed", req.Count);
               
            }
            return Json(_request, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPlans()
        {
            var plans =
                _regionalPsnpPlanService.FindBy(p => p.StatusID == (int) Cats.Models.Constant.PlanStatus.Approved).
                    Select(n => n.Plan.PlanName).Distinct();
            return Json(plans, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult RequisitionPie()
        //{ 
        //    //var currentPlan = _hrdService.FindBy(t => t.Status == 3).FirstOrDefault().PlanID;
        //    //var requests = _reliefRequisitionService.FindBy(t => t. == currentPlan);

            
        //    //return Json(r, JsonRequestBehavior.AllowGet);
        //}
        
        public ActionResult GetPsnpRequisitions()
        {
            var requests = _reliefRequisitionService.GetAllReliefRequisition().OrderByDescending(t => t.RequestedDate);
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