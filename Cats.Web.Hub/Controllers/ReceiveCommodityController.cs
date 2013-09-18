using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Cats.Models.Hub;
using Cats.Services.Hub;

namespace Cats.Web.Hub.Controllers
{
    public class ReceiveDetailController : BaseController
    {
       // private CTSContext db = new CTSContext();
        private IReceiveDetailService _receiveDetailService;
        private ICommodityService _commodityService;
        private ICommodityGradeService _commodityGradeService;
        private IReceiveService _receiveService;
        private IUnitService _unitService;

        public ReceiveDetailController(IReceiveDetailService receiveDetailService,
                                       ICommodityService commodityService,
                                       ICommodityGradeService commodityGradeService,
                                       IReceiveService receiveService,IUnitService unitService)
        {
            _receiveDetailService = receiveDetailService;
            _commodityService = commodityService;
            _commodityGradeService = commodityGradeService;
            _receiveService = receiveService;
            _unitService = unitService;
        }
        //
        // GET: /ReceiveDetail/

        public virtual ViewResult Index()
        {
            var receiveDetails = _receiveDetailService.GetAllReceiveDetail();
                //db.ReceiveDetails.Include("Commodity").Include("CommodityGrade").Include("Receive").Include("Unit");
            return View(receiveDetails.ToList());
        }

       
        //
        // GET: /ReceiveDetail/Create

        public virtual ActionResult Create()
        {
            ViewBag.CommodityID = new SelectList(_commodityService.GetAllCommodity(), "CommodityID", "Name");
            ViewBag.CommodityGradeID = new SelectList(_commodityGradeService.GetAllCommodityGrade(), "CommodityGradeID", "Name");
            ViewBag.ReceiveID = new SelectList(_receiveService.GetAllReceive(), "ReceiveID", "SINumber");
            ViewBag.UnitID = new SelectList(_unitService.GetAllUnit(), "UnitID", "Name");
            return View();
        } 

        //
        // POST: /ReceiveDetail/Create

        [HttpPost]
        public virtual ActionResult Create(ReceiveDetail receiveDetail)
        {
            if (ModelState.IsValid)
            {
               
                _receiveDetailService.AddReceiveDetail(receiveDetail);
                return RedirectToAction("Index");  
            }

            ViewBag.CommodityID = new SelectList(_commodityService.GetAllCommodity(), "CommodityID", "Name", receiveDetail.CommodityID);
            ViewBag.CommodityGradeID = new SelectList(_commodityGradeService.GetAllCommodityGrade(), "CommodityGradeID", "Name");
            ViewBag.ReceiveID = new SelectList(_receiveService.GetAllReceive(), "ReceiveID", "SINumber", receiveDetail.ReceiveID);
            ViewBag.UnitID = new SelectList(_unitService.GetAllUnit(), "UnitID", "Name", receiveDetail.UnitID);
            return View(receiveDetail);
        }
        
        //
        // GET: /ReceiveDetail/Edit/5

        public virtual ActionResult Edit(string id)
        {
            var ReceiveDetail = _receiveDetailService.Get(m=>m.ReceiveDetailID==Guid.Parse(id),null).FirstOrDefault();
                //db.ReceiveDetails.Single(r => r.ReceiveDetailID == Guid.Parse(id));
            ViewBag.CommodityID = new SelectList(_commodityService.GetAllCommodity(), "CommodityID", "Name", ReceiveDetail.CommodityID);
            ViewBag.CommodityGradeID = new SelectList(_commodityGradeService.GetAllCommodityGrade(), "CommodityGradeID", "Name");
            ViewBag.ReceiveID = new SelectList(_receiveService.GetAllReceive(), "ReceiveID", "SINumber", ReceiveDetail.ReceiveID);
            ViewBag.UnitID = new SelectList(_unitService.GetAllUnit(), "UnitID", "Name", ReceiveDetail.UnitID);
            ViewBag.UnitID = new SelectList(_unitService.GetAllUnit().ToList(), "UnitID", "Name", ReceiveDetail.UnitID);
            return View(ReceiveDetail);
        }

        //
        // POST: /ReceiveDetail/Edit/5

        [HttpPost]
        public virtual ActionResult Edit(ReceiveDetail ReceiveDetail)
        {
            if (ModelState.IsValid)
            {
               
                _receiveDetailService.EditReceiveDetail(ReceiveDetail);
                return RedirectToAction("Index");
            }
            ViewBag.CommodityID = new SelectList(_commodityService.GetAllCommodity(), "CommodityID", "Name", ReceiveDetail.CommodityID);
            ViewBag.CommodityGradeID = new SelectList(_commodityGradeService.GetAllCommodityGrade(), "CommodityGradeID", "Name");
            ViewBag.ReceiveID = new SelectList(_receiveService.GetAllReceive(), "ReceiveID", "SINumber", ReceiveDetail.ReceiveID);
            ViewBag.UnitID = new SelectList(_unitService.GetAllUnit(), "UnitID", "Name", ReceiveDetail.UnitID);
            ViewBag.UnitID = new SelectList(_unitService.GetAllUnit().ToList(), "UnitID", "Name", ReceiveDetail.UnitID);
            return View(ReceiveDetail);
        }

        
       

        protected override void Dispose(bool disposing)
        {
            _receiveDetailService.Dispose();
            _commodityService.Dispose();
            _commodityGradeService.Dispose();
            _receiveService.Dispose();
            _unitService .Dispose();
            base.Dispose(disposing);
        }
    }
}