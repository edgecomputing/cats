using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Regional.Models;
using Cats.Services.Dashboard;
using Cats.Services.EarlyWarning;
using Cats.Services.Logistics;

namespace Cats.Areas.Regional.Controllers
{
    public class FetchDataController : Controller
    {
        //
        // GET: /Regional/FetchData/
        private readonly IRegionalDashboard _regionalDashboard;
        private readonly IRegionalRequestService _regionalRequestService;
        private readonly IReliefRequisitionService _reliefRequisitionService;
        private readonly IAdminUnitService _adminUnitService;
        private readonly IFDPService _fdpService;
        private readonly IHRDService _hrdService;
        private readonly IUtilizationHeaderSerivce _utilization;


        public FetchDataController(IRegionalDashboard regionalDashboard,
            IRegionalRequestService regionalRequestService,
            IReliefRequisitionService reliefRequisitionService,
            IAdminUnitService adminUnitService,
            IFDPService fdpService,
            IHRDService hrdService,
            IUtilizationHeaderSerivce utilization

            )
        {
            _regionalDashboard = regionalDashboard;
            _regionalRequestService = regionalRequestService;
            _reliefRequisitionService = reliefRequisitionService;
            _adminUnitService = adminUnitService;
            _fdpService = fdpService;
            _hrdService = hrdService;
            _utilization = utilization;
        }

        public JsonResult AllocationChanges(int regionID)
        {
            var allocationChanges = _regionalDashboard.GetAllocationChange(regionID);
            return Json(allocationChanges, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Requests(int regionID)
        {
            var requests = _regionalDashboard.GetRecentRequests(regionID);
            return Json(requests, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Requisitions(int regionID)
        {
            var result = _regionalDashboard.GetRecentRequisitions(regionID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RequisitionPercentage(int regionID)
        {
            var result = _regionalDashboard.RequisitionsPercentage(regionID);
            //int sum = 0;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Dispatches(int regionID)
        {
            var result = _regionalDashboard.GetRecentDispatches(regionID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RegionalData(int regionID)
        {
            var requests = _regionalRequestService.FindBy(t => t.RegionID == regionID);
            var requisitions = _reliefRequisitionService.FindBy(t => t.RegionID == regionID);
            var totalRequests = requests.Count();
            var currentPlan = _hrdService.FindBy(t => t.Status == 3).FirstOrDefault().PlanID;
            var utilizations = _utilization.FindBy(t => t.PlanID == currentPlan);
            
            var sum18 = 0;
            var sum518 = 0;
            var sum5 = 0;
            var female = 0;
            var male = 0;

            if(utilizations!=null){
            foreach (var i in utilizations) {
                sum18 = +(i.FemaleAbove18Years+i.MaleAbove18Years);
                sum5 = +(i.FemaleLessThan5Years + i.MaleLessThan5Years);
                sum518 = +(i.FemaleBetween5And18Years + i.MaleBetween5And18Years);
                female = +(i.FemaleAbove18Years + i.FemaleBetween5And18Years + i.FemaleLessThan5Years);
                male = +(i.MaleAbove18Years + i.MaleBetween5And18Years + i.MaleLessThan5Years);
            }}


            var draft = (from r in requests
                         where r.Status == 1
                         select r).Count();

            var approved = (from r in requests
                            where r.Status == 2
                            select r).Count();

            var closed = (from r in requests
                          where r.Status == 3
                          select r).Count();

            var federalApp = (from r in requests
                              where r.Status >= 4
                              select r).Count();

            var reqApp = (from r in requisitions
                          where r.Status == 2
                          select r).Count();

            var reqDraft = (from r in requisitions
                            where r.Status == 1
                            select r).Count();

            var reqHub = (from r in requisitions
                          where r.Status >= 3
                          select r).Count();

            var d = new DashboardData();

            if (totalRequests != 0)
            {
                d.ApprovedRequests = (decimal)approved;
                d.PendingRequests = (decimal)(draft);
                d.HubAssignedRequests = (decimal)(closed);
                d.FederalApproved = (decimal)(federalApp);

            }
            if (requisitions.Count() != 0)
            {
                d.ApprovedRequisitions = ((decimal)(reqApp));
                d.HubAssignedRequisitions = ((decimal)(reqHub));
                d.PendingRequisitions = ((decimal)(reqDraft));
            }

            d.Above18 = sum18;
            d.Bet5And8 = sum518;
            d.Below5 = sum5;
            d.Female = female;
            d.Male = male;

            d.IncomingCommodity = 25131;
            d.IncomingDispatches = 2142;

            return Json(d, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ImportantNumbers(int regionID)
        {
            var requests = _regionalRequestService.FindBy(t => t.RegionID == regionID);
            var requisitions = _reliefRequisitionService.FindBy(t => t.RegionID == regionID);
            var fdps = _fdpService.FindBy(t => t.AdminUnit.AdminUnit2.AdminUnit2.AdminUnitID == regionID);
            var currentHRD = _hrdService.FindBy(t => t.Status == 3).FirstOrDefault();
            var bene = currentHRD.HRDDetails.Where(t => t.AdminUnit.AdminUnit2.AdminUnit2.AdminUnitID == regionID).Sum(
                e => e.NumberOfBeneficiaries);

            var d = new ImportantNumbers()
            {
                TotalCommodity = 4502,
                TotalFDPS = fdps.Count,
                TotalPeople = bene,
                TotalRequests = requests.Count,
                TotalRequistions = requisitions.Count
            };
            return Json(d, JsonRequestBehavior.AllowGet);
        }
    }
}