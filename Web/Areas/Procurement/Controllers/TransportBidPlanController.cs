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
                                            ,IHubService hubServiceParam)
                                        
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

        public bool loadLookups(TransportBidPlan transportbidplan)
        {
           ViewBag.ProgramID = new SelectList(_programService.GetAllProgram(), "ProgramID", "Name", transportbidplan.ProgramID);
           // new SelectList(
           // ViewBag.RegionID = new SelectList(_adminUnitService.GetAllAdminUnit(), "AdminUnitID", "Name", transportbidplan.RegionID);
            //ViewBag.RegionID = new SelectList(_adminUnitService.FindBy(t => t.AdminUnitTypeID == 2), "AdminUnitID", "Name", transportbidplan.RegionID);
            return true;
            
        }

        //
        // GET: /Procurement/TransportBidPlan/

        public ActionResult Index()
        {
            List<Cats.Models.TransportBidPlan> list = _transportBidPlanService.GetAllTransportBidPlan();
            return View(list);
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
                /*db.Entry(transportbidplan).State = EntityState.Modified;
                db.SaveChanges();*/
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

        public Dictionary<string, TransportBidPlanDetail> organizeList(List<TransportBidPlanDetail> bidList)
        {
            System.Collections.Generic.Dictionary<string, TransportBidPlanDetail> ret = new Dictionary<string, TransportBidPlanDetail>();
            
            foreach(var i in bidList)
            {
                string hash=i.BidPlanID + "_" + i.ProgramID + "_" + i.SourceID + "_" + i.DestinationID;
                ret.Add (hash,i);// = i;
            }
            return ret;
        }
        //
        // GET: /Procurement/TransportBidPlan/Details/5

        public ActionResult WarehouseSelection(int id = 0)
        {
            TransportBidPlan transportbidplan = fetchFromDB(id);
            List<TransportBidPlanDetail> bidDetails=_transportBidPlanDetailService.GetAllTransportBidPlanDetail();
            Dictionary<string, TransportBidPlanDetail> matrix = organizeList(bidDetails);

            ViewBag.RegionCollection =_adminUnitService.FindBy(t => t.AdminUnitTypeID == 2);
            ViewBag.bidPlan = transportbidplan;
            ViewBag.HubCollection = this._hubService.GetAllHub();
            ViewBag.ProgramCollection = this._programService.GetAllProgram();
            ViewBag.RegionTotals = getRegionTotals(id);

            if (transportbidplan == null)
            {
                return HttpNotFound();
            }
            return View(matrix);
        }
        public List<RegionTotalViewModel> getRegionTotals(int bidplanid)
        {
            List<RegionTotalViewModel> ret = new List<RegionTotalViewModel>();
            List<Program> ProgramCollection = this._programService.GetAllProgram();
            List<AdminUnit> RegionCollection = _adminUnitService.FindBy(t => t.AdminUnitTypeID == 2);
            foreach(var r in RegionCollection)
            {
                foreach (var p in ProgramCollection)
                {
                    double amount = this._transportBidPlanDetailService.GetRegionPlanTotal(bidplanid,r.AdminUnitID, p.ProgramID);
                    RegionTotalViewModel rt = new RegionTotalViewModel { Program = p, Region = r, Amount = amount };
                    ret.Add(rt);
                }
            }
            return ret;
        }
        [HttpPost]
        public ActionResult EditAJAX(TransportBidPlanDetail transportbidplan)
        {
            
            if (ModelState.IsValid)
            {
                if (transportbidplan.TransportBidPlanDetailID == 0)
                {
                    _transportBidPlanDetailService.AddTransportBidPlanDetail(transportbidplan);
                    ViewBag.status = "itemadded";
                }
                else
                {
                    if (transportbidplan.Quantity >= 0)
                    {
                        _transportBidPlanDetailService.UpdateTransportBidPlanDetail(transportbidplan);
                        ViewBag.status = "itemupdated";
                    }
                    else
                    {
                        TransportBidPlanDetail newModel = new TransportBidPlanDetail();
                        newModel.BidPlanID = transportbidplan.BidPlanID;
                        newModel.DestinationID = transportbidplan.DestinationID;
                        newModel.ProgramID = transportbidplan.ProgramID;
                        newModel.Quantity = 0;
                        newModel.SourceID = transportbidplan.SourceID;

                        _transportBidPlanDetailService.DeleteById(transportbidplan.TransportBidPlanDetailID);
                        ViewBag.status = "itemdeleted";
                        return View(newModel);
                    }

                }
                /*db.Entry(transportbidplan).State = EntityState.Modified;
                db.SaveChanges();*/
                //return RedirectToAction("Index");
            }
            return View(transportbidplan);
            //loadLookups(transportbidplan);
           // return View(transportbidplan);
        }
    }
}