using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Areas.EarlyWarning.Controllers;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Models.Security;
using Cats.Services.Common;
using Cats.Services.EarlyWarning;
using Cats.Services.Security;
using Kendo.Mvc.UI;
using NUnit.Framework;
using Moq;
using log4net;
using Cats.Services.Security;
using Cats.Models.Security;
using System.Web;
using System.Security.Principal;
using System.Web.Mvc;
namespace Cats.Tests.ControllersTests
{   [TestFixture]
    public class NeedAssessmentTest
    {
        private  NeedAssessmentController _needAssessmentController;

        #region Setup

       
        [SetUp]
        public void Init()
        {
            var needAssessment = new List<NeedAssessment>()
                                     {
                                         new NeedAssessment
                                             {
                                                 NeedAID = 1,
                                                 Region = 1,
                                                 Season = 1,
                                                 Year = 1982,
                                                 NeedADate = new DateTime(2/2/2000),
                                                 NeddACreatedBy = 4,
                                                 NeedAApproved = true,
                                                 NeedAApprovedBy = 4,
                                                 TypeOfNeedAssessment = 1,
                                                 Remark = "testing need assessment"
                                             },

                                            new NeedAssessment
                                                {
                                                    NeedAID = 2,
                                                    Region = 2,
                                                    Season = 2,
                                                    Year = 1983,
                                                    NeedADate = new DateTime(3/3/2003),
                                                    NeddACreatedBy = 5,
                                                    NeedAApproved = false,
                                                    NeedAApprovedBy = 5,
                                                    TypeOfNeedAssessment =2,
                                                    Remark = "testing need assessment object two"
                                                }
                                     };


            var needAssessmentService = new Mock<INeedAssessmentService>();
            needAssessmentService.Setup(n => n.GetAllNeedAssessment()).Returns(needAssessment);


            var needAssessmentHeader = new List<NeedAssessmentHeader>()
                                           {
                                               new NeedAssessmentHeader
                                                   {
                                                       NeedAID = 1,
                                                       NAHeaderId = 1,
                                                       Zone = 3,
                                                       Remark = "need asssessment header remark"

                                                   }
                                           };

            var needAssessmentHeaderService = new Mock<INeedAssessmentHeaderService>();
            needAssessmentHeaderService.Setup(h => h.GetAllNeedAssessmentHeader());


            var needAssessmentDetail = new List<NeedAssessmentDetail>()
                                           {
                                               new NeedAssessmentDetail
                                                   {
                                                       NeedAId = 1,
                                                       NAId = 1,
                                                       ProjectedMale = 30,
                                                       ProjectedFemale = 30,
                                                       RegularPSNP = 40,
                                                       PSNP = 50,
                                                       NonPSNP = 60,
                                                       Contingencybudget = 70,
                                                       TotalBeneficiaries = 70,
                                                       PSNPFromWoredasMale = 80,
                                                       PSNPFromWoredasFemale = 80,
                                                       PSNPFromWoredasDOA = 90,
                                                       NonPSNPFromWoredasMale = 100,
                                                       NonPSNPFromWoredasFemale = 100,
                                                       NonPSNPFromWoredasDOA = 120,
                                                       AdminUnit=new AdminUnit{Name="x",AdminUnitID= 1,ParentID=1},
NeedAssessmentHeader=new NeedAssessmentHeader(){AdminUnit=new AdminUnit(){Name="Afar"}}
                                                       
                                                   },

                                                   
                                                new NeedAssessmentDetail
                                                   {
                                                       NeedAId = 1,
                                                       NAId = 1,
                                                       ProjectedMale = 800,
                                                       ProjectedFemale = 678,
                                                       RegularPSNP = 90,
                                                       PSNP = 90,
                                                       NonPSNP = 90,
                                                       Contingencybudget = 120,
                                                       TotalBeneficiaries = 90,
                                                       PSNPFromWoredasMale = 76,
                                                       PSNPFromWoredasFemale = 098,
                                                       PSNPFromWoredasDOA = 90,
                                                       NonPSNPFromWoredasMale = 66,
                                                       NonPSNPFromWoredasFemale = 89,
                                                       NonPSNPFromWoredasDOA = 90,
                                                       AdminUnit=new AdminUnit{Name="x",AdminUnitID= 1,ParentID=1},
NeedAssessmentHeader=new NeedAssessmentHeader(){AdminUnit=new AdminUnit(){Name="Afar"}}
                                                       
                                                   }
                                                       
                                           };

            var needAssessmentDetailService = new Mock<INeedAssessmentDetailService>();
            needAssessmentDetailService.Setup(d => d.GetAllNeedAssessmentDetail()).Returns(needAssessmentDetail);
            needAssessmentDetailService.Setup(t => t.FindBy(It.IsAny<Expression<Func<NeedAssessmentDetail, bool>>>())).
                Returns(needAssessmentDetail);
            var season = new List<Season>()
                            {
                                new Season
                                    {
                                        SeasonID = 1,
                                        Name = "Meher"
                                    },
                                    new Season
                                        {
                                            SeasonID = 2,
                                            Name = "Belg"
                                        }

                                    
                            };

            var seasonService = new Mock<ISeasonService>();
            seasonService.Setup(s => s.GetAllSeason()).Returns(season);


            var adminUnit = new List<AdminUnit>()
                                {
                                    new AdminUnit
                                        {
                                            AdminUnitID = 1,
                                            Name = "Dire Dawa"
                                        },
                                    new AdminUnit
                                        {
                                            AdminUnitID = 2,
                                            Name = "Harar"
                                        }
                                };

            var adminUnitService = new Mock<IAdminUnitService>();
            adminUnitService.Setup(a => a.GetAllAdminUnit()).Returns(adminUnit);



            var TypeOfAssessment = new List<TypeOfNeedAssessment>
                                       {
                                           new TypeOfNeedAssessment
                                               {
                                                   TypeOfNeedAssessmentID = 1,
                                                   TypeOfNeedAssessment1 = "Main",
                                                   Remark = "Type of need assessment remark"
                                               },
                                           new TypeOfNeedAssessment
                                               {
                                                   TypeOfNeedAssessmentID = 2,
                                                   TypeOfNeedAssessment1 = "Rapid",
                                                   Remark = "Rapid"
                                               },
                                           new TypeOfNeedAssessment
                                               {
                                                   TypeOfNeedAssessmentID = 3,
                                                   TypeOfNeedAssessment1 = "Diaster",
                                                   Remark = "Disaster"
                                               }

                                       };

            var typeOfNeedAssessmentService = new Mock<ITypeOfNeedAssessmentService>();
            typeOfNeedAssessmentService.Setup(t => t.GetAllTypeOfNeedAssessment()).Returns(TypeOfAssessment);

            var log = new Mock<ILog>();
            var plan = new List<Plan>
                {
                    new Plan
                        {
                            PlanID = 1,
                            PlanName = "NeedAssessmentPlan",
                            ProgramID = 1,
                            StartDate = new DateTime(12/12/2013),
                            EndDate = new DateTime(12/12/2014)
                        },
                    new Plan
                        {
                            PlanID = 2,
                            PlanName = "NeedAssessmentPlan2",
                            ProgramID = 1,
                            StartDate = new DateTime(12/12/2013),
                            EndDate = new DateTime(12/12/2014)
                        }
                };
            var planService = new Mock<IPlanService>();
            planService.Setup(m => m.GetAllPlan()).Returns(plan);

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
                                          Description = "AssessmentCreated",
                                          StatusID = 3,
                                          WorkflowID = 1
                                      }
                              };
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
            var commonService = new Mock<ICommonService>();
            commonService.Setup(t => t.GetAminUnits(It.IsAny<Expression<Func<AdminUnit, bool>>>(),
                      It.IsAny<Func<IQueryable<AdminUnit>, IOrderedQueryable<AdminUnit>>>(),
                      It.IsAny<string>())).Returns(adminUnit);
            commonService.Setup(t => t.GetStatus(It.IsAny<WORKFLOW>())).Returns(_status);

            

            _needAssessmentController=new NeedAssessmentController(needAssessmentService.Object,
                                                                    adminUnitService.Object,
                                                                    needAssessmentHeaderService.Object,
                                                                    needAssessmentDetailService.Object,
                                                                    seasonService.Object,
                                                                    typeOfNeedAssessmentService.Object,
                                                                    log.Object, planService.Object, commonService.Object,userAccountService.Object);
        }
        [TearDown]
        public void Dispose()
        {
            _needAssessmentController.Dispose();
        }
        #endregion


        #region Tests
        [Test]
        public void CanShowIndex()
        {
            var result = _needAssessmentController.Index();
            Assert.IsNotNull(result);
        }

        [Test]
        public void CanReadDetail()
        {
            var request = new DataSourceRequest();
            var result = _needAssessmentController.NeedAssessmentDetailRead(request, 1);
            Assert.IsNotNull(result);

        }

        #endregion
    }
}
