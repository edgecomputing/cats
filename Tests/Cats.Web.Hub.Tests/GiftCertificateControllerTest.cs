using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Cats.Models.Hubs;
using Cats.Services.Hub;
using Cats.Web.Hub.Controllers;
using Moq;
using NUnit.Framework;

namespace Cats.Web.Hub.Tests
{
    public class GiftCertificateControllerTest
    {
        private GiftCertificateController _giftCertificateController;

        #region SetUp
        [SetUp]
        void Init ()
        {
            var giftCerfificate = new List<GiftCertificate>
                {
                    new GiftCertificate {GiftCertificateID = 1,GiftDate = new DateTime(12/12/2013),DonorID = 2,SINumber = "12121",ReferenceNo = "45461212",
                                         Vessel = "MAERSK UTAH",ETA = new DateTime(11/11/2012),IsPrinted = false,ProgramID = 1,DModeOfTransport = 14,
                                         PortName = "Djibouti"},
                   new GiftCertificate {GiftCertificateID = 2,GiftDate = new DateTime(12/12/2013),DonorID = 1,SINumber = "963258",ReferenceNo = "478547",
                                         Vessel = "MSC MARYLENA",ETA = new DateTime(11/11/2012),IsPrinted = false,ProgramID = 2,DModeOfTransport = 14,
                                         PortName = "Djibouti"}

                };
            var giftCerfificateService = new Mock<IGiftCertificateService>();
            giftCerfificateService.Setup(t => t.GetAllGiftCertificate()).Returns(giftCerfificate);

            var commodities = new List<Commodity>
                {
                    new Commodity {CommodityID = 1, Name = "Cereal", CommodityCode = "CER", CommodityTypeID = 1, ParentID = null},
                    new Commodity {CommodityID = 5, Name = "Wheat", CommodityCode = null, CommodityTypeID = 1, ParentID = 1},
                };
            var commodityService = new Mock<ICommodityService>();
            commodityService.Setup(t => t.GetAllCommodity()).Returns(commodities);

            var userProfiles = new List<UserProfile>
                {
                    new UserProfile {UserProfileID = 1, UserName = "Abebe", Password = "123456", Email = "123ddf@yahoo.com"},
                    new UserProfile {UserProfileID = 2, UserName = "Kebede", Password="632541", Email = "321df@yahoo.com"},

                };
            var userProfileService = new Mock<IUserProfileService>();
            userProfileService.Setup(t => t.GetAllUserProfile()).Returns(userProfiles);

            var receiptAllocation = new List<ReceiptAllocation>
                {
                    new ReceiptAllocation { ReceiptAllocationID = new Guid("f4df54545"),PartitionID = 0,IsCommited = false,ETA = new DateTime(12/12/2013),
                                            ProjectNumber = "WFP 12000/682.4",GiftCertificateDetailID = null,CommodityID = 1,SINumber = "00016252-P1",UnitID = 1,
                                            QuantityInUnit = 412312,QuantityInMT = 12,HubID = 1,DonorID = 2,ProgramID = 1,CommoditySourceID = 1,IsClosed = false,
                                            PurchaseOrder = "231200801p",SupplierName = "kality(faffa F.S.C)",SourceHubID = 5,OtherDocumentationRef = null,Remark = null}
                };
            var receiptAllocationService = new Mock<IReceiptAllocationService>();
            receiptAllocationService.Setup(t => t.GetAllReceiptAllocation()).Returns(receiptAllocation);

            var commodityTypes = new List<CommodityType>
                {
                    new CommodityType{ CommodityTypeID = 1, Name = "Food"}, 
                    new CommodityType{ CommodityTypeID = 2, Name = "Non Food"}
                };
            var commodityTypeService = new Mock<ICommodityTypeService>();
            commodityTypeService.Setup(t => t.GetAllCommodityType()).Returns(commodityTypes);

            var detail = new List<Detail>
                {
                  new Detail {DetailID = 1,Name = "Birr",Description = null,MasterID = 1,SortOrder = 1},
                   new Detail {DetailID = 2,Name = "Birr",Description = null,MasterID = 2,SortOrder = 2}
                };
            var detailService = new Mock<IDetailService>();
            detailService.Setup(t => t.GetAllDetail()).Returns(detail);

            var donor = new List<Donor>
                {
                    new Donor { DonorID = 1,Name = "WFP",DonorCode = "WFP",IsResponsibleDonor = true,IsSourceDonor = true,LongName = "UN - World Food Program"},
                    new Donor { DonorID = 2,Name = "USAID",DonorCode = "UAD",IsResponsibleDonor = false,IsSourceDonor = true,LongName = "United States Agency For International Development"}
                };
            var donorService = new Mock<IDonorService>();
            donorService.Setup(t => t.GetAllDonor()).Returns(donor);

            var program = new List<Program>
                {
                    new Program { ProgramID = 1,Name = "PSNP"},
                    new Program {ProgramID = 2,Name ="Relief"}

                };
            var programService = new Mock<IProgramService>();
            programService.Setup(t => t.GetAllProgram()).Returns(program);

            var giftCertificateDetail = new List<GiftCertificateDetail>
                {
                    new GiftCertificateDetail { GiftCertificateDetailID = 1,PartitionID = 0,TransactionGroupID = 0,GiftCertificateID = 1,CommodityID = 1,
                                                WeightInMT = 12345,BillOfLoading = "MSCDJ639709",AccountNumber = 6281,EstimatedPrice = 806490,EstimatedTax = 12,
                                                YearPurchased = 2012,DFundSourceID = 6,DCurrencyID =1,DBudgetTypeID = 9,ExpiryDate = new DateTime(12/12/2014)}
                };
            var giftCertificateDetailService = new Mock<IGiftCertificateDetailService>();
            giftCertificateDetailService.Setup(t => t.GetAllGiftCertificateDetail()).Returns(giftCertificateDetail);


            _giftCertificateController = new GiftCertificateController(giftCerfificateService.Object, commodityService.Object, userProfileService.Object,
                                                                       receiptAllocationService.Object, detailService.Object, commodityTypeService.Object,
                                                                       donorService.Object, programService.Object, giftCertificateDetailService.Object);

        }
        [TearDown]
        public void Dispose()
        {
            _giftCertificateController.Dispose();
        }

