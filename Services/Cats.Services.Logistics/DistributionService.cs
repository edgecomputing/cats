

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;
using Cats.Services.Logistics;


namespace Cats.Services.Logistics
{

    public class DistributionService : IDistributionService
    {
        private readonly IUnitOfWork _unitOfWork;


        public DistributionService()
        {
            this._unitOfWork = new UnitOfWork();
        }
        #region Default Service Implementation
        public bool AddDistribution(Distribution distribution)
        {
            _unitOfWork.DistributionRepository.Add(distribution);
            _unitOfWork.Save();
            return true;

        }
        public bool EditDistribution(Distribution distribution)
        {
            _unitOfWork.DistributionRepository.Edit(distribution);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteDistribution(Distribution distribution)
        {
            if (distribution == null) return false;
            _unitOfWork.DistributionRepository.Delete(distribution);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.DistributionRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.DistributionRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<Distribution> GetAllDistribution()
        {
            return _unitOfWork.DistributionRepository.GetAll();
        }
        public Distribution FindById(int id)
        {
            return _unitOfWork.DistributionRepository.FindById(id);
        }
        public List<Distribution> FindBy(Expression<Func<Distribution, bool>> predicate)
        {
            return _unitOfWork.DistributionRepository.FindBy(predicate);
        }
        public IEnumerable<Distribution> Get(
           Expression<Func<Distribution, bool>> filter = null,
           Func<IQueryable<Distribution>, IOrderedQueryable<Distribution>> orderBy = null,
           string includeProperties = "")
        {
            return _unitOfWork.DistributionRepository.Get(filter, orderBy, includeProperties);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }



       
    }
}


