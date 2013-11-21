using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.EarlyWarning.Controllers;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Models.Security;
using Cats.Services.Common;
using Cats.Services.EarlyWarning;
using Cats.Services.Security;
using Kendo.Mvc.UI;
using Moq;
using NUnit.Framework;

namespace Cats.Tests.ControllersTests
{  
    [TestFixture]
   public class PlanControllerTest
   {
       private PlanController _planController;
       #region Setup
       [SetUp]
       public void init()
       {
           var plan = new List<Plan>
               {
                   new Plan {PlanID = 1,Program = new Program { ProgramID = 1,Name = "Releif"},PlanName = "Mehere 2006",StartDate = new DateTime(01/02/2006),EndDate = new DateTime(02/12/2004),Status = 1},
                   new Plan {PlanID = 2,Program = new Program { ProgramID = 1,Name = "Releif"},PlanName = "Belg 2006",StartDate = new DateTime(01/02/2006),EndDate = new DateTime(02/12/2004),Status = 1}
               };
           var planService = new Mock<IPlanService>();
           planService.Setup(m => m.GetAllPlan()).Returns(plan);

           planService.Setup(t => t.GetPrograms()).Returns(new List<Program>()
                                                                     {
                                                                         new Program()
                                                                             {ProgramID = 1, Description = "Relief"}
                                                                     });

           var _status = new List<Cats.Models.WorkflowStatus>()
                              {
                                  new WorkflowStatus() {  Description = "Draft",StatusID = 1,  WorkflowID = 7 },
                                  new WorkflowStatus() { Description = "Approved",StatusID = 2, WorkflowID = 7 },
                                  new WorkflowStatus(){ Description = "AssessmentCreated",StatusID = 3, WorkflowID = 7 },
                                  new WorkflowStatus(){ Description = "HRDCreated",StatusID = 4, WorkflowID = 7 }
                               };           
                                     
            var commonService = new Mock<ICommonService>();
            commonService.Setup(t => t.GetStatus(It.IsAny<WORKFLOW>())).Returns(_status);

           var hrd = new List<HRD>
               {
                   new HRD { HRDID =1,RationID =1,SeasonID = 1, Status = 1,CreatedBY = 1,CreatedDate =new DateTime(12/12/2012),
                              PublishedDate =new DateTime(12/12/2013),Year = 2012},
                    new HRD { HRDID =2,RationID =1,SeasonID = 2, Status = 2,CreatedBY = 2,CreatedDate =new DateTime(12/12/2012),
                              PublishedDate =new DateTime(12/12/2013),Year = 2012}
               };

           var hrdService = new Mock<IHRDService>();
           hrdService.Setup(m => m.GetAllHRD()).Returns(hrd);

           var needAssessment = new List<NeedAssessment>
               {
                   new NeedAssessment {NeedAID =1,Region = 1,PlanID = 1,Season = 1,TypeOfNeedAssessment = 2,NeedADate = new DateTime(12/12/2005)},
                   new NeedAssessment {NeedAID =2,Region = 2,PlanID = 1,Season = 1,TypeOfNeedAssessment = 2,NeedADate = new DateTime(12/12/2005)}
               };
           var needAssessmentService = new Mock<INeedAssessmentService>();
           needAssessmentService.Setup(m => m.GetAllNeedAssessment()).Returns(needAssessment);

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

           _planController=new PlanController(planService.Object,userAccountService.Object,commonService.Object,needAssessmentService.Object,hrdService.Object);

       }

       [TearDown]
       public void Dispose()
       {
           _planController.Dispose();
       }
       #endregion

       #region Tests
       [Test]
       public void CanShowPlanIndex()
       {
           var result = _planController.Index();
           Assert.IsNotNull(result);
       }
       [Test]
     public void CanCreatePlan()
       {
           var plan = new Plan
               {
                   PlanID = 1,
                   ProgramID = 1,
                   PlanName = "Mehere 2006",
                   StartDate = new DateTime(01 / 02 / 2006),
                   EndDate = new DateTime(02 / 12 / 2004),
                   Status = 1
               };
           var result = _planController.Create(plan);
           Assert.IsNotNull(plan);
       }
    
       [Test]
        public void CanEditPlan()
       {
           var plan = new Plan();
           var result = _planController.Edit(plan);
           Assert.IsNotNull(result);
       }
        //[Test]
        //public void CanGetPlanForEdit()
        //{

        //    var plan = new Plan
        //    {
        //        PlanID = 1,
        //        ProgramID = 1,
        //        PlanName = "Mehere 2006",
        //        StartDate = new DateTime(01 / 02 / 2006),
        //        EndDate = new DateTime(02 / 12 / 2004),
        //        Status = 1
        //    };
        //    var result = (ViewResult)_planController.Edit(plan.PlanID);

        //    var planModel = (Plan)result.Model;

        //    //Assert
        //    Assert.IsInstanceOf<Plan>(result.Model);

        //}

       #endregion
   }
}
