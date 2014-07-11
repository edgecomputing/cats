using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Cats.Data.Repository;
using Cats.Data.UnitWork;
using Moq;
using NUnit.Framework;
using Cats.Services.Common;
using Cats.Models;
namespace Cats.Tests.Service_Tests.TransportOrder
{
    [TestFixture]
    public class TransportOrdreServiceTest
    {
        #region setup /tear down

        private INotificationService _notificationService;
      
       private readonly string _destinationURl = "http://Procurement/TransportOrder/NotificationIndex";
        [SetUp]
        public void Init()
        {
            var notification = new List<Notification>()
                                   {
                                       new Notification()
                                           {
                                               Text = "transport order one",
                                               Url=_destinationURl,
                                               RecordId = 1,
                                               IsRead = false,
                                               TypeOfNotification = "New transport order",
                                               CreatedDate = DateTime.Now,
                                               Id = 1,
                                               Application = "Hub Managers"
                                           },
                                           new Notification()
                                           {
                                              Text = "transport order two",
                                               Url=_destinationURl,
                                               RecordId = 2,
                                               IsRead = false,
                                               TypeOfNotification = "New transport order",
                                               CreatedDate = DateTime.Now,
                                               Id = 1,
                                               Application = "Hub Managers"
                                           },
                                   };


         


            var notificationRepository = new Mock<IGenericRepository<Notification>>();
            notificationRepository.Setup(t => t.Get(It.IsAny<Expression<Func<Notification, bool>>>(), null, It.IsAny<string>())).
                Returns(
                    notification.AsQueryable());
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(t => t.NotificationRepository).Returns(notificationRepository.Object);
            _notificationService = new NotificationService(unitOfWork.Object);

            //var fakeContext = new Mock<HttpContextBase>();
            //var request = new Mock<HttpRequestBase>();
            //request.Setup(t => t.Url.Authority).Returns("");
            //fakeContext.Setup(t => t.Request).Returns(request.Object);
                
            
        }
        #endregion
        [Test]
        public void CanNotificationBeSaved()
        {
            var notification = new Notification()
                                   {
                                       Text = "transport order two",
                                       Url = _destinationURl,
                                       RecordId = 2,
                                       IsRead = false,
                                       TypeOfNotification = "New transport order",
                                       CreatedDate = DateTime.Now,
                                       Id = 1,
                                       Application = "Hub Managers"
                                   };

            var tranportRequsition = new TransportRequisition()
                                         {
                                             CertifiedBy = 1,
                                             CertifiedDate = DateTime.Today,
                                             RequestedBy = 1,
                                             RequestedDate = DateTime.Today,
                                             TransportRequisitionID = 1,
                                             TransportRequisitionNo = "TRN-001",
                                             Status = 1,
                                             Remark = "Remark",
                                         };
            var hubId = new List<int>(){1,2,3};
            var resultLogistics = _notificationService.AddNotificationForLogistcisFromEarlyWaring("",1, 1, "reg-001");
            var resultProcurement = _notificationService.AddNotificationForProcurementFromLogistics("",tranportRequsition);
            var resultHubManager = _notificationService.AddNotificationForHubManagersFromTransportOrder("",1, "trans-001",hubId);


            Assert.IsTrue(resultLogistics);
            Assert.IsTrue(resultProcurement);
            Assert.IsTrue(resultHubManager);
        }
    }
}
