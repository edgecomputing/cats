using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using Cats.Areas.Logistics.Controllers;
using Cats.Data.UnitWork;
using Cats.Models;
using Cats.Models.ViewModels;
using Cats.Services.EarlyWarning;
using Cats.Services.Logistics;
using Moq;
using NUnit.Framework;

namespace Cats.Tests.ControllersTests
{
    [TestFixture]
    public class BeneficiaryAllocationControllerTests
    {
        private BeneficiaryAllocationController _benficiaryAllocationController;
        #region SetUp / TearDown

        [SetUp]
        public void Init()
        {
            var beneficiaryAllocations = new List<BeneficiaryAllocation>()
                                     {
                                         new BeneficiaryAllocation()
                                             {
                                                 RequisitionNo = "REQ001",
                                                 FDP = "FDP-001",
                                                 DonorID = 1,
                                                 Donor = "Don-001",
                                                 CommodityID = 1,
                                                 Commodity = "Grain",
                                                 BeneficiaryNo = 100,
                                                 Amount = 20,
                                                 FDPID = 1,
                                                 Program = "EW",
                                                 ProgramID=1,
                                                 Region = "Afar",
                                                 RegionID=1,
                                                 RequisitionID=1,
                                                 ZoneID=2,
                                                 Zone="Zone2",
                                                 WoredaID=1,
                                                 Woreda = "Woreda 2"
                                                 
                                                
                                             },
                                         new BeneficiaryAllocation()
                                             {
                                                 RequisitionNo = "REQ001",
                                                 FDP = "FDP-002",
                                                 DonorID = 1,
                                                 Donor = "Don-001",
                                                 CommodityID = 1,
                                                 Commodity = "Grain",
                                                 BeneficiaryNo = 100,
                                                 Amount = 20,
                                                 FDPID = 2,
                                                 Program = "EW",
                                                 ProgramID=1,
                                                 Region = "Afar",
                                                 RegionID=1,
                                                 RequisitionID=1,
                                                 ZoneID=2,
                                                 Zone="Zone2",
                                                 WoredaID=1,
                                                
                                             },
                                     };
            var beneficiaryAllocaitonService = new Mock<IBeneficiaryAllocationService>();
            beneficiaryAllocaitonService.Setup(t => t.GetBenficiaryAllocation(It.IsAny<Expression<Func<BeneficiaryAllocation,bool>>>())).Returns(beneficiaryAllocations);
            beneficiaryAllocaitonService.Setup(
                t => t.GetBenficiaryAllocation(It.IsAny<Expression<Func<BeneficiaryAllocation, bool>>>())).Returns(
                    beneficiaryAllocations);
            var adminUnitService = new Mock<IAdminUnitService>();
            adminUnitService.Setup(t => t.GetRegions()).Returns(new List<AdminUnit>());
            adminUnitService.Setup(t => t.GetZones(It.IsAny<int>())).Returns(new List<AdminUnit>());
            var reliefRequisitonService = new Mock<IReliefRequisitionService>();
            reliefRequisitonService.Setup(t => t.Get(It.IsAny<Expression<Func<ReliefRequisition, bool>>>(),null,It.IsAny<string>())).Returns(
                new List<ReliefRequisition>());

            _benficiaryAllocationController = new BeneficiaryAllocationController(beneficiaryAllocaitonService.Object, adminUnitService.Object, reliefRequisitonService.Object);

           
        }

        [TearDown]
        public void Dispose()
        {
            
        }

        #endregion

        #region Tests

        [Test]
        public void CanDisplayListBenficiaryAllocation()
        {
            //Act

            var result = _benficiaryAllocationController.Index();

            //Assert

            Assert.IsInstanceOf<List<BeneficiaryAllocation>>(((ViewResult)result).Model);
        }
        [Test]
        public void BefeficiaryAllocationCountShouldBe2()
        {
            //Act
            var result = _benficiaryAllocationController.Index();
            //Assert

            Assert.AreEqual(2,((List<BeneficiaryAllocation >)((ViewResult)result).Model).Count( ));
        }

        #endregion
    }
}