        #endregion

        #region Tests
        [Test]
        public void CanShowIndex()
        {
            var result = _giftCertificateController.Index();
            Assert.IsNotNull(result);
        }

        [Test]
        public void CanSelectGiftCertificateDetails()
        {
            var result = _giftCertificateController.SelectGiftCertificateDetails(1);
            Assert.IsNotNull(result);



        }
        [Test]
        public void CanShowDetails()
        {
            var result = _giftCertificateController.Details(1);
            Assert.IsNotNull(result);

            var model = result.Model;

            Assert.IsInstanceOf<GiftCertificate>(model);
        }
         
        [Test]
        public void CanCreateNewGiftcertificate()
        {
            var giftcertificate=new GiftCertificate
                {
                    GiftCertificateID = 1,
                    GiftDate = new DateTime(12/12/2013),
                    DonorID = 2,
                    SINumber = "12121",
                    ReferenceNo = "45461212",
                    Vessel = "MAERSK UTAH",
                    ETA = new DateTime(11/11/2012),
                    IsPrinted = false,
                    ProgramID = 1,
                    DModeOfTransport = 14,
                    PortName = "Djibouti"
                };
           // var result = _giftCertificateController.Create(giftcertificate);

        }

        [Test]
        public void CanEditGiftCertificate ()
        {
            var result = _giftCertificateController.Edit(1) as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model;
            Assert.IsInstanceOf<GiftCertificate>(model);
        }
       [Test]
        public void CanShowMonthlySummary()
       {
           var result = _giftCertificateController.MonthlySummary();
           Assert.IsNotNull(result);
       }
        [Test]
        public void CanShowChartView()
        {
            var result = _giftCertificateController.ChartView() as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model;
            Assert.IsInstanceOf<GiftCertificate>(model);
        }
        #endregion


    }
}
