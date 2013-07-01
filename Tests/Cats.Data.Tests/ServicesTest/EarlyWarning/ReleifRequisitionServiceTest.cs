using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions.Types;
using Cats.Data.Repository;
using Cats.Data.UnitWork;
using Cats.Services.EarlyWarning;
using NUnit.Framework;
using Cats.Models;
using Moq;

namespace Cats.Data.Tests.ServicesTest.EarlyWarning
{
    [TestFixture]
    public class ReleifRequisitionServiceTest
    {
        #region SetUp / TearDown

        [SetUp]
        public void Init()
        { }

        [TearDown]
        public void Dispose()
        { }

        #endregion

        #region Tests

        [Test]
        public void Test()
        {
        }

        #endregion
        [Test]
        public void Can_Get_List_Of_Requisition()
        {
            //Arrange
            var requisitonList = new List<ReliefRequisition>()
                                     {
                                         new ReliefRequisition()
                                             {
                                                 RequisitionNo = "REQ001"
                                                 ,
                                                 ApprovedBy = 1
                                                 ,
                                                 ApprovedDate = DateTime.Today
                                                 ,
                                                 CommodityID = 1
                                                 ,
                                                 ProgramID = 1
                                                 ,
                                                 RegionID = 1
                                                 ,
                                                 ReliefRequisitionID = 1
                                                 ,
                                                 Status = 1
                                                 ,
                                                 Round = 1
                                                 ,
                                                 ZoneID = 1
                                                 ,
                                                 RequestedBy = 1
                                                 ,
                                                 RequisitionDate = DateTime.Today

                                             },
                                         new ReliefRequisition()
                                             {
                                                 RequisitionNo = "REQ001"
                                                 ,
                                                 ApprovedBy = 2
                                                 ,
                                                 ApprovedDate = DateTime.Today
                                                 ,
                                                 CommodityID = 2
                                                 ,
                                                 ProgramID = 2
                                                 ,
                                                 RegionID = 2
                                                 ,
                                                 ReliefRequisitionID = 2
                                                 ,
                                                 Status = 1
                                                 ,
                                                 Round = 2
                                                 ,
                                                 ZoneID = 2
                                                 ,
                                                 RequestedBy = 2
                                                 ,
                                                 RequisitionDate = DateTime.Today

                                             },
                                     };
            var mockReleifRepositroy = new Mock<IGenericRepository<ReliefRequisition>>();
            //Mock GetAll requistions
            mockReleifRepositroy.Setup(t => t.GetAll()).Returns(requisitonList);

            //Here we are going to mock our IUnitOfWork
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();


            //Here we are going to inject our repository to the property 
            //mock.SetupProperty(m => m.ProductRepository).SetReturnsDefault(mock1.Object);
            mockUnitOfWork.Setup(m => m.ReliefRequisitionRepository).Returns(mockReleifRepositroy.Object);

            //Now our UnitOfWork is ready to be injected to the service
            //Here we inject UnitOfWork to constractor of our service
            var relifRequistionService = new ReliefRequisitionService(mockUnitOfWork.Object);


            //Act
            var expectedRequistionList = relifRequistionService.GetAllReliefRequisition();


            //Assert

            Assert.AreEqual(expectedRequistionList.Count, requisitonList.Count );


        }

        [Test]
        public void Can_Find_Requisition_By_Id_()
        {
            //Arrange
            var requisitonList = new List<ReliefRequisition>()
                                     {
                                         new ReliefRequisition()
                                             {
                                                 RequisitionNo = "REQ001"
                                                 ,
                                                 ApprovedBy = 1
                                                 ,
                                                 ApprovedDate = DateTime.Today
                                                 ,
                                                 CommodityID = 1
                                                 ,
                                                 ProgramID = 1
                                                 ,
                                                 RegionID = 1
                                                 ,
                                                 ReliefRequisitionID = 1
                                                 ,
                                                 Status = 1
                                                 ,
                                                 Round = 1
                                                 ,
                                                 ZoneID = 1
                                                 ,
                                                 RequestedBy = 1
                                                 ,
                                                 RequisitionDate = DateTime.Today

                                             },
                                         new ReliefRequisition()
                                             {
                                                 RequisitionNo = "REQ001"
                                                 ,
                                                 ApprovedBy = 2
                                                 ,
                                                 ApprovedDate = DateTime.Today
                                                 ,
                                                 CommodityID = 2
                                                 ,
                                                 ProgramID = 2
                                                 ,
                                                 RegionID = 2
                                                 ,
                                                 ReliefRequisitionID = 2
                                                 ,
                                                 Status = 1
                                                 ,
                                                 Round = 2
                                                 ,
                                                 ZoneID = 2
                                                 ,
                                                 RequestedBy = 2
                                                 ,
                                                 RequisitionDate = DateTime.Today

                                             },
                                     };
            var mockReleifRepositroy = new Mock<IGenericRepository<ReliefRequisition>>();
            //Mock GetAll requistions
            mockReleifRepositroy.Setup(t => t.FindById(It.IsAny<int>())).Returns((int id) => requisitonList.
                                                                                                 FirstOrDefault(
                                                                                                     t =>
                                                                                                     t.
                                                                                                         ReliefRequisitionID ==
                                                                                                     id));

            //Here we are going to mock our IUnitOfWork
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();


            //Here we are going to inject our repository to the property 
            //mock.SetupProperty(m => m.ProductRepository).SetReturnsDefault(mock1.Object);
            mockUnitOfWork.Setup(m => m.ReliefRequisitionRepository).Returns(mockReleifRepositroy.Object);

            //Now our UnitOfWork is ready to be injected to the service
            //Here we inject UnitOfWork to constractor of our service
            var relifRequistionService = new ReliefRequisitionService(mockUnitOfWork.Object);


            //Act
            var expectedRequistion = relifRequistionService.FindById(1);
            var actualRequistion = requisitonList.Find(t => t.ReliefRequisitionID == 1);


            //Assert

            Assert.AreEqual(expectedRequistion.ReliefRequisitionID, actualRequistion.ReliefRequisitionID);
            Assert.AreSame(expectedRequistion,actualRequistion);


        }
    }
}
