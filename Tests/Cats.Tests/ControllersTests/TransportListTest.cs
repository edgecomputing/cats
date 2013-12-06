using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Cats.Areas.Procurement.Controllers;
using Cats.Models;
using Cats.Services.Procurement;
using Kendo.Mvc.UI;
using NUnit.Framework;
using Moq;
namespace Cats.Tests.ControllersTests
{
    [TestFixture]
    public class TransportListTest
    {
        private TransportListController _transportListController;

        [SetUp]
        public void Init()
        {
            var transporters = new List<Transporter>()
                                  {
                                      new Transporter()
                                          {
                                                          TransporterID = 1,
                                                          Name = "Bert",
                                                          Region = 4,
                                                          SubCity = "Arada",
                                                          Zone = 1,
                                                          MobileNo = "09123786554",
                                                          Capital = 20000
                                          }
                                  };

            var transporterQuotation = new List<TransportBidQuotation>()
                                           {
                                               new TransportBidQuotation()
                                                   {
                                                       TransportBidQuotationID=1,
                                                       Bid = new Bid()
                                                                 {
                                                                      BidID =1,
        StartDate=new DateTime(1/1/2000),
        EndDate =new DateTime(2/2/2000),
        BidNumber = "BID-001",
        OpeningDate=new DateTime(3/3/2000),
        StatusID = 2,
        TransportBidPlanID = 1
                                                                 },
                                                                 BidID = 1,
                                                                 Transporter = new Transporter()
                                                                                   {
                                                                                    TransporterID = 1,
                                                          Name = "Bert",
                                                          Region = 4,
                                                          SubCity = "Arada",
                                                          Zone = 1,
                                                          MobileNo = "09123786554",
                                                          Capital = 20000   
                                                                                   },
                                                                                   TransporterID = 1,
                                                                                   Source = new Hub()
                                                                                                {
                                                                                                 HubID   = 1,
                                                                                                 HubOwnerID = 2,
                                                                                                 Name = "sanple hub",
                                                                                                 
                                                                                                },
                                                                                                Destination = new AdminUnit()
                                                                                                                  {
                                                                                                                      AdminUnitID = 3,
                                                                                                                      Name = "Tigray"
                                                                                                                  },
                                                                                                                  Tariff = 12,
                                                                                                                  IsWinner = true,
                                                                                                                  Remark = "sample remark"
                                                   }
                                           };

            var transportquotation = new  Mock<ITransportBidQuotationService>();
            var transporterService = new  Mock<ITransporterService>();

            transportquotation.Setup(s => s.GetAllTransportBidQuotation()).Returns(transporterQuotation);
            transporterService.Setup(t => t.GetAllTransporter()).Returns(transporters);

            _transportListController=new TransportListController(transporterService.Object,transportquotation.Object);

        }

        [TearDown]
        public void Dispose()
        {
            _transportListController.Dispose();
        }


        [Test]
        public  void Index()
        {
            var result = _transportListController.Index();
            Assert.IsNotNull(result);
        }

        [Test]
        public void ReadTransporters()
        {
            var request = new DataSourceRequest();
            var result = _transportListController.ReadTransporters(request);
            Assert.IsNotNull(result);
        }
        
        [Test]
        public void TransporterBidDetail()
        {
            var result = _transportListController.TransporterBidDetail(1);
            Assert.IsNotNull(result);
        }

        [Test]
        public void ShowBidByTransporter()
        {
            var request = new DataSourceRequest();
            var result = _transportListController.ShowBidByTransporter(request,1);
            Assert.IsInstanceOf<JsonResult>(result);
        }

    }
}
