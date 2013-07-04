using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using System.Text;
using System.Threading.Tasks;
using Cats.Areas.Logistics.Controllers;
using Cats.Services.EarlyWarning;
using Cats.Models;


namespace Cats.Tests
{
    [TestFixture]
    class ProjectCodeControllerTest : ControllerTestBase
    {
        public readonly IDispatchAllocationDetailService MockDispatchAllocationService;

        public ProjectCodeControllerTest()
        {
            List<DispatchAllocation> regionalRequestTest = new List<DispatchAllocation>();
            {
                new DispatchAllocation { RequisitionNo = "1", Round = 2, CommodityID = 9, Amount = 1000, Year = 2012, ShippingInstructionID = 1, ProjectCodeID=1 };
                new DispatchAllocation { RequisitionNo = "2", Round = 2, CommodityID = 10, Amount = 1000, Year = 2012, ShippingInstructionID = 2, ProjectCodeID = 2 };
            }
            ;
            List<AdminUnit> adminUnitTest = new List<AdminUnit>();
            {
                new AdminUnit() { AdminUnitID = 1, Name = "Afar", NameAM = null, AdminUnitTypeID = 2, ParentID = 1 };
            }
            ;
            List<Commodity> commodityTest = new List<Commodity>
                {
                    new Commodity { CommodityID = 5, Name = "Grain",LongName = "",CommodityTypeID = 1, ParentID = 1 },
                    new Commodity { CommodityID = 6, Name = "Oil",LongName = "",CommodityTypeID = 1, ParentID = 1 },
                    new Commodity { CommodityID = 8, Name = "CSB",LongName = "",CommodityTypeID = 1, ParentID = 3 },
               };

            
            Mock<IDispatchAllocationDetailService> mockDispatchAllocationService = new Mock<IDispatchAllocationDetailService>();

           
            mockDispatchAllocationService.Setup(m => m.FindBy(req => req.RequisitionNo == "2")).Returns(regionalRequestTest);

            this.MockDispatchAllocationService = mockDispatchAllocationService.Object;
        }

        private TestContext _testContextInstance;
        private ProjectAllocationController _controller;
        

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return _testContextInstance;
            }
            set
            {
                _testContextInstance = value;
            }
        }
        [SetUp]
        public void Setup()
        {
            
            
            _controller = new ProjectAllocationController();

        }

        [Test]
        public void RequestController_Constructor_Test()
        {
            try
            {
                _controller = new ProjectAllocationController();
               
            }
            catch (Exception e)
            {

                Assert.Fail(e.Message);
            }
        }
        [Test]
        public void sould_Return_IndexView()
        {
            var result = _controller.Index();
            Assert.IsInstanceOf(typeof(ViewResult), result);
        }
        [Test]
        public void sould_Return_EditView()
        {
            var result = _controller.DispatchDetail(1);
            Assert.IsInstanceOf(typeof(ViewResult), result);
        }
        [Test]
        public void Dispatch_Detail_Test()
        {

            List<DispatchAllocation> expected = new List<DispatchAllocation>();
            {
                new DispatchAllocation { RequisitionNo = "1", Round = 2, CommodityID = 9, Amount = 1000, Year = 2012, ShippingInstructionID = 1, ProjectCodeID = 1 };
                new DispatchAllocation { RequisitionNo = "2", Round = 2, CommodityID = 10, Amount = 1000, Year = 2012, ShippingInstructionID = 2, ProjectCodeID = 2 };
            }
             
            List<DispatchAllocation> actual = (List<DispatchAllocation>)MockDispatchAllocationService.FindBy(m => m.RequisitionNo == "1");
            Assert.AreEqual(actual, expected);
            foreach (var regionalRequestExpected in expected)
            {
                Assert.IsTrue(actual.Contains(actual.Find(r => r.RequisitionNo == regionalRequestExpected.RequisitionNo)));
            }
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Edit_ProjectCode_Test()
        {

            List<DispatchAllocation> expected = new List<DispatchAllocation>();
            {
                new DispatchAllocation { RequisitionNo = "1", Round = 2, CommodityID = 9, Amount = 1000, Year = 2012, ShippingInstructionID = 1, ProjectCodeID = 1 };
                new DispatchAllocation { RequisitionNo = "2", Round = 2, CommodityID = 10, Amount = 1000, Year = 2012, ShippingInstructionID = 2, ProjectCodeID = 2 };
            }

            bool actual = MockDispatchAllocationService.Save();
            Assert.AreEqual(actual, true);
            List<DispatchAllocation> actualList = (List<DispatchAllocation>)MockDispatchAllocationService.FindBy(m => m.RequisitionNo == "1");
            foreach (var regionalRequestExpected in expected)
            {
                Assert.IsTrue(actualList.Contains(actualList.Find(r => r.RequisitionNo == regionalRequestExpected.RequisitionNo)));
            }
            Assert.AreEqual(expected, actual);
        }
    }
}
