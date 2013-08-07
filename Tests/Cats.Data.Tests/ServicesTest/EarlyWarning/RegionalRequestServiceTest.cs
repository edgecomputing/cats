using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using Cats.Data.Repository;
using Cats.Data.UnitWork;
using Cats.Models;
using Cats.Services.EarlyWarning;
using Moq;
using NUnit.Framework;

namespace Cats.Data.Tests.ServicesTest.EarlyWarning
{
    [TestFixture]
    public class RegionalRequestServiceTest
    {
        #region SetUp / TearDown

        private IRegionalRequestService _regionalRequestService;
        [SetUp]
        public void Init()
        {
            var requestDetails = new List<RegionalRequestDetail>()
                                     {
                                         new RegionalRequestDetail()
                                             {
                                                 Beneficiaries = 10,
                                                 RegionalRequestID = 1,
                                                 Fdpid = 1,
                                                 RegionalRequestDetailID = 1,
                                                 Fdp =
                                                     new FDP
                                                         {
                                                             AdminUnitID = 1,
                                                             Name = "Fdp 1",
                                                             AdminUnit =
                                                                 new AdminUnit
                                                                     {Name = "Woreda 1", AdminUnitID = 2, ParentID = 4}
                                                         }
                                             },
                                              new RegionalRequestDetail()
                                             {
                                                 Beneficiaries = 10,
                                                 RegionalRequestID = 1,
                                                 Fdpid = 2,
                                                 RegionalRequestDetailID = 1,
                                                 Fdp =
                                                     new FDP
                                                         {
                                                             AdminUnitID = 1,
                                                             Name = "Fdp 1",
                                                             AdminUnit =
                                                                 new AdminUnit
                                                                     {Name = "Woreda 1", AdminUnitID = 2, ParentID = 4}
                                                         }
                                             }
                                     };
            var regionalRequestRepository = new Mock<IGenericRepository<RegionalRequest>>();
            var regionalRequestDetailRepository = new Mock<IGenericRepository<RegionalRequestDetail>>();

            regionalRequestDetailRepository.Setup(
                t =>
                t.Get(It.IsAny<Expression<Func<RegionalRequestDetail, bool>>>(),
                      It.IsAny<Func<IQueryable<RegionalRequestDetail>, IOrderedQueryable<RegionalRequestDetail>>>(),
                      It.IsAny<string>())).Returns(requestDetails);


            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(t => t.RegionalRequestRepository).Returns(regionalRequestRepository.Object);
            unitOfWork.Setup(t => t.RegionalRequestDetailRepository).Returns(regionalRequestDetailRepository.Object);
            this._regionalRequestService = new RegionalRequestService(unitOfWork.Object);
        }

        [TearDown]
        public void Dispose()
        { }

        #endregion

        #region Tests

        [Test]
        public void CanGetZonesFoodRequested()
        {
            //ACT
            var result = _regionalRequestService.GetZonesFoodRequested(1);
            //Assert

            Assert.IsInstanceOf<List<int?>>(result);
            Assert.AreEqual(1,result.Count);
        }

        #endregion
    }
}
