using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Cats.Data.Repository;
using Cats.Data.UnitWork;
using Cats.Models;
using Cats.Services.Logistics;
using Moq;
using NUnit.Framework;

namespace Cats.Data.Tests.ServicesTest.Logistics
{
    [TestFixture]
    public class TransportRequisitionServiceTests
    {
        #region SetUp / TearDown

        private List<int> _reliefRequisitions;
        private List<TransportRequisition> _transportRequisitions;
        private TransportRequisitionService _transportRequisitionService;
            [SetUp]
        public void Init()
            {
                _reliefRequisitions = new List<int> {1};
                var unitOfWork = new Mock<IUnitOfWork>();
                var transportRequisitionReqository = new Mock< IGenericRepository<TransportRequisition>>();

                transportRequisitionReqository.Setup(t => t.Add(It.IsAny<TransportRequisition>())).Returns(
                    (TransportRequisition transportRequisiton) =>
                        {
                            _transportRequisitions.Add(transportRequisiton);
                            return true;
                        });

                unitOfWork.Setup(t => t.TransportRequisitionRepository).Returns(transportRequisitionReqository.Object);
                _transportRequisitionService = new TransportRequisitionService(unitOfWork.Object);

            }

        [TearDown]
        public void Dispose()
        {
           
        }

        #endregion

        #region Tests

        [Test]
        public void ShouldCreateTransportRequision()
        {
           
            //Act
            var result = _transportRequisitionService.CreateTransportRequisitions(_reliefRequisitions);
            //Assert
            Assert.IsInstanceOf<List<TransportRequisition>>(result);
        }

        [Test]
        public void SholdNotCreateTransportRequisitionIfNoReliefRequisition()
        {
            //Act
            var result = _transportRequisitionService.CreateTransportRequisitions(new List<int>());
            //Assert
            Assert.IsEmpty(result);
        }

        #endregion
    }
}
