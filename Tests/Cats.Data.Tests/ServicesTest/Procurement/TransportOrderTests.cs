using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Data.Repository;
using Cats.Data.UnitWork;
using Cats.Models.ViewModels;
using Moq;
using NUnit.Framework;
using Cats.Services.Procurement;
using Cats.Models;
namespace Cats.Data.Tests.ServicesTest.Procurement
{
    [TestFixture]
    public class TransportOrderTests
    {
        #region SetUp / TearDown

        private IList<ReliefRequisition> _reliefRequisitions;
        private IList<TransportOrder> _transportOrders;

        [SetUp]
        public void Init()
        {
            _reliefRequisitions = new List<ReliefRequisition>()
                                      {
                                          new ReliefRequisition()
                                              {
                                                  RegionID = 1,
                                                  ProgramID = 1,
                                                  CommodityID = 1,
                                                  ZoneID = 1,
                                                  RequisitionNo = "REQ-001",
                                                  Round = 1,
                                                  RegionalRequestID = 1,
                                                  RequisitionID = 1,
                                                  Status = 1,
                                                  HubAllocation = new HubAllocation()
                                                                      {
                                                                          HubAllocationID = 1,
                                                                          HubID = 1,
                                                                          RequisitionID=1,
                                                                          
                                                                      },
                                                  ReliefRequisitionDetails = new Collection<ReliefRequisitionDetail>
                                                                                 {
                                                                                     new ReliefRequisitionDetail()
                                                                                         {
                                                                                             RequisitionID = 1,
                                                                                             RequisitionDetailID = 1,
                                                                                             Amount = 100,
                                                                                             CommodityID = 1,
                                                                                             FDPID = 1,
                                                                                             BenficiaryNo = 10,
                                                                                             DonorID = 1
                                                                                         },
                                                                                     new ReliefRequisitionDetail()
                                                                                         {
                                                                                             RequisitionID = 1,
                                                                                             RequisitionDetailID = 2,
                                                                                             Amount = 50,
                                                                                             CommodityID = 1,
                                                                                             FDPID = 2,
                                                                                             BenficiaryNo = 10,
                                                                                             DonorID = 1
                                                                                         },
                                                                                     new ReliefRequisitionDetail()
                                                                                         {
                                                                                             RequisitionID = 1,
                                                                                             RequisitionDetailID = 3,
                                                                                             Amount = 60,
                                                                                             CommodityID = 1,
                                                                                             FDPID = 1,
                                                                                             BenficiaryNo = 10,
                                                                                             DonorID = 1
                                                                                         },
                                                                                     new ReliefRequisitionDetail()
                                                                                         {
                                                                                             RequisitionID = 1,
                                                                                             RequisitionDetailID = 4,
                                                                                             Amount = 70,
                                                                                             CommodityID = 1,
                                                                                             FDPID = 2,
                                                                                             BenficiaryNo = 10,
                                                                                             DonorID = 1
                                                                                         }
                                                                                 }
                                              }
                                      };
            _transportOrders = new List<TransportOrder>();
        }

        [TearDown]
        public void Dispose()
        { }

        #endregion

        #region Tests

        [Test]
        public void Can_Get_All_Requisions_With_Project_Code()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockReliefRequisitionRepository = new Mock<IGenericRepository<ReliefRequisition>>();
            mockReliefRequisitionRepository.Setup(
                t => t.Get(It.IsAny<Expression<Func<ReliefRequisition, bool>>>(), null, It.IsAny<string>())).Returns(
                    _reliefRequisitions.AsQueryable());
            var transportOrderRepository =
                new Mock<IGenericRepository<TransportOrder>>();
            transportOrderRepository.Setup(
                t => t.Get(It.IsAny<Expression<Func<TransportOrder, bool>>>(), null, It.IsAny<string>())).Returns(
                    _transportOrders);
            var transportOrderService = new TransportOrderService(mockUnitOfWork.Object);

            //Act 

            var assignedRequisitons = transportOrderService.GetProjectCodeAssignedRequisitions();


            //Assert

            Assert.IsInstanceOf<IList<ReliefRequisition>>(assignedRequisitons);
            Assert.IsNotEmpty(assignedRequisitons);





        }
        [Test]
        public void Can_Generate_Requisiton_Ready_To_Dispatch()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockReliefRequisitionRepository = new Mock<IGenericRepository<ReliefRequisition>>();
            mockReliefRequisitionRepository.Setup(
                t => t.Get(It.IsAny<Expression<Func<ReliefRequisition, bool>>>(), null, It.IsAny<string>())).Returns(
                    _reliefRequisitions.AsQueryable());
            var transportOrderRepository =
                 new Mock<IGenericRepository<TransportOrder>>();
            transportOrderRepository.Setup(
                t => t.Get(It.IsAny<Expression<Func<TransportOrder, bool>>>(), null, It.IsAny<string>())).Returns(
                    _transportOrders);
            var transportOrderService = new TransportOrderService(mockUnitOfWork.Object);

            //Act
            var requisitionToDispatch = transportOrderService.GetRequisitionToDispatch();

            //Assert

            Assert.IsInstanceOf<IList<RequisitionToDispatch>>(requisitionToDispatch);
        }

        #endregion
    }
}
