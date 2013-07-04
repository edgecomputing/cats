using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Cats.Areas.EarlyWarning.Controllers;
using Cats.Areas.Logistics.Controllers;
using Cats.Models;
using Cats.Services.EarlyWarning;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cats.Tests
{

    [TestFixture]
    public class TestClass
    {
        //[TestFixture]
        //public class RequestControllerTest 
        //{

        //    public readonly IRegionalRequestService MockRegionalRequestService;
        //    public readonly IFDPService MockFdpService;
        //    public readonly IAdminUnitService MockAdminUnitService;
        //    public readonly IProgramService MockProgramService;
        //    public readonly ICommodityService MockCommodityService;
        //    public readonly IRegionalRequestDetailService MockRegionalRequestDetailService;


        //    public RequestControllerTest()
        //    {
        //        List<RegionalRequest> regionalRequestTest = new List<RegionalRequest>();
        //        {
        //            new RegionalRequest { RegionalRequestID = 1, ProgramId = 1, Round = 2, RegionID = 9, ReferenceNumber = "AA1234", Year = 2012, Remark = "remarks" };
        //            new RegionalRequest { RegionalRequestID = 2, ProgramId = 2, Round = 4, RegionID = 7, ReferenceNumber = "1234", Year = 2012, Remark = "" };
        //        }
        //        ;
        //        //Mock the Regional Request Service Using Moq 
        //        Mock<IRegionalRequestService> mockRegionalRequestService = new Mock<IRegionalRequestService>();
        //        Mock<IFDPService> mockFDPService = new Mock<IFDPService>();


        //        // return all regional requests
        //        mockRegionalRequestService.Setup(m => m.GetAllReliefRequistion()).Returns(regionalRequestTest);

        //        //return regional requests by id
        //        mockRegionalRequestService.Setup(mr => mr.FindById(
        //           It.IsAny<int>())).Returns((int i) => regionalRequestTest.Where(x => x.RegionalRequestID == i).Single());
        //        this.MockRegionalRequestService = mockRegionalRequestService.Object;

        //    }



        //    private TestContext _testContextInstance;

        //    /// <summary>
        //    ///Gets or sets the test context which provides
        //    ///information about and functionality for the current test run.
        //    ///</summary>
        //    public TestContext TestContext
        //    {
        //        get
        //        {
        //            return _testContextInstance;
        //        }
        //        set
        //        {
        //            _testContextInstance = value;
        //        }
        //    }


        //    [Test]
        //    public void Get_All_Regional_Requests_Test()
        //    {
        //        List<RegionalRequest> expected = new List<RegionalRequest>();
        //        {
        //            new RegionalRequest { RegionalRequestID = 1, ProgramId = 1, Round = 2, RegionID = 9, ReferenceNumber = "AA1234", Year = 2012, Remark = "remarks" };
        //            new RegionalRequest { RegionalRequestID = 2, ProgramId = 2, Round = 4, RegionID = 7, ReferenceNumber = "1234", Year = 2012, Remark = "" };
        //        }
        //        ;

        //        List<RegionalRequest> actual = MockRegionalRequestService.GetAllReliefRequistion();

        //        //Assert.IsInstanceOfType(actual, expected.GetType());
        //        Assert.AreEqual(actual.Count, expected.Count);
        //        foreach (var regionalRequestExpected in expected)
        //        {
        //            Assert.IsTrue(actual.Contains(actual.Find(r => r.RegionalRequestID == regionalRequestExpected.RegionalRequestID)));
        //        }
        //        Assert.AreEqual(expected, actual);

        //    }

        //    [Test]
        //    public void Edit_Regional_Request_Test()
        //    {
        //        // var controller = new RequestController();
        //        //var result = controller.Details(2) as ViewResult;
        //        //Assert.AreEqual(,);

        //    }
        //    [Test]
        //    public void Add_New_Regional_Request_Test()
        //    {


        //    }
        //    [Test]
        //    public void Regional_Requests_Details_Tets()
        //    {
        //        IRegionalRequestService regionalRequestService = this.MockRegionalRequestService;
        //        IFDPService fdpService = this.MockFdpService;
        //        IAdminUnitService adminUnitService = this.MockAdminUnitService;
        //        IProgramService programService = this.MockProgramService;
        //        ICommodityService commodityService = this.MockCommodityService;
        //        IRegionalRequestDetailService regionalRequestDetailService = this.MockRegionalRequestDetailService;

        //        RequestController target = new RequestController(regionalRequestService, fdpService, adminUnitService,
        //                                                          programService, commodityService, regionalRequestDetailService);
        //        int id = 1;
        //        ViewResult expected = new ViewResult();
        //        expected.ViewData.Model = new RegionalRequest()
        //        {
        //            RegionalRequestID = 1,
        //            RequistionDate = new DateTime(2010, 1, 18),
        //            ProgramId = 1,
        //            Round = 2,
        //            RegionID = 9,
        //            ReferenceNumber = "AA2154",
        //            Year = 2012,
        //            Remark = "remarks",
        //        };

        //        ActionResult actual;

        //        actual = target.Details(id);
        //        ViewResult result = actual as ViewResult;
        //        Assert.IsNotNull(result);

        //        //RegionalRequest RegionalRequest1 = result.ViewData.Model as RegionalRequest;
        //        //RegionalRequest RegionalRequest2 = expected.ViewData.Model as RegionalRequest;
        //        //Assert.AreEqual(RegionalRequest1.RegionalRequestID, RegionalRequest2.RegionalRequestID);

        //    }

        //    [Test]
        //    public void Regional_Request_Index_Get_Test()
        //    {

        //        RequestController controller = new RequestController(MockRegionalRequestService, MockFdpService, MockAdminUnitService,
        //                                                             MockProgramService, MockCommodityService, MockRegionalRequestDetailService);
        //        ViewResult view = (ViewResult)controller.Index();
        //        Assert.AreEqual("", view.ViewName);

        //    }

        //    List<DispatchAllocation> DispatchList = new List<DispatchAllocation>();

            [Test]
            public void Can_Test_GetDispatchDetail()
            {
                //Arange
                             
                ProjectAllocationController AllocationController = new ProjectAllocationController();
                Mock<IDispatchAllocationDetailService> DA = new Mock<IDispatchAllocationDetailService>();
                //AllocationController.Setup(m => m.DispatchDetail());
                ViewResult result = AllocationController.DispatchDetail() as ViewResult;
                Assert.IsNotNull(result);
            }
            [Test]
            public void Index_View_Test()
            {
                ProjectAllocationController controller = new ProjectAllocationController();
                ViewResult view = controller.DispatchDetail();
                Assert.AreEqual("DispatchDetail", view.ViewName);
            }


        //}
    }
}
