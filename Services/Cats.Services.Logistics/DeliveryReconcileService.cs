using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.Logistics
{
    public class DeliveryReconcileService : IDeliveryReconcileService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeliveryReconcileService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Implementation of IDeliveryReconcileService

        public bool AddDeliveryReconcile(DeliveryReconcile deliveryReconcile)
        {
            _unitOfWork.DeliveryReconcileRepository.Add(deliveryReconcile);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteDeliveryReconcile(DeliveryReconcile deliveryReconcile)
        {
            if (deliveryReconcile == null) return false;
            _unitOfWork.DeliveryReconcileRepository.Delete(deliveryReconcile);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.DeliveryReconcileRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.DeliveryReconcileRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool EditDeliveryReconcile(DeliveryReconcile deliveryReconcile)
        {
            _unitOfWork.DeliveryReconcileRepository.Edit(deliveryReconcile);
            _unitOfWork.Save();
            return true;
        }

        public DeliveryReconcile FindById(int id)
        {
            return _unitOfWork.DeliveryReconcileRepository.FindById(id);
        }

        public List<DeliveryReconcile> GetAllDeliveryReconciles()
        {
            return _unitOfWork.DeliveryReconcileRepository.GetAll();
        }

        public List<DeliveryReconcile> FindBy(Expression<Func<DeliveryReconcile, bool>> predicate)
        {
            return _unitOfWork.DeliveryReconcileRepository.FindBy(predicate);
        }

        public IEnumerable<DeliveryReconcile> Get(Expression<Func<DeliveryReconcile, bool>> filter = null, 
            Func<IQueryable<DeliveryReconcile>, IOrderedQueryable<DeliveryReconcile>> orderBy = null, 
            string includeProperties = "")
        {
            return _unitOfWork.DeliveryReconcileRepository.Get(filter, orderBy, includeProperties);
        }

        #endregion
    }
}
