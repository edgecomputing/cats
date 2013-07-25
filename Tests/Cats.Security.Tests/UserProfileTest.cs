using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using FluentAssertions.Types;
using NUnit.Framework;
using Cats.Data.Repository;
using Cats.Services.Security;
using Cats.Models.Security;

namespace Cats.Security.Tests
{
    [TestFixture]
    class UserProfileTest
    {
         #region SetUp / TearDown

        [SetUp]
        public void Init()
        { }

        [TearDown]
        public void Dispose()
        { }

        #endregion


        [Test]
        public void Can_Get_List_Of_User_Info()
        {
            var users = new List<UserInfo>{
                new UserInfo()
                {
                UserAccountId=123
                ,
                UserName="johnsmith"
                ,
                Password="4gEGXQVUZSYVwyDACh1byO3KRp1ywnkOJBUtDB4rYYk="
                ,
                Disabled=false
                ,               
                LoggedIn=true
                ,
                LogginDate=DateTime.Today
                ,
                LogOutDate=DateTime.Today
                 ,
                FailedAttempts=1
                ,
                FirstName="John"
                ,
                LastName="Smith"
                ,
                GrandFatherName="Michael"
                ,
                Email="company@gmail.com"
                ,
                LanguageCode="EN"
                ,
                Calendar="EN"
                ,
                Keyboard="EN"
                ,
                PreferedWeightMeasurment="MT"
                ,
                DefaultTheme="theme"
                ,
                UserSID=new byte[] {0,0,0,1}
                }
                ,
                new UserInfo()
                {
                UserAccountId=124
                ,
                UserName="muluken"
                ,
                Password="4gEGXQVUZSYVwyDACh1byO3KRp1ywnkOJBUtDB4rYYk="
                ,
                Disabled=false
                ,
                LoggedIn=true
                ,
                LogginDate=DateTime.Today
                ,
                LogOutDate=DateTime.Today
                ,
                FailedAttempts=1
                ,              
                FirstName="muluken"
                ,
                LastName="melese"
                ,
                GrandFatherName="unknown"
                ,
                Email="muluken@gmail.com"
                ,
                LanguageCode="AM"
                ,
                Calendar="AM"
                ,
                Keyboard="AM"
                ,
                PreferedWeightMeasurment="BA"
                ,
                DefaultTheme="theme"
                ,
                UserSID=new byte[] {0,0,0,2}
                }
        };
            var mockUserInfoRepository = new Mock<Cats.Data.Security.IGenericRepository<UserInfo>>();

            mockUserInfoRepository.Setup(t => t.GetAll()).Returns(users);

            Mock<Cats.Data.Security.UnitOfWork> mockUnitOfWork = new Mock<Data.Security.UnitOfWork>();

            mockUnitOfWork.Setup(m => m.UserInfoRepository).Returns(mockUserInfoRepository.Object);

            var userAccountService = new UserAccountService(mockUnitOfWork.Object);

            var expectedUserAccount = userAccountService.GetUsers();

            Assert.AreEqual(expectedUserAccount, users);
        }
        [Test]
        public void Can_Get_User_By_UserAccountId()
        {
            var users = new List<UserInfo>{
                new UserInfo()
                {
                UserAccountId=123
                ,
                UserName="johnsmith"
                ,
                Password="4gEGXQVUZSYVwyDACh1byO3KRp1ywnkOJBUtDB4rYYk="
                ,
                Disabled=false
                ,               
                LoggedIn=true
                ,
                LogginDate=DateTime.Today
                ,
                LogOutDate=DateTime.Today
                 ,
                FailedAttempts=1
                ,
                FirstName="John"
                ,
                LastName="Smith"
                ,
                GrandFatherName="Michael"
                ,
                Email="company@gmail.com"
                ,
                LanguageCode="EN"
                ,
                Calendar="EN"
                ,
                Keyboard="EN"
                ,
                PreferedWeightMeasurment="MT"
                ,
                DefaultTheme="theme"
                ,
                UserSID=new byte[] {0,0,0,1}
                }
                ,
                new UserInfo()
                {
                UserAccountId=124
                ,
                UserName="muluken"
                ,
                Password="4gEGXQVUZSYVwyDACh1byO3KRp1ywnkOJBUtDB4rYYk="
                ,
                Disabled=false
                ,
                LoggedIn=true
                ,
                LogginDate=DateTime.Today
                ,
                LogOutDate=DateTime.Today
                ,
                FailedAttempts=1
                ,              
                FirstName="muluken"
                ,
                LastName="melese"
                ,
                GrandFatherName="unknown"
                ,
                Email="muluken@gmail.com"
                ,
                LanguageCode="AM"
                ,
                Calendar="AM"
                ,
                Keyboard="AM"
                ,
                PreferedWeightMeasurment="BA"
                ,
                DefaultTheme="theme"
                ,
                UserSID=new byte[] {0,0,0,2}
                }
        };

            var mockUserInfoRepository = new Mock<Cats.Data.Security.IGenericRepository<UserInfo>>();

            mockUserInfoRepository.Setup(t => t.FindById(It.IsAny<int>())).Returns((int userAccountId) => users.FirstOrDefault(t => t.UserAccountId == userAccountId));

            Mock<Cats.Data.Security.UnitOfWork> mockUnitOfWork = new Mock<Data.Security.UnitOfWork>();

            mockUnitOfWork.Setup(m => m.UserInfoRepository).Returns(mockUserInfoRepository.Object);

            var userAccountService = new UserAccountService(mockUnitOfWork.Object);

            var expectedUserAccountId = userAccountService.FindById(123);
            var actualUserAccountId = users.Find(t => t.UserAccountId == 123);

            Assert.AreEqual(expectedUserAccountId.UserAccountId, actualUserAccountId.UserAccountId);
            Assert.AreSame(expectedUserAccountId, actualUserAccountId);
        }
    }
}
