using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using Cats.Models.Hubs;
using Cats.Services.Hub;
using Cats.Web.Hub.Controllers;

using Moq;
using NUnit.Framework;

namespace Cats.Web.Hub.Tests
{
    [TestFixture]
    public class SettingControllerTests
    {
        #region SetUp / TearDown

        private SettingController _settingController;
        [SetUp]
        public void Init()
        {
            var setting = new List<Setting>()
                              {
                                  new Setting()
                                      {
                                          Key = "SMTPServer",
                                          Value = "CtsServer",
                                          Type = "String",
                                          Option = "",
                                          Category = "SMTP Config",
                                      }
                              };
            var settingService = new Mock<ISettingService>();

            settingService.Setup(t => t.FindBy(It.IsAny<Expression<Func<Setting, bool>>>())).Returns(setting);
            var userProfileService = new Mock<IUserProfileService>();
            _settingController=new SettingController(settingService.Object,userProfileService.Object);
        }

        [TearDown]
        public void Dispose()
        {_settingController.Dispose(); }

        #endregion

        #region Tests

        [Test]
        public void ShouldGetSysSetting()
        {
            //Act
            var result = _settingController.SysSettings();

            //Assert

            Assert.IsInstanceOf<SMTPInfo>(((ViewResult)result).Model);
        }

        [Test]
        public void ShouldPrepareForSysSettings()
        {
            //Act
            var result = _settingController.SysSettingsUpdate();

            //Assert

            Assert.IsInstanceOf<SMTPInfo>(((ViewResult)result).Model);
        }

        [Test]
        public void ShouldEditSMTPConfig()
        {
            //Act
            var result = _settingController.EditConfigSMTP(new SMTPInfo());
            //Assert
            Assert.IsInstanceOf<JsonResult>(result);
        }

        #endregion
    }
}
