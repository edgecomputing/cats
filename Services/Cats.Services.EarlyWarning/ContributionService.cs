using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
  public class ContributionService:IContributionService
    {
         private readonly IUnitOfWork _unitOfWork;
         public ContributionService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public bool AddContribution(Contribution contribution)
        {
            _unitOfWork.ContributionRepository.Add(contribution);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteContribution(Contribution contribution)
        {
            if (contribution == null) return false;
            _unitOfWork.ContributionRepository.Delete(contribution);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.ContributionRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.ContributionRepository.Delete(entity);
            _unitOfWork.Save();
            return true;

        }

        public bool EditContribution(Contribution contribution)
        {
            _unitOfWork.ContributionRepository.Edit(contribution);
            _unitOfWork.Save();
            return true;
        }

        public Contribution FindById(int id)
        {
            return _unitOfWork.ContributionRepository.FindById(id);
        }

        public List<Contribution> GetAllContribution()
        {
            return _unitOfWork.ContributionRepository.GetAll();
        }

        public List<Contribution> FindBy(Expression<Func<Contribution, bool>> predicate)
        {
            return _unitOfWork.ContributionRepository.FindBy(predicate);
        }

        public IEnumerable<Contribution> Get(Expression<Func<Contribution, bool>> filter = null, Func<IQueryable<Contribution>, IOrderedQueryable<Contribution>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.ContributionRepository.Get(filter, orderBy, includeProperties);
        }

        public void Dispose()
        {
           _unitOfWork.Dispose();
        }
    }
}
