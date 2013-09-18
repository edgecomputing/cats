using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Cats.Models.Hub;
using Cats.Services.Hub;
using Cats.Models.Hub.ViewModels;
using Cats.Models.Hub.ViewModels.Dispatch;
using Cats.Web.Hub.Controllers.Allocations;
using Moq;
using NUnit.Framework;

namespace DRMFSS.Web.Test
{
    [TestFixture]
    public class DispatchAllocationControllerTest
    {
        #region SetUp / TearDown

        private DispatchAllocationController _dispatchAllocationController;
        [SetUp]
        public void Init()
        {
            var dispatchAllocationService = new Mock<IDispatchAllocationService>();

            dispatchAllocationService.Setup(t => t.GetAvailableCommodities(It.IsAny<string>())).Returns(new List
                                                                                                            <Commodity>()
                                                                                                            {
                                                                                                                new Commodity
                                                                                                                    ()
                                                                                                                    {
                                                                                                                        CommodityID
                                                                                                                            =
                                                                                                                            1,
                                                                                                                        CommodityCode
                                                                                                                            =
                                                                                                                            "Com-1"
                                                                                                                    }
                                                                                                            });
            var list = new List<SIBalance>()
                                           {
                                               new SIBalance()
                                                   {
                                                       Commodity = "CSB",
                                                       SINumberID = 1,
                                                       CommitedToFDP = 20,
                                                       ProjectCodeID = 1,
                                                       SINumber = "1",
                                                       AvailableBalance = 40,
                                                       CommitedToOthers = 10,
                                                       Dispatchable = 30,
                                                       Program = "Relief",
                                                       Project = "1",
                                                       ReaminingExpectedReceipts = 1,
                                                       TotalDispatchable = 20
                                                   }};
            dispatchAllocationService.Setup(
                t => t.GetUncommitedSIBalance(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(
                    (int hubId, int commodityId, string unitMeasure) => list
                       );
            var userProfileService = new Mock<IUserProfileService>();
            var shippingInstructionService = new Mock<IShippingInstructionService>();
            var projectCodeService = new Mock<IProjectCodeService>();
            var otherDispatchAllocationService = new Mock<IOtherDispatchAllocationService>();
            var transporterService = new Mock<TransporterService>();
            var commonService = new Mock<ICommonService>();
            var adminUnitService = new Mock<IAdminUnitService>();
            var fdpService = new Mock<IFDPService>();
            var hubService = new Mock<IHubService>();
            var commodityTypeService = new Mock<ICommodityTypeService>();

            this._dispatchAllocationController = new DispatchAllocationController(
                dispatchAllocationService.Object,
                userProfileService.Object,
                otherDispatchAllocationService.Object,
                shippingInstructionService.Object,
                projectCodeService.Object,
                transporterService.Object,
                commonService.Object,
                adminUnitService.Object,
                fdpService.Object,
                hubService.Object,
                commodityTypeService.Object);

        }

        [TearDown]
        public void Dispose()
        {
            _dispatchAllocationController.Dispose();
        }

        #endregion

        #region Tests

        [Test]
        public void ShouldDisplayListOfRequisitionNumbers()
        {
            //Act
            var result = (ViewResult)_dispatchAllocationController.Index();

            //Assert
            Assert.IsInstanceOf<List<string>>(result.Model);
        }
        [Test]
        public void ShouldDisplayListOfDispatchAllocations()
        {
            //Act
            var result = (ViewResult)_dispatchAllocationController.AllocationList();

            //Assert
            Assert.IsInstanceOf<IEnumerable<DispatchAllocationViewModelDto>>(result.Model);
        }
        [Test]
        public void ShouldReturnListOfAvailableSINumbers()
        {
            //Act
            var result = _dispatchAllocationController.GetAvailableSINumbers(1);
            //Act
            Assert.IsInstanceOf<JsonResult>(result);
        }

        [Test]
        public void ShouldReturnEmptyForUnavailableRequisitionNo()
        {
            //Act
            var result = _dispatchAllocationController.GetCommodities(string.Empty);

            //Assert

            Assert.IsInstanceOf<EmptyResult>(result);
        }

        [Test]
        public void ShouldReturnSIBalance()
        {
            //Act
            var result = (ViewResult)_dispatchAllocationController.SiBalances("R-001");

            //Assert
            var x = new JsonResult();

            Assert.IsInstanceOf<List<SIBalance>>(result.Model);
        }

        [Test]
        public void ShouldDisplayFDPAllocationForOneCommodity()
        {
            //Act
            var result = (ViewResult)_dispatchAllocationController.GetAllocations("R-001", 1, true);

            //Assert
            Assert.IsInstanceOf<List<DispatchAllocationViewModelDto>>(result.Model);

        }

        [Test]
        public void ShouldDisplayAllSIBalances()
        {
            //Act
            var result = (ViewResult)_dispatchAllocationController.GetSIBalances();

            //Assert
            Assert.IsInstanceOf<List<SIBalance>>(result.Model);
        }
        [Test]
        public void ShouldReturnAvailableRequisitions()
        {
            //Act
            var result = _dispatchAllocationController.AvailableRequistions(true);
            //Assert
            Assert.IsInstanceOf<JsonResult>(result);
        }

        [Test]
        public void ShouldPrepareForCreateDispatchAllocation()
        {
            //Act
            var result = (ViewResult)_dispatchAllocationController.Create();

            //Assert
            Assert.IsInstanceOf<DispatchAllocationViewModel>(result.Model);
        }

        [Test]
        public void ShouldCrateDispatchAllocation()
        {
            //Act
            var id = Guid.NewGuid();
            var dispatchAllocation = new DispatchAllocationViewModel()
            {
                CommodityID = 1,
                Amount = 10,
                Beneficiery = 120,
                BidRefNo = "1",
                CommodityTypeID = 1,
                DispatchAllocationID = id,
                DonorID = 1,
                FDPID = 1,
                HubID = 1,
                Month = 1,
                PartitionID = 1,
                ProgramID = 1,
                ProjectCodeID = 1,
                RegionID = 1,
                RequisitionNo = "R-001",
                Round = 1,
                ShippingInstructionID = 1,
                TransporterID = 1,
                Unit = 1,
                WoredaID = 3,
                Year = 2012,
                ZoneID = 2
            };
            _dispatchAllocationController.Create(dispatchAllocation);

            var result = (ViewResult)_dispatchAllocationController.Index();

            //Assert
            Assert.AreEqual(2, ((List<string>)result.Model).Count);

        }

        [Test]
        public void CanPrepareCreateLoan()
        {
            //Act
            var result = (ViewResult)_dispatchAllocationController.CreateLoan();

            //Assert
            Assert.IsInstanceOf<OtherDispatchAllocationViewModel>(result.Model);
        }

        [Test]
        public void CanPrepareEditLoan()
        {
            //Act
            var result = (ViewResult)_dispatchAllocationController.EditLoan("L-001");

            //Assert
            Assert.IsInstanceOf<OtherDispatchAllocationViewModel>(result.Model);
        }

        [Test]
        public void SouldSaveLoan()
        {
            //Arrange
            var id = Guid.NewGuid();
            var dispatchAllocation = new OtherDispatchAllocationViewModel()
            {
                CommodityID = 1,
                CommodityTypeID = 1,
                PartitionID = 1,
                ProgramID = 1,
                AgreementDate = DateTime.Today,
                EstimatedDispatchDate = DateTime.Today.AddDays(3),
                FromHubID = 2,
                ToHubID = 1,
                IsClosed = true,
                OtherDispatchAllocationID = id,
                ProjectCode = "P-001",
                QuantityInMT = 12,
                QuantityInUnit = 12,
                ReasonID = 1,
                ReferenceNumber = "Rf-001",
                UnitID = 1

            };
            //Act

            var result = _dispatchAllocationController.SaveLoan(dispatchAllocation);

            //Assert
            Assert.IsInstanceOf<RedirectToRouteResult>(result);
        }
        #endregion
    }
}
