using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.Procurement
{

    public class TransportOrderDetailService : ITransportOrderDetailService
    {
        private readonly IUnitOfWork _unitOfWork;


        public TransportOrderDetailService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        #region Default Service Implementation
        public bool AddTransportOrderDetail(TransportOrderDetail transportOrderDetail)
        {
            _unitOfWork.TransportOrderDetailRepository.Add(transportOrderDetail);
            _unitOfWork.Save();
            return true;

        }
        public bool EditTransportOrderDetail(TransportOrderDetail transportOrderDetail)
        {
            _unitOfWork.TransportOrderDetailRepository.Edit(transportOrderDetail);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteTransportOrderDetail(TransportOrderDetail transportOrderDetail)
        {
            if (transportOrderDetail == null) return false;
            _unitOfWork.TransportOrderDetailRepository.Delete(transportOrderDetail);
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
        public List<TransportOrderDetail> GetAllTransportOrderDetail()
        {
            return _unitOfWork.TransportOrderDetailRepository.GetAll();
        }
        public TransportOrderDetail FindById(int id)
        {
            return _unitOfWork.TransportOrderDetailRepository.FindById(id);
        }
        public List<TransportOrderDetail> FindBy(Expression<Func<TransportOrderDetail, bool>> predicate)
        {
            return _unitOfWork.TransportOrderDetailRepository.FindBy(predicate);
        }

        public IEnumerable<TransportOrderDetail> Get(
            Expression<Func<TransportOrderDetail, bool>> filter = null,
            Func<IQueryable<TransportOrderDetail>, IOrderedQueryable<TransportOrderDetail>> orderBy = null,
            string includeProperties = "")
        {
            return _unitOfWork.TransportOrderDetailRepository.Get(filter, orderBy, includeProperties);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }


        public List<vwTransportOrder> GeTransportOrderRpt(int id)
        {
            return _unitOfWork.VwTransportOrderRepository.Get(t => t.TransportOrderID == id).ToList();
        }
       
    }
}
