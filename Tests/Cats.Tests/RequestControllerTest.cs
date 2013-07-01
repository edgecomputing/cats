using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Cats.Areas.EarlyWarning.Controllers;
using Cats.Models;
using Cats.Services.EarlyWarning;
namespace Cats.Tests
{
    [TestFixture]
    public class RequestControllerTest : ControllerTestBase
    {

        public readonly IRegionalRequestService MockRegionalRequestService;
        public readonly IFDPService MockFdpService;
        public readonly IAdminUnitService MockAdminUnitService;
        public readonly IProgramService MockProgramService;
        public readonly ICommodityService MockCommodityService;
        public readonly IRegionalRequestDetailService MockRegionalRequestDetailService;



        public RequestControllerTest()
        {
            List<RegionalRequest> regionalRequestTest = new List<RegionalRequest>();
            {
                new RegionalRequest { RegionalRequestID = 1, ProgramId = 1, Round = 2, RegionID = 9, ReferenceNumber = "AA1234", Year = 2012, Remark = "remarks" };
                new RegionalRequest { RegionalRequestID = 2, ProgramId = 2, Round = 4, RegionID = 7, ReferenceNumber = "1234", Year = 2012, Remark = "" };
            }
            ;
            List<AdminUnit> adminUnitTest=new List<AdminUnit>();
            {
                new AdminUnit() {AdminUnitID = 1, Name = "Afar", NameAM = null, AdminUnitTypeID = 2, ParentID = 1};
            }
            ;
            List<Commodity> commodityTest = new List<Commodity>
                {
                    new Commodity { CommodityID = 5, Name = "Yellow Wheat",LongName = "",CommodityTypeID = 1, ParentID = 1 },
                    new Commodity { CommodityID = 6, Name = "Green Wheat",LongName = "",CommodityTypeID = 1, ParentID = 1 },
                    new Commodity { CommodityID = 8, Name = "Beans",LongName = "",CommodityTypeID = 1, ParentID = 3 },
               };

            //Mock the Regional Request Service Using Moq 
            Mock<IRegionalRequestService> mockRegionalRequestService = new Mock<IRegionalRequestService>();
            Mock<IFDPService> mockFDPService = new Mock<IFDPService>();
            Mock<IAdminUnitService> mockAdminUnitService = new Mock<IAdminUnitService>();
            Mock<IProgramService> mockProgramService = new Mock<IProgramService>();
            Mock<ICommodityService> mockCommodityService = new Mock<ICommodityService>();
            Mock<IRegionalRequestDetailService> mockRegionalRequestDetailService=new Mock<IRegionalRequestDetailService>();


        
            // return all regional requests
            mockRegionalRequestService.Setup(m => m.GetAllReliefRequistion()).Returns(regionalRequestTest);
            mockRegionalRequestService.Setup(m => m.Get(t=>t.RegionalRequestID==2,null,null));

            mockFDPService.Setup(m => m.GetAllFDP()).Returns(new List<FDP>());
            mockAdminUnitService.Setup(m => m.FindBy(au => au.AdminUnitTypeID==2)).Returns(adminUnitTest);
            mockProgramService.Setup(m => m.GetAllProgram()).Returns(new List<Program>());
            mockCommodityService.Setup(m => m.GetAllCommodity()).Returns(commodityTest);
            

            //return regional requests by id
            mockRegionalRequestService.Setup(mr => mr.FindById(
               It.IsAny<int>())).Returns((int i) => regionalRequestTest.Single(x => x.RegionalRequestID == i));

            this.MockRegionalRequestService = mockRegionalRequestService.Object;
            this.MockAdminUnitService = mockAdminUnitService.Object;
            this.MockFdpService = mockFDPService.Object;
            this.MockProgramService = mockProgramService.Object;
            this.MockCommodityService = mockCommodityService.Object;
            this.MockRegionalRequestDetailService = mockRegionalRequestDetailService.Object;

        }

        private TestContext _testContextInstance;
        private RequestController _controller;

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
            
            
            _controller = new RequestController(MockRegionalRequestService, MockFdpService, MockAdminUnitService, MockProgramService, MockCommodityService, MockRegionalRequestDetailService);

        }
        
