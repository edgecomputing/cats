using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models;
using Cats.Data;
using Cats.Services.Procurement;
using Cats.Services.EarlyWarning;
using Cats.Areas.Procurement.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;


namespace Cats.Areas.Procurement.Controllers
{
    public class TransportBidPlanController : Controller
    {
        private readonly ITransportBidPlanService _transportBidPlanService;
        private readonly IAdminUnitService _adminUnitService;
        private readonly IProgramService _programService;
        private readonly ITransportBidPlanDetailService _transportBidPlanDetailService;
        private readonly IHubService _hubService;

        public TransportBidPlanController(ITransportBidPlanService transportBidPlanServiceParam
                                          , IAdminUnitService adminUnitServiceParam
                                          , IProgramService programServiceParam
                                          , ITransportBidPlanDetailService transportBidPlanDetailServiceParam
                                            , IHubService hubServiceParam)
        {
            this._transportBidPlanService = transportBidPlanServiceParam;
            this._adminUnitService = adminUnitServiceParam;
            this._programService = programServiceParam;
            this._transportBidPlanDetailService = transportBidPlanDetailServiceParam;
            this._hubService = hubServiceParam;
        }
        public TransportBidPlan fetchFromDB(int id)
        {
            TransportBidPlan transportbidplan = _transportBidPlanService.FindById(id);
            return transportbidplan;
        }
        public TransportBidPlanViewModel CopyToView(TransportBidPlan transportbidplan)
        {
            return new TransportBidPlanViewModel{TransportBidPlanID=transportbidplan.TransportBidPlanID
                                                ,Year=transportbidplan.Year
                                                ,YearHalf=transportbidplan.YearHalf
                                                };
        }
        public IEnumerable<TransportBidPlanViewModel> CopyListToView(IEnumerable<Cats.Models.TransportBidPlan> bidplan)
        {
            List<TransportBidPlanViewModel> ret=new List<TransportBidPlanViewModel>();
            foreach (TransportBidPlan i in bidplan)
            {
                ret.Add(CopyToView(i));
            }
            return ret.ToList();
        }
        public bool loadLookups(TransportBidPlan transportbidplan)
        {
            ViewBag.ProgramID = new SelectList(_programService.GetAllProgram(), "ProgramID", "Name", transportbidplan.ProgramID);
            return true;

        }

        //
        // GET: /Procurement/TransportBidPlan/

        public ActionResult Index()
        {
            List<Cats.Models.TransportBidPlan> list = _transportBidPlanService.GetAllTransportBidPlan();
            return View(CopyListToView(list));
        }
        public ActionResult GetListJson([DataSourceRequest] DataSourceRequest request)
        {
            //JsonRequestBehavior.AllowGet ;
            IEnumerable<Cats.Models.TransportBidPlan> list = _transportBidPlanService.GetAllTransportBidPlan();
            return Json(CopyListToView(list).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /Procurement/TransportBidPlan/Details/5

        public ActionResult Details(int id = 0)
        {
            TransportBidPlan transportbidplan = fetchFromDB(id);
            if (transportbidplan == null)
            {
                return HttpNotFound();
            }
            return View(transportbidplan);
        }

        //
        // GET: /Procurement/TransportBidPlan/Create

        public ActionResult Create()
        {
            loadLookups(new TransportBidPlan());
            return View();
        }

        //
        // POST: /Procurement/TransportBidPlan/Create

        [HttpPost]
        public ActionResult Create(TransportBidPlan transportbidplan)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = "Invalid Model";
                _transportBidPlanService.AddTransportBidPlan(transportbidplan);
                return RedirectToAction("Index");
            }

            loadLookups(transportbidplan);
            return View(transportbidplan);
        }

        //
        // GET: /Procurement/TransportBidPlan/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TransportBidPlan transportbidplan = fetchFromDB(id);
            if (transportbidplan == null)
            {
                return HttpNotFound();
            }
            loadLookups(transportbidplan);

            return View(transportbidplan);
        }

        //
        // POST: /Procurement/TransportBidPlan/Edit/5

        [HttpPost]
        public ActionResult Edit(TransportBidPlan transportbidplan)
        {
            if (ModelState.IsValid)
            {
                _transportBidPlanService.UpdateTransportBidPlan(transportbidplan);
                return RedirectToAction("Index");
            }
            loadLookups(transportbidplan);
            return View(transportbidplan);
        }

        //
        // GET: /Procurement/TransportBidPlan/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TransportBidPlan transportbidplan = fetchFromDB(id);
            if (transportbidplan == null)
            {
                return HttpNotFound();
            }
            return View(transportbidplan);
        }

        //
        // POST: /Procurement/TransportBidPlan/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            //TransportBidPlan transportbidplan = fetchFromDB(id);
            _transportBidPlanService.DeleteById(id);
            return RedirectToAction("Index");
        }

       

        public List<AdminUnit> GetAllWoredas(int regionID)
        {
            List<AdminUnit> AdminUnits = _adminUnitService.FindBy(t => t.AdminUnitTypeID == 3 && t.AdminUnit2.AdminUnitID==regionID);
            return AdminUnits;
        }
        public List<WarehouseProgramViewModel> GetAllWarehouseProgram()
        {
            List<Hub> hubs = _hubService.GetAllHub();
            List<WarehouseProgramViewModel> ret=
               ( from hub in hubs
                    select new WarehouseProgramViewModel
                    {
                        WarehouseID = hub.HubId,
                        WarehouseName = hub.Name,
                        PSNP=0,
                        Relief=0
                    }).ToList();
            return ret;
        }
        //
        // GET: /Procurement/TransportBidPlan/Details/5

        public ActionResult WarehouseSelectionDetail(WarehouseSelectionFilterViewModel model)
        {
            ViewBag.RegionCollection = _adminUnitService.FindBy(t => t.AdminUnitTypeID == 2);
            List<WarehouseProgramViewModel> table= GetAllWarehouseProgram();
            return View(table);

        }

       
    }
}