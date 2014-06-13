using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Hub.Models;
using Cats.Services.Hub;
using Cats.Services.Hub.Interfaces;
using Cats.Models.Hubs;
using Cats.Helpers;

namespace Cats.Areas.Hub.Controllers
{
    public class HubDashboardController : Controller
    {
        //
        // GET: /Hub/HubDashboard/
        private readonly IStockStatusService _stockStatusService;
        private readonly IDispatchService _dispatchService;
        private readonly IDispatchAllocationService _dispatchAllocationService;

        public HubDashboardController(IStockStatusService stockStatusService,IDispatchService dispatchService,
                                      IDispatchAllocationService dispatchAllocationService)
        {
            _stockStatusService = stockStatusService;
            _dispatchService = dispatchService;
            _dispatchAllocationService = dispatchAllocationService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult StockStatus(int hub, int program)
        {

            var st = _stockStatusService.GetStockSummaryD(1, DateTime.Now);

            //st.Take()
            var value = st.Find(t => t.HubID == hub);

            var free = (value.TotalFreestock / value.TotalPhysicalStock) * 100;
            var commited = ((value.TotalPhysicalStock - value.TotalFreestock) / value.TotalPhysicalStock) * 100;

            var q = (from s in st
                     where s.HubID == hub
                     select s);

            //var free = q.First;
            // return Json(q, JsonRequestBehavior.AllowGet);

            var j = new StockStatusViewModel()
            {
                freeStockAmount = value.TotalFreestock,
                freestockPercent = free,
                physicalStockAmount = (value.TotalPhysicalStock - value.TotalFreestock),
                physicalStockPercent = commited,
                totalStock = value.TotalPhysicalStock
            };

            return Json(j, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CommodityStockStatus(int hub, int program)
        {
            var st = _stockStatusService.GetFreeStockStatusD(hub, program, DateTime.Now);
            var q = (from s in st
                     select new HubFreeStockView
                     {
                         CommodityName = s.CommodityName,
                         FreeStock = s.FreeStock.ToPreferedWeightUnit(),
                         PhysicalStock = s.PhysicalStock.ToPreferedWeightUnit()
                     });

            return Json(q, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RecentDispatches()
        {
            var currentUser = UserAccountHelper.GetUser(HttpContext.User.Identity.Name);
            if (currentUser.DefaultHub!=null)
            {
                var result = _dispatchService.FindBy(m => m.HubID == currentUser.DefaultHub).OrderByDescending(m=>m.DispatchID);
                var dispatched = GetDispatch(result);
                return Json(dispatched, JsonRequestBehavior.AllowGet);
            }
            
            return Json(null, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RecentDispatchAllocation()
        {
            var currentUser = UserAccountHelper.GetUser(HttpContext.User.Identity.Name);
            if (currentUser.DefaultHub != null)
            {
                var result = _dispatchAllocationService.FindBy(m => m.HubID == currentUser.DefaultHub).OrderByDescending(m => m.DispatchAllocationID);
                var dispached = GetDispatchAllocation(result);
                return Json(dispached, JsonRequestBehavior.AllowGet);
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }
        private IEnumerable<HubRecentDispachesViewModel> GetDispatch(IEnumerable<Dispatch> dispatchs)
        {
            return (from dispatch in dispatchs
                    select new HubRecentDispachesViewModel()
                        {
                            BidNumber = dispatch.BidNumber,
                            GIN=dispatch.GIN,
                            FDPName = dispatch.FDP.Name,
                            RequisitionNo = dispatch.RequisitionNo,
                            Commodity = dispatch.DispatchDetails.Single().Commodity.Name,
                            DispatchedAmount = dispatch.DispatchDetails.Sum(m=>m.DispatchedQuantityInMT),
                            Transporter = dispatch.Transporter.Name


                        }).Take(5);
        }
        private IEnumerable<HubRecentDispachesViewModel> GetDispatchAllocation(IEnumerable<DispatchAllocation> dispatchAllocations)
        {
            return (from dispatchAllocation in dispatchAllocations
                    select new HubRecentDispachesViewModel()
                        {
                            BidNumber = dispatchAllocation.BidRefNo,
                            FDPName = dispatchAllocation.FDP.Name,
                            RequisitionNo = dispatchAllocation.RequisitionNo,
                            //BeneficiaryNumber = dispatchAllocation.Beneficiery,
                            Program = dispatchAllocation.Program.Name,
                            Commodity = dispatchAllocation.Commodity.Name,
                            DispatchedAmount = dispatchAllocation.Amount,
                            Transporter = dispatchAllocation.Transporter.Name
                        }).Take(5);
        }
    }
}