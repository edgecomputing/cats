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
    public class TransportOrderServiceTests
    {
        #region SetUp / TearDown

        private IList<ReliefRequisition> _reliefRequisitions;
        private IList<TransportOrder> _transportOrders;
        private TransportOrderService _transportOrderService;
        private IList<BidWinner> _transportBidWinners;
        [SetUp]
        public void Init()
        {
            _transportBidWinners = new List<BidWinner>()
                                       {
                                           new BidWinner()
                                               {
                                                   SourceID= 1,
                                                   DestinationID= 1,
                                                   Tariff= 100,
                                                   TransporterID = 1
                                               }
                                       };

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
                                                     Status = 4,
                                                 }
                                         };
            var _tranportRequisitionDetail = new List<TransportRequisitionDetail>()
                                                   {
                                                       new TransportRequisitionDetail
                                                           {
                                                               RequisitionID=1,
                                                               TransportRequisitionID=1
                                                           }
                                                   };
            _transportOrders = new List<TransportOrder>();



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
            mockReliefRequisitionRepository.Setup(t => t.FindById(It.IsAny<int>())).Returns((int id) => _reliefRequisitions
                                                                                                          .ToList().
                                                                                                          Find
                                                                                                          (t =>
                                                                                                           t.
                                                                                                               RequisitionID ==
                                                                                                           id));

            var mockReliefRequisionDetailRepository = new Mock<IGenericRepository<ReliefRequisitionDetail>>();
            //mockReliefRequisionDetailRepository.Setup(
            //     t => t.Get(It.IsAny<Expression<Func<ReliefRequisitionDetail, bool>>>(), It.IsAny<Func<IQueryable<ReliefRequisitionDetail>, IOrderedQueryable<ReliefRequisitionDetail>>>(), It.IsAny<string>())).Returns(

            //             _reliefRequisitions.First().ReliefRequisitionDetails.AsQueryable()

            //         );

            mockUnitOfWork.Setup(t => t.ReliefRequisitionDetailRepository).Returns(
                mockReliefRequisionDetailRepository.Object);

            var mockTransportBidWinnerDetailRepository = new Mock<IGenericRepository<BidWinner>>();
            mockTransportBidWinnerDetailRepository.Setup(t => t.Get(It.IsAny<Expression<Func<BidWinner, bool>>>(), null, It.IsAny<string>())).Returns(_transportBidWinners.AsQueryable());

            mockUnitOfWork.Setup(t => t.BidWinnerRepository).Returns(
                mockTransportBidWinnerDetailRepository.Object);

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

            mockUnitOfWork.Setup(t => t.Save());

            var transportRequisitionDetailRepository = new Mock<IGenericRepository<TransportRequisitionDetail>>();
            transportRequisitionDetailRepository.Setup(t => t.Get(It.IsAny<Expression<Func<TransportRequisitionDetail, bool>>>(), null, It.IsAny<string>())).Returns(_tranportRequisitionDetail);
            mockUnitOfWork.Setup(t => t.TransportRequisitionDetailRepository).Returns(
                transportRequisitionDetailRepository.Object);

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
        public void CanGetAllTransportRequisions()
        {
            var assignedRequisitons = new List<TransportRequisition>()
                                       {
                                           new TransportRequisition()
                                               {
                                                   CertifiedBy= 1,
                                                   CertifiedDate= DateTime.Today,
                                                   Remark= "",
                                                   RequestedBy = 1,
                                                   RequestedDate = DateTime.Today,
                                                   Status = 1,
                                                   TransportRequisitionNo = "009",
                                                   TransportRequisitionID = 1
                                               }
                                       };


            //Act 

            //TODO:Please mock the transportrequisitionrepository and test here


            //Assert

            Assert.IsInstanceOf<List<TransportRequisition>>(assignedRequisitons);
            Assert.IsNotEmpty(assignedRequisitons);





        }


        [Test]

        public void ShouldCreateTransportOrders()
        {

            //Act 

            var requisitions = new List<int>()
                                   {
                                       1
                                   };
            var createdTransportOrders = _transportOrderService.CreateTransportOrder(requisitions);

            //Assert

            Assert.IsInstanceOf<List<TransportOrder>>(createdTransportOrders);
        }
        //[Test]
        //public void Should_Raise_Error_If_NoTransporter()
        //{
        //  //Arrange
        //    _transportBidWinners = new List<TransportBidWinnerDetail>()
        //                               {
        //                                   new TransportBidWinnerDetail()
        //                                       {
        //                                           HubID=2,
        //                                           WoredaID=2,
        //                                           TariffPerQtl=100,
        //                                           TransporterID = 1
        //                                       }
        //                               };


        //    Assert.Throws<Exception>(Create_Transport_Orders);
        //}
        //private void Create_Transport_Orders()
        //{
        //    var requisitionToDispatch = _transportOrderService.GetRequisitionToDispatch().ToList();
        //   _transportOrderService.CreateTransportOrder(requisitionToDispatch);
        //}
        #endregion
    }
}
