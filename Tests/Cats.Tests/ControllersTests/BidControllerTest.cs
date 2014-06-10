using System;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Procurement.Controllers;
using Cats.Models.Constant;
using Cats.Models.Security;
using Cats.Services.EarlyWarning;
using Cats.Services.Procurement;
using Cats.Services.Common;
using Cats.Services.Security;
using Kendo.Mvc.UI;
using Moq;
using NUnit.Framework;
using Cats.Models;
using System.Collections.Generic;

namespace Cats.Tests.ControllersTests
{
    [TestFixture]
    public class BidControllerTest
    {

        private IBidService MockBidService ;
        private IBidDetailService MockBidDetail;
        private IAdminUnitService MockAdminUnitService;
        private IStatusService MockStatusService;
        private ITransportBidPlanService MockTransportBidPlanService;
        private ITransportBidPlanDetailService MockTransportBidPlanDetailService;
        private IApplicationSettingService MockApplicationSetting;
        private BidController _bidController;
      

      
        [SetUp]
        public void SetUp()
          {

            List<Bid> bidTest = new List<Bid>();
              {
                  new Bid() { BidID=1,BidNumber ="PP452",StartDate=new DateTime(2012/10/10),EndDate=new DateTime(2013/12/11),OpeningDate = new DateTime(2013/12/12),StatusID =1};
                  new Bid() { BidID = 2,BidNumber ="AAA123",StartDate = new DateTime(2012 / 10 / 10), EndDate = new DateTime(2013 / 12 / 11),OpeningDate = new DateTime(2012/11/13),StatusID =2};
                  new Bid() { BidID = 3,BidNumber="QW123",StartDate = new DateTime(2012 / 10 / 10), EndDate = new DateTime(2013 / 12 / 11),OpeningDate = new DateTime(2012/05/06),StatusID =1};
              }
            
             List<AdminUnit> adminUnitTest=new List<AdminUnit>();
              {
                  new AdminUnit() {AdminUnitID = 1, Name = "Afar", NameAM = null, AdminUnitTypeID = 2, ParentID = 1};
              }
            

            List<BidDetail> bidDetailTest=new List<BidDetail>();
              {
                  new BidDetail()
                      {
                          BidDetailID = 1,
                          BidID = 1,
                          AmountForPSNPProgram = 200,
                          AmountForReliefProgram = 300,
                          BidDocumentPrice = 10,
                          CPO = 1000
                      };
                  new BidDetail()
                  {
                      BidDetailID = 2,
                      BidID = 1,
                      AmountForPSNPProgram = 200,
                      AmountForReliefProgram = 300,
                      BidDocumentPrice = 10,
                      CPO = 1000
                  };
              }


             //Mock the Regional Request Service Using Moq 
            Mock<IBidService> mockBidService = new Mock<IBidService>();
            Mock<IBidDetailService> mockBidDetailService=new Mock<IBidDetailService>();
            Mock<IAdminUnitService> mockAdminUnitService=new Mock<IAdminUnitService>();
            //Mock<IStatusService> mockStatusService=new Mock<IStatusService>();
            //Mock<ITransportBidPlanService> mockTransportBidPlanService=new Mock<ITransportBidPlanService>();
            //Mock<ITransportBidPlanDetailService> mockTransportBidPlanDetailService = new Mock<ITransportBidPlanDetailService>();

            mockBidService.Setup(m => m.GetAllBid()).Returns(bidTest);
            mockAdminUnitService.Setup(m => m.FindBy(au => au.AdminUnitTypeID==2)).Returns(adminUnitTest);
            mockBidDetailService.Setup(m => m.GetAllBidDetail()).Returns(bidDetailTest);
           

            this.MockAdminUnitService = mockAdminUnitService.Object;
            this.MockBidService = mockBidService.Object;
            this.MockBidDetail = mockBidDetailService.Object;

            var userAccountService = new Mock<IUserAccountService>();
            userAccountService.Setup(t => t.GetUserInfo(It.IsAny<string>())).Returns(new UserInfo()
            {
                UserName = "x",
                DatePreference = "en"
            });
            var fakeContext = new Mock<HttpContextBase>();
            var identity = new GenericIdentity("User");
            var principal = new GenericPrincipal(identity, null);
            fakeContext.Setup(t => t.User).Returns(principal);
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.Setup(t => t.HttpContext).Returns(fakeContext.Object);

            var transportBidQuotationService = new Mock<ITransportBidQuotationService>();
            transportBidQuotationService.Setup(m => m.GetAllTransportBidQuotation()).Returns(new List <TransportBidQuotation>
                {
                    new TransportBidQuotation() {BidID = 1,DestinationID = 1,TransportBidQuotationID = 1}
                });

             var bidWinner = new List<BidWinner>
            {
                new BidWinner
                    {
                        BidWinnerID = 1,
                        BidID = 1,
                        DestinationID = 23,
                        SourceID = 4,
                        Tariff = 12,
                        Position = 1,
                        Bid = new Bid
                            {
                                BidID = 1,
                                StartDate = new DateTime(12/12/2004),
                                EndDate = new DateTime(12/12/2005),
                                TransportBidPlanID = 1
                            }


                    }
            };
            var bidWinnerService = new Mock<IBidWinnerService>();
            bidWinnerService.Setup(m => m.GetAllBidWinner()).Returns(bidWinner);

             var transporter = new List<Transporter>()
                                  {
                                      new Transporter()
                                          {
                                                          TransporterID = 1,
                                                          Name = "Bert",
                                                          Region = 4,
                                                          SubCity = "Arada",
                                                          Zone = 1,
                                                          MobileNo = "09123786554",
                                                          Capital = 20000
                                          }
                                  };

            var transporterService = new Mock<ITransporterService>();
            transporterService.Setup(m => m.GetAllTransporter()).Returns(transporter);
            var hub = new List<Hub>
                {
                    new Hub() {HubID = 1, Name = "Adama", HubOwnerID = 1},
                    new Hub() {HubID = 2, Name = "Dire Dawa", HubOwnerID = 1}
                };

             var workFlowStatus = new List<WorkflowStatus>
                {
                     new WorkflowStatus {
                                          Description = "Draft",
                                          StatusID = 1,
                                          WorkflowID = 8
                                        },
                                  new WorkflowStatus
                                      {
                                          Description = "Approved",
                                          StatusID = 2,
                                          WorkflowID = 8
                                      },
                                  new WorkflowStatus
                                      {
                                          Description = "Signed",
                                          StatusID = 3,
                                          WorkflowID = 8
                                      },
                                          new WorkflowStatus
                                      {
                                          Description = "Disqualified",
                                          StatusID = 4,
                                          WorkflowID = 8
                                      }
                };
             var workFlowStatusService = new Mock<IWorkflowStatusService>();

            var hubService = new Mock<IHubService>();
            hubService.Setup(m => m.GetAllHub()).Returns(hub);
            _bidController = new BidController(MockBidService, MockBidDetail, MockAdminUnitService, MockStatusService,
                                            MockTransportBidPlanService,MockTransportBidPlanDetailService,MockApplicationSetting,
                                            userAccountService.Object,transportBidQuotationService.Object,bidWinnerService.Object,
                                            transporterService.Object, hubService.Object, workFlowStatusService.Object);
            _bidController.ControllerContext = controllerContext.Object;

          }
        
