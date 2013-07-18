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
using Cats.Services.Logistics;
using Moq;
using NUnit.Framework;

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
                                                                         IsSelected = false
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

                _transportRequisitionController=new TransportRequisitionController(_transportRequisitionService.Object);
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
        [Test]
        public void ShouldConfirmApproval()
        {
            //Act
            var result = _transportRequisitionController.Approve(1);
            
            //Assert

            Assert.IsInstanceOf<TransportRequisition>(((ViewResult)result).Model);
            Assert.AreEqual((int)TransportRequisitionStatus.Draft,((TransportRequisition)(((ViewResult)result).Model)).Status);
        }
        #endregion
    }
}
