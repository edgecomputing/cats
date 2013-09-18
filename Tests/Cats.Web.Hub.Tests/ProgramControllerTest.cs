using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cats.Models.Hub;
using Cats.Services.Hub;
using Cats.Web.Hub.Controllers;
using Moq;
using NUnit.Framework;

namespace DRMFSS.Web.Test
{
    public class ProgramControllerTest
    {
        private ProgramController _programController;

        #region SetUp
        [SetUp]
        void Init()
        {
            var program = new List<Program>
                {
                    new Program { ProgramID = 1,Name = "PSNP"},
                    new Program {ProgramID = 2,Name ="Relief"}

                };
            var programService = new Mock<IProgramService>();
            programService.Setup(t => t.GetAllProgram()).Returns(program);
            _programController = new ProgramController(programService.Object);
        }
         [TearDown]
        public void Dispose()
        {
            _programController.Dispose();
        }

        #endregion

        #region Tests
         [Test]
        public void CanShowIndex()
        {
            var viewResult = _programController.Index();
            Assert.IsNotNull(viewResult);
        }
         [Test]
        public void CanShowDetails()
        {
            var result = _programController.Details(1);
            Assert.IsNotNull(result);

            var model = result.Model;
            Assert.IsNotNull(model);
        }
         [Test]
        public  void CanCreateProgram()
        {
            var result = _programController.Create();
            Assert.IsNotNull(result);

        }
         [Test]
        public void CanCreateNewProgram()
        {
            var program = new Program {ProgramID = 1, Name = "PSNP"};
             var result = _programController.Create(program);
             Assert.IsNotNull(result);

        }
        [Test]
        public void CanDeleteProgram()
        {
            var result = _programController.Delete(1);
            Assert.IsNotNull(result);
        }

        [Test]
        public void CanShowDeleteConfirmation()
        {
            var result = _programController.DeleteConfirmed(1);
            Assert.IsNotNull(result);

        }
     #endregion


    }
}
