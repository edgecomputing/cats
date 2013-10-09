using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Cats.Services.Hub;
using Cats.Models.Hub.ViewModels.Dispatch;
using Cats.Web.Hub.Controllers.Allocations;
using Moq;
using NUnit.Framework;

namespace Cats.Web.Hub.Tests
{
    [TestFixture]
    public class DispatchPlanControllerTest
    {
        #region SetUp / TearDown

        private DispatchPlanController _dispatchPlanController;
        [SetUp]
        public void Init()
        {
            var requisitionSummary = new List<RequisitionSummary>()
                                         {
                                             new RequisitionSummary
                                                 {
                                                     CommodityName = "CSB",
                                                     FdpCount = 20,
                                                     Region = "Afar",
                                                     RequistionNo = "R-001",
                                                     Status = "Open",
                                                     Zone = "Afdera"
                                                 }
                                         };
            var dispatchAllocationService = new Mock<IDispatchAllocationService>();
            dispatchAllocationService.Setup(t => t.GetSummaryForUncommitedAllocations(1)).Returns(requisitionSummary);
            var userProfileService = new Mock<IUserProfileService>();
            this._dispatchPlanController = new DispatchPlanController(dispatchAllocationService.Object,userProfileService.Object);
        }

        [TearDown]
        public void Dispose()
        { _dispatchPlanController.Dispose();}

        #endregion

        #region Tests

        [Test]
        public void CanDisplaySummaryForUncommitedAlloctions()
        {
            //Act
            var result = (ViewResult)_dispatchPlanController.ListRequisitions();

            //Assert
            Assert.IsInstanceOf<List<RequisitionSummary>>(result.Model);
            Assert.AreEqual(1,((List<RequisitionSummary>)result.Model).Count);
        }

        #endregion
    }
}
