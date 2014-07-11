using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Cats.Models;
using Cats.Services.Administration;
using Cats.Web.Adminstration.Controllers;
using Kendo.Mvc.UI;
using Moq;
using NUnit.Framework;

namespace Cats.Web.Adminstration.Tests
{  
   [TestFixture]
   public class ProgramControllerTest
   {
       private ProgramController _programController;

       [SetUp]
       public void Init()
       {
           var program = new List<Program>()
               {
                   new Program {ProgramID = 1,Name = "PSNP",LongName = "PSNP"},
                   new Program {ProgramID = 2,Name = "Relief",LongName = "Relief"}
               };

           var programService = new Mock<IProgramService>();
           programService.Setup(m => m.GetAllProgram()).Returns(program);

           _programController=new ProgramController(programService.Object);

       }
       [TearDown]
       public void Dispose()
       {
           _programController.Dispose();
       }

       #region Tests
       [Test]
       public void CanShowIndex()
       {
           var result = _programController.Index();
           Assert.IsNotNull(result);
       }
       [Test]
       public void CanReadProgram()
       {
           var request = new DataSourceRequest();
           var result = _programController.Program_Read(request);
           Assert.IsInstanceOf<JsonResult>(result);
           Assert.IsNotNull(result);
       }
       [Test]
       public void CanDeleteProgram()
       {
           var result = _programController.Delete(1);
           Assert.IsInstanceOf<ActionResult>(result);
           //Assert.IsNotNull(result);
       }

       #endregion

   }
}
