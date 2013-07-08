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
        private TransportOrderService _transportOrderService;

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
                                                  ZoneID = 2,
                                                  RequisitionNo = "REQ-001",
                                                  Round = 1,
                                                  RegionalRequestID = 1,
                                                  RequisitionID = 1,
                                                  Status = 1,

                                                  AdminUnit = new AdminUnit
                                                                  {
                                                                      AdminUnitID = 2,
                                                                      Name = "Zone1"
                                                                  },
                                                  AdminUnit1 = new AdminUnit
                                                                   {
                                                                       AdminUnitID = 1,
                                                                       Name = "Region1"
                                                                   },
                                                  Commodity = new Commodity
                                                                  {
                                                                      CommodityID = 1,
                                                                      CommodityCode = "C1",
                                                                      Name = "CSB"
                                                                  },
                                                  HubAllocations = new List<HubAllocation>(){new HubAllocation()
                                                                      {
                                                                          HubAllocationID = 1,
                                                                          HubID = 1,
                                                                          RequisitionID = 1,
                                                                          Hub = new Hub
                                                                                    {
                                                                                        HubId = 1,
                                                                                        Name = "Test Hub",

                                                                                    }

                                                                      }},

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

            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockReliefRequisitionRepository = new Mock<IGenericRepository<ReliefRequisition>>();
            mockReliefRequisitionRepository.Setup(
                t => t.Get(It.IsAny<Expression<Func<ReliefRequisition, bool>>>(), It.IsAny<Func<IQueryable<ReliefRequisition>, IOrderedQueryable<ReliefRequisition>>>(), It.IsAny<string>())).Returns(
                    (Expression<Func<ReliefRequisition, bool>> perdicate, Func<IQueryable<ReliefRequisition>, IOrderedQueryable<ReliefRequisition>> obrderBy, string prop) =>
                    {
                        var
                            result = _reliefRequisitions.AsQueryable();
                        return result;
                    }
                );

            mockUnitOfWork.Setup(t => t.ReliefRequisitionRepository).Returns(mockReliefRequisitionRepository.Object);
            var transportOrderRepository =
                new Mock<IGenericRepository<TransportOrder>>();
            transportOrderRepository.Setup(
                t => t.Get(It.IsAny<Expression<Func<TransportOrder, bool>>>(), null, It.IsAny<string>())).Returns(
                    (Expression<Func<TransportOrder, bool>> perdicate, string prop) =>
                    {
                        var
                            result
                                =
                                _transportOrders
                                    .
                                    AsQueryable
                                    ();
                        return result;
                    }
                );
            _transportOrderService = new TransportOrderService(mockUnitOfWork.Object);

            //Act 
        }

        [TearDown]
        public void Dispose()
        {
            _transportOrderService.Dispose();
        }

        #endregion

        #region Tests

        [Test]
        public void Can_Get_All_Requisions_With_Project_Code()
        {



            //Act 

            var assignedRequisitons = _transportOrderService.GetProjectCodeAssignedRequisitions().ToList();


            //Assert

            Assert.IsInstanceOf<IList<ReliefRequisition>>(assignedRequisitons);
            Assert.IsNotEmpty(assignedRequisitons);





        }
        [Test]
        public void Can_Generate_Requisiton_Ready_To_Dispatch()
        {


            //Act 

            var requisitionToDispatch = _transportOrderService.GetRequisitionToDispatch().ToList();

            //Assert

            Assert.IsInstanceOf<IList<RequisitionToDispatch>>(requisitionToDispatch);
            Assert.AreEqual(1, requisitionToDispatch.Count());
        }

        #endregion
    }
}
