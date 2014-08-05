using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Cats.Data.UnitWork;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Models.ViewModels;
using Cats.Services.Common;

namespace Cats.Services.Procurement
{

    public class TransportOrderService : ITransportOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITransporterService _transporterService;
        private readonly INotificationService _notificationService;


        public TransportOrderService(IUnitOfWork unitOfWork, ITransporterService transporterService, INotificationService notificationService)
        {
            this._unitOfWork = unitOfWork;
            this._transporterService = transporterService;
            _notificationService = notificationService;
        }

        #region Default Service Implementation
        public bool AddTransportOrder(TransportOrder transportOrder)
        {
            _unitOfWork.TransportOrderRepository.Add(transportOrder);
            _unitOfWork.Save();
            return true;

        }
        public bool EditTransportOrder(TransportOrder transportOrder)
        {
            _unitOfWork.TransportOrderRepository.Edit(transportOrder);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteTransportOrder(TransportOrder transportOrder)
        {
            if (transportOrder == null) return false;
            _unitOfWork.TransportOrderRepository.Delete(transportOrder);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.TransportOrderRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.TransportOrderRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<TransportOrder> GetAllTransportOrder()
        {
            return _unitOfWork.TransportOrderRepository.GetAll();
        }
        public TransportOrder FindById(int id)
        {
            return _unitOfWork.TransportOrderRepository.FindById(id);
        }
        public List<TransportOrder> FindBy(Expression<Func<TransportOrder, bool>> predicate)
        {
            return _unitOfWork.TransportOrderRepository.FindBy(predicate);
        }

        public IEnumerable<TransportOrder> Get(
            Expression<Func<TransportOrder, bool>> filter = null,
            Func<IQueryable<TransportOrder>, IOrderedQueryable<TransportOrder>> orderBy = null,
            string includeProperties = "")
        {
            var test = _unitOfWork.TransportOrderRepository.Get(filter, orderBy, includeProperties);
            return _unitOfWork.TransportOrderRepository.Get(filter, orderBy, includeProperties);
        }

        public IEnumerable<TransportOrder> GetByHub(Expression<Func<TransportOrder, bool>> filter = null, 
            Func<IQueryable<TransportOrder>, IOrderedQueryable<TransportOrder>> orderBy = null,
            string includeProperties = "", int hubId = 0, int statusId = 0)
        {
        //    var transportOrderDetail =
        //        ;
            var transportOrder = (
                from c in _unitOfWork.TransportOrderDetailRepository.FindBy(x => x.SourceWarehouseID == hubId)
                select c.TransportOrder).Where(x => x.StatusID == statusId).Distinct().ToList();
            return transportOrder;
        }
        public IEnumerable<TransportOrder> GetFilteredTransportOrder(IEnumerable<TransportRequisitionDetail> transportRequsitionDetails ,int statusId)
        {
            var transportRequistionDetail = transportRequsitionDetails.Select(m => m.RequisitionID).Distinct();
            var transportOrder =
                (from order in
                     _unitOfWork.TransportOrderDetailRepository.FindBy(
                         m => transportRequistionDetail.Contains(m.RequisitionID))
                 select order.TransportOrder).Where(m=>m.StatusID==statusId);
            return transportOrder;
        }
        public List<Program> GetPrograms()
        {
            return _unitOfWork.ProgramRepository.GetAll();
        }

        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }





        //public IEnumerable<RequisitionToDispatch> GetRequisitionToDispatch()
        //{
        //    var requisitions = GetProjectCodeAssignedRequisitions();

        //    var result = (from requisition in requisitions
        //                  select new RequisitionToDispatch
        //                             {
        //                                 HubID =requisition.HubAllocations.FirstOrDefault().HubID,
        //                                 RequisitionID = requisition.RequisitionID,
        //                                 RequisitionNo = requisition.RequisitionNo,
        //                                 RequisitionStatus = requisition.Status.Value,
        //                                 ZoneID = requisition.ZoneID.Value,
        //                                 QuanityInQtl = requisition.ReliefRequisitionDetails.Sum(m => m.Amount),
        //                                 OrignWarehouse = requisition.HubAllocations.FirstOrDefault().Hub.Name,
        //                                 CommodityID = requisition.CommodityID.Value,
        //                                 CommodityName = requisition.Commodity.Name,
        //                                 Zone = requisition.AdminUnit.Name,
        //                                 RegionID = requisition.RegionID.Value,
        //                                 RegionName = requisition.AdminUnit1.Name,



        //                             });


        //    return result;
        //}


        //public IEnumerable<ReliefRequisition> GetProjectCodeAssignedRequisitions()
        //{
        //    return _unitOfWork.ReliefRequisitionRepository.Get(t => t.Status == (int)REGIONAL_REQUEST_STATUS.HubAssigned, null,
        //                                                  "HubAllocations,ReliefRequisitionDetails,Program,AdminUnit1,AdminUnit,Commodity");
        //}

        //public IEnumerable<ReliefRequisitionDetail> GetProjectCodeAssignedRequisitionDetails()
        //{
        //    return _unitOfWork.ReliefRequisitionDetailRepository.Get(t => t.ReliefRequisition.Status == (int)REGIONAL_REQUEST_STATUS.HubAssigned, null,
        //                                                  "ReliefRequisition");
        //}

        public IEnumerable<TransportOrderDetail> GetTransportOrderDetail(int requisitionId)
        {
            return _unitOfWork.TransportOrderDetailRepository.Get(t => t.RequisitionID == requisitionId);
        }

        public IEnumerable<ReliefRequisition> GetTransportOrderReleifRequisition(int status)
        {
            return _unitOfWork.ReliefRequisitionRepository.Get(r => r.Status == 6); //This will return reuisitions where thier transport order is made
        }

        public IEnumerable<TransportOrderDetail> GetTransportOrderDetailByTransportId(int transportId)
        {
            return _unitOfWork.TransportOrderDetailRepository.Get(t => t.TransportOrderID == transportId);
        }
        //TODO:Factor Out  to single responiblity Principle 
        public bool CreateTransportOrder(int transportRequisitionId)
        {
            //var requId=_unitOfWork.TransportRequisitionDetailRepository.FindBy(t=>t.TransportRequisitionID==)
            var transporterAssignedRequisionDetails = AssignTransporterForEachWoreda(transportRequisitionId);

            var transporters = (from item in transporterAssignedRequisionDetails select item.TransporterID).Distinct().ToList();


            //If we reached here all location got transporter 
            var transportOrders = new List<TransportOrder>();

            foreach (var transporter in transporters)
            {
                //TODO:Check what all number should come from and implement
                var transportOrder = new TransportOrder();
                transportOrder.TransporterID = transporter;
                transportOrder.OrderDate = DateTime.Today;
                transportOrder.StartDate = DateTime.Today;
                transportOrder.EndDate = DateTime.Today;
                transportOrder.TransportOrderNo = Guid.NewGuid().ToString();
                transportOrder.OrderExpiryDate = DateTime.Today.AddDays(10);
                
                transportOrder.PerformanceBondReceiptNo = "PERFORMANCE-BOND-NO";
                //var transporterName = _unitOfWork.TransporterRepository.FindById(transporter).Name;
                transportOrder.ContractNumber = Guid.NewGuid().ToString();
                //string.Format("{0}/{1}/{2}/{3}", "LTCD", DateTime.Today.day, DateTime.Today.Year, transporterName.Substring(0, 1));
                transportOrder.TransporterSignedDate = DateTime.Today;
                transportOrder.RequestedDispatchDate = DateTime.Today;
                transportOrder.ConsignerDate = DateTime.Today;
                transportOrder.StartDate = DateTime.Today.AddDays(3);
                transportOrder.EndDate = DateTime.Today.AddDays(13);
                transportOrder.StatusID = (int)TransportOrderStatus.Draft;
                var transportLocations = transporterAssignedRequisionDetails.FindAll(t => t.TransporterID == transporter).Distinct();

                foreach (var transporterRequisition in transportLocations)
                {
                    //var currentBid = _unitOfWork.BidRepository.FindBy(t => t.StatusID == int.Parse(BidStatus.Active.ToString())).FirstOrDefault();
                    var transReq = transporterRequisition;
                    //var activeBidStatusID = int.Parse(BidStatus.Active.ToString());
                    var bidWinner =
                    _unitOfWork.BidWinnerRepository.Get(
                        t => t.SourceID == transReq.HubID && t.DestinationID == transReq.WoredaID && t.Position == 1 &&
                            t.Bid.StatusID == 5).FirstOrDefault();
                    if (bidWinner != null)
                    {
                        transportOrder.BidDocumentNo = _unitOfWork.BidRepository.FindById(bidWinner.BidID).BidNumber;
                    }
                    else
                    {
                        transportOrder.BidDocumentNo = "Bid-Number";
                        //_unitOfWork.BidWinnerRepository.FindById(transporter).Bid.BidNumber;
                    }

                    var requisionsDetails =
                        _unitOfWork.ReliefRequisitionDetailRepository.Get(
                            t =>
                            t.RequisitionID == transReq.RequisitionID &&
                            t.FDP.AdminUnitID == transReq.WoredaID, null, "ReliefRequisition").ToList();

                    foreach (var reliefRequisitionDetail in requisionsDetails)
                    {
                        var transportOrderDetail = new TransportOrderDetail();
                        //transportOrderDetail.ZoneID = reliefRequisitionDetail.ReliefRequisition.ZoneID;
                        transportOrderDetail.CommodityID = reliefRequisitionDetail.CommodityID;
                        transportOrderDetail.FdpID = reliefRequisitionDetail.FDPID;
                        transportOrderDetail.RequisitionID = reliefRequisitionDetail.RequisitionID;
                        transportOrderDetail.QuantityQtl = reliefRequisitionDetail.Amount;
                        transportOrderDetail.TariffPerQtl = transReq.TariffPerQtl;
                        transportOrderDetail.SourceWarehouseID = transReq.HubID;
                        transportOrder.TransportOrderDetails.Add(transportOrderDetail);
                    }

                }
                _unitOfWork.TransportOrderRepository.Add(transportOrder);
                transportOrders.Add(transportOrder);


            }


            var requisition = _unitOfWork.TransportRequisitionRepository.Get(t => t.TransportRequisitionID == transportRequisitionId).FirstOrDefault();

            requisition.Status = (int)TransportRequisitionStatus.Closed;

            var transportRequisitionDetails =
                _unitOfWork.TransportRequisitionDetailRepository.Get(t => t.TransportRequisitionID == transportRequisitionId).ToList();
            foreach (var transportRequisitionDetail in transportRequisitionDetails)
            {
                var reliefRequisition =
                    _unitOfWork.ReliefRequisitionRepository.Get(
                        t => t.RequisitionID == transportRequisitionDetail.RequisitionID).FirstOrDefault();
                reliefRequisition.Status = (int)ReliefRequisitionStatus.TransportOrderCreated;
            }

            _unitOfWork.Save();
            //TODO:Identity if Transport order number to be auto generated , and where to get contract number.

            foreach (var transportOrder in transportOrders)
            {
                var transporterName = _unitOfWork.TransporterRepository.FindById(transportOrder.TransporterID).Name;
                transportOrder.TransportOrderNo = string.Format("TRN-ORD-{0}", transportOrder.TransportOrderID);
                transportOrder.ContractNumber = string.Format("{0}/{1}/{2}/{3}", "LTCD", DateTime.Today.Day, DateTime.Today.Year, transporterName.Substring(0, 2));
            }

            _unitOfWork.Save();





            return true;
        }



        private List<TransporterRequisition> AssignTransporterForEachWoreda(int transportRequisitionId)
        {

            var transportRequision = _unitOfWork.TransportRequisitionDetailRepository.Get(
                t => t.TransportRequisitionID == transportRequisitionId, null, null).Select(t => t.RequisitionID);

            var reqDetails = _unitOfWork.ReliefRequisitionDetailRepository.Get(t => transportRequision.Contains(t.RequisitionID));
            var transportSourceDestination = new List<TransporterRequisition>();
            foreach (var reliefRequisitionDetail in reqDetails)
            {
                var transportRequisition = new TransporterRequisition();
                //var requi =
                //    _unitOfWork.ReliefRequisitionRepository.Get(
                //        t => t.RequisitionID == reliefRequisitionDetail.RequisitionID, null, "HubAllocations").FirstOrDefault();
                transportRequisition.TransportRequisitionDetailID = reliefRequisitionDetail.ReliefRequisition.TransportRequisitionDetails.First().TransportRequisitionDetailID;
                transportRequisition.RequisitionID = reliefRequisitionDetail.RequisitionID;
                transportRequisition.HubID = _unitOfWork.HubAllocationRepository.FindBy(t => t.RequisitionID == reliefRequisitionDetail.RequisitionID).FirstOrDefault().HubID;//requi.HubAllocations.FirstOrDefault().HubID;
                transportRequisition.WoredaID = reliefRequisitionDetail.FDP.AdminUnitID;
                var transportBidWinner = _transporterService.GetCurrentBidWinner(transportRequisition.HubID,
                                                                          transportRequisition.WoredaID);
                //_unitOfWork.BidWinnerRepository.Get(
                //    t => t.SourceID == transportRequisition.HubID && t.DestinationID == transportRequisition.WoredaID).FirstOrDefault();
                if (transportBidWinner == null)
                {
                    var transReqWithoutTransporter = new TransReqWithoutTransporter();
                    transReqWithoutTransporter.TransportRequisitionDetailID = transportRequisition.TransportRequisitionDetailID;
                    transReqWithoutTransporter.RequisitionDetailID = reliefRequisitionDetail.RequisitionDetailID;
                    transReqWithoutTransporter.IsAssigned = false;
                    _unitOfWork.TransReqWithoutTransporterRepository.Add(transReqWithoutTransporter);
                    _unitOfWork.Save();
                    //throw new Exception(string.Format("Transporter Couldn't be found for from {0} to {1}", transportRequisition.HubID, transportRequisition.WoredaID));
                }
                else
                {
                    //TODO: these commented lines should be figured out how they affect the rest of the code
                    transportRequisition.TransporterID = transportBidWinner.TransporterID;
                    transportRequisition.TariffPerQtl = transportBidWinner.Tariff!=null ? (decimal) transportBidWinner.Tariff : 0;

                    transportSourceDestination.Add(transportRequisition);
                }

            }
            return transportSourceDestination;
        }

        public List<vwTransportOrder> GeTransportOrderRpt(int id)
        {
            return _unitOfWork.VwTransportOrderRepository.Get(t => t.TransportOrderID == id).ToList();
        }
        public List<Transporter> GetTransporter()
        {
            return _unitOfWork.TransporterRepository.GetAll();
        }
        public bool ReAssignTransporter(IEnumerable<TransportRequisitionWithoutWinnerModel> transReqWithTransporter, int transporterID)
        {
            if (transReqWithTransporter != null && transporterID != 0)
            {


                var transportOrder = new TransportOrder();
                transportOrder.TransporterID = transporterID;
                transportOrder.OrderDate = DateTime.Today;
                transportOrder.StartDate = DateTime.Today;
                transportOrder.EndDate = DateTime.Today;
                transportOrder.TransportOrderNo = Guid.NewGuid().ToString();
                transportOrder.OrderExpiryDate = DateTime.Today.AddDays(10);
                var currentBid = _unitOfWork.BidRepository.FindBy(t => t.StatusID == 3).FirstOrDefault();
                var transporterName = _unitOfWork.TransporterRepository.FindById(transportOrder.TransporterID).Name;
                if (currentBid != null)
                {
                    var bidID = currentBid.BidID;
                    transportOrder.BidDocumentNo = _unitOfWork.BidRepository.FindById(bidID).BidNumber;
                }
                else
                {
                    transportOrder.BidDocumentNo = "Bid-Number";

                }
                transportOrder.PerformanceBondReceiptNo = "PERFORMANCE-BOND-NO";
                //var transporterName = _unitOfWork.TransporterRepository.FindById(transporter).Name;
                transportOrder.ContractNumber = Guid.NewGuid().ToString();
                //string.Format("{0}/{1}/{2}/{3}", "LTCD", DateTime.Today.day, DateTime.Today.Year, transporterName.Substring(0, 1));
                transportOrder.TransporterSignedDate = DateTime.Today;
                transportOrder.RequestedDispatchDate = DateTime.Today;
                transportOrder.ConsignerDate = DateTime.Today;
                transportOrder.StatusID = (int)TransportOrderStatus.Draft;
                var lastOrder = _unitOfWork.TransportOrderRepository.GetAll();
                if (lastOrder.Count!=0)
                {
                    transportOrder.TransportOrderNo = string.Format("TRN-ORD-{0}", lastOrder.Last().TransportOrderID + 1);
                }
                else
                {
                    transportOrder.TransportOrderNo = string.Format("TRN-ORD-{0}", 1);
                }
                transportOrder.ContractNumber = string.Format("{0}/{1}/{2}/{3}", "LTCD", DateTime.Today.Day,
                                                              DateTime.Today.Year, transporterName.Substring(0, 3));
                foreach (var detail in transReqWithTransporter)
                {
                    var transportOrderDetail = new TransportOrderDetail();
                    transportOrderDetail.CommodityID = detail.CommodityID;
                    transportOrderDetail.FdpID = detail.FdpID;
                    transportOrderDetail.RequisitionID = detail.RequisitionID;
                    transportOrderDetail.QuantityQtl = detail.QuantityQtl;
                    //since users don't specify tariff value
                    transportOrderDetail.TariffPerQtl = 0;
                    transportOrderDetail.SourceWarehouseID = detail.HubID;
                    transportOrder.TransportOrderDetails.Add(transportOrderDetail);
                }
                bool isSaved = _unitOfWork.TransportOrderRepository.Add(transportOrder);
                _unitOfWork.Save();
                if (isSaved)
                {
                    foreach (var item in transReqWithTransporter)
                    {
                        var withoutTransporter =
                            _unitOfWork.TransReqWithoutTransporterRepository.FindById(item.TransReqWithoutTransporterID);
                        withoutTransporter.IsAssigned = true;
                        _unitOfWork.TransReqWithoutTransporterRepository.Edit(withoutTransporter);
                        _unitOfWork.Save();
                    }

                }


                return true;
            }
            return false;

        }
        
        private class TransporterRequisition
        {
            public int HubID { get; set; }
            public int WoredaID { get; set; }
            public int RequisitionID { get; set; }
            public int TransporterID { get; set; }
            public decimal TariffPerQtl { get; set; }
            public int TransportRequisitionDetailID { get; set; }
        }

        public List<Hub> GetHubs()
        {
            return _unitOfWork.HubRepository.GetAll();
        }
        public bool ApproveTransportOrder(TransportOrder transportOrder)
        {
            if (transportOrder != null)
            {
                try
                {
                    var hubId = new List<int>();
                    var transport = transportOrder.TransportOrderDetails.Select(c => c.SourceWarehouseID).ToList().Distinct();
                    {
                        hubId.AddRange(transport);
                    }
                    AddToNotification(transportOrder.TransportOrderID, transportOrder.TransportOrderNo,hubId);
                }
                catch
                {




                }

                transportOrder.StatusID = (int)TransportOrderStatus.Approved;
                _unitOfWork.TransportOrderRepository.Edit(transportOrder);
                _unitOfWork.Save();

                return true;
            }
            return false;

        }

        public bool GeneratDispatchPlan(int transportOrderId)
        {

            var transportOrder =
                            _unitOfWork.TransportOrderRepository.Get(
                                t =>
                                t.TransportOrderID == transportOrderId && t.StatusID == (int)TransportOrderStatus.Approved).FirstOrDefault();
            if (transportOrder == null) return false;

            var transportOrderDetails =
                _unitOfWork.TransportOrderDetailRepository.Get(t => t.TransportOrderID == transportOrderId, null, "ReliefRequisition").ToList();

            foreach (var transportOrderDetail in transportOrderDetails)
            {
                var requisition = transportOrderDetail.ReliefRequisition;
                var fdpId = transportOrderDetail.FdpID;
                var requisitionDetail =
                    _unitOfWork.ReliefRequisitionDetailRepository.Get(
                        t => t.RequisitionID == requisition.RequisitionID && t.FDPID == fdpId).FirstOrDefault();
                var dispatchAllocation = new DispatchAllocation();
                dispatchAllocation.DispatchAllocationID = Guid.NewGuid();

                dispatchAllocation.Beneficiery = requisitionDetail != null ? requisitionDetail.BenficiaryNo : 0;
                dispatchAllocation.Amount = transportOrderDetail.QuantityQtl;
                dispatchAllocation.BidRefNo = transportOrder.BidDocumentNo;
                dispatchAllocation.CommodityID = transportOrderDetail.CommodityID;
                dispatchAllocation.ContractEndDate = transportOrder.StartDate;
                dispatchAllocation.ContractEndDate = transportOrder.EndDate;
                dispatchAllocation.DonorID = transportOrderDetail.DonorID;
                dispatchAllocation.FDPID = transportOrderDetail.FdpID;
                dispatchAllocation.HubID = transportOrderDetail.SourceWarehouseID;
                dispatchAllocation.TransporterID = transportOrder.TransporterID;
               // dispatchAllocation.IsClosed = false;
                dispatchAllocation.Month = requisition.Month;
                dispatchAllocation.Round = requisition.Round;
                
                dispatchAllocation.TransportOrderID = transportOrderId;
                dispatchAllocation.ProgramID = requisition.ProgramID;
                dispatchAllocation.RequisitionNo = requisition.RequisitionNo;
                dispatchAllocation.RequisitionId = requisition.RequisitionID;
                dispatchAllocation.PartitionId = 0;
                var sipc =
                    _unitOfWork.SIPCAllocationRepository.FindBy(
                        t => t.RequisitionDetailID == requisitionDetail.RequisitionDetailID);
                var si = sipc.Find(t => t.AllocationType == "SI");
                if (si != null)
                    dispatchAllocation.ShippingInstructionID = si.Code;
                var pc = sipc.Find(t => t.AllocationType == "PC");
                if (pc != null)
                    dispatchAllocation.ProjectCodeID = pc.Code;
                //dispatchAllocation.Unit //i have no idea where to get it
                // dispatchAllocation.StoreID  //Would be set null and filled by user later
                //dispatchAllocation.Year= requisition.Year ; //Year is not available 
                _unitOfWork.DispatchAllocationRepository.Add(dispatchAllocation);
            }
            transportOrder.StatusID = (int)TransportOrderStatus.Closed;
            _unitOfWork.Save();

            return true;
        }


        private void AddToNotification(int transportOrderId, string transportOrderNo,List<int> hubId )
        {
            try
            {
                if (HttpContext.Current == null) return;
                string destinationURl;
                if (HttpContext.Current.Request.Url.Host == "localhost")
                {
                    destinationURl = "http://" + HttpContext.Current.Request.Url.Authority +
                                     "/Hub/TransportOrder/NotificationIndex?recordId=" + transportOrderId;
                    return;
                }
                destinationURl = "http://" + HttpContext.Current.Request.Url.Authority +
                                 HttpContext.Current.Request.ApplicationPath +
                                 "/Hub/TransportOrder/NotificationIndex?recordId=" + transportOrderId;
                _notificationService.AddNotificationForHubManagersFromTransportOrder(destinationURl, transportOrderId,
                                                                                     transportOrderNo,hubId);
            }
            catch (Exception)
            {

                ;
            }

        }

    }
}




