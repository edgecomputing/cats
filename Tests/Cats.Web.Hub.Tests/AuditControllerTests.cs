using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Cats.Models.Hub;
using Cats.Services.Hub;
using Cats.Web.Hub.Controllers;
using Moq;
using NUnit.Framework;

namespace DRMFSS.Web.Test
{
    [TestFixture]
    public class AuditControllerTests
    {
        #region SetUp / TearDown

        private AuditController _auditController;
        [SetUp]
        public void Init()
        {
            var auditService = new Mock<IAuditService>();
            auditService.Setup(
                t =>
                t.GetChanges(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                             It.IsAny<string>(), It.IsAny<string>())).Returns(new List<FieldChange>()
                                                                                  {
                                                                                      new FieldChange()
                                                                                          {
                                                                                              ChangeDate = DateTime.Today,
                                                                                              ChangedBy = "MRX",
                                                                                              ChangedValue = "XYZ",
                                                                                              FieldName = "Name",
                                                                                              PreviousValue = "MRY"
                                                                                          }
                                                                                  });
            this._auditController = new AuditController(auditService.Object);
        }

        [TearDown]
        public void Dispose()
        { _auditController.Dispose(); }

        #endregion

        #region Tests

        [Test]
        public void ShouldGetAudits()
        {
            //Act
            var result = _auditController.Audits("1", "FDP", "Name", "", "FDPID", "FDPID");
            //Assert
            Assert.IsInstanceOf<PartialViewResult>(result);

        }

        #endregion
    }
}
