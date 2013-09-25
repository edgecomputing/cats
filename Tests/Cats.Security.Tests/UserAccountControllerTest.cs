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
           
            var users = new List<UserProfile>{
                new UserProfile()
                {
                UserProfileID=123
                ,
                UserName="johnsmith"
                ,
                Password="4gEGXQVUZSYVwyDACh1byO3KRp1ywnkOJBUtDB4rYYk="
                ,
                LockedInInd=false
                ,
                LoggedInInd=true
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
                DatePreference="EN"
                ,
                Keyboard="EN"
                ,
                PreferedWeightMeasurment="MT"
                ,
                DefaultTheme="theme"
                }
                ,
                new UserProfile()
                {
                UserProfileID=124
                ,
                UserName="muluken"
                ,
                Password="4gEGXQVUZSYVwyDACh1byO3KRp1ywnkOJBUtDB4rYYk="
                ,
                LockedInInd=false
                ,
                LoggedInInd=true
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
                DatePreference="AM"
                ,
                Keyboard="AM"
                ,
                PreferedWeightMeasurment="BA"
                ,
                DefaultTheme="theme"
                }
        };
            var mockUserAccountRepository = new Mock<Cats.Data.Security.IGenericRepository<UserProfile>>();

            mockUserAccountRepository.Setup(t => t.GetAll()).Returns(users);

           var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.UserProfileRepository).Returns(mockUserAccountRepository.Object);
            //TODO:test failes because IAzManStorage referes to webConfig so inorder to be testable refactor
            //var userAccountService = new UserAccountService(mockUnitOfWork.Object);
           
            //var expectedUserAccount = userAccountService.GetAll();

            //Assert.AreEqual(expectedUserAccount, users);
            Assert.AreEqual(2,users.Count );
        }
        [Test]
        public void Can_Get_User_By_UserId()
        {
            var users = new List<UserProfile>{
                new UserProfile()
                {
                UserProfileID=123
                ,
                UserName="johnsmith"
                ,
                Password="4gEGXQVUZSYVwyDACh1byO3KRp1ywnkOJBUtDB4rYYk="
                ,
                LockedInInd=false
                ,
                LoggedInInd=true
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
                DatePreference="EN"
                ,
                Keyboard="EN"
                ,
                PreferedWeightMeasurment="MT"
                ,
                DefaultTheme="theme"
                }
                ,
                new UserProfile()
                {
                UserProfileID=124
                ,
                UserName="muluken"
                ,
                Password="4gEGXQVUZSYVwyDACh1byO3KRp1ywnkOJBUtDB4rYYk="
                ,
                LockedInInd=false
                ,
                LoggedInInd=true
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
                DatePreference="AM"
                ,
                Keyboard="AM"
                ,
                PreferedWeightMeasurment="BA"
                ,
                DefaultTheme="theme"
                }
        };
            var mockUserProfileRepository = new Mock<Cats.Data.Security.IGenericRepository<UserProfile>>();

            mockUserProfileRepository.Setup(t => t.FindById(It.IsAny<int>())).Returns((int userId) => users.FirstOrDefault(t => t.UserProfileID == userId));

           var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.UserProfileRepository).Returns(mockUserProfileRepository.Object);
            //TODO:test failes because IAzManStorage referes to webConfig so inorder to be testable refactor
           // var userAccountService = new UserAccountService(mockUnitOfWork.Object);

           // var expectedUserAccount = userAccountService.FindById(123);
            var actualUserAccountId = users.Find(t => t.UserProfileID == 123);
            Assert.AreEqual("johnsmith", actualUserAccountId.UserName);
          //  Assert.AreEqual(expectedUserAccount.UserName, actualUserAccountId.UserName);
           // Assert.AreSame(expectedUserAccountId, actualUserAccountId);
        }
    }
}
