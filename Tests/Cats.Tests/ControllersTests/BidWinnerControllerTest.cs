using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Procurement.Controllers;
using Cats.Models;
using Cats.Models.Security;
using Cats.Services.EarlyWarning;
using Cats.Services.Procurement;
using Cats.Services.Security;
using Kendo.Mvc.UI;
using Moq;
using NUnit.Framework;

namespace Cats.Tests.ControllersTests
{
    [TestFixture]
   public class BidWinnerControllerTest
    {
        private BidWinnerController _bidWinnerController;
        #region Setup
        [SetUp]
        public void init()
        {
            var bidWinner = new List<BidWinner>
                {
                    new BidWinner
                        {
                            BidWinnerID = 1,
                            BidID = 2,
                            Bid = new Bid
                                {
                                    BidID=1,
                                    BidNumber ="PP452",
                                    StartDate=new DateTime(2012/10/10),
                                    EndDate=new DateTime(2013/12/11),
                                    OpeningDate = new DateTime(2013/12/12),
                                    StatusID =1 
                                },
                            Status = 1,
                            SourceID = 2,
                            DestinationID = 55,
                            Amount = 123,
                            Tariff = 12,
                            CommodityID = 1,
                            Position = 1,
                        }

                };

            var bidWinnerService = new Mock<IBidWinnerService>();
            bidWinnerService.Setup(m => m.GetAllBidWinner()).Returns(bidWinner);

            bidWinnerService.Setup(m => m.GetBidsWithWinner()).Returns(new List<Bid>()
                {
                    new Bid()
                        {
                            BidID = 1,
                            BidNumber = "PP452",
                            StartDate = new DateTime(2012/10/10),
                            EndDate = new DateTime(2013/12/11),
                            OpeningDate = new DateTime(2013/12/12),
                            StatusID = 1
                        }
                });
                                   

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
           _bidWinnerController=new BidWinnerController(bidWinnerService.Object,userAccountService.Object,workFlowStatusService.Object);
           _bidWinnerController.ControllerContext = controllerContext.Object; 
        }
        [TearDown]
        public void Dispose()
        {
            _bidWinnerController.Dispose();
        }
        #endregion
        #region Tests
        [Test]
        public void CanShowListofBids()
        {
            var result = _bidWinnerController.Index();
            Assert.IsNotNull(result);
        }
        [Test]
        public void CanReadBid()
        {
            var request = new DataSourceRequest();
            var result = (JsonResult)_bidWinnerController.Bid_Read(request);
            Assert.IsNotNull(result);

        }
        [Test]
        public void CanDisqualifyWinner()
        {
            var result = _bidWinnerController.DisqualifiedWinner(1);
            //Assert.IsInstanceOf<BidWinner>(result.Model);
            Assert.IsNotNull(result);
            
        }
      [Test]
        public void CanEditWinner()
      {
          var result = _bidWinnerController.Edit(1);
          //Assert.IsInstanceOf<BidWinner>(result.Model);
          Assert.IsNotNull(result);

      }
          #endregion

    }
}
