using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Logistics.Models;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Services.Common;
using Cats.Services.EarlyWarning;
using Cats.Services.Logistics;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Areas.Logistics.Controllers
{
    public class LocalPurchaseController : Controller
    {
        //
        // GET: /Logistics/LocalPurchase/
        private readonly ILocalPurchaseService _localPurchaseService;
        private readonly ICommonService _commonService;
        private readonly ILocalPurchaseDetailService _localPurchaseDetailService;
        private readonly IGiftCertificateService _giftCertificateService;
        private readonly IShippingInstructionService _shippingInstructionService;
        public LocalPurchaseController( ILocalPurchaseService localPurchaseService,ICommonService commonService,ILocalPurchaseDetailService localPurchaseDetailService,
                                        IGiftCertificateService giftCertificateService,IShippingInstructionService shippingInstructionService)
        {
            _localPurchaseService = localPurchaseService;
            _commonService = commonService;
            _localPurchaseDetailService = localPurchaseDetailService;
            _giftCertificateService = giftCertificateService;
            _shippingInstructionService = shippingInstructionService;
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            ViewBag.ProgramID = new SelectList(_commonService.GetPrograms(),"ProgramID","Name");
            ViewBag.CommodityID =new SelectList( _commonService.GetCommodities(),"CommodityID","Name");
            ViewBag.CommodityTypeID =new SelectList( _commonService.GetCommodityTypes(),"CommodityTypeID","Name");
            ViewBag.DonorID = new SelectList( _commonService.GetDonors(),"DonorID","Name");
            var localpurchase = new LocalPurchaseWithDetailViewModel
                {
                    CommoditySource="Local Purchase",
                    LocalPurchaseDetailViewModels = GetNewLocalPurchaseDetail()
                };
            return View(localpurchase);

        }
        public ActionResult Details(int id)
        {
            var localPurchase = _localPurchaseService.FindById(id);
            ViewBag.ProgramID = new SelectList(_commonService.GetPrograms(), "ProgramID", "Name",localPurchase.ProgramID);
            ViewBag.CommodityID = new SelectList(_commonService.GetCommodities(), "CommodityID", "Name",localPurchase.CommodityID);
            ViewBag.CommodityTypeID = new SelectList(_commonService.GetCommodityTypes(), "CommodityTypeID", "Name");
            ViewBag.DonorID = new SelectList(_commonService.GetDonors(), "DonorID", "Name",localPurchase.DonorID);
            if (localPurchase!=null)
            {
                var localPurchaseWithDetailViewModel = new LocalPurchaseWithDetailViewModel()
                    {
                        LocalPurchaseID = localPurchase.LocalPurchaseID,
                        ProgramID = localPurchase.ProgramID,
                        DonorID = localPurchase.DonorID,
                        CommodityID = localPurchase.DonorID,
                        ProjectCode = localPurchase.ProjectCode,
                        SINumber = localPurchase.ShippingInstruction.Value,
                        ReferenceNumber = localPurchase.ReferenceNumber,
                        SupplierName = localPurchase.SupplierName,
                        PurchaseOrder = localPurchase.PurchaseOrder,
                        Quantity = localPurchase.Quantity,
                        StatusID = localPurchase.StatusID,
                        CommoditySource = "Local Purchase",
                        LocalPurchaseDetailViewModels = GetLocalPurchaseDetail(localPurchase.LocalPurchaseDetails)

                    };
                return View(localPurchaseWithDetailViewModel);
            }
            return RedirectToAction("Index");
        }
        public ActionResult SaveLocalPurchase(LocalPurchaseWithDetailViewModel localPurchaseWithDetailViewModel)
        {
            if(localPurchaseWithDetailViewModel!=null)
            {
                var shippingInstractionID = CheckAvilabilityOfSiNumber(localPurchaseWithDetailViewModel.SINumber);
                if (shippingInstractionID != 0)
                {
                    if (!CheckAvailabilityOfSiInLocalPurchase(localPurchaseWithDetailViewModel.SINumber))
                    {

                        SaveNewLocalPurchase(localPurchaseWithDetailViewModel, shippingInstractionID);
                    }
                    
                }
                else
                {
                    var si = AddSiNumber(localPurchaseWithDetailViewModel.SINumber);
                    if (si != -1)
                        SaveNewLocalPurchase(localPurchaseWithDetailViewModel, si);// second in doation table
                }
            }
            return RedirectToAction("Index");
        }

        private bool SaveNewLocalPurchase(LocalPurchaseWithDetailViewModel localPurchaseWithDetailViewModel, int sippingInstractionID)
        {
            try
            {


                var localPurchase = new LocalPurchase()
                {
                    DateCreated = DateTime.Now,
                    CommodityID = localPurchaseWithDetailViewModel.CommodityID,
                    DonorID = localPurchaseWithDetailViewModel.DonorID,
                    ProgramID = localPurchaseWithDetailViewModel.ProgramID,
                    ShippingInstructionID = sippingInstractionID,
                    PurchaseOrder = localPurchaseWithDetailViewModel.PurchaseOrder,
                    SupplierName = localPurchaseWithDetailViewModel.SupplierName,
                    Quantity = localPurchaseWithDetailViewModel.Quantity,
                    ReferenceNumber = localPurchaseWithDetailViewModel.ReferenceNumber,
                    ProjectCode = localPurchaseWithDetailViewModel.ProjectCode,
                    StatusID = (int)LocalPurchaseStatus.Draft,


                };

                foreach (var localPurchaseDetail in localPurchaseWithDetailViewModel.LocalPurchaseDetailViewModels
                    .Select(localPurchaseDetail => new LocalPurchaseDetail()
                {
                    HubID = localPurchaseDetail.HubID,
                    AllocatedAmount = localPurchaseDetail.AllocatedAmonut,
                    RecievedAmount = localPurchaseDetail.RecievedAmonut,
                    LocalPurchase = localPurchase
                }))
                {
                    _localPurchaseDetailService.AddLocalPurchaseDetail(localPurchaseDetail);
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public ActionResult UpdateLocalPurchase(LocalPurchaseWithDetailViewModel localPurchaseDetailViewModel)
        {
            var localPurchase = _localPurchaseService.FindById(localPurchaseDetailViewModel.LocalPurchaseID);
            var index = 0;
            if (localPurchase!=null)
            {
                localPurchase.CommodityID = localPurchaseDetailViewModel.CommodityID;
                localPurchase.DonorID = localPurchaseDetailViewModel.DonorID;
                localPurchase.ProgramID = localPurchaseDetailViewModel.ProgramID;
                localPurchase.PurchaseOrder = localPurchaseDetailViewModel.PurchaseOrder;
                localPurchase.SupplierName = localPurchaseDetailViewModel.SupplierName;
                localPurchase.Quantity = localPurchaseDetailViewModel.Quantity;
                localPurchase.ReferenceNumber = localPurchaseDetailViewModel.ReferenceNumber;
                localPurchase.ProjectCode = localPurchaseDetailViewModel.ProjectCode;
                _localPurchaseService.EditLocalPurchase(localPurchase);
                //var localPurchaseDetailsViewModel = localPurchaseDetailViewModel.LocalPurchaseDetailViewModels.ToArray();
                //foreach (var localPurchaseDetail in localPurchase.LocalPurchaseDetails)
                //{
                //    localPurchaseDetail.AllocatedAmount = localPurchaseDetailsViewModel[index].AllocatedAmonut;
                //    localPurchaseDetail.RecievedAmount = localPurchaseDetailsViewModel[index].RecievedAmonut;
                //    //localPurchaseDetail.LocalPurchase = localPurchase;
                //    _localPurchaseDetailService.EditLocalPurchaseDetail(localPurchaseDetail);
                //    index++;

                //}
                ModelState.AddModelError("Sucess",@"Local Purchase Sucessfully Updated");
                return RedirectToAction("Details", new {id = localPurchase.LocalPurchaseID});
            }
            return RedirectToAction("Index");
        }
        public ActionResult LocalPurchase_Read([DataSourceRequest] DataSourceRequest request)
        {
            var localPurchase = _localPurchaseService.GetAllLocalPurchase().OrderByDescending(m => m.LocalPurchaseID).ToList();
            var localPurchseToDisplay = GetLocalPurchase(localPurchase);
            return Json(localPurchseToDisplay.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult HubsLocalPurchaseDetail_Read([DataSourceRequest] DataSourceRequest request,int localPurchaseID)
        {
            var localPurchaseDetail = _localPurchaseDetailService.FindBy(m => m.LocalPurchaseID == localPurchaseID);
            if (localPurchaseDetail.Count != 0)
            {
                var localPurchaseToDisplay = GetLocalPurchaseDetail(localPurchaseDetail);
                return Json(localPurchaseToDisplay.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            var newLocalPurchaseDetail = GetNewLocalPurchaseDetail();
            return Json(newLocalPurchaseDetail.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        private IEnumerable<LocalPurchaseDetailViewModel> GetLocalPurchaseDetail(IEnumerable<LocalPurchaseDetail> localPurchaseDetails)
        {
            return (from localPurchaseDetail in localPurchaseDetails
                    select new LocalPurchaseDetailViewModel
                        {
                            LocalPurchaseDetailID = localPurchaseDetail.LocalPurchaseDetailID,
                            LocalPurchaseID = localPurchaseDetail.LocalPurchaseID,
                            HubID = localPurchaseDetail.HubID,
                            HubName = localPurchaseDetail.Hub.Name,
                            AllocatedAmonut = localPurchaseDetail.AllocatedAmount,
                            RecievedAmonut = localPurchaseDetail.RecievedAmount,
                            RemainingAmonut = _localPurchaseService.GetRemainingAmount(localPurchaseDetail.LocalPurchaseID)
                            
                        });

        }
        private IEnumerable<LocalPurchaseViewModel> GetLocalPurchase(IEnumerable<LocalPurchase> localPurchases)
        {
            return (from localPurchase in localPurchases
                    select new LocalPurchaseViewModel
                        {
                            LocalPurchaseID = localPurchase.LocalPurchaseID,
                            CommodityID = localPurchase.CommodityID,
                            Commodity = localPurchase.Commodity.Name,
                            ProgramID = localPurchase.ProgramID,
                            Program = localPurchase.Program.Name,
                            DonorID = localPurchase.DonorID,
                            DonorName = localPurchase.Donor.Name,
                            SupplierName = localPurchase.SupplierName,
                            ReferenceNumber = localPurchase.ReferenceNumber,
                            SiNumber = localPurchase.ShippingInstruction.Value,
                            Quantity = localPurchase.Quantity,
                            ProjectCode = localPurchase.ProjectCode,
                            Status = _commonService.GetStatusName(WORKFLOW.LocalPUrchase, localPurchase.StatusID)
                            //CreatedDate = localPurchase.DateCreated,
                           
                        });

        }

        private IEnumerable<LocalPurchaseDetailViewModel> GetNewLocalPurchaseDetail()
        {
            var hubs = _localPurchaseService.GetAllHub().Where(m=>m.HubOwnerID==1);
            return (from hub in hubs
                    select new LocalPurchaseDetailViewModel
                        {
                            HubID = hub.HubID,
                            HubName = hub.Name,
                            AllocatedAmonut = 0,
                            RecievedAmonut = 0
                        });
        }
        public JsonResult GetGiftCertificateInfo(string siNumber)
        {
            var giftCertificate = _giftCertificateService.FindBy(m => m.ShippingInstruction.Value == siNumber).FirstOrDefault();
            if (giftCertificate!=null)
            {
                var giftDetail = (from detail in giftCertificate.GiftCertificateDetails
                                  select new LocalPurchaseFromGiftCertificateInfo
                                      {
                                          GiftCertificateID = detail.GiftCertificateID,
                                          CommodityID = detail.CommodityID,
                                          CommodityName = detail.Commodity.Name,
                                          ProgramName = giftCertificate.Program.Name,
                                          DonorID = giftCertificate.DonorID,
                                          DonorName = giftCertificate.Donor.Name,
                                          QuantityInMT = detail.WeightInMT,
                                          CommoditySource = "Local Purchase"
                                      });
                return Json(giftDetail, JsonRequestBehavior.AllowGet);
            }
            return null;
        }
        [HttpGet]
        public JsonResult AutoCompleteSiNumber(string term)
        {
            var result = (from siNumber in _shippingInstructionService.GetAllShippingInstruction()
                          where siNumber.Value.ToLower().StartsWith(term.ToLower())
                          select siNumber.Value);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        private int CheckAvilabilityOfSiNumber(string siNumber)
        {
            try
            {
                var siId = _shippingInstructionService.GetShipingInstructionId(siNumber);
                return siId;
            }
            catch (Exception)
            {

                return 0;
            }
        }

        private Boolean CheckAvailabilityOfSiInLocalPurchase(string siNumber)
        {
            var shippingInstructionID = _shippingInstructionService.FindBy(m => m.Value == siNumber).FirstOrDefault().ShippingInstructionID;
            try
            {
                var siId = _localPurchaseService.FindBy(d => d.ShippingInstructionID == shippingInstructionID).SingleOrDefault();
                if (siId == null)
                    return false;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        private int AddSiNumber(string siNumber)
        {
            try
            {
                var shippingInstruction = new Cats.Models.ShippingInstruction();
                shippingInstruction.Value = siNumber;
                _shippingInstructionService.AddShippingInstruction(shippingInstruction);
                return shippingInstruction.ShippingInstructionID;
            }
            catch (Exception)
            {

                return -1;
            }
        }
        public ActionResult Approve(int id)
        {
            var localPurchase = _localPurchaseService.FindById(id);
            if (localPurchase!=null)
            {
                //localPurchase.StatusID = (int) LocalPurchaseStatus.Approved;
                _localPurchaseService.Approve(localPurchase);
                return RedirectToAction("details", new {id = localPurchase.LocalPurchaseID});
            }
            return RedirectToAction("Index");
        }
    }
}