        [Test]
        public void Can_fetch_all_Bid_Lists()
        {
             List<Bid> expected = new List<Bid>();
              {
                  new Bid() { BidID=1,BidNumber ="PP452",StartDate=new DateTime(2012/10/10),EndDate=new DateTime(2013/12/11),OpeningDate = new DateTime(2013/02/03),StatusID =1};
                  new Bid() { BidID = 2,BidNumber ="AAA123",StartDate = new DateTime(2012 / 10 / 10), EndDate = new DateTime(2013 / 12 / 11),OpeningDate = new DateTime(2012/12/11),StatusID =2};
              }
           
            List<Bid> actual = MockBidService.GetAllBid();
            Assert.AreEqual(actual.Count,expected.Count);
        }

        [Test]
        public void Bid_Index_Test()
        {
            ActionResult actual = _bidController.Index();
            ViewResult result = actual as ViewResult;
            Assert.IsNotNull(result);
        }


        [Test]
        public void Edit_Bid_Test()
        {
            List<Bid> expected = new List<Bid>();
            {
                new Bid() { BidID = 1, BidNumber = "PP452", StartDate = new DateTime(2012 / 10 / 10), EndDate = new DateTime(2013 / 12 / 11), OpeningDate = new DateTime(2013 / 02 / 03), StatusID = 1 };
                new Bid() { BidID = 2, BidNumber = "AAA123", StartDate = new DateTime(2012 / 10 / 10), EndDate = new DateTime(2013 / 12 / 11), OpeningDate = new DateTime(2012 / 12 / 11), StatusID = 2 };
            }
          
            List<Bid> actual = (List<Bid>) MockBidService.Get(m => m.BidID == 1);
            Assert.AreEqual(actual,expected);

            foreach (var bidExpected in expected)
            {
                Assert.IsTrue(actual.Contains(actual.Find(r => r.BidID == bidExpected.BidID)));
            }
            Assert.AreEqual(expected, actual);

        }
        [Test]
        public void Can_Activate_BID()
        {
            var result = _bidController.MakeActive(1);
            Assert.IsNotNull(result);
        }
        [Test]
        public void Can_Show_List_Of_Active_Bids()
        {
            var dataSourceRequest = new DataSourceRequest();
           
            var result = _bidController.Bid_Read(dataSourceRequest,(int)BidStatus.Active);
          
            Assert.IsNotNull(result);
        }
    }
}