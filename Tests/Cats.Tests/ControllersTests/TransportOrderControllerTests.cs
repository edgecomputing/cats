using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Procurement.Controllers;
using Cats.Areas.Procurement.Models;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Models.Security;
using Cats.Models.ViewModels;
using Cats.Services.EarlyWarning;
using Cats.Services.Logistics;
using Cats.Services.Procurement;
using Cats.Services.Security;
using Kendo.Mvc.UI;
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
                                                StartDate = DateTime.Today,
                                                EndDate = DateTime.Today,
                                                Transporter=new Transporter
                                                                {
                                                                    TransporterID=1,
                                                                     Name="Trans"
                                                                }
                                                
                                            }
                                    };
            var mockTransportOrderService = new Mock<ITransportOrderService>();
            //mockTransportOrderService.Setup(t => t.GetRequisitionToDispatch()).Returns(requisitionsToDispatch);
            mockTransportOrderService.Setup(t => t.GetAllTransportOrder()).Returns(transportOrders);

            var mockTransportRequisitionService = new Mock<ITransportRequisitionService>();
            mockTransportRequisitionService.Setup(t => t.Get(It.IsAny<Expression<Func<TransportRequisition, bool>>>(), null, It.IsAny<string>())).Returns(requisitionsToDispatch);

            var workflowStatusService = new Mock<IWorkflowStatusService>();
            workflowStatusService.Setup(t => t.GetStatusName(It.IsAny<WORKFLOW>(), It.IsAny<int>())).Returns("Approved");


            var logService = new Mock<ILog>();


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

            var TransReqWithoutTransporter = new List<TransReqWithoutTransporter>
                {
                    new TransReqWithoutTransporter {TransReqWithoutTransporterID = 1,TransportRequisitionDetailID = 1,IsAssigned = false},
                    new TransReqWithoutTransporter {TransReqWithoutTransporterID = 2,TransportRequisitionDetailID = 2,IsAssigned = true}
                };

            var transReqWithoutTransporterService = new Mock<ITransReqWithoutTransporterService>();
            
            transReqWithoutTransporterService.Setup(m => m.GetAllTransReqWithoutTransporter()).Returns(
                TransReqWithoutTransporter);
            
            var transporterOrderDetail = new List<TransportOrderDetail>
                {
                    new TransportOrderDetail
                        {
                            TransportOrderDetailID = 1,
                            TransportOrderID = 1,
                            RequisitionID = 1,
                            FdpID = 5,
                            QuantityQtl = 200,
                            TariffPerQtl = 12
                        },
                    new TransportOrderDetail
                        {
                            TransportOrderDetailID = 1,
                            TransportOrderID = 1,
                            RequisitionID = 1,
                            FdpID = 5,
                            QuantityQtl = 200,
                            TariffPerQtl = 12
                        },
                };
            var transporterOrderDetailService = new Mock<ITransportOrderDetailService>();
            transporterOrderDetailService.Setup(m => m.GetAllTransportOrderDetail()).Returns(transporterOrderDetail);

             var adminUnit = new List<AdminUnit>
                {
                    new AdminUnit {AdminUnitID = 1, Name = "Adminunit name", AdminUnitTypeID = 2},
                    new AdminUnit {AdminUnitID = 2, Name = "AdminUnit", AdminUnitTypeID = 2}
                };
            var adminUnitService = new Mock<IAdminUnitService>();
            adminUnitService.Setup(m => m.GetAllAdminUnit()).Returns(adminUnit);

            var transporter = new List<Transporter>
                {
                    new Transporter {TransporterID = 1,Name = "Elete Deration"},
                    new Transporter {TransporterID = 2,Name = "Asemamaw"}
                };

            var transporterService = new Mock<ITransporterService>();
            transporterService.Setup(m => m.GetAllTransporter()).Returns(transporter);

            var transportBidQuotation = new List<TransportBidQuotation>
                {
                    new TransportBidQuotation{TransportBidQuotationID = 1,BidID = 1},
                    new TransportBidQuotation{TransportBidQuotationID = 2,BidID = 2},
                };
            var transportBidQuotationService = new Mock<TransportBidQuotationService>();
            transportBidQuotationService.Setup(m => m.GetAllTransportBidQuotation()).Returns(transportBidQuotation);

            _transportOrderController = new TransportOrderController(mockTransportOrderService.Object, mockTransportRequisitionService.Object,
                                                                     workflowStatusService.Object, logService.Object, userAccountService.Object,
                                                                     transReqWithoutTransporterService.Object, transporterOrderDetailService.Object,
                                                                     adminUnitService.Object, transporterService.Object, transportBidQuotationService.Object);
            //var transporterOrderDetailService = new Mock<ITransportOrderDetailService>();
            //transporterOrderDetailService.Setup(m => m.GetAllTransportOrderDetail()).Returns(transporterOrderDetail);
            //_transportOrderController = new TransportOrderController(mockTransportOrderService.Object, mockTransportRequisitionService.Object,
            //                                                         workflowStatusService.Object, logService.Object, userAccountService.Object,
            //                                                         transReqWithoutTransporterService.Object,transporterOrderDetailService.Object
            //                                                         adminUnitService.Object,transporterService.Object);
            _transportOrderController.ControllerContext = controllerContext.Object;
        }

        [TearDown]
        public void Dispose()
        { _transportOrderController.Dispose();}

        #endregion

        //#region Tests

        //[Test]
        //public void CanDisplayTransportRequisitions()
        //{

        //    //Act
        //    var result = _transportOrderController.TransportRequisitions();

        //    //Assert

        //    Assert.IsInstanceOf<ViewResult>(result);

        //}
        //[Test]
        //public void ShouldGenerateTransportOrderForSelectedTransportRequisition()
        //{
        //    //Act


        //    _transportOrderController.CreateTransportOrder(1);
        //    var request = new Kendo.Mvc.UI.DataSourceRequest();
        //    var result = _transportOrderController.TransportOrder_Read(request);
        //    //Assert
        //    Assert.IsInstanceOf<JsonResult>(result);
        //}

        //[Test]
        //public void CanShowSubstituteTransporters()
        //{
        //    //Act
        //    var request = new Kendo.Mvc.UI.DataSourceRequest();
        //    var result = _transportOrderController.SuggestedSubstituteTransporters(request, 1);
        //    //Assert
        //    Assert.IsInstanceOf<JsonResult>(result);
        //}

        //[Test]
        //public void CanChangeTransportersForTransportOrderContract()
        //{
        //    //Act
        //    var request = new Kendo.Mvc.UI.DataSourceRequest();
        //    var substituteTransporterOrder = new List<SubstituteTransporterOrder>
        //        {
        //            new SubstituteTransporterOrder
        //                {
        //                    WoredaID = 1,
        //                    Woreda = "Woreda 1",
        //                    TransportersStandingList = new List<TransportBidQuotationViewModel>
        //                        {
        //                            new TransportBidQuotationViewModel {TransportBidQuotationID = 1},
        //                            new TransportBidQuotationViewModel {TransportBidQuotationID = 1},
        //                        }
        //                }
        //        };


        //    var result = _transportOrderController.ChangeTransporters(request, substituteTransporterOrder, 1);
        //    //Assert
        //    Assert.IsInstanceOf<JsonResult>(result);
        //}
        //#endregion
        //[Test]
        //public void CanShowTransportContract()
        //{
        //    var result = _transportOrderController.TransportContract(1);
        //    Assert.IsNotNull(result);
        //}
        
    }
}
