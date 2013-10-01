using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using Cats.Areas.Procurement.Controllers;
using Cats.Areas.Procurement.Models;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Models.ViewModels;
using Cats.Services.EarlyWarning;
using Cats.Services.Logistics;
using Cats.Services.Procurement;
using Moq;
using NUnit.Framework;
using log4net;

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
                                                OrderExpiryDate=DateTime.Today,
                                                Transporter=new Transporter
                                                                {
                                                                    TransporterID=1,
                                                                     Name="Trans"
                                                                }
                                                
                                            }
                                    };
            var mockTransportOrderService = new Mock<ITransportOrderService>();
           // mockTransportOrderService.Setup(t => t.GetRequisitionToDispatch()).Returns(requisitionsToDispatch);
            mockTransportOrderService.Setup(t => t.GetAllTransportOrder()).Returns(transportOrders);

            var mockTransportRequisitionService = new Mock<ITransportRequisitionService>();
            mockTransportRequisitionService.Setup(t => t.Get(It.IsAny<Expression<Func<TransportRequisition, bool>>>(), null, It.IsAny<string>())).Returns(requisitionsToDispatch);

            var workflowStatusService = new Mock<IWorkflowStatusService>();
            workflowStatusService.Setup(t => t.GetStatusName(It.IsAny<WORKFLOW>(), It.IsAny<int>())).Returns("Approved");

            var logService = new Mock<ILog>();
            _transportOrderController = new TransportOrderController(mockTransportOrderService.Object, mockTransportRequisitionService.Object,workflowStatusService.Object,logService.Object);

        }

        [TearDown]
        public void Dispose()
        { _transportOrderController.Dispose();}

        #endregion

        #region Tests

        [Test]
        public void CanDisplayTransportRequisitions()
        {
            //Act
            var result = _transportOrderController.TransportRequisitions();

            //Assert

            Assert.IsInstanceOf<List<TransportRequisitionSelect>>(result.Model);
            Assert.AreEqual(1, ((IEnumerable<TransportRequisitionSelect>)result.Model).Count());
        }
        [Test]
        public void ShouldGenerateTransportOrderForSelectedTransportRequisition()
        {
            //Act
             var requisitions = new List<int>()
                                   {
                                       1
                                   };
          
            _transportOrderController.CreateTransportOrder(requisitions);
            var request =new Kendo.Mvc.UI.DataSourceRequest();
            var result = _transportOrderController.TransportOrder_Read(request);
            //Assert
            Assert.IsInstanceOf<JsonResult>(result);
        }
        #endregion
    }
}
