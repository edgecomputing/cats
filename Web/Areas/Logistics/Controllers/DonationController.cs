using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Cats.Areas.Logistics.Models;
using Cats.Models;
using Cats.Services.Hub;
using Cats.Services.Logistics;
using Cats.ViewModelBinder;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using GiftCertificateService = Cats.Services.EarlyWarning.GiftCertificateService;
using ICommodityService = Cats.Services.EarlyWarning.ICommodityService;
using ICommodityTypeService = Cats.Services.EarlyWarning.ICommodityTypeService;
using ICommonService = Cats.Services.Common.ICommonService;
using IHubService = Cats.Services.EarlyWarning.IHubService;
using IShippingInstructionService = Cats.Services.EarlyWarning.IShippingInstructionService;
using ITransactionService = Cats.Services.Transaction.ITransactionService;


namespace Cats.Areas.Logistics.Controllers
{
    public class DonationController : Controller
    {
        //
        // GET: /Logistics/Donation/
        
        private readonly IReceiptAllocationService _receiptAllocationService;
        private readonly GiftCertificateService _giftCertificateService;
        private readonly ICommodityService _commodityService;
        private readonly ICommodityTypeService _commodityTypeService;
        private readonly IHubService _hubService;
        private readonly ICommonService _commonService;
        private readonly IShippingInstructionService _shippingInstructionService;
        private readonly IDonationPlanHeaderService _donationPlanHeaderService;
        private readonly IDonationPlanDetailService _donationPlanDetailService;
        private readonly ITransactionService _transactionService;
        public DonationController(
            IReceiptAllocationService receiptAllocationService,
            ICommodityService commodityService,
            ICommonService commonService,
            IShippingInstructionService shippingInstructionService, 
            GiftCertificateService giftCertificateService, 
            ICommodityTypeService commodityTypeService, 
            IHubService hubService, 
            IDonationPlanDetailService donationPlanDetailService, 
            IDonationPlanHeaderService donationPlanHeaderService, ITransactionService transactionService)
        {
            _receiptAllocationService = receiptAllocationService;
            _commodityService = commodityService;
            _commonService = commonService;
            _shippingInstructionService = shippingInstructionService;
           _giftCertificateService = giftCertificateService;
            _commodityTypeService = commodityTypeService;
            _hubService = hubService;
            _donationPlanDetailService = donationPlanDetailService;
            _donationPlanHeaderService = donationPlanHeaderService;
            _transactionService = transactionService;
        }

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult ReadDonationPlan([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                List<DonationPlanHeader> donationHeader = null;
                donationHeader = _donationPlanHeaderService.GetAllDonationPlanHeader().Where(r => r.IsCommited == false).ToList();
                var receiptViewModel = ReceiptPlanViewModelBinder.GetReceiptHeaderPlanViewModel(donationHeader);
                return Json(receiptViewModel.ToList().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return null;


            }
        }

       
        public ActionResult AddNewDonation()
        {
            var model = InitDonationViewModel();
           return PartialView("addNewDonation", model);
        }
       

        public ActionResult AddNewDonationPlan(string siNumber = null,int typeOfLoad =1)
        {
            try
            {

           if (siNumber!=null)
            {

                if (typeOfLoad == 1)
                {
                    return View(LoadFromGiftCertificiate(siNumber));
                }

                var siId = _shippingInstructionService.GetShipingInstructionId(siNumber);
                return View(LoadFromDonation(siId));
            }

            var model = InitDonationViewModel();
            return View(model);
            }
            catch (Exception)
            {

                var model = InitDonationViewModel();
                return View(model);
            }
        }



        public DonationViewModel LoadFromGiftCertificiate(string siNumber)
        {
            var donationViewModel = InitDonationViewModel();


            var giftCertificate = _giftCertificateService.GetAllGiftCertificate().SingleOrDefault(d => d.ShippingInstruction.Value == siNumber);
            if (giftCertificate != null)
            {
                donationViewModel.Commodities.Clear();
                donationViewModel.Donors.Clear();
                donationViewModel.Programs.Clear();



                foreach (Cats.Models.GiftCertificateDetail giftCertificateDetail in giftCertificate.GiftCertificateDetails)
                {
                    donationViewModel.Commodities.Add(giftCertificateDetail.Commodity);
                }

                donationViewModel.CommodityID = giftCertificate.GiftCertificateDetails[0].CommodityID;
                donationViewModel.Donors.Add(giftCertificate.Donor);
                donationViewModel.DonorID = giftCertificate.DonorID;
                donationViewModel.Programs.Add(giftCertificate.Program);
                donationViewModel.ProgramID = giftCertificate.ProgramID;
                donationViewModel.GiftCertificateID = giftCertificate.GiftCertificateID;
                donationViewModel.SINumber = siNumber;
                donationViewModel.ETA = giftCertificate.ETA;
                donationViewModel.CommodityTypeID = giftCertificate.GiftCertificateDetails[0].Commodity.CommodityTypeID;
                donationViewModel.CommodityName = giftCertificate.GiftCertificateDetails[0].Commodity.Name;
                donationViewModel.DonorName = giftCertificate.Donor.Name;
                donationViewModel.ProgramName = giftCertificate.Program.Name;
                donationViewModel.CommomdityTypeName =
                    giftCertificate.GiftCertificateDetails[0].Commodity.CommodityType.Name;

                return donationViewModel;

            }
            return donationViewModel;
        }


        private DonationViewModel LoadFromDonation(int shippinInstructionId)
        {
            var donation = _donationPlanHeaderService.FindBy(s => s.ShippingInstructionId == shippinInstructionId).SingleOrDefault();
            if (donation != null)
            {
                int index = 0;
               var detailList = new List<DonationViewModel>();
                var donationViewModel = InitDonationViewModel();


                donationViewModel.ETA = donation.ETA;
                donationViewModel.CommodityID = donation.CommodityID;
                donationViewModel.ProgramID = donation.ProgramID;
                donationViewModel.DonorID = donation.DonorID;
                donationViewModel.IsCommited = donation.IsCommited;
                donationViewModel.GiftCertificateID = donation.GiftCertificateID;
                donationViewModel.DonationHeaderPlanID = donation.DonationHeaderPlanID;
                donationViewModel.AllocationDate = donation.AllocationDate;
                donationViewModel.EnteredBy = donation.EnteredBy;
                donationViewModel.WieghtInMT = donation.DonatedAmount;
                donationViewModel.ShippingInstructionId = donation.ShippingInstructionId;
                donationViewModel.CommodityTypeID = (int) donation.CommodityTypeID;
                donationViewModel.CommodityName = donation.Commodity.Name;
                donationViewModel.DonorName = donation.Donor.Name;
                donationViewModel.ProgramName = donation.Program.Name;
                donationViewModel.CommomdityTypeName = donation.CommodityType.Name;

                var list = donation.DonationPlanDetails.Select(detail => new DonationDetail
                                                                             {
                                                                                 HubID = detail.HubID, 
                                                                                 Hub = detail.Hub.Name,
                                                                                 AllocatedAmount = detail.AllocatedAmount,
                                                                                 ReceivedAmount = detail.ReceivedAmount, 
                                                                                 Balance = detail.Balance
                                                                             }).ToList();


                donationViewModel.DonationPlanDetails =  list;
                
                   
                
                return donationViewModel;
            }
            return null;
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
                    var shippingInstruction = _shippingInstructionService.FindBy(t => t.Value == receiptAllocationViewModel.SINumber).FirstOrDefault();
                    var gc = new Cats.Models.GiftCertificate();
                    if (shippingInstruction != null)
                        gc = _giftCertificateService.FindBySINumber(shippingInstruction.Value);

                    if (gc != null)
                    {
                        var gcd = gc.GiftCertificateDetails.FirstOrDefault(p => p.CommodityID == receiptAllocationViewModel.CommodityID);
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
                receiptAllocation.CommoditySourceID = Cats.Models.Hubs.CommoditySource.Constants.DONATION;

                receiptAllocation.ReceiptAllocationID = Guid.NewGuid();
                _receiptAllocationService.AddReceiptAllocation(receiptAllocation);

                return RedirectToAction("Index");


            }


            return RedirectToAction("Index");

        }
        private DonationViewModel InitDonationViewModel()
        {



            var program = _commonService.GetPrograms().ToList();
            var donor = _commonService.GetDonors().ToList();
            var hub = _hubService.GetAllHub().ToList();
            var commodityType = _commodityTypeService.GetAllCommodityType();
            var commodity = _commodityService.GetAllCommodity();

            var donationViewModel = new DonationViewModel(commodity, donor, hub, program, commodityType)
                                        {
                                            DonationPlanDetails = GetNewDonationDetail(),
                                            ETA = DateTime.Now
                                            
                                        };
            return donationViewModel;
        }


        public ActionResult LoadBySi(string id)
            {

                var redirectUrl = new UrlHelper(Request.RequestContext).Action("AddNewDonationPlan", "Donation",new { Area = "Logistics", siNumber = id });
                return Json(new {Url = redirectUrl});

            }

        public JsonResult Load(string id)
        {
            var giftCertificate = _giftCertificateService.GetAllGiftCertificate().SingleOrDefault(d => d.ShippingInstruction.Value == id);
            return giftCertificate != null ? Json(new {donorId = giftCertificate.Donor.Name,
                programId = giftCertificate.Program.Name,
                eta=giftCertificate.ETA,
                quantity = giftCertificate.GiftCertificateDetails[0].WeightInMT, 
                comodity = giftCertificate.GiftCertificateDetails[0].Commodity.Name,
                commodityType = giftCertificate.GiftCertificateDetails[0].Commodity.CommodityType.Name
            }, JsonRequestBehavior.AllowGet) : null;
        }

    
        
        private IEnumerable<DonationDetail> GetNewDonationDetail()
        {
            var hubs = _hubService.GetAllHub().Where(h => h.HubOwnerID == 1);
            return hubs.Select(hub => new DonationDetail
                                          {
                                              HubID = hub.HubID, 
                                              Hub = hub.Name, 
                                              AllocatedAmount = 0, 
                                              ReceivedAmount = 0, 
                                              Balance = 0
                                          }).ToList();
        }
        public ActionResult SaveHeader(string  id)
        {
            try
            {
                var donationModel = LoadFromGiftCertificiate(id);
                SaveNewDonationPlan(donationModel,DoesSIExistInShippingInstruction(id));
                return RedirectToAction("AddNewDonationPlan", "Donation", new { Area = "Logistics", siNumber = id, typeOfLoad = 0 });

            }
            catch (Exception)
            {

               return null;
            }
        }

        public JsonResult GetGiftCertificates()
        {
            var giftCertificate =
                _giftCertificateService.GetAllGiftCertificate().Select(g => g.ShippingInstruction.Value);
            return Json(giftCertificate, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Save(DonationViewModel donationViewModel)
        {

            if (ModelState.IsValid)
            {


                if (donationViewModel != null)
                {
                    var siId = DoesSIExistInShippingInstruction(donationViewModel.SINumber);
                    if (siId != 0)
                    {
                        if (!DoesSIExistInDonationHeader(donationViewModel.SINumber))
                        {

                            SaveNewDonationPlan(donationViewModel, siId);
                            
                        }
                        else
                        {

                            UpdateDonationPlan(donationViewModel, siId);
                        }
                    }
                    else
                    {
                        var si = InsertInToShippingInstructionTable(donationViewModel.SINumber);
                            //first in shippins instruction table
                        if (si != -1)
                            SaveNewDonationPlan(donationViewModel, si); // second in doation table
                    }


                }
                return RedirectToAction("Index");

            }

            var model = InitDonationViewModel();
           return View("AddNewDonationPlan",model);
        }

        private bool SaveNewDonationPlan(DonationViewModel donationViewModel,int siId)
        {
            try
            {

           
            var donationHeader = new DonationPlanHeader
            {
                AllocationDate = DateTime.Now,
                CommodityID = donationViewModel.CommodityID,
                DonorID = donationViewModel.DonorID,
                ETA = donationViewModel.ETA,
                IsCommited = false,
                ProgramID = donationViewModel.ProgramID,
                ShippingInstructionId = siId,
                DonatedAmount = donationViewModel.WieghtInMT,
                CommodityTypeID = donationViewModel.CommodityTypeID

            };

            foreach (var donationDetail in donationViewModel.DonationPlanDetails.Select(donationPlanDetail => new DonationPlanDetail
                                                                                                                  {
                                                                                                                      HubID = donationPlanDetail.HubID,
                                                                                                                      AllocatedAmount = donationPlanDetail.AllocatedAmount,
                                                                                                                      ReceivedAmount = donationPlanDetail.ReceivedAmount,
                                                                                                                      Balance = donationPlanDetail.Balance,
                                                                                                                      DonationPlanHeader = donationHeader
                                                                                                                  }))
            {
                _donationPlanDetailService.AddDonationPlanDetail(donationDetail);
            }

                _transactionService.PostDonationPlan(donationHeader);
            return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        private bool UpdateDonationPlan(DonationViewModel donationViewModel, int shippinInstructionId)
        {
            try
            {
                var index = 0;
                var donation = _donationPlanDetailService.FindBy(s => s.DonationPlanHeader.ShippingInstructionId == shippinInstructionId).ToList();
                
                if (donation.Count > 0)
                {
                    


                    var detailArray = donationViewModel.DonationPlanDetails.ToArray();
                    foreach (var donationPlanDetail in donation)
                    {
                        donationPlanDetail.DonationPlanHeader.AllocationDate = DateTime.Now;
                        donationPlanDetail.DonationPlanHeader.CommodityID = donationViewModel.CommodityID;
                        donationPlanDetail.DonationPlanHeader.DonorID = donationViewModel.DonorID;
                        donationPlanDetail.DonationPlanHeader.ETA = donationPlanDetail.DonationPlanHeader.ETA;
                        donationPlanDetail.DonationPlanHeader.IsCommited = false;
                        donationPlanDetail.DonationPlanHeader.ProgramID = donationViewModel.ProgramID;
                        donationPlanDetail.DonationPlanHeader.ShippingInstructionId = shippinInstructionId;
                        donationPlanDetail.DonationPlanHeader.DonatedAmount = donationViewModel.WieghtInMT;
                        donationPlanDetail.DonationPlanHeader.CommodityTypeID = donationViewModel.CommodityTypeID;

                        donationPlanDetail.AllocatedAmount = detailArray[index].AllocatedAmount;
                        donationPlanDetail.ReceivedAmount = detailArray[index].ReceivedAmount;
                        donationPlanDetail.Balance = detailArray[index].Balance;
                        donationPlanDetail.HubID = detailArray[index].HubID;
                       // donationPlanDetail.DonationPlanHeader = donation;
                        _donationPlanDetailService.EditDonationPlanDetail(donationPlanDetail);
                        index++;
                    }
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }
        private int DoesSIExistInShippingInstruction(string siNumber)
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

        private Boolean DoesSIExistInDonationHeader(string siNumber)
        {
            try
            {
                var siId =
                    _donationPlanHeaderService.FindBy(d => d.ShippingInstruction.Value == siNumber).SingleOrDefault();
                if (siId == null)
                    return false;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        private int InsertInToShippingInstructionTable(string siNumber)
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




        public bool TriggerReceive(int donationPlanId)
        {
            try
            {
                var donationDetail =
                    _donationPlanDetailService.FindBy(r => r.DonationHeaderPlanID == donationPlanId).ToList();
                    
                foreach (var detail in donationDetail)
                {
                    var receiptAllocation = new Cats.Models.Hubs.ReceiptAllocation
                                                {
                                                    ReceiptAllocationID = Guid.NewGuid(),
                                                    CommodityID =
                                                        detail.DonationPlanHeader.CommodityID,
                                                    IsCommited = false,
                                                    ETA =
                                                        detail.DonationPlanHeader.ETA,
                                                    ProjectNumber = "12-12",
                                                    SINumber =
                                                        detail.DonationPlanHeader.ShippingInstruction.Value,
                                                        
                                                    QuantityInMT = detail.AllocatedAmount,
                                                    HubID = detail.HubID,
                                                    ProgramID =
                                                        detail.DonationPlanHeader.ProgramID,
                                                       
                                                    GiftCertificateDetailID = detail.DonationPlanHeader.GiftCertificateID,
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

        public ActionResult CloseLocalPlan(int donationPlanHeaderId)
        {
            if (TriggerReceive(donationPlanHeaderId))
            {
                var donationHeader =
                    _donationPlanHeaderService.FindBy(d => d.DonationHeaderPlanID == donationPlanHeaderId).
                        SingleOrDefault();
                if (donationHeader != null)
                {
                    donationHeader.IsCommited = true;
                    _donationPlanHeaderService.EditDonationPlanHeader(donationHeader);
                    return RedirectToAction("Index", "Receive", new { Area = "Hub" });
                }
            }
            return null;
        }

    }
}
