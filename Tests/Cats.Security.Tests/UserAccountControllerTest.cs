using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.Security;
using Moq;
using FluentAssertions.Types;
using NUnit.Framework;
using Cats.Data.Repository;
using Cats.Services.Security;
using Cats.Models.Security;

namespace Cats.Security.Tests
{
    [TestFixture]
    class UserAccountControllerTest
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
        public void Can_Get_List_Of_Users()
        {
           
            var users = new List<UserAccount>{
                new UserAccount()
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
                }
                ,
                new UserAccount()
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
                LogOutDate=DateTime.Today,FailedAttempts=1
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
                }
        };
            var mockUserAccountRepository = new Mock<Cats.Data.Security.IGenericRepository<UserAccount>>();

            mockUserAccountRepository.Setup(t => t.GetAll()).Returns(users);

           var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.UserRepository).Returns(mockUserAccountRepository.Object);

            var userAccountService = new UserAccountService(mockUnitOfWork.Object);

            var expectedUserAccount = userAccountService.GetAll();

            Assert.AreEqual(expectedUserAccount, users);
            
        }
        [Test]
        public void Can_Get_User_By_UserId()
        {
            var users = new List<UserAccount>{
                new UserAccount()
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
                }
                ,
                new UserAccount()
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
                LogOutDate=DateTime.Today,FailedAttempts=1
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
                }
        };
            var mockUserAccountRepository = new Mock<Cats.Data.Security.IGenericRepository<UserAccount>>();

            mockUserAccountRepository.Setup(t => t.FindById(It.IsAny<int>())).Returns((int userId) => users.FirstOrDefault(t => t.UserAccountId == userId));

           var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.UserRepository).Returns(mockUserAccountRepository.Object);

            var userAccountService = new UserAccountService(mockUnitOfWork.Object);

            var expectedUserAccount = userAccountService.FindById(123);
            var actualUserAccountId = users.Find(t => t.UserAccountId == 123);

            Assert.AreEqual(expectedUserAccount.UserName, actualUserAccountId.UserName);
           // Assert.AreSame(expectedUserAccountId, actualUserAccountId);
        }
    }
}
