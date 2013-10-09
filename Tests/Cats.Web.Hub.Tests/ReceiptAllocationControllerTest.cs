using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Cats.Models.Hub;
using Cats.Services.Hub;
using Cats.Web.Hub.Controllers.Allocations;

using Moq;
using NUnit.Framework;

namespace Cats.Web.Hub.Tests
{
    [TestFixture]
    public class ReceiptAllocationControllerTest
    {
        #region SetUp / TearDown

        private ReceiptAllocationController _receiptAllocationController;
        [SetUp]
        public void Init()
        {
            //TODO:Please after running the test setup the appropriate methods in each service
            var receiptAllocationService = new Mock<IReceiptAllocationService>();
            var userProfileService = new Mock<IUserProfileService>();
            var commoditySourceService = new Mock<ICommoditySourceService>();
            var giftCertificateService = new Mock<IGiftCertificateService>();
            var commodityService = new Mock<ICommodityService>();
            var donorService = new Mock<IDonorService>();
            var giftCertificateDetailService = new Mock<IGiftCertificateDetailService>();
            var hubService = new Mock<IHubService>();
            var programService = new Mock<IProgramService>();
            var commodityTypeService = new Mock<ICommodityTypeService>();
            _receiptAllocationController = new ReceiptAllocationController(receiptAllocationService.Object,
                                                                           userProfileService.Object,
                                                                           commoditySourceService.Object,
                                                                           giftCertificateService.Object,
                                                                           commodityService.Object, donorService.Object,
                                                                           giftCertificateDetailService.Object,
                                                                           hubService.Object,
                                                                           programService.Object,
                                                                           commodityTypeService.Object);

        }

        [TearDown]
        public void Dispose()
        { _receiptAllocationController.Dispose(); }

        #endregion

        #region Tests

        [Test]
        public void CanSelfReference()
        {
            //Act
            var result = (JsonResult)_receiptAllocationController.SelfReference(1, 1);
            //Assert

            Assert.IsTrue((bool)result.Data);
        }

        [Test]
        public void SINumberShouldBeUnique()
        {
            //Act
            var result = (JsonResult)_receiptAllocationController.SINotUnique("1", 1);
            //Assert
            Assert.IsTrue((bool)result.Data);
        }


        [Test]
        public void ShouldDisplayAllocationListForSelectedISNumber()
        {
            //Act
            var result = (PartialViewResult)_receiptAllocationController.AllocationList("1", 1);
            //Assert
            Assert.IsInstanceOf<List<ReceiptAllocation>>(result.Model);
            Assert.AreEqual(1, ((List<ReceiptAllocation>)result.Model).Count);
        }

        [Test]
        public void ShouldDisplayAllReciptAllocation()
        {
            //Act
            var result = _receiptAllocationController.Index();
            //Assert
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsInstanceOf<List<ReceiptAllocation>>(((ViewResult)result).Model);
            Assert.AreEqual(1, ((List
                                     <ReceiptAllocation>)(((ViewResult)result).Model)).Count);
        }


        [Test]
        public void ShouldPrepareForCreate()
        {
            //Act
            var result = _receiptAllocationController.Create(1);

            //Assert
            Assert.IsInstanceOf<ReceiptAllocationViewModel>(((ViewResult)result).Model);
        }

        [Test]
        public void ShouldCreateReceiptAllocation()
        {
            //Arrange
            var id = Guid.NewGuid();
            var receiptAllocationViewModel = new ReceiptAllocationViewModel()
                                                 {
                                                     CommodityID = 1,
                                                     CommodityTypeID = 1,
                                                     CommoditySourceID = 1,
                                                     DonorID = 1,
                                                     ETA = DateTime.Today,
                                                     GiftCertificateDetailID = 1,
                                                     HubID = 1,
                                                     IsCommited = true,
                                                     PartitionID = 1,
                                                     ProgramID = 1,
                                                     OtherDocumentationRef = "DOC-001",
                                                     PurchaseOrder = "PO-001",
                                                     ProjectNumber = "PRJ-001",
                                                     QuantityInMT = 10,
                                                     ReceiptAllocationID = id
                                                 };

            //Act
            _receiptAllocationController.Create(receiptAllocationViewModel);
            var result = (ViewResult)_receiptAllocationController.Index();

            //Assert

            Assert.AreEqual(2, ((List<ReceiptAllocation>)result.Model).Count);
        }

