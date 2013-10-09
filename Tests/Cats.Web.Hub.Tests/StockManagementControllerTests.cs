using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Cats.Models.Hub;
using Cats.Services.Hub;
using Cats.Models.Hub.ViewModels.Common;
using Cats.Models.Hub.ViewModels.Report;
using Cats.Web.Hub.Controllers.Reports;
using Moq;
using NUnit.Framework;

namespace Cats.Web.Hub.Tests
{
    [TestFixture]
    public class StockManagementControllerTests
    {
        #region SetUp / TearDown

        private StockManagementController _stockManagementController;
        [SetUp]
        public void Init()
        {
            var userProfiles = new List<UserProfile>
                {
                    new UserProfile {UserProfileID = 1, UserName = "Nathnael", Password = "passWord", Email = "123@edge.com"},
                    new UserProfile {UserProfileID = 2, UserName = "Banty", Password="passWord", Email = "321@edge.com"},

                };
            var userProfileService = new Mock<IUserProfileService>();
            userProfileService.Setup(t => t.GetAllUserProfile()).Returns(userProfiles);

            var programs = new List<Program>
                {
                    new Program{ProgramID = 1, Name = "Relief"},
                    new Program{ProgramID = 2, Name = "PSNP"}
                };
            var programService = new Mock<IProgramService>();
            programService.Setup(t => t.GetAllProgram()).Returns(programs);

            var commodityTypes = new List<CommodityType>
                {
                    new CommodityType{ CommodityTypeID = 1, Name = "Food"}, 
                    new CommodityType{ CommodityTypeID = 2, Name = "Non Food"}
                };
            var commodityTypeService = new Mock<ICommodityTypeService>();
            commodityTypeService.Setup(t => t.GetAllCommodityType()).Returns(commodityTypes);

            var commoditySource = new List<CommoditySource>
                {
                    new CommoditySource {CommoditySourceID = 1, Name = "Donation"},
                    new CommoditySource {CommoditySourceID = 2, Name = "Loan"},
                    new CommoditySource {CommoditySourceID = 3, Name = "Local Purchase"},
                };
            var commoditySourceService = new Mock<ICommoditySourceService>();
            commoditySourceService.Setup(t => t.GetAllCommoditySource()).Returns(commoditySource);

            var projectCodes = new List<ProjectCode>
                {
                    new ProjectCode{ProjectCodeID = 76, Value = "10685 MT"},
                    new ProjectCode{ProjectCodeID = 77, Value = "1314.3"}
                };
            var projectCodeService = new Mock<IProjectCodeService>();
            projectCodeService.Setup(t => t.GetAllProjectCode()).Returns(projectCodes);

            var shippingInstructions = new List<ShippingInstruction>
                {
                    new ShippingInstruction{ShippingInstructionID = 104, Value = "00013753"},
                    new ShippingInstruction{ShippingInstructionID = 102, Value = "00014110"}
                };
            var shippingInstructionService = new Mock<IShippingInstructionService>();
            shippingInstructionService.Setup(t => t.GetAllShippingInstruction()).Returns(shippingInstructions);
            var receiveService = new Mock<IReceiveService>();
            var storeService = new Mock<IStoreService>();
            var hubService = new Mock<IHubService>();
            var adminUnitService = new Mock<IAdminUnitService>();
            var dispatchAllocationServie = new Mock<IDispatchAllocationService>();
            var donorService = new Mock<IDonorService>();
            _stockManagementController = new StockManagementController(
                userProfileService.Object, 
                programService.Object,
                commodityTypeService.Object, 
                commoditySourceService.Object, 
                projectCodeService.Object, 
                shippingInstructionService.Object,
                receiveService.Object,
                storeService.Object,
                hubService.Object,
               adminUnitService.Object, 
               dispatchAllocationServie.Object,
               donorService.Object);
        }

        [TearDown]
        public void Dispose()
        {
            _stockManagementController.Dispose();
        }

        #endregion

        #region Tests

        [Test]
        public void CanViewArrivalsVsReceipts()
        {
            //ACT
            var viewResult = _stockManagementController.ArrivalsVsReceipts() as ViewResult;

            //ASSERT
            Assert.NotNull(viewResult);
            var model = viewResult.Model;
            Assert.IsInstanceOf<ArrivalsVsReceiptsViewModel>(model);
        }

        [Test]
        public void CanArrivalsVsReceiptsReport()
        {
            //ACT
            //TODO: Seed data into ArrivalsVsReceiptsViewModel before proceding with testing
            var viewModel = new ArrivalsVsReceiptsViewModel {};
            var viewResult = _stockManagementController.ArrivalsVsReceiptsReport(viewModel) as ViewResult;

            //ASSERT
            Assert.NotNull(viewResult);
            Assert.IsInstanceOf<IEnumerable<Program>>(viewResult.ViewBag.Program);
            Assert.IsInstanceOf<IEnumerable<CommodityType>>(viewResult.ViewBag.CommodityTypes);
            Assert.IsInstanceOf<IEnumerable<CommoditySource>>(viewResult.ViewBag.CommoditySources);
        }

