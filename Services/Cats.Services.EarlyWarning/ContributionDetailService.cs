using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;

namespace Cats.Services.EarlyWarning
{
   public class ContributionDetailService:IContributionDetailService
   {
       private readonly IUnitOfWork _unitOfWork;
       public ContributionDetailService(IUnitOfWork unitOfWork)
       {
           _unitOfWork = unitOfWork;
       }
        public bool AddContributionDetail(Models.ContributionDetail contributionDetail)
        {
            _unitOfWork.ContributionDetailRepository.Add(contributionDetail);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteContributionDetail(Models.ContributionDetail contributionDetail)
        {
            if (contributionDetail == null) return false;
            _unitOfWork.ContributionDetailRepository.Delete(contributionDetail);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.ContributionDetailRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.ContributionDetailRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool EditContributionDetail(Models.ContributionDetail contributionDetail)
        {
            _unitOfWork.ContributionDetailRepository.Edit(contributionDetail);
            _unitOfWork.Save();
            return true;
        }

        public Models.ContributionDetail FindById(int id)
        {
            return _unitOfWork.ContributionDetailRepository.FindById(id);
        }

        public List<Models.ContributionDetail> GetAllContributionDetail()
        {
            return _unitOfWork.ContributionDetailRepository.GetAll();
        }

        public List<Models.ContributionDetail> FindBy(System.Linq.Expressions.Expression<Func<Models.ContributionDetail, bool>> predicate)
        {
            return _unitOfWork.ContributionDetailRepository.FindBy(predicate);
        }

        public IEnumerable<Models.ContributionDetail> Get(System.Linq.Expressions.Expression<Func<Models.ContributionDetail, bool>> filter = null, Func<IQueryable<Models.ContributionDetail>, IOrderedQueryable<Models.ContributionDetail>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.ContributionDetailRepository.Get(filter, orderBy, includeProperties);
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
