using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Procurement.Controllers;
using Cats.Areas.Procurement.Models;
using Cats.Models;
using Cats.Models.Security;
using Cats.Services.Common;
using Cats.Services.EarlyWarning;
using Cats.Services.Procurement;
using Cats.Services.Security;
using Kendo.Mvc.UI;
using Moq;
using NUnit.Framework;

namespace Cats.Tests.ControllersTests
{
    [TestFixture]
    public class BidAdministrationControllerTest
    {
        private BidAdministrationController _bidAdministrationController;
        #region Setup
        [SetUp]
        public void Init()
        {
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

            var applicationSetting = new List<ApplicationSetting>
            {  
                new ApplicationSetting
                {
                   SettingID = 1,
                   SettingName = "CurrentBid",
                  SettingValue = "1"
                }

            };
            var applicationSettingService = new Mock<IApplicationSettingService>();
            applicationSettingService.Setup(m => m.FindBy(b => b.SettingName == "CurrentBid")).Returns(applicationSetting);

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
                                      }
                };
            var workFlowStatusService = new Mock<IWorkflowStatusService>();

            var userAccountService = new Mock<IUserAccountService>();
            userAccountService.Setup(t => t.GetUserInfo(It.IsAny<string>())).Returns(new UserInfo
            {
                UserName = "user",
                DatePreference = "en"
            });

            var bid = new List<Bid>
            {
                new Bid
                    {
                        BidID = 1,
                        StartDate = new DateTime(12/12/2004),
                        EndDate = new DateTime(12/12/2005),
                        TransportBidPlanID = 1
                    },
        new Bid
            {
                        BidID = 2,
                        StartDate = new DateTime(12/12/2004),
                        EndDate = new DateTime(12/12/2005),
                        TransportBidPlanID = 2
                
            }

            };

            var bidService = new Mock<IBidService>();
            bidService.Setup(m => m.FindById(1)).Returns(new Bid
                {
                    BidID = 1,
                    StartDate = new DateTime(12 / 12 / 2004),
                    EndDate = new DateTime(12 / 12 / 2005),
                    TransportBidPlanID = 1
                });

            var transportBidPlan = new TransportBidPlan
            {
               
                        TransportBidPlanID = 1,
                        Year = 2012,
                        YearHalf = 1,
                        ProgramID = 1,
                        Program = new Program { ProgramID = 1, Name = "Relief" },
                        TransportBidPlanDetails = new List<TransportBidPlanDetail>
                            {
                                new TransportBidPlanDetail
                                    {
                                        TransportBidPlanDetailID = 1,
                                        BidPlanID = 1,
                                        BidPlan = new TransportBidPlan {TransportBidPlanID = 1,ProgramID = 1,Year = 2012,YearHalf = 2},
                                        ProgramID = 1,
                                        Program = new Program {ProgramID = 1,Name = "Relief"},
                                        SourceID = 25,
                                        Source = new Hub {HubID = 1,Name = "Adama"},
                                        DestinationID = 2,
                                        Destination = new AdminUnit {AdminUnitID = 1,Name = "Admin Unit Name"},
                                        Quantity = 123
                                    },
                                new TransportBidPlanDetail
                                    {
                                        TransportBidPlanDetailID = 2,
                                        BidPlanID = 1,
                                        BidPlan = new TransportBidPlan {TransportBidPlanID = 2,ProgramID = 2,Year = 2012,YearHalf = 2},
                                        ProgramID = 1,
                                        Program = new Program {ProgramID = 1,Name = "PSNP"},
                                        SourceID = 24,
                                        Source = new Hub {HubID = 2,Name = "Kombolecha"},
                                        DestinationID = 3,
                                        Destination = new AdminUnit {AdminUnitID = 3,Name = "Admin Unit Name"},
                                        Quantity = 100
                                    }
                            }
                    
            };

            var transportPlanService = new Mock<ITransportBidPlanService>();
            transportPlanService.Setup(m => m.FindById(It.IsAny<int>())).Returns(transportBidPlan);

            var fakeContext = new Mock<HttpContextBase>();
            var identity = new GenericIdentity("User");
            var principal = new GenericPrincipal(identity, null);
            fakeContext.Setup(t => t.User).Returns(principal);
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.Setup(t => t.HttpContext).Returns(fakeContext.Object);

            _bidAdministrationController = new BidAdministrationController(bidWinnerService.Object, applicationSettingService.Object, workFlowStatusService.Object,
                                                                         userAccountService.Object, bidService.Object, transportPlanService.Object);
        }


        [TearDown]
        public void Dispose()
        {
            _bidAdministrationController.Dispose();
        }

        #endregion

        [Test]
        public void CanShowBidsWithoutRfq()
        {
            var result = _bidAdministrationController.WithoutRFQ();

            Assert.IsNotNull(result);
        }
        [Test]
        public void CanShowIndexView()
        {
            var result = _bidAdministrationController.Index(1);
            Assert.IsNotNull(result);

            //Assert.IsInstanceOf<ViewResult>(result);
            //Assert.IsInstanceOf<SelectedBidWinnerViewModel>(((ViewResult)result).Model);
        }
        //[Test]
        //public void CanReadBidPlanDetail()
        //{
        //    var request = new DataSourceRequest();

        //    var result = (JsonResult)_bidAdministrationController.BidPlanDetail_Read(request, 1);
        //    Assert.IsNotNull(result);

        //}
       
    }
}