        [Test]
        public void CanViewReceipts()
        {
            //ACT
            var viewResult = _stockManagementController.Receipts() as ViewResult;

            //ASSERT
            Assert.NotNull(viewResult);
            var model = viewResult.Model;
            Assert.IsInstanceOf<ReceiptsViewModel>(model);
        }

        [Test]
        public void CanReceiptsReport()
        {
            //ACT
            //TODO: Seed data into ReceiptsViewModel before proceding with testing
            var viewModel = new ReceiptsViewModel { };
            var viewResult = _stockManagementController.ReceiptsReport(viewModel) as ViewResult;

            //ASSERT
            Assert.NotNull(viewResult);
            Assert.IsInstanceOf<IEnumerable<Program>>(viewResult.ViewBag.Program);
            Assert.IsInstanceOf<IEnumerable<CommodityType>>(viewResult.ViewBag.CommodityTypes);
            Assert.IsInstanceOf<IEnumerable<CommoditySource>>(viewResult.ViewBag.CommoditySources);
        }

        [Test]
        public void CanViewStockBalance()
        {
            //ACT
            var viewResult = _stockManagementController.StockBalance() as ViewResult;

            //ASSERT
            Assert.NotNull(viewResult);
            var model = viewResult.Model;
            Assert.IsInstanceOf<StockBalanceViewModel>(model);
        }

        [Test]
        public void CanBackPostStockBalanceReport()
        {
            //ACT
            //TODO: Seed data into StockBalanceViewModel before proceding with testing
            var viewModel = new StockBalanceViewModel { };
            var viewResult = _stockManagementController.StockBalanceReport(viewModel) as ViewResult;

            //ASSERT
            Assert.NotNull(viewResult);
            Assert.IsInstanceOf<IEnumerable<Program>>(viewResult.ViewBag.Program);
            Assert.IsInstanceOf<IEnumerable<CommodityType>>(viewResult.ViewBag.CommodityTypes);
        }

        [Test]
        public void CanViewDispatches()
        {
            //ACT
            var viewResult = _stockManagementController.Dispatches() as ViewResult;

            //ASSERT
            Assert.NotNull(viewResult);
            var model = viewResult.Model;
            Assert.IsInstanceOf<DispatchesViewModel>(model);
        }

        [Test]
        public void CanBackPostDispatchesReport()
        {
            //ACT
            //TODO: Seed data into DispatchesViewModel before proceding with testing
            var viewModel = new DispatchesViewModel { };
            var viewResult = _stockManagementController.DispatchesReport(viewModel) as ViewResult;

            //ASSERT
            Assert.NotNull(viewResult);
            Assert.IsInstanceOf<IEnumerable<Program>>(viewResult.ViewBag.Program);
            Assert.IsInstanceOf<IEnumerable<CommodityType>>(viewResult.ViewBag.CommodityTypes);
        }

        [Test]
        public void CanViewCommittedVsDispatched()
        {
            //ACT
            var viewResult = _stockManagementController.CommittedVsDispatched() as ViewResult;

            //ASSERT
            Assert.NotNull(viewResult);
            var model = viewResult.Model;
            Assert.IsInstanceOf<CommittedVsDispatchedViewModel>(model);
        }

        [Test]
        public void CanBackPostCommittedVsDispatchedReport()
        {
            //ACT
            //TODO: Seed data into CommittedVsDispatchedViewModel before proceding with testing
            var viewModel = new CommittedVsDispatchedViewModel { };
            var viewResult = _stockManagementController.CommittedVsDispatchedReport(viewModel) as ViewResult;

            //ASSERT
            Assert.NotNull(viewResult);
            Assert.IsInstanceOf<IEnumerable<Program>>(viewResult.ViewBag.Program);
            Assert.IsInstanceOf<IEnumerable<CommodityType>>(viewResult.ViewBag.CommodityTypes);
        }

        [Test]
        public void CanViewInTransit()
        {
            //ACT
            var viewResult = _stockManagementController.InTransit() as ViewResult;

            //ASSERT
            Assert.NotNull(viewResult);
            var model = viewResult.Model;
            Assert.IsInstanceOf<InTransitViewModel>(model);
        }

        [Test]
        public void CanBackPostInTransitReprot()
        {
            //ACT
            //TODO: Seed data into InTransitViewModel before proceding with testing
            var viewModel = new InTransitViewModel { };
            var viewResult = _stockManagementController.InTransitReprot(viewModel) as ViewResult;

            //ASSERT
            Assert.NotNull(viewResult);
            Assert.IsInstanceOf<IEnumerable<Program>>(viewResult.ViewBag.Program);
            Assert.IsInstanceOf<IEnumerable<CommodityType>>(viewResult.ViewBag.CommodityTypes);
        }

