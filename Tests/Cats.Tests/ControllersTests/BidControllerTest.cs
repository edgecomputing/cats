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
        private List<Bid> _bids;
        
        public BidControllerTest()
          {

            List<Bid> bidTest = new List<Bid>();
              {
                  new Bid() { BidID=1,BidNumber ="PP452",StartDate=new DateTime(2012/10/10),EndDate=new DateTime(2013/12/11)};
                  new Bid() { BidID = 2,BidNumber ="AAA123",StartDate = new DateTime(2012 / 10 / 10), EndDate = new DateTime(2013 / 12 / 11) };
                  new Bid() { BidID = 3,BidNumber="QW123",StartDate = new DateTime(2012 / 10 / 10), EndDate = new DateTime(2013 / 12 / 11) };
              }
            ;
             List<AdminUnit> adminUnitTest=new List<AdminUnit>();
            {
                new AdminUnit() {AdminUnitID = 1, Name = "Afar", NameAM = null, AdminUnitTypeID = 2, ParentID = 1};
            }
            ;

            List<BidDetail> bidDetailTest=new List<BidDetail>();
              {
                  new BidDetail()
                      {
                          BidDetailID = 1,
                          BidID = 1,
                          RegionID = 2,
                          AmountForPSNPProgram = 200,
                          AmountForReliefProgram = 300,
                          BidDocumentPrice = 10,
                          CBO = 1000
                      };
                  new BidDetail()
                  {
                      BidDetailID = 2,
                      BidID = 1,
                      RegionID = 2,
                      AmountForPSNPProgram = 200,
                      AmountForReliefProgram = 300,
                      BidDocumentPrice = 10,
                      CBO = 1000
                  };
              }


             //Mock the Regional Request Service Using Moq 
            Mock<IBidService> mockBidService = new Mock<IBidService>();
            Mock<BidDetailService> mockBidDetailService=new Mock<BidDetailService>();
            Mock<IAdminUnitService> mockAdminUnitService=new Mock<IAdminUnitService>();


            mockBidService.Setup(m => m.GetAllBid()).Returns(bidTest);
           // mockAdminUnitService.Setup(m => m.FindBy(au => au.AdminUnitTypeID==2)).Returns(adminUnitTest);
           

            this.MockAdminUnitService = mockAdminUnitService.Object;
            this.MockBidService = mockBidService.Object;
            


          }
        [SetUp]
        public void SetUp()
        {
            _bidController=new BidController(MockBidService,MockBidDetail,MockAdminUnitService);
        }

        [Test]
        public void Bid_Controller_Constructor_Test()
        {
            try
            {
                _bidController=new BidController(MockBidService, MockBidDetail, MockAdminUnitService);
            }
            catch (Exception e)
            {

                Assert.Fail(e.Message);
            }
        }
        
        [Test]
        public void Can_fetch_all_Bid_Lists()
        {
             List<Bid> expected = new List<Bid>();
              {
                  new Bid() { BidID=1,BidNumber ="PP452",StartDate=new DateTime(2012/10/10),EndDate=new DateTime(2013/12/11)};
                  new Bid() { BidID = 2,BidNumber ="AAA123",StartDate = new DateTime(2012 / 10 / 10), EndDate = new DateTime(2013 / 12 / 11) };
              }
            ;
            List<Bid> actual = MockBidService.GetAllBid();
            Assert.AreEqual(actual.Count,expected.Count);
        }

        [Test]
        public void Bid_Index_Test()
        {
            ActionResult actual = _bidController.Index();
            ViewResult result = actual as ViewResult;
            Assert.IsNotNull(result);
        }


        [Test]
        public void Edit_Bid_Test()
        {
            List<Bid> expected = new List<Bid>();
            {
                new Bid() { BidID = 1, BidNumber = "PP452", StartDate = new DateTime(2012 / 10 / 10), EndDate = new DateTime(2013 / 12 / 11) };
                new Bid() { BidID = 2, BidNumber = "AAA123", StartDate = new DateTime(2012 / 10 / 10), EndDate = new DateTime(2013 / 12 / 11) };
            }
            ;
            List<Bid> actual = (List<Bid>) MockBidService.Get(m => m.BidID == 1);
            Assert.AreEqual(actual,expected);

            foreach (var bidExpected in expected)
            {
                Assert.IsTrue(actual.Contains(actual.Find(r => r.BidID == bidExpected.BidID)));
            }
            Assert.AreEqual(expected, actual);

        }
    }
}
