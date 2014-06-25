

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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

    }
}

 
      
