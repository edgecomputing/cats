using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Cats.Areas.Logistics.Models;
using Cats.Models;
using Cats.Models.Hubs;
using Cats.Services.Hub;
using Cats.Services.Logistics;
using Cats.Services.EarlyWarning;
using Cats.Services.Security;
using Cats.ViewModelBinder;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using GiftCertificateService = Cats.Services.EarlyWarning.GiftCertificateService;
using ICommodityService = Cats.Services.EarlyWarning.ICommodityService;
using ICommodityTypeService = Cats.Services.EarlyWarning.ICommodityTypeService;
using ICommonService = Cats.Services.Common.ICommonService;
using IDonorService = Cats.Services.EarlyWarning.IDonorService;
using IGiftCertificateDetailService = Cats.Services.EarlyWarning.IGiftCertificateDetailService;
using IHubService = Cats.Services.EarlyWarning.IHubService;
using IProgramService = Cats.Services.EarlyWarning.IProgramService;
using IShippingInstructionService = Cats.Services.EarlyWarning.IShippingInstructionService;


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
        private readonly Cats.Services.EarlyWarning.ICommodityService _commodityService;
        private readonly ICommodityTypeService _commodityTypeService;
        private readonly IProgramService _programService;
        private readonly IDonorService _donorService;
        private readonly Cats.Services.EarlyWarning.IHubService _hubService;
        private readonly Cats.Services.Common.ICommonService _commonService;
        private readonly IShippingInstructionService _shippingInstructionService;
        private readonly IGiftCertificateDetailService _giftCertificateDetailService;
        private readonly ICommoditySourceService _commoditySourceService;
      
        public DonationController(
            IReceiptAllocationService receiptAllocationService,
         IUserAccountService userAccountService,
         ICommodityService commodityService, 
          ICommonService commonService, 
            IShippingInstructionService shippingInstructionService, 
            IGiftCertificateDetailService giftCertificateDetailService, 
            ICommoditySourceService commoditySourceService, GiftCertificateService giftCertificateService, IDonorService donorService, IProgramService programService, ICommodityTypeService commodityTypeService, IHubService hubService, IReceiptPlanDetailService receiptPlanDetailService, IReceiptPlanService receiptPlanService)
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

                var donationViewModel = PopulateLookup();
              
              
                var giftCertificate = _giftCertificateService.GetAllGiftCertificate().SingleOrDefault(d => d.ShippingInstruction.Value == siNumber);
                if (giftCertificate !=null )
                {
                    donationViewModel.Commodities.Clear();
                    donationViewModel.Donors.Clear();
                    donationViewModel.Programs.Clear();


                   
                        foreach (Cats.Models.GiftCertificateDetail giftCertificateDetail in giftCertificate.GiftCertificateDetails)
                        {
                            donationViewModel.Commodities.Add(giftCertificateDetail.Commodity);
                        }

                    donationViewModel.Donors.Add(giftCertificate.Donor);
                    donationViewModel.DonorID = giftCertificate.DonorID;
                    donationViewModel.Programs.Add(giftCertificate.Program);
                    donationViewModel.ProgramID = giftCertificate.ProgramID;

                    donationViewModel.SINumber = siNumber;
                    donationViewModel.ETA = giftCertificate.ETA;



                    return View(donationViewModel);

                }
            }

            var model = PopulateLookup();
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
                    var gc = new Cats.Models.GiftCertificate();
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
        private DonationViewModel PopulateLookup()
        {



            var program = _commonService.GetPrograms().ToList();
            var donor = _commonService.GetDonors().ToList();
            var hub = _hubService.GetAllHub().ToList();
            var commodityType = _commodityTypeService.GetAllCommodityType();
            var commodity = _commodityService.GetAllCommodity();

          var donationViewModel = new DonationViewModel(commodity, donor, hub  , program,commodityType);
            return donationViewModel;
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
                    return RedirectToAction("Index", "Receive", new { Area = "Hub" });
                }
            }
            return null;
        }

    }
}
