using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Regional.Models;
using Cats.Services.Dashboard;
using Cats.Services.EarlyWarning;

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
        

        public FetchDataController(IRegionalDashboard regionalDashboard,
            IRegionalRequestService regionalRequestService,
            IReliefRequisitionService reliefRequisitionService,
            IAdminUnitService adminUnitService,
            IFDPService fdpService,
            IHRDService hrdService
            )
        {
            _regionalDashboard = regionalDashboard;
            _regionalRequestService = regionalRequestService;
            _reliefRequisitionService = reliefRequisitionService;
            _adminUnitService = adminUnitService;
            _fdpService = fdpService;
            _hrdService = hrdService;
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
            
            var d = new DashboardData()
                {
                    ApprovedRequests = (decimal)approved*100/totalRequests,
                    PendingRequests = (decimal)(draft * 100) / totalRequests,
                    HubAssignedRequests = (decimal)(closed * 100) / totalRequests,
                    FederalApproved = (decimal)(federalApp * 100) / totalRequests,

                    ApprovedRequisitions = ((decimal)(reqApp * 100) / requisitions.Count()),
                    HubAssignedRequisitions = ((decimal)(reqHub * 100) / requisitions.Count()),
                    PendingRequisitions = ((decimal)(reqDraft * 100) / requisitions.Count()),

                    Above18 = 45,
                    Bet5And8 = 26,
                    Below5 = 29,
                    Female = 24152,
                    Male = 21451,
                    IncomingCommodity = 25131,
                    IncomingDispatches = 2142
                };

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