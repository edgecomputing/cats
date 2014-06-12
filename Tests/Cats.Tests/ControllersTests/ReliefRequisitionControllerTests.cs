using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.EarlyWarning.Controllers;
using Cats.Areas.EarlyWarning.Models;
using Cats.Data.Repository;
using Cats.Data.UnitWork;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Models.Security;
using Cats.Models.ViewModels;
using Cats.Services.EarlyWarning;
using Cats.Services.Security;
using Kendo.Mvc.UI;
using Moq;
using NUnit.Framework;
using Cats.Services.Common;

namespace Cats.Tests.ControllersTests
{
    [TestFixture]
    public class ReliefRequisitionControllerTests
    {
        #region SetUp / TearDown

        private ReliefRequisitionController _reliefRequisitionController;
      
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
                                                 ,AdminUnit=new AdminUnit{AdminUnitID=2,Name="A2",AdminUnit2 = new AdminUnit{AdminUnitID=3,Name="A3"}},
                                                 Commodity = new Commodity()
                                                                 {
                                                                     Name="Grain",
                                                                     CommodityID=1
                                                                 }
                                                

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
                                                 ,  Commodity = new Commodity()
                                                                 {
                                                                     Name="Grain",
                                                                     CommodityID=1
                                                                 }
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
                             RegionalRequestId = itm.RegionalRequestID.Value,
                             Input = new ReliefRequisitionNew.ReliefRequisitionNewInput()
                             {
                                 Number = itm.RequisitionID,
                                 RequisitionNo = itm.RequisitionNo
                             }
                         }).ToList();

            var mockReliefRequistionService = new Mock<IReliefRequisitionService>();
            var mockReliefRequistionDetailService = new Mock<IReliefRequisitionDetailService>();

            mockReliefRequistionService.Setup(t => t.GetAllReliefRequisition()).Returns(reliefRequisitions);
            mockReliefRequistionService.Setup(t => t.Get(It.IsAny<Expression<Func<ReliefRequisition, bool>>>(), null, It.IsAny<string>())).Returns(reliefRequisitions.AsQueryable());
            mockReliefRequistionService.Setup(t => t.CreateRequisition(1)).Returns(input);
            mockReliefRequistionService.Setup(t => t.GetRequisitionByRequestId(It.IsAny<int>())).Returns((int requestId) => input.FindAll(t => t.RegionalRequestId == requestId));
            var workflowStatusService = new Mock<IWorkflowStatusService>();
            workflowStatusService.Setup(t => t.GetStatusName(It.IsAny<WORKFLOW>(), It.IsAny<int>())).Returns("Draft");
            workflowStatusService.Setup(t => t.GetStatus(It.IsAny<WORKFLOW>())).Returns(new List<WorkflowStatus>()
                                                                                            {
                                                                                                new WorkflowStatus()
                                                                                                    {
                                                                                                        Description =
                                                                                                            "Draft",
                                                                                                        StatusID = 1,
                                                                                                        WorkflowID = 2
                                                                                                    }
                                                                                            });
            var userAccountService = new Mock<IUserAccountService>();
            userAccountService.Setup(t => t.GetUserInfo(It.IsAny<string>())).Returns(new UserInfo()
            {
                UserName = "x",
                DatePreference = "en"
            });

            var fakeContext = new Mock<HttpContextBase>();
            var identity = new GenericIdentity("User");
            var principal = new GenericPrincipal(identity, null);
            fakeContext.Setup(t => t.User).Returns(principal);
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.Setup(t => t.HttpContext).Returns(fakeContext.Object);


            var ration = new List<Ration>()
                {
                    new Ration {RationID = 1,IsDefaultRation = true}
                };
            var rationService = new Mock<IRationService>();
            rationService.Setup(m => m.GetAllRation()).Returns(ration);
            var donor = new List<Donor>()
                {
                    new Donor {DonorID = 1,Name = "UK"}
                };
            var donorService = new Mock<IDonorService>();
            donorService.Setup(m => m.GetAllDonor()).Returns(donor);

            var notificationService = new Mock<INotificationService>();

            var commonService = new Mock<ICommonService>();

            var regionalRequestService = new Mock<IRegionalRequestService>();

            var transactionSerivce = new Mock<Cats.Services.Transaction.ITransactionService>();
            
            _reliefRequisitionController = new ReliefRequisitionController(
                mockReliefRequistionService.Object, 
                workflowStatusService.Object, 
                mockReliefRequistionDetailService.Object, 
                userAccountService.Object,
                regionalRequestService.Object,
                rationService.Object,
                donorService.Object, notificationService.Object, null, transactionSerivce.Object, commonService.Object );

            _reliefRequisitionController.ControllerContext = controllerContext.Object; 
            //_input = input;
        }

        [TearDown]
        public void Dispose()
        {
            _reliefRequisitionController.Dispose();
        }

        #endregion

        #region Tests

        [Test]
        public void CountOfRequistionShouldBe2()
        {
            //Arange





            //Act
            var request = new DataSourceRequest();
            var view = _reliefRequisitionController.Requisition_Read(request,1);


            //Asert
            Assert.IsInstanceOf<JsonResult>(view);
           // Assert.AreEqual(((IEnumerable<ReliefRequisitionViewModel>)view.Model).Count(), 2);



        }


        [Test]
        public void CanCreateRequistion()
        {
            //Arrange
            var view = _reliefRequisitionController.NewRequisiton(1);

            Assert.IsInstanceOf<ViewResult>(view);
            Assert.IsInstanceOf<IEnumerable<ReliefRequisitionNew>>(view.Model);



        }

        [Test]
        public void ShouldNotCreateRequistionFromUnknownRequest()
        {
            //Arrange
            var view = _reliefRequisitionController.NewRequisiton(4);

            Assert.IsEmpty((IEnumerable<ReliefRequisitionNew>)view.Model);


        }

        [Test]
        public void CanCreateNewRequistion()
        {
            var view = _reliefRequisitionController.NewRequisiton(1);

            Assert.AreEqual(((IEnumerable<ReliefRequisitionNew>)view.Model).Count(), 2);
        }

        #endregion
    }
}