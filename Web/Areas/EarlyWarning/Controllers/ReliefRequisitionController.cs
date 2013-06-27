using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.EarlyWarning.Models;
using Cats.Models;
using Cats.Services.EarlyWarning;


namespace Cats.Areas.EarlyWarning.Controllers
{
    public class ReliefRequisitionController : Controller
    {
        //
        // GET: /EarlyWarning/ReliefRequisition/

        private IReliefRequistionService _reliefRequistionService;
        private IFDPService _fdpService;
        private IRoundService _roundService;
        private IAdminUnitService _adminUnitService;
        private IProgramService _programService;
        private ICommodityService _commodityService;
        private IReliefRequisitionDetailService _reliefRequisitionDetailService;
        public ReliefRequisitionController(IReliefRequistionService reliefRequistionService
           , IFDPService fdpService
            , IRoundService roundService
            , IAdminUnitService adminUnitService,
            IProgramService programService,
            ICommodityService commodityService,
            IReliefRequisitionDetailService reliefRequisitionDetailService)
        {
            this._reliefRequistionService = reliefRequistionService;
            this._adminUnitService = adminUnitService;
            this._commodityService = commodityService;
            this._fdpService = fdpService;
            this._roundService = roundService;
            this._programService = programService;
            this._reliefRequisitionDetailService = reliefRequisitionDetailService;
        }
        public ActionResult Index()
        {

            var reliefrequistions = _reliefRequistionService.Get(null, null, "AdminUnit,Program,Round");
            return View(reliefrequistions.ToList());
        }
        [HttpGet]
        public ActionResult New()
        {
            var relifRequisition = new ReliefRequistion();


            ViewBag.RegionID = new SelectList(_adminUnitService.FindBy(t => t.AdminUnitTypeID == 2), "AdminUnitID", "Name");
            ViewBag.ProgramID = new SelectList(_programService.GetAllProgram(), "ProgramID", "Name");
            ViewBag.RoundID = new SelectList(_roundService.GetAllRound(), "RoundID", "RoundNumber");
            ViewBag.CommodityID = new SelectList(_commodityService.GetAllCommodity(), "CommodityID", "Name");
            ViewBag.FDPID = new SelectList(_fdpService.GetAllFDP(), "FDPID", "Name");

            var fdpList = _fdpService.GetAllFDP();
            var releifDetails = (from fdp in fdpList
                                 select new ReliefRequisitionDetail()
                                 {
                                     Beneficiaries = 0,
                                     Fdpid = fdp.FDPID,

                                 }).ToList();
            relifRequisition.ReliefRequisitionDetails = releifDetails;
            // PrepareFdpList(fdpList);


            return View(relifRequisition);
        }

        //
        // GET: /ReliefRequisitoin/Details/5

        public ActionResult Details(int id = 0)
        {
            ReliefRequistion reliefrequistion = _reliefRequistionService.Get(t => t.ReliefRequistionId == id, null, "AdminUnit,Program,Round").FirstOrDefault();
            if (reliefrequistion == null)
            {
                return HttpNotFound();
            }
            return View(reliefrequistion);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var reliefRequistion =
                _reliefRequistionService.Get(t => t.ReliefRequistionId == id, null, "ReliefRequisitionDetails,ReliefRequisitionDetails.Fdp," +
                                                                                    "ReliefRequisitionDetails.Fdp.AdminUnit,ReliefRequisitionDetails.Fdp.AdminUnit.AdminUnit2").
                    FirstOrDefault();
            
            var reliefRequistionDetail = reliefRequistion.ReliefRequisitionDetails;
            var input = (from itm in reliefRequistionDetail
                         select new ReliefRequisitionDetailEdit
                           {
                               ReliefRequisitionDetailId = itm.ReliefRequisitionDetailId,
                               ReliefRequistionId = itm.ReliefRequistionId,
                               Fdp = itm.Fdp.Name,
                               Wereda= itm.Fdp.AdminUnit.Name,
                               Zone= itm.Fdp.AdminUnit.AdminUnit2.Name ,

                               Beneficiaries = itm.Beneficiaries,
                               Input = new ReliefRequisitionDetailEdit.ReliefRequisitionDetailEditInput()
                               {
                                   Number = itm.ReliefRequisitionDetailId,
                                   Grain = itm.Grain,
                                   Pulse = itm.Pulse,
                                   Oil = itm.Oil,
                                   CSB = itm.CSB,
                                   Beneficiaries = itm.Beneficiaries
                               }
                           });            
            return View(input);
        }
        [HttpPost]
        public ActionResult Edit(List<ReliefRequisitionDetailEdit.ReliefRequisitionDetailEditInput> input)
        {
            var requId = 0;
            foreach (var reliefRequisitionDetailEditInput in input)
            {
              
                var tempReliefRequistionDetail =
                    _reliefRequisitionDetailService.FindById(reliefRequisitionDetailEditInput.Number);
                requId = tempReliefRequistionDetail.ReliefRequistionId;
                tempReliefRequistionDetail.Beneficiaries = reliefRequisitionDetailEditInput.Beneficiaries;
                tempReliefRequistionDetail.CSB = reliefRequisitionDetailEditInput.CSB;
                tempReliefRequistionDetail.Oil = reliefRequisitionDetailEditInput.Oil;
                tempReliefRequistionDetail.Grain = reliefRequisitionDetailEditInput.Grain;
                tempReliefRequistionDetail.Pulse = reliefRequisitionDetailEditInput.Pulse;

            }
            _reliefRequisitionDetailService.Save();
         


            return RedirectToAction("Edit","ReliefRequisition",new {id=requId});
        }



       
       
        [HttpPost]
        public ActionResult New(ReliefRequistion reliefRequistion)
        {
            if (reliefRequistion != null)
            {
                var fdpList = _fdpService.GetAllFDP();
                var releifDetails = (from fdp in fdpList
                                     select new ReliefRequisitionDetail()
                                     {
                                         Beneficiaries = 0,
                                         Fdpid = fdp.FDPID,
                                         Grain=0,
                                         Pulse=0,
                                         Oil=0,
                                         CSB=0

                                     }).ToList();
                reliefRequistion.ReliefRequisitionDetails = releifDetails;
                _reliefRequistionService.AddReliefRequistion(reliefRequistion);
                return RedirectToAction("Edit", "ReliefRequisition", new { id = reliefRequistion.ReliefRequistionId });
            }
            return View(new ReliefRequistion());
        }
    }
}
