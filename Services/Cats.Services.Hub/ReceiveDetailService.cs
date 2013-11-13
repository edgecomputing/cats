

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Cats.Data.Hub;
using Cats.Models.Hubs;

namespace Cats.Services.Hub
{

    public class ReceiveDetailService : IReceiveDetailService
    {
        private readonly IUnitOfWork _unitOfWork;


        public ReceiveDetailService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddReceiveDetail(ReceiveDetail receiveDetail)
        {
            _unitOfWork.ReceiveDetailRepository.Add(receiveDetail);
            _unitOfWork.Save();
            return true;

        }
        public bool EditReceiveDetail(ReceiveDetail receiveDetail)
        {
            _unitOfWork.ReceiveDetailRepository.Edit(receiveDetail);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteReceiveDetail(ReceiveDetail receiveDetail)
        {
            if (receiveDetail == null) return false;
            _unitOfWork.ReceiveDetailRepository.Delete(receiveDetail);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.ReceiveDetailRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.ReceiveDetailRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<ReceiveDetail> GetAllReceiveDetail()
        {
            return _unitOfWork.ReceiveDetailRepository.GetAll();
        }
        public ReceiveDetail FindById(int id)
        {
            return _unitOfWork.ReceiveDetailRepository.FindById(id);
        }
        public List<ReceiveDetail> FindBy(Expression<Func<ReceiveDetail, bool>> predicate)
        {
            return _unitOfWork.ReceiveDetailRepository.FindBy(predicate);
        }
        #endregion

       
        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

        public IEnumerable<ReceiveDetail> Get(System.Linq.Expressions.
           Expression<Func<ReceiveDetail, bool>> filter = null,
           Func<IQueryable<ReceiveDetail>, IOrderedQueryable<ReceiveDetail>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.ReceiveDetailRepository.Get(filter, orderBy, includeProperties);
        }


        /// Gets the by receive id.
        /// </summary>
        /// <param name="receiveId">The receive id.</param>
        /// <returns></returns>
        public List<ReceiveDetail> GetByReceiveId(Guid receiveId)
        {
            return _unitOfWork.ReceiveDetailRepository.FindBy(v => v.ReceiveID == receiveId).ToList();
           
        }


        public List<ReceiveDetailViewModelDto> ByReceiveIDetached(Guid? receiveId, string weightMeasurmentCode)
        {
            List<ReceiveDetailViewModelDto> receiveDetails = new List<ReceiveDetailViewModelDto>();

            var query = (from rD in _unitOfWork.ReceiveDetailRepository.Get()
                         where rD.ReceiveID == receiveId
                         select new ReceiveDetailViewModelDto()
                         {
                             ReceiveID = rD.ReceiveID,
                             ReceiveDetailID = rD.ReceiveDetailID,
                             CommodityName = rD.Commodity.Name,
                             UnitName = rD.Unit.Name,
                             ReceivedQuantityInMT = rD.TransactionGroup.Transactions.FirstOrDefault().QuantityInMT,
                             ReceivedQuantityInUnit = rD.TransactionGroup.Transactions.FirstOrDefault().QuantityInUnit,
                             SentQuantityInMT = Math.Abs(rD.SentQuantityInMT),
                             SentQuantityInUnit = rD.SentQuantityInUnit

                         });
            foreach (var receiveDetailViewModelDto in query)
            {

                if (weightMeasurmentCode == "qn")
                {
                    receiveDetailViewModelDto.ReceivedQuantityInMT = Math.Abs(receiveDetailViewModelDto.ReceivedQuantityInMT) * 10;
                    receiveDetailViewModelDto.SentQuantityInMT = Math.Abs(receiveDetailViewModelDto.SentQuantityInMT) * 10;

                }
                else
                {
                    receiveDetailViewModelDto.ReceivedQuantityInMT = Math.Abs(receiveDetailViewModelDto.ReceivedQuantityInMT);
                    receiveDetailViewModelDto.SentQuantityInMT = Math.Abs(receiveDetailViewModelDto.SentQuantityInMT);
                }
                receiveDetailViewModelDto.ReceivedQuantityInUnit =
                    Math.Abs(receiveDetailViewModelDto.ReceivedQuantityInUnit);
                receiveDetails.Add(receiveDetailViewModelDto);
            }
            return receiveDetails;
        }
    }
}


