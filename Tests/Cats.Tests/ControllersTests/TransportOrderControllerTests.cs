using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Cats.Areas.Procurement.Controllers;
using Cats.Models;
using Cats.Models.ViewModels;
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
            var requisitionsToDispatch = new List<RequisitionToDispatch>()
                                             {
                                                 new RequisitionToDispatch()
                                                     {
                                                         CommodityID = 1,
                                                         CommodityName = "CSB",
                                                         HubID = 1,
                                                         OrignWarehouse = "Nazreth",
                                                         QuanityInQtl = 100,
                                                         RegionID = 1,
                                                         RegionName = "Amhara",
                                                         RequisitionID = 1,
                                                         RequisitionNo = "REQ-001",
                                                         ZoneID = 1,
                                                         Zone = "Bahrdar",
                                                         RequisitionStatus = 3,
                                                         RequisitionStatusName = "HubAssigned"


                                                     }
                                             };
            var mockTransportOrderService = new Mock<ITransportOrderService>();
            mockTransportOrderService.Setup(t => t.GetRequisitionToDispatch()).Returns(requisitionsToDispatch);
            _transportOrderController = new TransportOrderController(mockTransportOrderService.Object);

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

            Assert.IsInstanceOf<List<RequisitionToDispatch>>(result.Model);
            Assert.AreEqual(1, ((IEnumerable<RequisitionToDispatch>)result.Model).Count());
        }
        [Test]
        public void Can_Should_Generate_Transport_Order_For_Selected_Transport_Requisition()
        {
            //Act
            var result1 = _transportOrderController.TransportRequisitions();
             _transportOrderController.CreateTransportOrder((List<RequisitionToDispatch>)result1.Model);
            var result = _transportOrderController.Index();
            //Assert
            Assert.IsInstanceOf<List<TransportOrder>>(result.Model);
        }
        #endregion
    }
}
