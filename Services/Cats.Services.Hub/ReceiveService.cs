

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Cats.Data.Hub;
using Cats.Data.Hub.UnitWork;
using Cats.Models.Hubs.ViewModels;
using Cats.Models.Hubs.ViewModels.Report;
using System.Linq;
using Cats.Models.Hubs;

namespace Cats.Services.Hub
{

    public class ReceiveService : IReceiveService
    {
        private readonly IUnitOfWork _unitOfWork;


        public ReceiveService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddReceive(Receive entity)
        {
            _unitOfWork.ReceiveRepository.Add(entity);
            _unitOfWork.Save();
            return true;

        }
        public bool EditReceive(Receive entity)
        {
            _unitOfWork.ReceiveRepository.Edit(entity);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteReceive(Receive entity)
        {
            if (entity == null) return false;
            _unitOfWork.ReceiveRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.ReceiveRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.ReceiveRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<Receive> GetAllReceive()
        {
            return _unitOfWork.ReceiveRepository.GetAll();
        }
        public Receive FindById(int id)
        {
            return _unitOfWork.ReceiveRepository.FindById(id);
        }
        public List<Receive> FindBy(Expression<Func<Receive, bool>> predicate)
        {
            return _unitOfWork.ReceiveRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

        /// <summary>
        /// Bies the hub id.
        /// </summary>
        /// <param name="hubId">The hub id.</param>
        /// <returns></returns>
        public List<Receive> ByHubId(int hubId)
        {
            return _unitOfWork.ReceiveRepository.FindBy(r => r.HubID == hubId);

        }

        public Receive FindById(System.Guid id)
        {
            return _unitOfWork.ReceiveRepository.FindBy(t => t.ReceiveID == id).FirstOrDefault();

        }

        public List<ReceiveViewModelDto> ByHubIdAndAllocationIDetached(int hubId, Guid receiptAllocationId)
        {
            List<ReceiveViewModelDto> receives = new List<ReceiveViewModelDto>();

            var query = (from r in _unitOfWork.ReceiveRepository.GetAll()
                         where r.HubID == hubId && r.ReceiptAllocationID == receiptAllocationId
                         select new ReceiveViewModelDto()
                         {
                             ReceiptDate = r.ReceiptDate,
                             GRN = r.GRN,
                             ReceivedByStoreMan = r.ReceivedByStoreMan,
                             ReceiveID = r.ReceiveID
                         });

            return query.ToList();
        }
        /// <summary>
        /// Return All Ports
        /// </summary>
        /// <returns></returns>
        public List<PortViewModel> GetALlPorts()
        {
            var receives = _unitOfWork.ReceiveRepository.GetAll();
            var ports = (from c in receives select new PortViewModel() { PortName = c.PortName }).Distinct().ToList();

            ports.Insert(0, new PortViewModel { PortName = "All Ports" });
            return ports;
        }
        public void Update(List<ReceiveDetail> inserted, List<ReceiveDetail> updated, List<ReceiveDetail> deleted, Receive receive)
        {

            if (receive != null)
            {

                _unitOfWork.ReceiveRepository.Edit(receive);
                _unitOfWork.Save();


                foreach (ReceiveDetail insert in inserted)
                {
                    //TODO THIS should be in transaction 
                    insert.ReceiveDetailID = Guid.NewGuid();
                    receive.ReceiveDetails.Add(insert);
                }

                foreach (ReceiveDetail delete in deleted)
                {
                    ReceiveDetail deletedCommodity = _unitOfWork.ReceiveDetailRepository.FindBy(p => p.ReceiveDetailID == delete.ReceiveDetailID).FirstOrDefault();
                    if (deletedCommodity != null)
                    {
                        _unitOfWork.ReceiveDetailRepository.Delete(deletedCommodity);
                    }
                }

                foreach (ReceiveDetail update in updated)
                {
                    ReceiveDetail updatedCommodity = _unitOfWork.ReceiveDetailRepository.FindBy(p => p.ReceiveDetailID == update.ReceiveDetailID).FirstOrDefault();
                    if (updatedCommodity != null)
                    {
                        updatedCommodity.CommodityID = update.CommodityID;
                        updatedCommodity.Description = update.Description;
                        updatedCommodity.ReceiveID = update.ReceiveID;
                        updatedCommodity.SentQuantityInMT = update.SentQuantityInMT;
                        // updatedCommodity.QuantityInMT = updatedCommodity.QuantityInMT;
                        updatedCommodity.SentQuantityInUnit = update.SentQuantityInUnit;
                        updatedCommodity.UnitID = update.UnitID;
                    }
                }
                _unitOfWork.Save();
            }

        }

        

        public ReceiveNewViewModel ReceiptAllocationToReceive(ReceiptAllocation receiptAllocation)
        {
            var viewModel = new ReceiveNewViewModel
            {
                ReceiptDate = DateTime.Now,
                ReceiptAllocationId = receiptAllocation.ReceiptAllocationID,
                SiNumber = receiptAllocation.SINumber,
                ProjectCode = receiptAllocation.ProjectNumber,
                Program = _unitOfWork.ProgramRepository.FindById(receiptAllocation.ProgramID).Name,
                ProgramId = receiptAllocation.ProgramID,
                CommodityType = _unitOfWork.CommodityTypeRepository.FindById(receiptAllocation.Commodity.CommodityTypeID).Name,
                CommodityTypeId = receiptAllocation.Commodity.CommodityTypeID,
            };

            if (CommoditySource.Constants.LOAN == receiptAllocation.CommoditySourceID
                || CommoditySource.Constants.SWAP == receiptAllocation.CommoditySourceID
                || CommoditySource.Constants.TRANSFER == receiptAllocation.CommoditySourceID
                || CommoditySource.Constants.REPAYMENT == receiptAllocation.CommoditySourceID)
            {
                if (receiptAllocation.SourceHubID.HasValue)
                {
                    viewModel.SourceHub = _unitOfWork.HubRepository.FindById(receiptAllocation.SourceHubID.GetValueOrDefault(0)).Name; 
                }
            }

            if (CommoditySource.Constants.LOCALPURCHASE == receiptAllocation.CommoditySourceID)
            {
                viewModel.SupplierName = receiptAllocation.SupplierName;
                viewModel.PurchaseOrder = receiptAllocation.PurchaseOrder;
            }

            viewModel.CommoditySource = receiptAllocation.CommoditySource.Name;
            viewModel.CommoditySourceTypeId = receiptAllocation.CommoditySourceID;
            viewModel.ReceiveDetailNewViewModels = new List<ReceiveDetailNewViewModel> { };
            return viewModel;
        }

        public bool IsGrnUnique(string grn)
        {
            var result = FindBy(p => p.GRN == grn).FirstOrDefault();
            return result == null;
        }

        public bool IsReceiveExcedeAllocation(ReceiveDetailNewViewModel receiveDetailNewViewModel, Guid receiptAllocationId)
        {
            var allocation = _unitOfWork.ReceiptAllocationRepository.FindBy(t => t.ReceiptAllocationID == receiptAllocationId).FirstOrDefault();
            decimal sum = 0;
            if (allocation != null && allocation.Receives != null)
                sum = allocation.Receives.Aggregate(sum, (current1, r) => r.ReceiveDetails.Aggregate(current1, (current, rd) => current + Math.Abs(rd.QuantityInMT)));
            var received = sum;

            if (allocation == null) return false;
            var remaining = allocation.QuantityInMT - received;

            //if its being edited
            if(receiveDetailNewViewModel.ReceiveId!=Guid.Empty)
            {
                var prevrecieve =
                    _unitOfWork.ReceiveRepository.FindById(receiveDetailNewViewModel.ReceiveId.GetValueOrDefault());
                remaining+= prevrecieve.ReceiveDetails.FirstOrDefault().QuantityInMT;

            }


            return receiveDetailNewViewModel.ReceivedQuantityInMt > remaining;
        }

        public AllocationStatusViewModel GetAllocationStatus(Guid receiptAllocationId)
        {
            var allocation = _unitOfWork.ReceiptAllocationRepository.FindBy(t => t.ReceiptAllocationID == receiptAllocationId).FirstOrDefault();
            decimal sum = 0;
            if (allocation != null && allocation.Receives != null)
                sum = allocation.Receives.Aggregate(sum, (current1, r) => r.ReceiveDetails.Aggregate(current1, (current, rd) => current + Math.Abs(rd.QuantityInMT)));
            var received = sum;

            if (allocation == null) return new AllocationStatusViewModel
            {
                TotalAllocation = 0,
                RemainingAllocation = 0,
                ReceivedAllocation = 0
            };
            var remaining = allocation.QuantityInMT - received;
            return new AllocationStatusViewModel
            {
                TotalAllocation = allocation.QuantityInMT,
                ReceivedAllocation = received,
                RemainingAllocation = remaining
            };
        }

        public bool IsReceiveGreaterThanSent(ReceiveDetailNewViewModel receiveDetailNewViewModel)
        {
            return receiveDetailNewViewModel.ReceivedQuantityInMt > receiveDetailNewViewModel.SentQuantityInMt &&
                receiveDetailNewViewModel.ReceivedQuantityInUnit >= receiveDetailNewViewModel.SentQuantityInUnit;
        }
    }
}



