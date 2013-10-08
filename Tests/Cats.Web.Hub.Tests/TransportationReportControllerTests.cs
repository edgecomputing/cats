using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Cats.Services.Hub;
using Cats.Models.Hub.ViewModels;
using Cats.Web.Hub.Controllers.Reports;
using Moq;
using NUnit.Framework;

namespace Cats.Web.Hub.Tests
{
    [TestFixture]
    public class TransportationReportControllerTests
    {
        #region SetUp / TearDown

        private TransportationReportController _transportationReportController;

        [SetUp]
        public void Init()
        {
            var transactionService = new Mock<ITransactionService>();
            var transporationReport = new List<TransporationReport>()
                                          {
                                              new TransporationReport()
                                                  {
                                                      Commodity="CSB",
                                                      QuantityInMT=14,
                                                      QuantityInUnit=14,
                                                      NoOfTrucks=3,
                                                      Program = "PSNP"
                                                  }
                                          };
            transactionService.Setup(
                t => t.GetTransportationReports(OperationMode.Recieve, DateTime.Today, DateTime.Today.AddDays(4))).
                Returns(transporationReport);
            var userProfileService = new Mock<IUserProfileService>();
            _transportationReportController=new TransportationReportController(transactionService.Object,userProfileService.Object);
        }

        [TearDown]
        public void Dispose()
        {_transportationReportController.Dispose(); }

        #endregion

        #region Tests

        [Test]
        public void ShouldDisplayReceiveTrend()
        {
            //Act
            var result = (ViewResult)_transportationReportController.ReceiveTrend(1, DateTime.Today.ToShortDateString(),
                                                                DateTime.Today.AddDays(4).ToShortDateString());
            var result2 = ((List<TransporationReport>) result.Model).First();
            //Assert
            Assert.AreEqual("PSNP", result2.Program);
        }

       

        #endregion
    }
}
