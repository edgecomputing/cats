using System;
using System.Collections.Generic;
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
        private TransportRequisition _transportRequisition;
            [SetUp]
        public void Init()
            {
                _transportRequisitions = new List<TransportRequisition>();
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
                unitOfWork.Setup(t => t.Save());
                _transportRequisitionService = new TransportRequisitionService(unitOfWork.Object);

                _transportRequisition = new TransportRequisition
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
            var result = _transportRequisitionService.CreateTransportRequisition(_reliefRequisitions);
            //Assert
            Assert.IsInstanceOf<TransportRequisition>(result);
        }

        [Test]
        public void SholdNotCreateTransportRequisitionIfNoReliefRequisition()
        {
            //Act
            var result = _transportRequisitionService.CreateTransportRequisition(new List<int>());
            //Assert
            Assert.IsNull(result);
        }


        [Test]
        public void ShouldAddTransportRequisition()
        {
            //Act
            var count = _transportRequisitions.Count;
            _transportRequisitionService.AddTransportRequisition(_transportRequisition);

            var result = _transportRequisitions.Count;

            Assert.AreEqual(1+count,result);
        }
        #endregion
    }
}
