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
   public class DetailDistributionService:IDetailDistributionService
    {
       private readonly IUnitOfWork _unitOfWork;


        public DetailDistributionService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddDetailDistribution(DetailDistribution DetailDistribution)
        {
            _unitOfWork.DetailDistributionRepository.Add(DetailDistribution);
            _unitOfWork.Save();
            return true;

        }
        public bool EditDetailDistribution(DetailDistribution DetailDistribution)
        {
            _unitOfWork.DetailDistributionRepository.Edit(DetailDistribution);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteDetailDistribution(DetailDistribution DetailDistribution)
        {
            if (DetailDistribution == null) return false;
            _unitOfWork.DetailDistributionRepository.Delete(DetailDistribution);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.DetailDistributionRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.DetailDistributionRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<DetailDistribution> GetAllDetailDistribution()
        {
            return _unitOfWork.DetailDistributionRepository.GetAll();
        }
        public DetailDistribution FindById(Guid id)
        {
            return _unitOfWork.DetailDistributionRepository.FindById(id);
        }
        public List<DetailDistribution> FindBy(Expression<Func<DetailDistribution, bool>> predicate)
        {
            return _unitOfWork.DetailDistributionRepository.FindBy(predicate);
        }
       public IEnumerable<DetailDistribution> Get(
            Expression<Func<DetailDistribution, bool>> filter = null,
            Func<IQueryable<DetailDistribution>, IOrderedQueryable<DetailDistribution>> orderBy = null,
            string includeProperties = "")
       {
           return _unitOfWork.DetailDistributionRepository.Get(filter, orderBy, includeProperties);
       }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }
    }
}
