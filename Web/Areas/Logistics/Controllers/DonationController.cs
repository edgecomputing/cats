using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Cats.Areas.Logistics.Models;
using Cats.Models;
using Cats.Models.Hubs;
using Cats.Services.Hub;
using Cats.Services.Logistics;
using Cats.Services.Security;
using Cats.ViewModelBinder;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using CommonService = Cats.Services.Common.CommonService;
using ICommodityTypeService = Cats.Services.EarlyWarning.ICommodityTypeService;
using IDonorService = Cats.Services.EarlyWarning.IDonorService;
using IHubService = Cats.Services.EarlyWarning.IHubService;
using IProgramService = Cats.Services.EarlyWarning.IProgramService;

namespace Cats.Areas.Logistics.Controllers
{
    public class DonationController : Controller
    {
        //
        // GET: /Logistics/Donation/
        private readonly IReceiptPlanService _receiptPlanService;
        private readonly IReceiptPlanDetailService _receiptPlanDetailService;
        private readonly IReceiptAllocationService _receiptAllocationService;
        private readonly IUserAccountService _userAccountService;
        private readonly GiftCertificateService _giftCertificateService;
        private readonly Cats.Services.Hub.ICommodityService _commodityService;
        private readonly Cats.Services.Hub.ICommodityTypeService _commodityTypeService;
        private readonly Cats.Services.Hub.IProgramService _programService;
        private readonly Cats.Services.Hub.IDonorService _donorService;
        private readonly Cats.Services.Hub.IHubService _hubService;
        private readonly CommonService _commonService;
        private readonly IShippingInstructionService _shippingInstructionService;
        private readonly IGiftCertificateDetailService _giftCertificateDetailService;
        private readonly ICommoditySourceService _commoditySourceService;
      
