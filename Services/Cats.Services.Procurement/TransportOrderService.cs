using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;
using Cats.Models.ViewModels;


namespace Cats.Services.Procurement
{

    public class TransportOrderService : ITransportOrderService
    {
        private readonly IUnitOfWork _unitOfWork;


        public TransportOrderService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
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
            return _unitOfWork.TransportOrderRepository.Get(filter, orderBy, includeProperties);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }



        public IEnumerable<RequisitionToDispatch> GetRequisitionToDispatch()
        {
            var requisitions = GetProjectCodeAssignedRequisitions();

            var result = (from requisition in requisitions
                          select new RequisitionToDispatch
                          {
                              HubID = requisition.HubAllocations.FirstOrDefault().HubID,
                              RequisitionID = requisition.RequisitionID,
                              RequisitionNo = requisition.RequisitionNo,
                              RequisitionStatus = requisition.Status.Value,
                              ZoneID = requisition.ZoneID.Value,
                              QuanityInQtl = requisition.ReliefRequisitionDetails.Sum(m => m.Amount),
                              OrignWarehouse = requisition.HubAllocations.FirstOrDefault().Hub.Name,
                              CommodityID = requisition.CommodityID.Value,
                              CommodityName = requisition.Commodity.Name,
                              Zone = requisition.AdminUnit.Name,
                              RegionID = requisition.RegionID.Value,
                              RegionName = requisition.AdminUnit1.Name,



                          });


            return result;
        }

        public IEnumerable<ReliefRequisition> GetProjectCodeAssignedRequisitions()
        {
            return _unitOfWork.ReliefRequisitionRepository.Get(t => t.Status == (int)REGIONAL_REQUEST_STATUS.HubAssigned, null,
                                                          "HubAllocations,ReliefRequisitionDetails,Program,AdminUnit1,AdminUnit,Commodity");
        }

        public IEnumerable<ReliefRequisitionDetail> GetProjectCodeAssignedRequisitionDetails()
        {
            return _unitOfWork.ReliefRequisitionDetailRepository.Get(t => t.ReliefRequisition.Status == (int)REGIONAL_REQUEST_STATUS.HubAssigned, null,
                                                          "ReliefRequisition");
        }




        public IEnumerable<TransportOrder> CreateTransportOrder(IEnumerable<int> requisitions)
        {

            var transporterAssignedRequisionDetails = AssignTransporterForEachWoreda(requisitions);

            var transporters = (from item in transporterAssignedRequisionDetails select item.TransporterID).Distinct().ToList();


            //If we reached here all location got transporter 
            var transportOrders = new List<TransportOrder>();

            foreach (var transporter in transporters)
            {
                //TODO:Check what all number should come from and implement
                var transportOrder = new TransportOrder();
                transportOrder.TransporterID = transporter;
                transportOrder.OrderDate = DateTime.Today;
                transportOrder.TransportOrderNo = Guid.NewGuid().ToString();
                transportOrder.OrderExpiryDate = DateTime.Today.AddDays(10);
                transportOrder.BidDocumentNo = "BID-DOC-No";
                transportOrder.PerformanceBondReceiptNo = "PERFORMANCE-BOND-NO";
                transportOrder.ContractNumber = Guid.NewGuid().ToString();
                var transportLocations = transporterAssignedRequisionDetails.FindAll(t => t.TransporterID == transporter).Distinct();

                foreach (var transporterRequisition in transportLocations)
                {
                    var requisionsDetails =
                        _unitOfWork.ReliefRequisitionDetailRepository.Get(
                            t =>
                            t.RequisitionID == transporterRequisition.RequisitionID &&
                            t.FDP.AdminUnitID == transporterRequisition.WoredaID).ToList();

                    foreach (var reliefRequisitionDetail in requisionsDetails)
                    {
                        var transportOrderDetail = new TransportOrderDetail();
                        //transportOrderDetail.ZoneID = reliefRequisitionDetail.ReliefRequisition.ZoneID;
                        transportOrderDetail.CommodityID = reliefRequisitionDetail.CommodityID;
                        transportOrderDetail.FdpID = reliefRequisitionDetail.FDPID;
                        transportOrderDetail.RequisitionID = reliefRequisitionDetail.RequisitionID;
                        transportOrderDetail.QuantityQtl = reliefRequisitionDetail.Amount;
                        transportOrderDetail.TariffPerQtl = transporterRequisition.TariffPerQtl;
                        transportOrderDetail.SourceWarehouseID = transporterRequisition.HubID;
                        transportOrder.TransportOrderDetails.Add(transportOrderDetail);
                    }

                }
                transportOrders.Add(transportOrder);


            }
            _unitOfWork.Save();

            foreach (var transportOrder in transportOrders)
            {
                transportOrder.TransportOrderNo = string.Format("TRN-ORD-{0}", transportOrder.TransportOrderID);
                transportOrder.ContractNumber = string.Format("CON-NUM-{0}", transportOrder.TransportOrderID);
            }
            _unitOfWork.Save();
            return transportOrders;
        }

        private List<TransporterRequisition> AssignTransporterForEachWoreda(IEnumerable<int> requisitions)
        {
            var requisionIds = requisitions.ToList();
            var reqDetails = _unitOfWork.ReliefRequisitionDetailRepository.Get(t => requisionIds.Contains(t.RequisitionID));
            var transportSourceDestination = new List<TransporterRequisition>();
            foreach (var reliefRequisitionDetail in reqDetails)
            {
                var transportRequisition = new TransporterRequisition();
                var requi =
                    _unitOfWork.ReliefRequisitionRepository.Get(
                        t => t.RequisitionID == reliefRequisitionDetail.RequisitionID, null, "HubAllocations").FirstOrDefault();
                transportRequisition.RequisitionID = reliefRequisitionDetail.RequisitionID;
                transportRequisition.HubID = requi.HubAllocations.FirstOrDefault().HubID;
                transportRequisition.WoredaID = reliefRequisitionDetail.FDP.AdminUnitID;
                var transportBidWinnerDetail =
                   _unitOfWork.TransportBidWinnerDetailRepository.Get(
                       t => t.HubID == transportRequisition.HubID && t.WoredaID == transportRequisition.WoredaID).FirstOrDefault();
                if (transportBidWinnerDetail == null)
                {
                    throw new Exception(string.Format("Transporter Couldn't be found for from {0} to {1}", transportRequisition.HubID, transportRequisition.WoredaID));
                }
                transportRequisition.TransporterID = transportBidWinnerDetail.TransporterID;
                transportRequisition.TariffPerQtl = transportBidWinnerDetail.TariffPerQtl;

                transportSourceDestination.Add(transportRequisition);
            }
            return transportSourceDestination;
        }
        public List<vwTransportOrder> GeTransportOrderRpt(int id)
        {
            return _unitOfWork.VwTransportOrderRepository.Get(t => t.TransportOrderID == id).ToList();
        }
        private class TransporterRequisition
        {
            public int HubID { get; set; }
            public int WoredaID { get; set; }
            public int RequisitionID { get; set; }
            public int TransporterID { get; set; }
            public decimal TariffPerQtl { get; set; }
        }
    }
}




