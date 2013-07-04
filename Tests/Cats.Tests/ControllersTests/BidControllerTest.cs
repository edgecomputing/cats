using System;
using System.Web.Mvc;
using Cats.Areas.Procurement.Controllers;
using Cats.Services.EarlyWarning;
using Cats.Services.Procurement;
using Moq;
using NUnit.Framework;
using Cats.Models;
using System.Collections.Generic;

namespace Cats.Tests.ControllersTests
{
    [TestFixture]
    public class BidControllerTest
    {

        private IBidService MockBidService  ;
        private IBidDetailService MockBidDetail;
        private IAdminUnitService MockAdminUnitService;
        private BidController _bidController;
        
        public BidControllerTest()
          {

            //List<Bid> bidTest = new List<Bid>();
            //{
            //    new Bid()
            //}
            //;
            // List<AdminUnit> adminUnitTest=new List<AdminUnit>();
            //{
            //    new AdminUnit() {AdminUnitID = 1, Name = "Afar", NameAM = null, AdminUnitTypeID = 2, ParentID = 1};
            //}

             //Mock the Regional Request Service Using Moq 
            Mock<IBidService> mockBidService = new Mock<IBidService>();
            Mock<BidDetailService> mockBidDetailService=new Mock<BidDetailService>();
            Mock<IAdminUnitService> mockAdminUnitService=new Mock<IAdminUnitService>();


            //mockBidService.Setup(m => m.GetAllBid()).Returns(bidTest);
           // mockAdminUnitService.Setup(m => m.FindBy(au => au.AdminUnitTypeID==2)).Returns(adminUnitTest);
           

            this.MockAdminUnitService = mockAdminUnitService.Object;
            this.MockBidService = mockBidService.Object;
            //this.MockBidDetail = MockBidDetail.Object;


          }
        [SetUp]
        public void SetUp()
        {
            _bidController=new BidController(MockBidService,MockBidDetail,MockAdminUnitService);
        }
        
        [Test]
        public void Bid_Index_Test()
        {
            ActionResult actual = _bidController.Index();
            ViewResult result = actual as ViewResult;
            //String viewName = "Index";
            Assert.IsNotNull(result);
            Assert.AreEqual("", result.ViewName);
        }
        [Test]
        public void Create_Bid_Test()
        {
            List<Bid> bidTests=new List<Bid>();
            {
                new Bid {BidID=1,BidNumber="ADC",StartDate="",EndDate=""};
            }
            var result = _bidController.Create(bidTests);
            Assert.IsTrue(result);
        }
    }
}
