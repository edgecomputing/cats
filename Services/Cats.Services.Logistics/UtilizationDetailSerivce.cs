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
   public class UtilizationDetailService:IUtilizationDetailSerivce
    {
       private readonly IUnitOfWork _unitOfWork;


       public UtilizationDetailService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddDetailDistribution(WoredaStockDistributionDetail DetailDistribution)
        {
            _unitOfWork.WoredaStockDistributionDetailRepository.Add(DetailDistribution);
            _unitOfWork.Save();
            return true;

        }
        public bool EditDetailDistribution(WoredaStockDistributionDetail DetailDistribution)
        {
            _unitOfWork.WoredaStockDistributionDetailRepository.Edit(DetailDistribution);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteDetailDistribution(WoredaStockDistributionDetail DetailDistribution)
        {
            if (DetailDistribution == null) return false;
            _unitOfWork.WoredaStockDistributionDetailRepository.Delete(DetailDistribution);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.WoredaStockDistributionDetailRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.WoredaStockDistributionDetailRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<WoredaStockDistributionDetail> GetAllDetailDistribution()
        {
            return _unitOfWork.WoredaStockDistributionDetailRepository.GetAll();
        }
        public WoredaStockDistributionDetail FindById(Guid id)
        {
            return _unitOfWork.WoredaStockDistributionDetailRepository.FindById(id);
        }
        public List<WoredaStockDistributionDetail> FindBy(Expression<Func<WoredaStockDistributionDetail, bool>> predicate)
        {
            return _unitOfWork.WoredaStockDistributionDetailRepository.FindBy(predicate);
        }
       public IEnumerable<WoredaStockDistributionDetail> Get(
            Expression<Func<WoredaStockDistributionDetail, bool>> filter = null,
            Func<IQueryable<WoredaStockDistributionDetail>, IOrderedQueryable<WoredaStockDistributionDetail>> orderBy = null,
            string includeProperties = "")
       {
           return _unitOfWork.WoredaStockDistributionDetailRepository.Get(filter, orderBy, includeProperties);
       }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }
    }
}
