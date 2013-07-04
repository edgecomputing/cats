using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using Cats.Areas.EarlyWarning.Controllers;
using Cats.Models;
using Cats.Services.EarlyWarning;
using Moq;
using NUnit.Framework;

namespace Cats.Tests.ControllersTests
{
    [TestFixture]
    public class RequestControllerTests
    {
        #region SetUp / TearDown

        private RequestController _requestController;
        [SetUp]
        public void Init()
        {
            var regionalRequests = new List<RegionalRequest>()
                                       {
                                           new RegionalRequest
                                               {
                                                   ProgramId = 1
                                                   ,
                                                   Round = 1
                                                   ,
                                                   RegionID = 1
                                                   ,
                                                   RegionalRequestID = 1
                                                   ,
                                                   RequistionDate = DateTime.Parse("7/3/2013")
                                                   ,
                                                   Year = DateTime.Today.Year
                                                   ,
                                                   Status=1,
                                                   RegionalRequestDetails = new List<RegionalRequestDetail>
                                                                                {
                                                                                    new RegionalRequestDetail
                                                                                        {
                                                                                            Beneficiaries = 100
                                                                                            ,
                                                                                            CSB = 10
                                                                                            ,
                                                                                            Grain = 20
                                                                                            ,
                                                                                            Oil = 30
                                                                                            ,
                                                                                            Pulse = 40
                                                                                            ,
                                                                                            Fdpid = 1
                                                                                            ,
                                                                                            RegionalRequestID = 1
                                                                                            ,
                                                                                            RegionalRequestDetailID = 1
                                                                                        },
                                                                                    new RegionalRequestDetail
                                                                                        {
                                                                                            Beneficiaries = 100
                                                                                            ,
                                                                                            CSB = 50
                                                                                            ,
                                                                                            Grain = 60
                                                                                            ,
                                                                                            Oil = 70
                                                                                            ,
                                                                                            Pulse = 80
                                                                                            ,
                                                                                            Fdpid = 2
                                                                                            ,
                                                                                            RegionalRequestID = 1
                                                                                            ,
                                                                                            RegionalRequestDetailID = 2
                                                                                        }
                                                                                }
                                               }

                                       };
            var adminUnit = new List<AdminUnit>()
                                {
                                    new AdminUnit
                                        {
                                            Name = "Temp Admin uni",
                                            AdminUnitID = 1
                                        }
                                };
            var mockRegionalRequestService = new Mock<IRegionalRequestService>();
            mockRegionalRequestService.Setup(
                t => t.GetSubmittedRequest(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(
                    (int region, int month, int status) =>
                        {
                         return   regionalRequests.FindAll(
                                t => t.RegionID == region && t.RequistionDate.Month == month && t.Status == status).ToList();
                        });
            mockRegionalRequestService.Setup(t => t.Get(It.IsAny<Expression<Func<RegionalRequest, bool>>>(), null, It.IsAny<string>())).Returns(regionalRequests.AsQueryable());
         
            var mockAdminUnitService = new Mock<IAdminUnitService>();
            mockAdminUnitService.Setup(t => t.FindBy(It.IsAny<Expression<Func<AdminUnit, bool>>>())).Returns(adminUnit);

            _requestController = new RequestController(mockRegionalRequestService.Object, null, mockAdminUnitService.Object, null, null, null);

        }

        [TearDown]
        public void Dispose()
        { }

        #endregion

        #region Tests

      [Test]
        public void Should_List_Submitted_Requests()
      {
          var view = _requestController.SubmittedRequest();

          Assert.AreEqual(1, ((IEnumerable<RegionalRequest>)view.Model).Count());
      }

        #endregion
    }
}
