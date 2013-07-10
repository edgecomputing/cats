using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        public IProjectCodeAllocationService _DispatchAllocationService;
        private ProjectAllocationController _controller;

        //[SetUp]
        //public void Init()
        //{
        //    IEnumerable<DispatchAllocation> regionalRequestTest = new List<DispatchAllocation>();
        //    {
        //        new DispatchAllocation { RequisitionNo = "1", Round = 2, CommodityID = 9, Amount = 1000, Year = 2012, ShippingInstructionID = 1, ProjectCodeID=1 };
        //        new DispatchAllocation { RequisitionNo = "2", Round = 2, CommodityID = 10, Amount = 1000, Year = 2012, ShippingInstructionID = 2, ProjectCodeID = 2 };
        //    }
        //    ;
        //    List<AdminUnit> adminUnitTest = new List<AdminUnit>();
        //    {
        //        new AdminUnit() { AdminUnitID = 1, Name = "Afar", NameAM = null, AdminUnitTypeID = 2, ParentID = 1 };
        //    }
        //    ;
        //    List<Commodity> commodityTest = new List<Commodity>
        //        {
        //            new Commodity { CommodityID = 5, Name = "Grain",LongName = "",CommodityTypeID = 1, ParentID = 1 },
        //            new Commodity { CommodityID = 6, Name = "Oil",LongName = "",CommodityTypeID = 1, ParentID = 1 },
        //            new Commodity { CommodityID = 8, Name = "CSB",LongName = "",CommodityTypeID = 1, ParentID = 3 },
        //       };

            
        //    Mock<IProjectCodeAllocationService> mockDispatchAllocationService = new Mock<IProjectCodeAllocationService>();
        //    mockDispatchAllocationService.Setup(m => m.FindBy(req => req.RequisitionNo == "2")).Returns(regionalRequestTest);

        //    mockDispatchAllocationService.Setup(
        //        t => t.FindBy(It.IsAny<Expression<Func<DispatchAllocation, bool>>>())).Returns(regionalRequestTest.AsQueryable());
        //    mockDispatchAllocationService.Setup(
        //        t => t.SaveProjectAllocation(regionalRequestTest)).Returns(true);

        //    this._DispatchAllocationService = mockDispatchAllocationService.Object;
           
        //    _controller = new ProjectAllocationController(null,mockDispatchAllocationService.Object);
        //}

        //private TestContext _testContextInstance;
        
        

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        //public TestContext TestContext
        //{
        //    get
        //    {
        //        return _testContextInstance;
        //    }
        //    set
        //    {
        //        _testContextInstance = value;
        //    }
        //}
        //[SetUp]
        //public void Setup()
        //{


        //    _controller = new ProjectAllocationController();

        //}

        [Test]
        public void RequestController_Constructor_Test()
        {
            try
            {
                //_controller = new ProjectAllocationController();
               
            }
            catch (Exception e)
            {

                Assert.Fail(e.Message);
            }
        }
       
        
        [Test]
        public void Dispatch_Detail_Test()
        {

            IEnumerable<DispatchAllocation> expected = new List<DispatchAllocation>();
            {
                new DispatchAllocation { RequisitionNo = "1", Round = 2, CommodityID = 9, Amount = 1000, Year = 2012, ShippingInstructionID = 1, ProjectCodeID = 1 };
                new DispatchAllocation { RequisitionNo = "2", Round = 2, CommodityID = 10, Amount = 1000, Year = 2012, ShippingInstructionID = 2, ProjectCodeID = 2 };
            }

            IEnumerable<DispatchAllocation> actual = (IEnumerable<DispatchAllocation>)_DispatchAllocationService.FindBy(m => m.RequisitionNo == "1");
            Assert.AreEqual(actual, expected);
            
        }

        [Test]
        public void Edit_ProjectCode_Test()
        {

            List<DispatchAllocation> expected = new List<DispatchAllocation>();
            {
                new DispatchAllocation { RequisitionNo = "1", Round = 2, CommodityID = 9, Amount = 1000, Year = 2012, ShippingInstructionID = 1, ProjectCodeID = 1 };
                new DispatchAllocation { RequisitionNo = "2", Round = 2, CommodityID = 10, Amount = 1000, Year = 2012, ShippingInstructionID = 2, ProjectCodeID = 2 };
            }

            bool actual = _DispatchAllocationService.SaveProjectAllocation(expected);
            Assert.AreEqual(true, actual);
            IEnumerable<DispatchAllocation> actualList = _DispatchAllocationService.FindBy(m => m.RequisitionNo == "1");
            foreach (var regionalRequestExpected in expected)
            {
                Assert.IsTrue(actualList.Contains(actualList.ToList().Find(r => r.RequisitionNo == regionalRequestExpected.RequisitionNo)));
            }
            
        }
    }
}
