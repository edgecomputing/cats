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
   public class DetailDistributionService:IUtilizationDetailSerivce
    {
       private readonly IUnitOfWork _unitOfWork;


        public DetailDistributionService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddDetailDistribution(UtilizationDetail DetailDistribution)
        {
            _unitOfWork.UtilizationDetailRepository.Add(DetailDistribution);
            _unitOfWork.Save();
            return true;

        }
        public bool EditDetailDistribution(UtilizationDetail DetailDistribution)
        {
            _unitOfWork.UtilizationDetailRepository.Edit(DetailDistribution);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteDetailDistribution(UtilizationDetail DetailDistribution)
        {
            if (DetailDistribution == null) return false;
            _unitOfWork.UtilizationDetailRepository.Delete(DetailDistribution);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.UtilizationDetailRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.UtilizationDetailRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<UtilizationDetail> GetAllDetailDistribution()
        {
            return _unitOfWork.UtilizationDetailRepository.GetAll();
        }
        public UtilizationDetail FindById(Guid id)
        {
            return _unitOfWork.UtilizationDetailRepository.FindById(id);
        }
        public List<UtilizationDetail> FindBy(Expression<Func<UtilizationDetail, bool>> predicate)
        {
            return _unitOfWork.UtilizationDetailRepository.FindBy(predicate);
        }
       public IEnumerable<UtilizationDetail> Get(
            Expression<Func<UtilizationDetail, bool>> filter = null,
            Func<IQueryable<UtilizationDetail>, IOrderedQueryable<UtilizationDetail>> orderBy = null,
            string includeProperties = "")
       {
           return _unitOfWork.UtilizationDetailRepository.Get(filter, orderBy, includeProperties);
       }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }
    }
}
