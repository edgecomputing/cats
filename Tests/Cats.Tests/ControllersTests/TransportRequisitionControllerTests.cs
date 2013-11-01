using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Cats.Areas.Logistics.Controllers;
using Cats.Areas.Procurement.Models;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Models.ViewModels;
using Cats.Services.EarlyWarning;
using Cats.Services.Logistics;
using Moq;
using NUnit.Framework;
using System.Web;
using System.Security.Principal;
using Cats.Services.Security;
using log4net;

namespace Cats.Tests.ControllersTests
{
    [TestFixture]
    public class TransportRequisitionControllerTests
    {
        #region SetUp / TearDown

      //  private ITransportRequisitionService _transportRequisitionService;
        private TransportRequisitionController _transportRequisitionController;
        private List<RequisitionToDispatchSelect> _requisitionToDispatches;
        private List<RequisitionToDispatch> _requisitionToDispatches1 ;
        [SetUp]
        public void Init()
            {
                _requisitionToDispatches1 = new List<RequisitionToDispatch>
                                               {
                                                   new RequisitionToDispatch
                                                       {
                                                           RegionID = 1,
                                                           RequisitionID = 1,
                                                           HubID = 1,
                                                           RequisitionNo = "REQ-001",
                                                           CommodityID = 1,
                                                           ZoneID = 1,
                                                           QuanityInQtl = 10,
                                                           RequisitionStatus = 1,
                                                           

                                                       }
                                               };
                _requisitionToDispatches = new List<RequisitionToDispatchSelect>
                                               {
                                                   new RequisitionToDispatchSelect
                                                       {
                                                           RegionID = 1,
                                                           RequisitionID = 1,
                                                           HubID = 1,
                                                           RequisitionNo = "REQ-001",
                                                           CommodityID = 1,
                                                           ZoneID = 1,
                                                           QuanityInQtl = 10,
                                                           RequisitionStatus = 1,
                                                           Input=new RequisitionToDispatchSelect.RequisitionToDispatchSelectInput()
                                                                     {
                                                                         Number=1,
                                                                         IsSelected =false
                                                                     }

                                                       }
                                               };
            var _transportRequisition = new TransportRequisition
                                            {
                                                Status = 1,
                                                RequestedDate = DateTime.Today,
                                                RequestedBy = 1,
                                                CertifiedBy = 1,
                                                CertifiedDate = DateTime.Today,
                                                Remark = "",
                                                TransportRequisitionNo = "REQ-001",
                                                TransportRequisitionID = 1,
                                                TransportRequisitionDetails = new List<TransportRequisitionDetail>()
                                                                                  {
                                                                                      new TransportRequisitionDetail
                                                                                          {
                                                                                              RequisitionID = 1,
                                                                                              TransportRequisitionDetailID
                                                                                                  = 1,
                                                                                              TransportRequisitionID = 1
                                                                                          }
                                                                                  }

                                            };
           var _transportRequisitionService = new Mock<ITransportRequisitionService>();
           _transportRequisitionService.Setup(t => t.GetRequisitionToDispatch()).Returns(_requisitionToDispatches1);
            _transportRequisitionService.Setup(t => t.FindById(It.IsAny<int>())).Returns(_transportRequisition);
            var _workflowStatusService = new Mock<IWorkflowStatusService>();
            _workflowStatusService.Setup(t => t.GetStatusName(It.IsAny<WORKFLOW>(), It.IsAny<int>())).Returns("Approved");

            var fakeContext = new Mock<HttpContextBase>();
            var identity = new GenericIdentity("User");
            var principal = new GenericPrincipal(identity, null);
            fakeContext.Setup(t => t.User).Returns(principal);
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.Setup(t => t.HttpContext).Returns(fakeContext.Object);

            var userAccountService = new Mock<IUserAccountService>();
            userAccountService.Setup(t => t.GetUserInfo(It.IsAny<string>())).Returns(new Models.Security.UserInfo() { UserName = "xx", DatePreference = "AM" });
            var logService = new Mock<ILog>();
            var hubAllocationService = new Mock<IHubAllocationService>();
            var projectCodeAllocationService = new Mock<IProjectCodeAllocationService>();
        
            _transportRequisitionController = new TransportRequisitionController(_transportRequisitionService.Object,
                                                                                 _workflowStatusService.Object,
                                                                                  userAccountService.Object,
                                                                                  logService.Object,hubAllocationService.Object,
                                                                                  projectCodeAllocationService.Object);
            _transportRequisitionController.ControllerContext = controllerContext.Object;
        }

        [TearDown]
        public void Dispose()
        {
            _transportRequisitionController.Dispose();
        }

        #endregion

        #region Tests

        [Test]
        public void CanShowListOfRequisitionsWithProjectCodeAndHubAssigned()
        {
            //Act
            var result = _transportRequisitionController.Requisitions();

            //Assert
            Assert.IsInstanceOf<List<RequisitionToDispatchSelect>>(((ViewResult)result).Model);
        }

        [Test]
        public void CandEditTransportRequisition()
        {
            //Act
            var result = _transportRequisitionController.Edit(1);

            //Assert
            Assert.IsInstanceOf<TransportRequisition>(((ViewResult)result).Model);
        }
     /*   [Test]
        public void ShouldConfirmApproval()
        {
            //Act
            var result = _transportRequisitionController.Approve(1);
            
            //Assert

            Assert.IsInstanceOf<TransportRequisition>(((ViewResult)result).Model);
            Assert.AreEqual((int)TransportRequisitionStatus.Draft,((TransportRequisition)(((ViewResult)result).Model)).Status);
        }*/
        #endregion
    }
}
