using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Logistics.Models;
using Cats.Models;
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
        private ILocalPurchaseDetailService _localPurchaseDetailService;
        private IGiftCertificateService _giftCertificateService;
        private IShippingInstructionService _shippingInstructionService;
        public LocalPurchaseController( ILocalPurchaseService localPurchaseService,ILocalPurchaseDetailService localPurchaseDetailService,
                                        IGiftCertificateService giftCertificateService,IShippingInstructionService shippingInstructionService)
        {
            _localPurchaseService = localPurchaseService;
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
            var localpurchase = new LocalPurchase();
            return View(localpurchase);

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
        private IEnumerable<LocalPurchaseDetailViewModel> GetNewLocalPurchaseDetail()
        {
            var hubs = _localPurchaseService.GetAllHub().Where(m=>m.HubID < 4);
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
    }
}
