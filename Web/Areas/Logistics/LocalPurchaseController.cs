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
        public LocalPurchaseController( ILocalPurchaseService localPurchaseService,ILocalPurchaseDetailService localPurchaseDetailService,
                                        IGiftCertificateService giftCertificateService)
        {
            _localPurchaseService = localPurchaseService;
            _localPurchaseDetailService = localPurchaseDetailService;
            _giftCertificateService = giftCertificateService;
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult HubsLocalPurchaseDetail_Read([DataSourceRequest] DataSourceRequest request,int localPurchaseID)
        {
            var localPurchaseDetail = _localPurchaseDetailService.FindBy(m => m.LocalPurchseID == localPurchaseID);
            if (localPurchaseDetail != null)
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
                    select new LocalPurchaseDetailViewModel()
                        {
                            LocalPurchaseDetailID = localPurchaseDetail.LocalPurchaseDetailID,
                            LocalPurchaseID = localPurchaseDetail.LocalPurchseID,
                            HubID = localPurchaseDetail.HubID,
                            HubName = localPurchaseDetail.Hub.Name,
                            AllocatedAmonut = localPurchaseDetail.AllocatedAmount,
                            RecievedAmonut = localPurchaseDetail.RecievedAmount
                        });

        }
        private IEnumerable<LocalPurchaseDetailViewModel> GetNewLocalPurchaseDetail()
        {
            var hubs = _localPurchaseService.GetAllHub();
            return (from hub in hubs
                    select new LocalPurchaseDetailViewModel()
                        {
                            HubID = hub.HubID,
                            HubName = hub.Name,
                            AllocatedAmonut = 0,
                            RecievedAmonut = 0
                        });
        }
    }
}
