using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using Cats.Areas.EarlyWarning.Controllers;
using Cats.Data.Repository;
using Cats.Data.UnitWork;
using Cats.Models;
using Cats.Models.ViewModels;
using Cats.Services.EarlyWarning;

using Moq;
using NUnit.Framework;

namespace Cats.Tests.ControllersTests
{
    [TestFixture]
    public class ReliefRequisitionControllerTests
    {
        #region SetUp / TearDown

        private ReliefRequisitionController _reliefRequisitionController;
      //  private ICommodityService _commodityService;
        private List<ReliefRequisitionNew> _input;

        [SetUp]
        public void Init()
        {
            List<ReliefRequisition> reliefRequisitions = new List<ReliefRequisition>()
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
                                                 RequisitionID = 1
                                                 ,
                                                 Status = 1
                                                 ,
                                                 Round = 1
                                                 ,
                                                 ZoneID = 1
                                                 ,
                                                 RequestedBy = 1
                                                 ,RegionalRequestID=1,
                                                 RequestedDate = DateTime.Today
                                                 ,Program=new Program(){ProgramID=1,Name="P1"},
                                                 AdminUnit1=new AdminUnit(){AdminUnitID=1,Name="R1"}
                                                 ,AdminUnit=new AdminUnit{AdminUnitID=2,Name="A2",AdminUnit2 = new AdminUnit{AdminUnitID=3,Name="A3"}}
                                                

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
                                                 RequisitionID = 2
                                                 ,
                                                 Status = 1
                                                 ,
                                                 Round = 2
                                                 ,
                                                 ZoneID = 2
                                                 ,
                                                 RequestedBy = 2,
                                                 RegionalRequestID=1,
                                                 RequestedDate = DateTime.Today
                                                  ,Program=new Program(){ProgramID=1,Name="P1"},
                                                 AdminUnit1=new AdminUnit(){AdminUnitID=1,Name="R1"}
                                                 ,AdminUnit=new AdminUnit{AdminUnitID=2,Name="A2",AdminUnit2 = new AdminUnit{AdminUnitID=3,Name="A3"}}
                                             },
                                     };

           
            var input = (from itm in reliefRequisitions
                         select new ReliefRequisitionNew()
                         {
                             //TODO:Include navigation property for commodity on relife requistion
                             Commodity = itm.CommodityID.ToString(),
                             Program = itm.Program.Name,
                             Region = itm.AdminUnit1.Name,
                             Round = itm.Round,
                             Zone = itm.AdminUnit.AdminUnit2.Name,
                             Status = itm.Status,
                             RequisitionID = itm.RequisitionID,
                             // RequestedBy = itm.UserProfile,
                             // ApprovedBy = itm.ApprovedBy,
                             RequestedDate = itm.RequestedDate,
                             ApprovedDate = itm.ApprovedDate,
                             RegionalRequestId=itm.RegionalRequestID.Value,
                             Input = new ReliefRequisitionNew.ReliefRequisitionNewInput()
                             {
                                 Number = itm.RequisitionID,
                                 RequisitionNo = itm.RequisitionNo
                             }
                         }).ToList();
          
            var mockReliefRequistionService = new Mock<IReliefRequisitionService>();
            mockReliefRequistionService.Setup(t => t.GetAllReliefRequisition()).Returns(reliefRequisitions);
            mockReliefRequistionService.Setup(t => t.Get(It.IsAny<Expression<Func<ReliefRequisition, bool>>>(), null, It.IsAny<string>())).Returns(reliefRequisitions.AsQueryable());
            mockReliefRequistionService.Setup(t => t.CreateRequisition(1)).Returns(input);
            mockReliefRequistionService.Setup(t => t.GetRequisitionByRequestId(It.IsAny<int>())).Returns((int requestId) => input.FindAll(t => t.RegionalRequestId == requestId));

            _reliefRequisitionController = new ReliefRequisitionController(mockReliefRequistionService.Object);

            _input = input;



        }

        [TearDown]
        public void Dispose()
        { }

        #endregion

        #region Tests

        [Test]
        public void Count_Of_Requistion_Should_Be_2()
        {
            //Arange





            //Act
            var view = _reliefRequisitionController.Requistions();


            //Asert
            Assert.IsInstanceOf<ViewResult>(view);
            Assert.AreEqual(((IEnumerable<ReliefRequisition>)view.Model).Count(), 2);



        }

       
        [Test]
        public void Can_Create_Requistion()
        {
           //Arrange
            var view = _reliefRequisitionController.NewRequisiton(1);
           
            Assert.IsInstanceOf<ViewResult>(view);
            Assert.IsInstanceOf<IEnumerable<ReliefRequisitionNew>>(view.Model);

        

        }

        [Test]
        public void Should_Not_Create_Requistion_From_Unknown_Request()
        {
            //Arrange
            var view=_reliefRequisitionController.NewRequisiton(4);

            Assert.IsEmpty((IEnumerable<ReliefRequisitionNew>) view.Model);

          
        }

        [Test]
        public void Can_Create_New_Requistion()
        {            
            var view = _reliefRequisitionController.NewRequisiton(1);

            Assert.AreEqual(((IEnumerable<ReliefRequisitionNew>)view.Model).Count(), 2);
        }

        #endregion
    }
}
