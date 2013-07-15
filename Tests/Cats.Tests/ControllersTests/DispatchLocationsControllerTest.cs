using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Areas.Procurement.Controllers;
using Cats.Models;
using Cats.Services.EarlyWarning;
using Cats.Services.Procurement;
using Moq;
using NUnit.Framework;

namespace Cats.Tests.ControllersTests
{
    [TestFixture]
    public class DispatchLocationsControllerTest
    {
        private IBidWinnerService MockBidWinnerService;
        private ITransportOrderService MockTransporterOrderService;
        private DispatchLocationsController _dispatchLocationsController;

        [SetUp]
        public void Setup()
        {
            List<BidWinner> bidWinner = new List<BidWinner>();
            {
                new BidWinner() { BidWinnerID = 1, BidID = 1, SourceID =1,DestinationID = 2,TransporterID = 2,
                                  Amount = 200,Tariff = 55,Position =1,Status =2,ExpiryDate = new DateTime(12/12/2012)};
                new BidWinner()
                {
                    BidWinnerID = 2,
                    BidID = 1,
                    SourceID = 1,
                    DestinationID = 5,
                    TransporterID = 1,
                    Amount = 200,
                    Tariff = 55,
                    Position = 1,
                    Status = 2,
                    ExpiryDate = new DateTime(12 / 11/ 2012)
                };

            }
            ;
            List<AdminUnit> adminUnit = new List<AdminUnit>();
            {
                new AdminUnit() { AdminUnitID = 1, Name = "Afar", NameAM = null, AdminUnitTypeID = 2, ParentID = 1 };
            }
            ;
            Mock<IBidWinnerService> mockBidWinnerService=new Mock<IBidWinnerService>();
            Mock<IAdminUnitService> mockAdminUnitService=new Mock<IAdminUnitService>();

            mockBidWinnerService.Setup(m => m.GetAllBidWinner()).Returns(bidWinner);
            this.MockBidWinnerService = mockBidWinnerService.Object;

            _dispatchLocationsController=new DispatchLocationsController(MockBidWinnerService,MockTransporterOrderService);
        }

        [Test]
        public void Can_fetch_all_BidWinner_Lists()
        {
            // List<BidWinner> expected = new List<BidWinner>();
            //{
            //    new BidWinner() { BidWinnerID = 1, BidID = 1, SourceID =1,DestinationID = 2,TransporterID = 2,
            //                      Amount = 200,Tariff = 55,Position =1,Status =2,ExpiryDate = new DateTime(12/12/2012)};
            //}
            //;
            //var transporter = "transporter";
            //var result = _dispatchLocationsController.Index(transporter);
            //Assert.IsNotNull(result);

            //var actual = MockBidWinnerService.GetAllBidWinner();
            //Assert.AreEqual(actual.Count, expected.Count);
            
        }

        [Test]
        public void Can_show_winners_detail()
        {
            //var result = _dispatchLocationsController.Details(1);
            //Assert.AreEqual(1, ((IEnumerable<BidWinner>)result).Count());
        }
        [Test]
        public void Dispatch_Locations_Controller_Constructor_Test()
        {
            try
            {
                _dispatchLocationsController = new DispatchLocationsController(MockBidWinnerService, MockTransporterOrderService);
            }
            catch (Exception e)
            {

                Assert.Fail(e.Message);
            }
        }


    }
}
