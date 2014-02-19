using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Logistics.Models;
using Cats.Helpers;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Services.Common;
using Cats.Services.Logistics;
using Cats.Services.Security;
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
        private readonly IUserAccountService _userAccountService;
        
        public TransferController(ITransferService transferService,ICommonService commonService,IUserAccountService userAccountService )
        {
            _transferService = transferService;
            _commonService = commonService;
            _userAccountService = userAccountService;
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
                  CommoditySourceID=5,
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
        public ActionResult Detail(int id)
        {
            var transfer = _transferService.FindById(id);
            if (transfer==null)
            {
                return HttpNotFound();
            }
            return View(transfer);
        }
        public ActionResult Transfer_Read([DataSourceRequest] DataSourceRequest request)
        {
            var transfer = _transferService.GetAllTransfer().OrderByDescending(m => m.TransferID);
            var transferToDisplay = GetAllTransfers(transfer);
            return Json(transferToDisplay.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public IEnumerable<TransferViewModel>  GetAllTransfers (IEnumerable<Transfer> transfers)
        {
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            return (from transfer in transfers
                    select new TransferViewModel
                        {
                            TransferID = transfer.TransferID,
                            SiNumber = transfer.ShippingInstruction.Value,
                            CommodityID = transfer.CommodityID,
                            Commodity = transfer.Commodity.Name,
                            CommoditySource = transfer.CommoditySource.Name,
                            Program = transfer.Program.Name,
                            SourceHubID = transfer.SourceHubID,
                            SourceHubName = transfer.Hub.Name,
                            Quantity = transfer.Quantity,
                            DestinationHubID = transfer.DestinationHubID,
                            DestinationHubName = transfer.Hub1.Name,
                            CreatedDate = transfer.CreatedDate.ToCTSPreferedDateFormat(datePref),
                            StatusName = _commonService.GetStatusName(WORKFLOW.LocalPUrchase, transfer.StatusID)

                        }
                   );
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
        public ActionResult Approve(int id)
        {
            var transfer = _transferService.FindById(id);
            if (transfer!=null)
            {
                _transferService.Approve(transfer);
                return RedirectToAction("Detail", new { id = transfer.TransferID });
                
            }
            ModelState.AddModelError("Errors",@"Unable to Approve the given Transfer");
            return RedirectToAction("Index");
        }
    }
}