        [Test]
        public void CanViewDeliveryAgainstDispatch()
        {
            //ACT
            var viewResult = _stockManagementController.DeliveryAgainstDispatch() as ViewResult;

            //ASSERT
            Assert.NotNull(viewResult);
            var model = viewResult.Model;
            Assert.IsInstanceOf<DeliveryAgainstDispatchViewModel>(model);
        }

        [Test]
        public void CanBackPostDeliveryAgainstDispatchReport()
        {
            //ACT
            //TODO: Seed data into DeliveryAgainstDispatchViewModel before proceding with testing
            var viewModel = new DeliveryAgainstDispatchViewModel { };
            var viewResult = _stockManagementController.DeliveryAgainstDispatchReport(viewModel) as ViewResult;

            //ASSERT
            Assert.NotNull(viewResult);
            Assert.IsInstanceOf<IEnumerable<Program>>(viewResult.ViewBag.Program);
            Assert.IsInstanceOf<IEnumerable<CommodityType>>(viewResult.ViewBag.CommodityTypes);
        }

        [Test]
        public void CanViewDistributionDeliveryDispatch()
        {
            //ACT
            var viewResult = _stockManagementController.DistributionDeliveryDispatch() as ViewResult;

            //ASSERT
            Assert.NotNull(viewResult);
            var model = viewResult.Model;
            Assert.IsInstanceOf<DistributionDeliveryDispatchViewModel>(model);
        }

        [Test]
        public void CanBackPostDistributionDeliveryDispatchReport()
        {
            //ACT
            //TODO: Seed data into DistributionDeliveryDispatchViewModel before proceding with testing
            var viewModel = new DistributionDeliveryDispatchViewModel { };
            var viewResult = _stockManagementController.DistributionDeliveryDispatchReport(viewModel) as ViewResult;

            //ASSERT
            Assert.NotNull(viewResult);
            Assert.IsInstanceOf<IEnumerable<Program>>(viewResult.ViewBag.Program);
            Assert.IsInstanceOf<IEnumerable<CommodityType>>(viewResult.ViewBag.CommodityTypes);
        }

        [Test]
        public void CanViewDistributionByOwner()
        {
            //ACT
            var viewResult = _stockManagementController.DistributionByOwner() as ViewResult;

            //ASSERT
            Assert.NotNull(viewResult);
            var model = viewResult.Model;
            Assert.IsInstanceOf<DistributionByOwnerViewModel>(model);
        }

        [Test]
        public void CanBackPostDistributionByOwnerReport()
        {
            //ACT
            //TODO: Seed data into DistributionByOwnerViewModel before proceding with testing
            var viewModel = new DistributionByOwnerViewModel { };
            var viewResult = _stockManagementController.DistributionByOwnerReport(viewModel) as ViewResult;

            //ASSERT
            Assert.NotNull(viewResult);
            Assert.IsInstanceOf<IEnumerable<Program>>(viewResult.ViewBag.Program);
            Assert.IsInstanceOf<IEnumerable<CommodityType>>(viewResult.ViewBag.CommodityTypes);
        }

        [Test]
        public void CanViewProjectCode()
        {
            //ACT
            var viewResult = _stockManagementController.ProjectCode() as ViewResult;

            //ASSERT
            Assert.NotNull(viewResult);
            Assert.IsInstanceOf<IEnumerable<SelectList>>(viewResult.ViewBag.ProjectCode);
            Assert.AreEqual("ProjectCodeId", ((SelectList)viewResult.ViewBag.ProjectCode).DataValueField);
            Assert.AreEqual("ProjectName", ((SelectList)viewResult.ViewBag.ProjectCode).DataTextField);
            Assert.IsInstanceOf<IEnumerable<ProjectCodeViewModel>>(((SelectList)viewResult.ViewBag.ProjectCode).Items);
        }

        [Test]
        public void CanViewShippingInstruction  ()
        {
            //ACT
            var viewResult = _stockManagementController.ShippingInstruction() as ViewResult;

            //ASSERT
            Assert.NotNull(viewResult);
            Assert.IsInstanceOf<IEnumerable<SelectList>>(viewResult.ViewBag.ShippingInstruction);
            Assert.AreEqual("ShippingInstructionId", ((SelectList)viewResult.ViewBag.ShippingInstruction).DataValueField);
            Assert.AreEqual("ShippingInstructionName", ((SelectList)viewResult.ViewBag.ShippingInstruction).DataTextField);
            Assert.IsInstanceOf<IEnumerable<ShippingInstructionViewModel>>(((SelectList)viewResult.ViewBag.ShippingInstruction).Items);
        }

        #endregion
    }
}
