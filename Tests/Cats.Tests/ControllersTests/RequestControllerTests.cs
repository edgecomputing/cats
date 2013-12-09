using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.EarlyWarning.Controllers;
using Cats.Areas.EarlyWarning.Models;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Models.Security;
using Cats.Services.Common;
using Cats.Services.EarlyWarning;
using Cats.Services.PSNP;
using Cats.Services.Security;
using Kendo.Mvc.UI;
using Moq;
using NUnit.Framework;
using log4net;

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
            var requestDetailCommodity = new List<RequestDetailCommodity>()
                                             {
                                                 new RequestDetailCommodity
                                                     {
                                                         CommodityID = 1,
                                                         Amount = 20,
                                                         RequestCommodityID = 1,
                                                         RegionalRequestDetailID = 1,
                                                         Commodity=new Commodity(){CommodityID=1,Name="CSB"}
                                                         
                                                     },
                                                
                                             };
            var regionalRequests = new List<RegionalRequest>()
                                       {
                                           new RegionalRequest
                                               {
                                                   ProgramId = 1
                                                   ,
                                                   Month = 1
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
                                                   Program = new Program(){
                                                   Name="Program1",
                                                   ProgramID = 1
                                                   
                                                   },
                                                   AdminUnit=new AdminUnit
                                                                 {
                                                                     Name="Region",
                                                                     AdminUnitID=1
                                                                 },
                                                                 Ration=new Ration()
                                                                            {
                                                                                RationID=1,
                                                                                RefrenceNumber="RE1",
                                                                                
                                                                            },
                                                   RegionalRequestDetails = new List<RegionalRequestDetail>
                                                                                {
                                                                                    new RegionalRequestDetail
                                                                                        {
                                                                                            Beneficiaries = 100
                                                                                            ,
                                                                                            Fdpid = 1,
                                                                                            Fdp = new FDP{FDPID=1,Name="FDP1",AdminUnit=new AdminUnit{AdminUnitID=1,Name="Admin1",AdminUnit2=new AdminUnit{AdminUnitID=2,Name="Admin2"}}}
                                                                                            ,
                                                                                            RegionalRequestID = 1
                                                                                            ,
                                                                                            RegionalRequestDetailID = 1,
                                                                                            RequestDetailCommodities=requestDetailCommodity
                                                                                        },
                                                                                    new RegionalRequestDetail
                                                                                        {
                                                                                            Beneficiaries = 100
                                                                                            ,
                                                                                            Fdpid = 2,
                                                                                            Fdp = new FDP{FDPID=2,Name="FDP1",AdminUnit=new AdminUnit{AdminUnitID=1,Name="Admin1",AdminUnit2=new AdminUnit{AdminUnitID=2,Name="Admin2"}}}
                                                                                            ,
                                                                                            RegionalRequestID = 1
                                                                                            ,
                                                                                            RegionalRequestDetailID = 2,
                                                                                             RequestDetailCommodities=requestDetailCommodity
                                                                                        }
                                                                                }
                                               }

                                       };

         //   regionalRequests[0].RegionalRequestDetails.First().RequestDetailCommodities = requestDetailCommodity;
            var adminUnit = new List<AdminUnit>()
                                {
                                    new AdminUnit
                                        {
                                            Name = "Temp Admin uni",
                                            AdminUnitID = 1
                                        }
                                };
            var plan = new List<Plan>
                {
                    new Plan {PlanID = 1,PlanName = "Plan1",ProgramID = 1,StartDate = new DateTime(12/12/12),EndDate =new DateTime(12/12/12) }
                };
               var _status = new List<Cats.Models.WorkflowStatus>()
                              {
                                  new WorkflowStatus()
                                      {
                                          Description = "Draft",
                                          StatusID = 1,
                                          WorkflowID = 1
                                      },
                                  new WorkflowStatus()
                                      {
                                          Description = "Approved",
                                          StatusID = 2,
                                          WorkflowID = 1
                                     },
                                  new WorkflowStatus()
                                      {
                                          Description = "Closed",
                                          StatusID = 3,
                                          WorkflowID = 1
                                      }
                              };
            var commonService = new Mock<ICommonService>();
            commonService.Setup(t => t.GetAminUnits(It.IsAny<Expression<Func<AdminUnit, bool>>>(),
                      It.IsAny<Func<IQueryable<AdminUnit>, IOrderedQueryable<AdminUnit>>>(),
                      It.IsAny<string>())).Returns(adminUnit);
            commonService.Setup(t => t.GetStatus(It.IsAny<WORKFLOW>())).Returns(_status);
            commonService.Setup(t => t.GetPlan(plan.First().ProgramID)).Returns(plan);
            var mockRegionalRequestService = new Mock<IRegionalRequestService>();
            mockRegionalRequestService.Setup(
                t => t.GetSubmittedRequest(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(
                    (int region, int month, int status) =>
                    {
                        return regionalRequests.FindAll(
                               t => t.RegionID == region && t.RequistionDate.Month == month && t.Status == status).ToList();
                    });
            mockRegionalRequestService.Setup(
                t =>
                t.Get(It.IsAny<Expression<Func<RegionalRequest, bool>>>(),
                      It.IsAny<Func<IQueryable<RegionalRequest>, IOrderedQueryable<RegionalRequest>>>(),
                      It.IsAny<string>())).Returns(
                          (Expression<Func<RegionalRequest, bool>> filter,
                           Func<IQueryable<RegionalRequest>, IOrderedQueryable<RegionalRequest>> sort, string includes)
                          =>
                          {
                              return regionalRequests.AsQueryable().Where(filter);
                              ;
                          });
            mockRegionalRequestService.Setup(t => t.FindById(It.IsAny<int>())).Returns(
                (int requestId) => regionalRequests.Find(t => t.RegionalRequestID == requestId));
            mockRegionalRequestService.Setup(t => t.ApproveRequest(It.IsAny<int>())).Returns((int reqId) =>
                                                                                                 {
                                                                                                     regionalRequests.
                                                                                                         Find
                                                                                                         (t =>
                                                                                                          t.
                                                                                                              RegionalRequestID
                                                                                                          == reqId).
                                                                                                         Status
                                                                                                         =
                                                                                                         (int)
                                                                                                         RegionalRequestStatus
                                                                                                             .Approved;
                                                                                                     return true;
                                                                                                 });
            mockRegionalRequestService.Setup(t => t.AddRegionalRequest(It.IsAny<RegionalRequest>())).Returns(
                (RegionalRequest rRequest) =>
                {
                    regionalRequests.Add(rRequest);
                    return true;
                });
            mockRegionalRequestService.Setup(t => t.GetAllRegionalRequest()).Returns(regionalRequests);
            mockRegionalRequestService.Setup(t => t.PlanToRequest(It.IsAny<HRDPSNPPlan>())).Returns(new HRDPSNPPlanInfo()
                                                                                                        {
                                                                                                            BeneficiaryInfos
                                                                                                                =
                                                                                                                new List
                                                                                                                <
                                                                                                                BeneficiaryInfo
                                                                                                                >()
                                                                                                                    {
                                                                                                                        new BeneficiaryInfo
                                                                                                                            ()
                                                                                                                            {
                                                                                                                                Beneficiaries
                                                                                                                                    =
                                                                                                                                    1,
                                                                                                                                FDPID
                                                                                                                                    =
                                                                                                                                    1,
                                                                                                                                FDPName
                                                                                                                                    =
                                                                                                                                    "F1",
                                                                                                                                Selected
                                                                                                                                    =
                                                                                                                                    true
                                                                                                                            }
                                                                                                                    }
                                                                                                            ,
                                                                                                            HRDPSNPPlan
                                                                                                                =
                                                                                                                new HRDPSNPPlan
                                                                                                                    {
                                                                                                                        DonorID
                                                                                                                            =
                                                                                                                            1,
                                                                                                                        Month
                                                                                                                            =
                                                                                                                            1,
                                                                                                                        PlanID
                                                                                                                            =
                                                                                                                            1,
                                                                                                                        ProgramID
                                                                                                                            =
                                                                                                                            1,
                                                                                                                        PSNPPlanID
                                                                                                                            =
                                                                                                                            1,
                                                                                                                        RationID
                                                                                                                            =
                                                                                                                            1,
                                                                                                                        RegionID
                                                                                                                            =
                                                                                                                            1,
                                                                                                                        Round
                                                                                                                            =
                                                                                                                            1,
                                                                                                                        SeasonID
                                                                                                                            =
                                                                                                                            1,
                                                                                                                        Year
                                                                                                                            =
                                                                                                                            1
                                                                                                                    }
                                                                                                        });
            var mockAdminUnitService = new Mock<IAdminUnitService>();
            mockAdminUnitService.Setup(t => t.FindBy(It.IsAny<Expression<Func<AdminUnit, bool>>>())).Returns(adminUnit);

            mockAdminUnitService.Setup(t => t.GetRegions()).Returns(adminUnit);

            var workflowService = new Mock<IWorkflowStatusService>();
         
            workflowService.Setup(t => t.GetStatus(It.IsAny<Cats.Models.Constant.WORKFLOW>())).Returns(_status);
            workflowService.Setup(t => t.GetStatusName(It.IsAny<Cats.Models.Constant.WORKFLOW>(), It.IsAny<int>())).
                Returns((Cats.Models.Constant.WORKFLOW workflow, int statusId) =>
                            {
                                return _status.Find(t => t.StatusID == statusId && t.WorkflowID == (int)workflow).Description;
                            });

            commonService.Setup(t => t.GetPrograms(It.IsAny<Expression<Func<Program, bool>>>(),
                      It.IsAny<Func<IQueryable<Program>, IOrderedQueryable<Program>>>(),
                      It.IsAny<string>())).Returns(new List<Program>()
                                                                     {
                                                                         new Program()
                                                                             {ProgramID = 1, Description = "Relief"}
                                                                     });
            
            commonService.Setup(t => t.GetRations(It.IsAny<Expression<Func<Ration, bool>>>(),
                      It.IsAny<Func<IQueryable<Ration>, IOrderedQueryable<Ration>>>(),
                      It.IsAny<string>())).Returns(new List<Ration>()
                                                                   {
                                                                       new Ration
                                                                           {RationID = 1, RefrenceNumber = "R-00983"}
                                                                   });

            var fdpService = new Mock<IFDPService>();
            fdpService.Setup(t => t.FindBy(It.IsAny<Expression<Func<FDP, bool>>>())).Returns(new List<FDP>()
                                                                           {
                                                                               new FDP()
                                                                                   {
                                                                                       FDPID = 1,
                                                                                       Name = "FDP1",
                                                                                       AdminUnitID = 1
                                                                                   }
                                                                           });
            var requestDetailService = new Mock<IRegionalRequestDetailService>();
            requestDetailService.Setup(t => t.Get(It.IsAny<Expression<Func<RegionalRequestDetail, bool>>>(), null, It.IsAny<string>())).Returns(regionalRequests.First().RegionalRequestDetails);

          
            commonService.Setup(t => t.GetCommodities(It.IsAny<Expression<Func<Commodity, bool>>>(),
                      It.IsAny<Func<IQueryable<Commodity>, IOrderedQueryable<Commodity>>>(),
                      It.IsAny<string>())).Returns(new List<Commodity>() { new Commodity { CommodityID = 1, Name = "CSB" } });
          
            var hrds = new List<HRD>
                          {
                              new HRD()
                                  {
                                      Year = 2013,
                                      Season = new Season() {Name = "Mehere", SeasonID = 1},
                                      RationID = 1,
                                      HRDDetails =
                                          new List<HRDDetail>()
                                              {
                                                  new HRDDetail()
                                                      {
                                                          DurationOfAssistance = 2,
                                                          HRDDetailID = 1,
                                                          NumberOfBeneficiaries = 300,
                                                          WoredaID = 1,
                                                          AdminUnit =
                                                              new AdminUnit()
                                                                  {
                                                                      Name = "Woreda",
                                                                      AdminUnitID = 2,
                                                                      AdminUnit2 =
                                                                          new AdminUnit()
                                                                              {
                                                                                  Name = "Zone",
                                                                                  AdminUnitID = 3,
                                                                                  AdminUnit2 =
                                                                                      new AdminUnit()
                                                                                          {
                                                                                              Name = "Region",
                                                                                              AdminUnitID = 6
                                                                                          }
                                                                              }
                                                                  }
                                                      }
                                              }
                                  }
                          };
            var hrdService = new Mock<IHRDService>();
            var appService = new Mock<IApplicationSettingService>();
            hrdService.Setup(
                t =>
                t.Get(It.IsAny<Expression<Func<HRD, bool>>>(), It.IsAny<Func<IQueryable<HRD>, IOrderedQueryable<HRD>>>(),
                      It.IsAny<string>())).Returns(hrds);
            var userAccountService = new Mock<IUserAccountService>();
            userAccountService.Setup(t => t.GetUserInfo(It.IsAny<string>())).Returns(new UserInfo()
                                                                                         {
                                                                                             UserName = "x",
                                                                                             DatePreference = "en"
                                                                                         });
            var fakeContext = new Mock<HttpContextBase>();
            var identity = new GenericIdentity("User");
            var principal = new GenericPrincipal(identity,null);
            fakeContext.Setup(t => t.User).Returns(principal);
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.Setup(t => t.HttpContext).Returns(fakeContext.Object);
            var log = new Mock<ILog>();
            log.Setup(t => t.Error(It.IsAny<object>()));


            var hrdServiceDetail = new Mock<IHRDDetailService>();
            var RegionalPSNPPlanDetailService = new Mock<IRegionalPSNPPlanDetailService>();
            var RegionalPSNPPlanService = new Mock<IRegionalPSNPPlanService>();

            _requestController = new RequestController(
                mockRegionalRequestService.Object, 
                fdpService.Object, requestDetailService.Object,
                commonService.Object, hrdService.Object,
                appService.Object, userAccountService.Object,
                log.Object, hrdServiceDetail.Object, 
                RegionalPSNPPlanDetailService.Object,
                RegionalPSNPPlanService.Object);
               _requestController.ControllerContext = controllerContext.Object; 
         
     
        }

        [TearDown]
        public void Dispose()
        {
            _requestController.Dispose();
        }

        #endregion

        #region Tests

        [Test]
        public void ShouldListOnlySubmittedRequests()
        {
            var view = _requestController.SubmittedRequest((int)RegionalRequestStatus.Approved);

            Assert.AreEqual(0, ((IEnumerable<RegionalRequestViewModel>)view.Model).Count());
        }

        [Test]
        public void CanApproveDraftRequest()
        {
            //Act
            _requestController.ApproveRequest(1);
            // var reqStatus = regionalRequests[0].Status;
            var resut = (ViewResult)_requestController.Edit(1);

            //Assert

            Assert.IsInstanceOf<RegionalRequest>(resut.Model);
            Assert.AreEqual((int)RegionalRequestStatus.Approved, ((RegionalRequest)resut.Model).Status);
        }
        [Test]
        public void CanGetRequestForEdit()
        {
            //Act
            var result = (ViewResult)_requestController.Edit(1);
            var regionalRequest = (RegionalRequest)result.Model;

            //Assert
            Assert.IsInstanceOf<RegionalRequest>(result.Model);
//            Assert.AreEqual(1, regionalRequest.RegionalRequestID);

        }

        [Test]
        public void CanCreateNewRegionalRequest()
        {
            int ProgramId = 1;
            /*
            var newRegionalRequest = new RegionalRequest
                {
                    ProgramId = 1
                    ,
                    Month = 2
                    ,
                    RegionID = 2
                    ,
                    RegionalRequestID = 4
                    ,
                    RequistionDate = DateTime.Parse("7/3/2013")
                    ,
                    Year = DateTime.Today.Year
                    ,
                    Status = 1,
                    Program = new Program()
                                  {
                                      Name = "Program1",
                                      ProgramID = 1

                                  },
                    AdminUnit = new AdminUnit
                                    {
                                        Name = "Region",
                                        AdminUnitID = 1
                                    },
                    Ration = new Ration()
                                 {
                                     RationID = 1,
                                     RefrenceNumber = "RE1",

                                 },
                    Plan = new Plan()
                        {
                            PlanID = 1,
                            PlanName = "Plan1",
                            StartDate = DateTime.Parse("7/3/2013"),
                            EndDate = DateTime.Parse("7/3/2013")
                        },
                    RegionalRequestDetails = new List<RegionalRequestDetail>
                                                 {
                                                     new RegionalRequestDetail
                                                         {
                                                             Beneficiaries = 100
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
                                                             Fdpid = 2
                                                             ,
                                                             RegionalRequestID = 1
                                                             ,
                                                             RegionalRequestDetailID = 2
                                                         }
                                                 }
                };
/*
            //Act
            
            var plan = new HRDPSNPPlan(){PlanID=1,DonorID=1,Month=1,ProgramID=1,PSNPPlanID=1,RationID=1,RegionID=1,Round=1,SeasonID=1,Year=1};
            
            _requestController.New(plan);
            var request = new DataSourceRequest();
            var result = (JsonResult)_requestController.Request_Read(request);
             * */
            //Assert
          //  Assert.IsInstanceOf<JsonResult>(result);
            Assert.AreEqual(1, ProgramId);
           

        }
        [Test]
        public void ShouldListAllRegionalRequests()
        {
            //Act
            var request = new DataSourceRequest();
            request.Page = 1;
            request.PageSize = 5;

            var result = (JsonResult)_requestController.Request_Read(request,-1);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, (((DataSourceResult)result.Data).Total));
        }
        [Test]
        public void AllocationModelShouldContainRegionalRequest()
        {
            //Arrange
            //Act
            var result = (ViewResult)_requestController.Allocation(1);

            //Assert
            Assert.IsInstanceOf<RegionalRequestViewModel>(result.Model);
        }
        [Test]
        public void ShouldPrepareReginalRequestForEdit()
        {
            //Act
            var result = (ViewResult)_requestController.Edit(1);

            //Assert

            Assert.IsInstanceOf<RegionalRequest>(result.Model);
        }
        [Test]
        public void ShouldRaiseHttpNotFound()
        {
            //Act
            var result = _requestController.Edit(10);

            //Assert
            Assert.IsInstanceOf<HttpNotFoundResult>(result);
        }
        [Test]
        public void ShouldUpdateRegionalRequest()
        {
            //Arrange

            var request = new RegionalRequest
                                      {
                                          ProgramId = 1
                                          ,
                                          Month = 1
                                          ,
                                          RegionID = 2
                                          ,
                                          RegionalRequestID = 1
                                          ,
                                          RequistionDate = DateTime.Parse("7/3/2013")
                                          ,
                                          Year = DateTime.Today.Year
                                          ,
                                          Status = 1,
                                      };


            //Act
            var result = _requestController.Edit(request);
            var result2 = (ViewResult)_requestController.Edit(1);
            //Assert

            Assert.IsInstanceOf<RedirectToRouteResult>(result);
//            Assert.AreEqual(2, ((RegionalRequest)result2.Model).RegionID);

        }

        [Test]
        public void ShouldDisplayRequestDetails()
        {
            //Act
            var result = (ViewResult)_requestController.Details(1);
            var resultMainRequest = _requestController.ViewData["Request_main_data"];

            //Assert
            Assert.IsInstanceOf<DataTable>(result.Model);
            Assert.IsInstanceOf<RegionalRequestViewModel>(resultMainRequest);

        }
        #endregion
    }
}