        public DonationController(
            IReceiptAllocationService receiptAllocationService,
         IUserAccountService userAccountService,
         Cats.Services.Hub.ICommodityService commodityService, 
          CommonService commonService, 
            IShippingInstructionService shippingInstructionService, 
            IGiftCertificateDetailService giftCertificateDetailService, 
            ICommoditySourceService commoditySourceService, GiftCertificateService giftCertificateService, Services.Hub.IDonorService donorService, Services.Hub.IProgramService programService, Services.Hub.ICommodityTypeService commodityTypeService, Services.Hub.IHubService hubService, IReceiptPlanDetailService receiptPlanDetailService, IReceiptPlanService receiptPlanService)
        {
            _receiptAllocationService = receiptAllocationService;
          
            _userAccountService = userAccountService;
            _commodityService = commodityService;
           
          
          
         
            _commonService = commonService;
            _shippingInstructionService = shippingInstructionService;
            _giftCertificateDetailService = giftCertificateDetailService;
            _commoditySourceService = commoditySourceService;
            _giftCertificateService = giftCertificateService;
            _donorService = donorService;
            _programService = programService;
            _commodityTypeService = commodityTypeService;
            _hubService = hubService;
            _receiptPlanDetailService = receiptPlanDetailService;
            _receiptPlanService = receiptPlanService;
        }

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult ReadReceiptPlan([DataSourceRequest] DataSourceRequest request, int receiptID = -1)
        {
            try
            {
                List<ReceiptPlan> receipts=null;
                if (receiptID != -1)
                {
                    receipts = _receiptPlanService.GetAllReceiptPlan().Where(r => r.IsClosed == false && r.ReceiptHeaderId == receiptID).ToList();
                    if(receipts.Count < 1)
                        receipts = _receiptPlanService.GetAllReceiptPlan().Where(r => r.IsClosed == false).ToList();
                }
                else
                {
                   receipts = _receiptPlanService.GetAllReceiptPlan().Where(r => r.IsClosed == false).ToList();
                }
                 
                var receiptViewModel = ReceiptPlanViewModelBinder.GetReceiptHeaderPlanViewModel(receipts);
                return Json(receiptViewModel.ToList().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return null;


            }
        }

        public ActionResult ReadReceiptPlanDetail([DataSourceRequest] DataSourceRequest request, int receiptID = -1)
        {
           List<ReceiptPlanDetail> receiptDetails = null;
            if (receiptID != -1)
            {
                receiptDetails = _receiptPlanDetailService.FindBy(r => r.ReceiptHeaderId == receiptID).ToList();
                if (receiptDetails.Count <1)
                    receiptDetails = _receiptPlanDetailService.GetNewReceiptPlanDetail(); 
            }
            else
            {
                receiptDetails = _receiptPlanDetailService.GetNewReceiptPlanDetail(); 
            }
            
            var receiptPlanDetailViewModel = ReceiptPlanViewModelBinder.GetDonationDetailViewModel(receiptDetails);
            return Json(receiptPlanDetailViewModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadReceiptAllocation([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var user = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name);
                List<Cats.Models.Hubs.ReceiptAllocation> list = _receiptAllocationService.GetUnclosedAllocationsDetached(3,1,false,user.PreferedWeightMeasurment,1);
                var receiptViewModel = ReceiptAllocationViewModelBinder.BindReceiptAllocationViewModel(list);
                return Json(receiptViewModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return null;

            }
        }

        public ActionResult AddNewDonationPlan(string siNumber = null)
        {

            if (siNumber!=null)
            {
               

                DonationHeaderViewModel receipt = null;
                var user = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name);
                var giftCertificateDetails =
                    _giftCertificateDetailService.GetAllGiftCertificateDetail().Where(
                        d => d.GiftCertificate.ShippingInstruction.Value == siNumber).ToList();
                if (giftCertificateDetails != null)
                {
                    foreach (var gcertificateDetail in giftCertificateDetails)
                    {

                        var receiptPlanDetail =
                            _receiptPlanService.FindBy(
                                f => f.GiftCertificateDetailId == gcertificateDetail.GiftCertificateDetailID).
                                SingleOrDefault();


                        if (receiptPlanDetail != null)
                        {
                            receipt = new DonationHeaderViewModel()
                                          {
                                              ETA = gcertificateDetail.GiftCertificate.ETA,
                                              Program = gcertificateDetail.GiftCertificate.Program.Name,
                                              Donor = gcertificateDetail.GiftCertificate.Donor.Name,
                                              Commodity = gcertificateDetail.Commodity.Name,
                                              CommodityType = gcertificateDetail.Commodity.CommodityType.Name,
                                              GiftCertificateDetailId = gcertificateDetail.GiftCertificateDetailID,
                                              WieghtInMT = gcertificateDetail.WeightInMT,
                                              ReceiptHeaderId = receiptPlanDetail.ReceiptHeaderId
                                          };
                        }
                        else
                        {
                            receipt = new DonationHeaderViewModel()
                            {
                                ETA = gcertificateDetail.GiftCertificate.ETA,
                                Program = gcertificateDetail.GiftCertificate.Program.Name,
                                Donor = gcertificateDetail.GiftCertificate.Donor.Name,
                                Commodity = gcertificateDetail.Commodity.Name,
                                CommodityType = gcertificateDetail.Commodity.CommodityType.Name,
                                GiftCertificateDetailId = gcertificateDetail.GiftCertificateDetailID,
                                WieghtInMT = gcertificateDetail.WeightInMT
                               
                            };
                        }
                        break;

                    }

                    return View(receipt);

                }
            }
             var model = new DonationHeaderViewModel();
             return View(model);
        }

            [HttpPost]
        public ActionResult AddNewDonationPlan(Cats.Models.Hubs.ReceiptAllocationViewModel receiptAllocationViewModel)
        {

            ModelState.Remove("SourceHubID");
            ModelState.Remove("SupplierName");
            ModelState.Remove("PurchaseOrder");
           

            
            if (ModelState.IsValid)
            {
                var receiptAllocation = receiptAllocationViewModel.GenerateReceiptAllocation();
                
                if (receiptAllocationViewModel.GiftCertificateDetailID == 0 || receiptAllocationViewModel.GiftCertificateDetailID == null)
                {
                    var shippingInstruction =_shippingInstructionService.FindBy(t => t.Value == receiptAllocationViewModel.SINumber).FirstOrDefault();
                    var gc = new Cats.Models.Hubs.GiftCertificate();
                    if (shippingInstruction != null)
                        gc = _giftCertificateService.FindBySINumber(shippingInstruction.Value);

                    if (gc != null)
                    {
                        var gcd =gc.GiftCertificateDetails.FirstOrDefault(p => p.CommodityID == receiptAllocationViewModel.CommodityID);
                        if (gcd != null) 
                        {
                            receiptAllocation.GiftCertificateDetailID = gcd.GiftCertificateDetailID;
                        }
                    }
                    else
                    {
                        receiptAllocation.GiftCertificateDetailID = null;
                    }
                }
               
               

                receiptAllocation.HubID = receiptAllocationViewModel.HubID;
                receiptAllocation.CommoditySourceID =Cats.Models.Hubs.CommoditySource.Constants.DONATION;
              
                receiptAllocation.ReceiptAllocationID = Guid.NewGuid();
                _receiptAllocationService.AddReceiptAllocation(receiptAllocation);

                return RedirectToAction("Index");
               
                
            }
            

            return RedirectToAction("Index");

        }
        private void PopulateLookup()
        {
           ViewBag.RegionID = new SelectList(_commonService.GetAminUnits(t => t.AdminUnitTypeID == 2), "AdminUnitID","Name");
           ViewBag.ProgramId = new SelectList(_commonService.GetPrograms().Take(2), "ProgramID", "Name");
           ViewBag.DonorID = new SelectList(_commonService.GetDonors(), "DonorId", "Name");
           ViewBag.HubID = new SelectList(_hubService.GetAllHub(), "HubID", "Name");
           ViewBag.CommodityTypeID = new SelectList(_commodityTypeService.GetAllCommodityType(), "CommodityTypeID", "Name");
           ViewBag.CommodityID = new SelectList(_commodityService.GetAllCommodity(), "CommodityID", "Name");

        }

        public ActionResult LoadBySi(string id)
        {

            var redirectUrl = new UrlHelper(Request.RequestContext).Action("AddNewDonationPlan", "Donation",
                                                                          new { Area = "Logistics", siNumber = id });
            return Json(new {Url = redirectUrl});

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SaveDonation([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<DonationDetailViewModel.DonationDetail> donationDetailViewModel, int id = -1)
        {
            ReceiptPlanDetail[] receieptDetailArray = new ReceiptPlanDetail[] {};
            var detailViewModel = donationDetailViewModel as DonationDetailViewModel.DonationDetail[] ?? donationDetailViewModel.ToArray();
             var receiptDetailPlan = _receiptPlanService.FindBy(p => p.GiftCertificateDetailId == id).SingleOrDefault();
             //if (receiptDetailPlan != null)
             //{
             //    receieptDetailArray = receiptDetailPlan.ReceiptPlanDetails.ToArray();
             //    for (var i = 0; i < detailViewModel.GetUpperBound(0); i++)
             //    {
             //        receieptDetailArray[i].Allocated = detailViewModel[i].Allocated;
             //        receieptDetailArray[i].Received = detailViewModel[i].Received;
             //        receieptDetailArray[i].Balance = detailViewModel[i].Balance;

             //        var detail = new ReceiptPlanDetail();
             //        detail = receieptDetailArray[i];
             //        _receiptPlanDetailService.EditReceiptPlanDetail(detail);
             //    }

             //}
             //else
             //{






                 var user = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name);
                 var receiptPlanHeader = new ReceiptPlan
                                             {
                                                 IsClosed = false,
                                                 ReceiptDate = DateTime.Now,
                                                 EnteredBy = user.UserProfileID
                                             };

                 foreach (var donationDetail in detailViewModel)
                 {
                     var receiptDetail = new ReceiptPlanDetail
                                             {
                                                 HubId = donationDetail.HubId,
                                                 Allocated = donationDetail.Allocated,
                                                 Received = donationDetail.Received,
                                                 Balance = donationDetail.Balance
                                             };

                     receiptPlanHeader.GiftCertificateDetailId = id;
                     receiptDetail.ReceiptPlan = receiptPlanHeader;
                     _receiptPlanDetailService.AddReceiptPlanDetail(receiptDetail);
                 }
            // }

            return Json(detailViewModel.ToDataSourceResult(request, ModelState));
        }

        private Cats.Models.Hubs.ReceiptAllocationViewModel BindReceiptAllocaitonViewModel()
        {

            var commodities = _commodityService.GetAllCommodity().DefaultIfEmpty().OrderBy(o => o.Name).ToList();
            var donors = _donorService.GetAllDonor().DefaultIfEmpty().OrderBy(o => o.Name).ToList();
            var hubs = _hubService.GetAllHub();
            
            var programs = _programService.GetAllProgram().DefaultIfEmpty().OrderBy(o => o.Name).ToList();
            var commoditySource = _commoditySourceService.GetAllCommoditySource().OrderBy(o => o.Name).ToList();
            var commodityTypes = _commodityTypeService.GetAllCommodityType().OrderBy(o => o.Name).ToList();
            var viewModel = new Cats.Models.Hubs.ReceiptAllocationViewModel(commodities, donors,hubs, programs,commoditySource, commodityTypes,null);

            return viewModel;
        }


        public bool TriggerReceive(int receiptHeaderId)
        {
            try
            {
                var receipt =
                    _receiptPlanDetailService.FindBy(r => r.ReceiptHeaderId == receiptHeaderId).ToList();
                foreach (var receiptPlanDetail in receipt)
                {
                    var receiptAllocation = new Cats.Models.Hubs.ReceiptAllocation
                                                {
                                                    ReceiptAllocationID = Guid.NewGuid(),
                                                    CommodityID =
                                                        receiptPlanDetail.ReceiptPlan.GiftCertificateDetail.CommodityID,
                                                    IsCommited = false,
                                                    ETA =
                                                        receiptPlanDetail.ReceiptPlan.GiftCertificateDetail.
                                                        GiftCertificate.ETA,
                                                    ProjectNumber = "12-12",
                                                    SINumber =
                                                        receiptPlanDetail.ReceiptPlan.GiftCertificateDetail.
                                                        GiftCertificate.ShippingInstruction.Value,
                                                    QuantityInMT = receiptPlanDetail.Allocated,
                                                    HubID = receiptPlanDetail.HubId,
                                                    ProgramID =
                                                        receiptPlanDetail.ReceiptPlan.GiftCertificateDetail.
                                                        GiftCertificate.ProgramID,
                                                        GiftCertificateDetailID = receiptPlanDetail.ReceiptPlan.GiftCertificateDetailId,
                                                    CommoditySourceID = 1,
                                                    IsClosed = false,
                                                    PartitionID = 0
                                                };

                    _receiptAllocationService.AddReceiptAllocation(receiptAllocation);
                }
                return true;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format(
                        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:",
                        DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    outputLines.AddRange(eve.ValidationErrors.Select(ve => string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage)));
                }
                // System.IO.File.AppendAllLines(@"c:\temp\errors.txt", outputLines);
                throw;
            }
              
            
        }

        public ActionResult CloseLocalPlan(int receiptHeaderId)
        {
            if (TriggerReceive(receiptHeaderId))
            {
                var receiptHeader =
                    _receiptPlanService.FindBy(f => f.ReceiptHeaderId == receiptHeaderId).SingleOrDefault();
                if (receiptHeader != null)
                {
                    receiptHeader.IsClosed = true;
                    _receiptPlanService.EditReceiptPlan(receiptHeader);
                    return RedirectToAction("Index", "ReceiptAllocation", new { Area = "Hub" });
                }
            }
            return null;
        }

    }
}
