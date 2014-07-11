using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
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
        private ITransportOrderService MockTransportOrderService;
 
        private DispatchLocationsController _dispatchLocationsController;

        [SetUp]
        public void Setup()
        {


            List<TransportOrder> transportOrder = new List<TransportOrder>();
            {
                new TransportOrder()
                {
                    TransportOrderID = 1,
                    TransportOrderNo = "123",
                    ContractNumber = "123",
                    OrderDate = new DateTime(12 / 12 / 2012),
                    RequestedDispatchDate = new DateTime(11 / 11 / 2012),
                    OrderExpiryDate = new DateTime(10 / 10 / 2012),
                    BidDocumentNo = "em/200/2006",
                    PerformanceBondReceiptNo = "123456",
                    TransporterID = 2,
                    ConsignerName = "name",
                    TransporterSignedName = "Signed name",
                    ConsignerDate = new DateTime(02 / 02 / 2013),
                    TransporterSignedDate = new DateTime(03 / 03 / 2012),
                };

            }
            ;
           
            Mock<ITransportOrderService> mockTransportOrderService=new Mock<ITransportOrderService>();

            mockTransportOrderService.Setup(m => m.GetAllTransportOrder()).Returns(transportOrder);
            this.MockTransportOrderService = mockTransportOrderService.Object;
            _dispatchLocationsController=new DispatchLocationsController(MockTransportOrderService);


        }

        [Test]
        public void Can_fetch_all_BidWinner_Lists()
        {
            List<TransportOrder> expected = new List<TransportOrder>();
            {
                new TransportOrder()
                {
                    TransportOrderID = 1,
                    TransportOrderNo = "123",
                    ContractNumber = "123",
                    OrderDate = new DateTime(12/12/2012),
                    RequestedDispatchDate = new DateTime(11/11/2012),
                    OrderExpiryDate=new DateTime(10/10/2012),
                    BidDocumentNo="em/200/2006",
                    PerformanceBondReceiptNo="123456",
                    TransporterID = 1,
                    ConsignerName="name",
                    TransporterSignedName="Signed name",
                    ConsignerDate=new DateTime(02/02/2013),
                    TransporterSignedDate=new DateTime(03/03/2012),
                };
            }
            string transporterName = "";
            var result = _dispatchLocationsController.Index( transporterName);
            Assert.IsNotNull(result);


            var actual = MockTransportOrderService.GetAllTransportOrder();
            Assert.AreEqual(actual.Count, expected.Count);
        }

        [Test]
        public void Can_show_Dispatch_Location_detail()
        {
            var result = _dispatchLocationsController.Details(1);
            Assert.IsNotNull(result);
            //Assert.AreEqual(1, ((IEnumerable<TransportOrder>)result).Count());
        }
        [Test]
        public void Dispatch_Locations_Controller_Constructor_Test()
        {
            try
            {
                _dispatchLocationsController = new DispatchLocationsController(MockTransportOrderService);


            }
            catch (Exception e)
            {

                Assert.Fail(e.Message);
            }
        }


    }
}
