using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Logistics.Models;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Services.Common;
using Cats.Services.Logistics;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Areas.Logistics.Controllers
{
    public class TransferController : Controller
    {
        //
        // GET: /Logistics/Transfer/
        private readonly ITransferService _transferService;
        private readonly ICommonService _commonService;
        
        public TransferController(ITransferService transferService,ICommonService commonService )
        {
            _transferService = transferService;
            _commonService = commonService;
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            var transfer = new TransferViewModel();
            transfer.CommoditySource = _commonService.GetCommditySourceName(5);//commodity source for transfer
            ViewBag.ProgramID = new SelectList(_commonService.GetPrograms(), "ProgramID", "Name");
            ViewBag.SourceHubID = new SelectList(_commonService.GetAllHubs(), "HubID", "Name");
            ViewBag.CommodityID = new SelectList(_commonService.GetCommodities(), "CommodityID", "Name");
            ViewBag.CommodityTypeID = new SelectList(_commonService.GetCommodityTypes(), "CommodityTypeID", "Name");
            ViewBag.DestinationHubID = new SelectList(_commonService.GetAllHubs(), "HubID", "Name");
            return View(transfer);
        }
        [HttpPost]
        public ActionResult Create(TransferViewModel transferViewModel)
        {
            if (ModelState.IsValid && transferViewModel != null)
            {
                var transfer = GetTransfer(transferViewModel);
                _transferService.AddTransfer(transfer);
                return RedirectToAction("Index");
            }
            return View(transferViewModel);
        }
        private Transfer GetTransfer(TransferViewModel transferViewModel)
        {
               var transfer = new Transfer()
                {
                  ShippingInstructionID=_commonService.GetShippingInstruction(transferViewModel.SiNumber),
                  SourceHubID=transferViewModel.SourceHubID,
                  ProgramID=transferViewModel.ProgramID,
                  CommoditySourceID=transferViewModel.CommoditySourceID,
                  CommodityID =transferViewModel.CommodityID,
                  DestinationHubID =transferViewModel.DestinationHubID,
                  ProjectCode=transferViewModel.ProjectCode,
                  Quantity=transferViewModel.Quantity,
                  CreatedDate=DateTime.Today,
                  ReferenceNumber=transferViewModel.ReferenceNumber,
                  StatusID=(int)LocalPurchaseStatus.Draft

                };
            return transfer;
        }
        public ActionResult Transfer_Read([DataSourceRequest] DataSourceRequest request)
        {
            var transfer = _transferService.GetAllTransfer().OrderByDescending(m => m.TransferID);
            var transferToDisplay = transfer.ToList();
            return Json(transferToDisplay.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetGiftCertificates()
        {


            var giftCertificate = (from gift in _commonService.GetAllGiftCertificates()
                                   select gift.ShippingInstruction.Value).ToList();
                                  // .Except(
                                       //from allocated in _receiptAllocationService.GetAllReceiptAllocation()
                                       //select allocated.SINumber).ToList();

            return Json(giftCertificate, JsonRequestBehavior.AllowGet);
        }
    }
}
