using System;
using System.Collections.Generic;
using Cats.Services.Hub;
using Cats.Web.Hub.Controllers;
using Moq;
using NUnit.Framework;
using Cats.Models.Hub;
namespace DRMFSS.Web.Test
{
    public class ReceiveCommodityControllerTest
    {
        private ReceiveDetailController _receiveDetailController;

        #region SetUp
        [SetUp]
        void Init()
        {
            var receiveDetail = new List<ReceiveDetail>
                {
                    new ReceiveDetail {ReceiveDetailID =new Guid("405cc2d4"),PartitionID = 1,ReceiveID = new Guid("405cc2d4-e50f"),
                                       CommodityID = 1,SentQuantityInUnit = 900000,SentQuantityInMT = 45000,UnitID = 1,Description = "Good"}

                };
            var receiveDetailService = new Mock<IReceiveDetailService>();
            receiveDetailService.Setup(t => t.GetAllReceiveDetail()).Returns(receiveDetail);

            var receive = new List<Receive>
                {
                    new Receive  { ReceiveID = new Guid("405cc2d4-e50f"),PartitionID = 1,HubID = 2,GRN = "0033789",
                                   CommodityTypeID = 1,SourceDonorID = 1,ResponsibleDonorID = 1,TransporterID = 21,PlateNo_Prime = "3-25584",
                                   PlateNo_Trailer = "3-09401",DriverName = "Tegene Werku",WeightBridgeTicketNumber = null,WeightBeforeUnloading = null,
                                   WeightAfterUnloading = null,ReceiptDate = new DateTime(12/12/2012),UserProfileID = 1,CreatedDate = new DateTime(11/11/2012),
                                   WayBillNo = "315392,315393,315394",CommoditySourceID = 1,Remark = null,VesselName = "EUGENIA B",ReceivedByStoreMan = "Tegene Werku",
                                   PurchaseOrder = null,SupplierName = "kality(faffa F.S.C)",ReceiptAllocationID =new Guid("23ds454d45") }
                 };
            var receiveService=new Mock<IReceiveService>();
            receiveService.Setup(t => t.GetAllReceive()).Returns(receive);

            var commodity = new List<Commodity>
                {
                    new Commodity
                        {CommodityID = 1, Name = "Pulse", CommodityTypeID = 1, CommodityCode = "PUL", ParentID = 1},
                    new Commodity
                        {CommodityID = 2, Name = "Oil", CommodityTypeID = 1, CommodityCode = "Oil", ParentID = 1},
                    new Commodity
                        {CommodityID = 3, Name = "CSB", CommodityTypeID = 1, CommodityCode = "CSB", ParentID = 1}
                };
            var commodityService = new Mock<ICommodityService>();
            commodityService.Setup(t => t.GetAllCommodity()).Returns(commodity);

            var commodityGrade = new List<CommodityGrade>
                {
                    new CommodityGrade {CommodityGradeID = 1, Name = "1st Grade", Description = null, SortOrder = 1},
                    new CommodityGrade {CommodityGradeID = 1, Name = "2st Grade", Description = null, SortOrder = 2},
                    new CommodityGrade {CommodityGradeID = 1, Name = "3st Grade", Description = null, SortOrder = 3}

                };

            var commodityGradeService = new Mock<ICommodityGradeService>();
            commodityGradeService.Setup(t => t.GetAllCommodityGrade()).Returns(commodityGrade);

            var unit = new List<Unit>
                {
                    new Unit {UnitID = 1, Name = "Bag"},
                    new Unit {UnitID = 2, Name = "Quintal"},
                    new Unit {UnitID = 3, Name = "Cartons"}
                };

            var unitService = new Mock<IUnitService>();
            unitService.Setup(t => t.GetAllUnit()).Returns(unit);

            _receiveDetailController = new ReceiveDetailController(receiveDetailService.Object,
                                                                      commodityService.Object,commodityGradeService.Object,
                                                                      receiveService.Object,unitService.Object);
        }

        [TearDown]
        public void Dispose()
        {
            _receiveDetailController.Dispose();
        }

        #endregion

        #region Tests

        [Test]
        public void CanShowIndex()
        {
            var result = _receiveDetailController.Index();
            Assert.IsNotNull(result);
        }
        [Test]
        public void CanEdit()
        {
            var result = _receiveDetailController.Edit("405cc2d4-e50f");
            Assert.IsNotNull(result);
            
        }

        [Test]
        public void CanCreateReceiveDetail()
        {
            var expected = new List<ReceiveDetail>
                {
                    new ReceiveDetail {ReceiveDetailID =new Guid("405cc2d4"),PartitionID = 1,ReceiveID = new Guid("405cc2d4-e50f"),
                                       CommodityID = 1,SentQuantityInUnit = 900000,SentQuantityInMT = 45000,UnitID = 1,Description = "Good"}

                };
           // var result = _receiveDetailController.Create(expected);
        }
        #endregion


    }
    
}