        [Test]
        public void RequestController_Constructor_Test()
        {
            try
            {
                _controller=new RequestController(MockRegionalRequestService, MockFdpService, MockAdminUnitService, MockProgramService, MockCommodityService, MockRegionalRequestDetailService);
;
            }
            catch (Exception e)
            {

                Assert.Fail(e.Message);
            }
        }

        [Test]
        public void Get_All_Regional_Requests_Test()
        {
            List<RegionalRequest> expected = new List<RegionalRequest>();
            {
                new RegionalRequest { RegionalRequestID = 1, ProgramId = 1, Round = 2, RegionID = 9, ReferenceNumber = "AA1234", Year = 2012, Remark = "remarks" };
                new RegionalRequest { RegionalRequestID = 2, ProgramId = 2, Round = 4, RegionID = 7, ReferenceNumber = "1234", Year = 2012, Remark = "" };
            }
            ;

            List<RegionalRequest> actual = MockRegionalRequestService.GetAllReliefRequistion();
            Assert.AreEqual(actual.Count, expected.Count);
            foreach (var regionalRequestExpected in expected)
            {
                Assert.IsTrue(actual.Contains(actual.Find(r => r.RegionalRequestID == regionalRequestExpected.RegionalRequestID)));
            }
            Assert.AreEqual(expected, actual);

        }


        [Test]
        public void Add_New_Regional_Request_Test()
        {
            
            ActionResult actual = _controller.New();
            ViewResult result = actual as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("", result.ViewName);

           
        }
        [Test]
        public void Edit_Regional_Request_Test()
        {
           
            List<RegionalRequest> expected = new List<RegionalRequest>();
            {
                new RegionalRequest { RegionalRequestID = 1, ProgramId = 1, Round = 2, RegionID = 9, ReferenceNumber = "AA1234", Year = 2012, Remark = "remarks" };
                new RegionalRequest { RegionalRequestID = 2, ProgramId = 2, Round = 4, RegionID = 7, ReferenceNumber = "1234", Year = 2012, Remark = "" };
            }

            List<RegionalRequest> actual = (List<RegionalRequest>) MockRegionalRequestService.Get(m => m.RegionalRequestID ==1);
            Assert.AreEqual(actual, expected);
            foreach (var regionalRequestExpected in expected)
            {
                Assert.IsTrue(actual.Contains(actual.Find(r => r.RegionalRequestID == regionalRequestExpected.RegionalRequestID)));
            }
            Assert.AreEqual(expected, actual);

        }

        [Test]
        public void Regional_Requests_Details_Tets()
        {
            int id = 1;
            ViewResult expected = new ViewResult();
            expected.ViewData.Model = new RegionalRequest()
            {
                RegionalRequestID = 1,
                RequistionDate = new DateTime(2010, 1, 18),
                ProgramId = 1,
                Round = 2,
                RegionID = 9,
                ReferenceNumber = "AA2154",
                Year = 2012,
                Remark = "remarks",
            };

            ActionResult actual;

            actual = _controller.Details(id);
            ViewResult result = actual as ViewResult;
            //Assert.IsNotNull();Null(result);

            //RegionalRequest RegionalRequest1 = result.ViewData.Model as RegionalRequest;
            //RegionalRequest RegionalRequest2 = expected.ViewData.Model as RegionalRequest;
            //Assert.AreEqual(RegionalRequest1.RegionalRequestID, RegionalRequest2.RegionalRequestID);

        }

        [Test]
        public void Regional_Request_Index_Get_Test()
        {

            
            ActionResult actual = _controller.Index();
            ViewResult result = actual as ViewResult;
            //String viewName = "Index";
            Assert.IsNotNull(result);
            Assert.AreEqual("", result.ViewName);

        }
        [Test]
        public void RequestController_Index_Action_Should_Fetch_Regional_Requests()
        {
            //Arange
            var expectedRequest = new RegionalRequest[0];
            //MockRegionalRequestService.GetAllReliefRequistion().

            //Act
            var actual = _controller.Index();


            //Assert
            Assert.IsInstanceOfType(typeof(ViewResult), actual);
            var viewResult = (ViewResult)actual;
            Assert.IsNotNull(actual);
            //Assert.AreEqual(regionalRequestTest, viewResult.ViewData.Model);

        }
       
    }

}
