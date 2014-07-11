using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.EarlyWarning.Controllers;
using Cats.Areas.EarlyWarning.Models;
using Cats.Helpers;
using Cats.Models;
using Cats.Models.Security;
using Cats.Services.EarlyWarning;
using Cats.Services.Security;
using Cats.ViewModelBinder;
using Kendo.Mvc.UI;
using log4net;
using Moq;
using NUnit.Framework;

namespace Cats.Tests.ControllersTests
{
    [TestFixture]
    public class HRDControllerTests
    {
        #region SetUp / TearDown

        private HRDController _hrdController;
        [SetUp]
        public void Init()
        {
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
            var rationDetails = new List<RationDetail>
                                    {
                                        new RationDetail() {Amount = 1, CommodityID = 1, RationID = 1,Commodity=new Commodity(){CommodityID=1,Name="Creal"}}
                                    };
            var hrdService = new Mock<IHRDService>();
            hrdService.Setup(
                t =>
                t.Get(It.IsAny<Expression<Func<HRD, bool>>>(), It.IsAny<Func<IQueryable<HRD>, IOrderedQueryable<HRD>>>(),
                      It.IsAny<string>())).Returns(hrds);


            var adminUnitService = new Mock<IAdminUnitService>();
            adminUnitService.Setup(t => t.GetRegions()).Returns(new List<AdminUnit>()
                                                                    {new AdminUnit() {AdminUnitID = 1, Name = "Region"}});
         
            var rationService = new Mock<IRationService>();
            var rationDetailService = new Mock<IRationDetailService>();

            rationDetailService.Setup(
                t =>
                t.Get(It.IsAny<Expression<Func<RationDetail, bool>>>(),
                      It.IsAny<Func<IQueryable<RationDetail>, IOrderedQueryable<RationDetail>>>(), It.IsAny<string>())).
                Returns(rationDetails);
            var hrdDetailService = new Mock<IHRDDetailService>();
            hrdDetailService.Setup(
                t =>
                t.Get(It.IsAny<Expression<Func<HRDDetail, bool>>>(),
                      It.IsAny<Func<IQueryable<HRDDetail>, IOrderedQueryable<HRDDetail>>>(), It.IsAny<string>())).
                Returns(hrds.First().HRDDetails);
            var commodityService = new Mock<ICommodityService>();
            var needAssesmentDetailService = new Mock<INeedAssessmentDetailService>();
            var needAssesmentHeaderService = new Mock<INeedAssessmentHeaderService>();
            var workflowSatusService = new Mock<IWorkflowStatusService>();
            var seasonService = new Mock<ISeasonService>();

            var userAccountService = new Mock<IUserAccountService>();
            var log = new Mock<ILog>();

            var plan = new List<Plan> 
                {
                    new Plan {PlanID = 1,PlanName = "Mehere 2005",ProgramID = 1,StartDate = new DateTime(12/12/2006),EndDate = new DateTime(12/12/2007)},
                     new Plan {PlanID = 2,PlanName = "Belg 2005",ProgramID = 1,StartDate = new DateTime(12/12/2006),EndDate = new DateTime(12/12/2007)}
                };

            var planService = new Mock<IPlanService>();
            planService.Setup(m => m.GetAllPlan()).Returns(plan);


            var transactionService = new Mock<Cats.Services.Transaction.ITransactionService>();
            
            userAccountService.Setup(t => t.GetUserInfo(It.IsAny<string>())).Returns(new UserInfo()
            {
                UserName = "x",
                DatePreference = "en",
                PreferedWeightMeasurment = "mt"
            });

            var fakeContext = new Mock<HttpContextBase>();
            var identity = new GenericIdentity("Admin");
           
            var principal = new GenericPrincipal(identity, null);
            fakeContext.Setup(t => t.User).Returns(principal);
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.Setup(t => t.HttpContext).Returns(fakeContext.Object);
            

            _hrdController = new HRDController(adminUnitService.Object,
                                               hrdService.Object,
                                               rationService.Object,
                                               rationDetailService.Object,
                                               hrdDetailService.Object,
                                               commodityService.Object,
                                               needAssesmentDetailService.Object,
                                               needAssesmentHeaderService.Object,
                                               workflowSatusService.Object,
                                               seasonService.Object, userAccountService.Object,
                                               log.Object, planService.Object,null
                );

           _hrdController.ControllerContext = controllerContext.Object;
        }

        [TearDown]
        public void Dispose()
        { _hrdController.Dispose();
            
        }

        #endregion

        #region Tests

        [Test]
        public void ShouldDisplayCompareTwoHRD()
        {
            //ACT
            var result = _hrdController.Compare();

            //Assert

            Assert.IsInstanceOf<ViewResult>(result);
           
        }
        [Test]
        public void ShouldCompareTwoHRD()
        {
            //ACT
            var request = new DataSourceRequest();
            var result = _hrdController.Compare_HRD(request,1,1,1);

            //Assert

            Assert.IsInstanceOf<JsonResult>(result);

        }
        [Test]
        public void ShouldDisplayHRDDetail()
        {
            //act
            var result = _hrdController.Detail(1);
            //
            Assert.AreEqual(1, ((DataTable)((ViewResult)result).Model).Rows.Count);
        }
        [Test]
        public void ShouldDisplayHRDSummary()
        {
            //act
            var result = _hrdController.RegionalSummary(1);
            //
            Assert.AreEqual(1, ((DataTable)((ViewResult)result).Model).Rows.Count);
        }
        #endregion
    }
}