        [Test]
        public void ShouldPrepareForEdit()
        {
            //Act
            var result = _receiptAllocationController.Edit("RA-001");

            //Assert
            Assert.IsInstanceOf<PartialViewResult>(result);
            Assert.IsInstanceOf<ReceiptAllocationViewModel>(((PartialViewResult)result).Model);
        }

        [Test]
        public void SIShouldBeAvailableInGiftCertificate()
        {
            //Acat
            var result = (JsonResult)_receiptAllocationController.SIMustBeInGift("1", 1);
            //Assert
            Assert.IsTrue((bool)result.Data);
        }

        [Test]
        public void ShouleEditReciptAllocation()
        {
            //Arrange
            var id = Guid.NewGuid();
            var receiptAllocationViewModel = new ReceiptAllocationViewModel()
                                                 {
                                                     CommodityID=1,
                                                     CommodityTypeID=1,
                                                     DonorID=1,
                                                     HubID=1,
                                                     PartitionID=1,
                                                     ProgramID=1,
                                                     QuantityInMT=20,
                                                     CommoditySourceID=2,
                                                     ETA=DateTime.Today,
                                                     GiftCertificateDetailID=1,
                                                     OtherDocumentationRef="REF-0012",
                                                     IsCommited=false,
                                                     ProjectNumber="PRJ-01",
                                                     ReceiptAllocationID=id,
                                                     SourceHubID=3,
                                                     SINumber="SI-001",
                                                     SupplierName="WF"

                                                 };

            //Act

            var result = _receiptAllocationController.Edit(receiptAllocationViewModel);

            //Assert
            Assert.IsNotInstanceOf<PartialViewResult>(result);

        }
        [Test]
        public void ShouldLoadReciptAllocationBySI()
        {
            //Act
            var result = _receiptAllocationController.LoadBySIPartial("1",1);

            //Assert
            Assert.IsInstanceOf<PartialViewResult>(result);
        }

        [Test]
        public void ShouldGetAllavailableSINumbers()
        {
            //Act
            var result = _receiptAllocationController.GetAvailableSINumbers();
            //Assert
            Assert.IsInstanceOf<JsonResult>(result);
        }

        [Test]
        public void ShouldGetAllAvailableSINumbersAsText()
        {
            //Act
            var result = _receiptAllocationController.GetAvailableSINumbersAsText(true, 1);
            //Assert
            Assert.IsInstanceOf<JsonResult>(result);
        }

        [Test]
        public void ShouldGetSIBalances()
        {
            //Act
            var result = _receiptAllocationController.GetSIBalances();

            //Assert
            Assert.IsInstanceOf<PartialViewResult>(result);
            Assert.IsInstanceOf<List<SIBalance>>(((PartialViewResult)result).Model);
        }

        [Test]
        public void ShouldNotReturnAnyBalanceForNullSINumbers()
        {
            //Act
            var result = _receiptAllocationController.GetBalance(string.Empty, 1);
            //Assert
            Assert.IsInstanceOf<EmptyResult>(result);
        }

        [Test]
        public void ShouldPrepareForDelete()
        {
            //Act
            var result = _receiptAllocationController.Delete("1");
            //Assert
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsInstanceOf<ReceiptAllocation>(((ViewResult)result).Model);
        }

        [Test]
        public void ShouldDeleteConfirmedReceiptAllocation()
        {
            //Act
           _receiptAllocationController.DeleteConfirmed("1");
            var result = (ViewResult)_receiptAllocationController.Index();
            //Assert
          Assert.AreEqual(0,((List<ReceiptAllocation>)result.Model).Count );
            
        }
        [Test]
        public void ShouldPrepareForClose()
        {
            //Act
            var result = _receiptAllocationController.Close("1");
            //Assert
            Assert.IsInstanceOf<PartialViewResult>(result);
            Assert.IsInstanceOf<ReceiptAllocation>(((PartialViewResult)result).Model);
        }

        [Test]
        public void ShouldCloseConfirmedReceiptAllocaiton()
        {
            //Act
            var result = _receiptAllocationController.CloseConfirmed("1");
            //Assert
           Assert.IsInstanceOf<ActionResult>(result);
        }

        [Test]
        public void ShouldGenerateProjectCode()
        {
            //Act
            var projectCode = "P1-C1-50";
            var result =(JsonResult) _receiptAllocationController.GenerateProjectCode("1", 1, 1, 50);
            //Assert
            //
            Assert.AreEqual(projectCode,result.Data.ToString());
        }
        #endregion
    }
}
