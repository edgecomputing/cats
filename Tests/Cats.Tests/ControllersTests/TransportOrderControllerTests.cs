using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Cats.Areas.Procurement.Controllers;
using Cats.Areas.Procurement.Models;
using Cats.Models;
using Cats.Models.ViewModels;
using Cats.Services.Logistics;
using Cats.Services.Procurement;
using Moq;
using NUnit.Framework;

namespace Cats.Tests.ControllersTests
{
    [TestFixture]
    public class TransportOrderControllerTests
    {
        #region SetUp / TearDown

        private TransportOrderController _transportOrderController;
        [SetUp]
        public void Init()
        {
            var requisitionsToDispatch = new List<TransportRequisition>()
                                             {
                                                 new TransportRequisition()
                                                     {
                                                         CertifiedBy = 1,
                                                         CertifiedDate = DateTime.Today ,
                                                         RequestedBy = 1,
                                                         RequestedDate = DateTime.Today,
                                                         TransportRequisitionID = 1,
                                                         TransportRequisitionNo = "TRN-001",
                                                         Status = 1,
                                                         Remark = "Remark",
                                                         


                                                     }
                                             };
            var transportOrders=new List<TransportOrder>()
                                    {
                                        new TransportOrder()
                                            {
                                                TransporterID=1,
                                                TransportOrderID=1,
                                                TransportOrderNo="TRN-01",
                                                PerformanceBondReceiptNo="PER-001",
                                                BidDocumentNo="BID-001",
                                                ContractNumber="CON-001",
                                                TransporterSignedName="MR x",
                                                TransporterSignedDate=DateTime.Today,
                                                ConsignerName = "Mr y",
                                                ConsignerDate=DateTime.Today,
                                                OrderDate=DateTime.Today,
                                                OrderExpiryDate=DateTime.Today
                                            }
                                    };
            var mockTransportOrderService = new Mock<ITransportOrderService>();
           // mockTransportOrderService.Setup(t => t.GetRequisitionToDispatch()).Returns(requisitionsToDispatch);
            mockTransportOrderService.Setup(t => t.GetAllTransportOrder()).Returns(transportOrders);

            var mockTransportRequisitionService = new Mock<ITransportRequisitionService>();
            mockTransportRequisitionService.Setup(t => t.GetAllTransportRequisition()).Returns(requisitionsToDispatch);
            _transportOrderController = new TransportOrderController(mockTransportOrderService.Object, mockTransportRequisitionService.Object);

        }

        [TearDown]
        public void Dispose()
        { _transportOrderController.Dispose();}

        #endregion

        #region Tests

        [Test]
        public void Can_Display_Transport_Requisitions()
        {
            //Act
            var result = _transportOrderController.TransportRequisitions();

            //Assert

            Assert.IsInstanceOf<List<TransportRequisitionSelect>>(result.Model);
            Assert.AreEqual(1, ((IEnumerable<TransportRequisitionSelect>)result.Model).Count());
        }
        [Test]
        public void Should_Generate_Transport_Order_For_Selected_Transport_Requisition()
        {
            //Act
             var requisitions = new List<int>()
                                   {
                                       1
                                   };
          
            _transportOrderController.CreateTransportOrder(requisitions);
            var result = _transportOrderController.Index();
            //Assert
            Assert.IsInstanceOf<List<TransportOrder>>(result.Model);
        }
        #endregion
    }
}
