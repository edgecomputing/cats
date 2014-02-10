using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Logistics.Models;
using Cats.Models;
using Cats.Services.Common;
using Cats.Services.EarlyWarning;
using Cats.Services.Logistics;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Areas.Logistics
{
    public class LocalPurchaseController : Controller
    {
        //
        // GET: /Logistics/LocalPurchase/
        private ILocalPurchaseService _localPurchaseService;
        private ICommonService _commonService;
        private ILocalPurchaseDetailService _localPurchaseDetailService;
        private IGiftCertificateService _giftCertificateService;
        private IShippingInstructionService _shippingInstructionService;
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
                    else
                    {

                        //UpdateLocalPurchase(localPurchaseWithDetailViewModel, shippingInstractionID);
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
                    StatusID = 1,


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
                            RecievedAmonut = localPurchaseDetail.RecievedAmount
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
    }
}
