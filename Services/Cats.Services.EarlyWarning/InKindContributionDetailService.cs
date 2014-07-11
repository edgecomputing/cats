using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;

namespace Cats.Services.EarlyWarning
{
   public class InKindContributionDetailService:IInkindContributionDetailService
   {
       private UnitOfWork _unitOfWork;
       public InKindContributionDetailService(UnitOfWork unitOfWork)
       {
           _unitOfWork = unitOfWork;
       } 
       public bool AddInKindContributionDetail(Models.InKindContributionDetail inKindContributionDetail)
       {
           _unitOfWork.InKindContributionDetailRepository.Add(inKindContributionDetail);
           _unitOfWork.Save();
           return true;
       }

        public bool DeleteInKindContributionDetail(Models.InKindContributionDetail inKindContributionDetail)
        {
            if (inKindContributionDetail == null) return false;
            _unitOfWork.InKindContributionDetailRepository.Delete(inKindContributionDetail);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.InKindContributionDetailRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.InKindContributionDetailRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool EditInKindContributionDetail(Models.InKindContributionDetail inKindContributionDetail)
        {
            _unitOfWork.InKindContributionDetailRepository.Edit(inKindContributionDetail);
            _unitOfWork.Save();
            return true;
        }

        public Models.InKindContributionDetail FindById(int id)
        {
            return _unitOfWork.InKindContributionDetailRepository.FindById(id);
        }

        public List<Models.InKindContributionDetail> GetAllInKindContributionDetail()
        {
            return _unitOfWork.InKindContributionDetailRepository.GetAll();
        }

        public List<Models.InKindContributionDetail> FindBy(System.Linq.Expressions.Expression<Func<Models.InKindContributionDetail, bool>> predicate)
        {
            return _unitOfWork.InKindContributionDetailRepository.FindBy(predicate);
        }

        public IEnumerable<Models.InKindContributionDetail> Get(System.Linq.Expressions.Expression<Func<Models.InKindContributionDetail, bool>> filter = null, Func<IQueryable<Models.InKindContributionDetail>, IOrderedQueryable<Models.InKindContributionDetail>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.InKindContributionDetailRepository.Get(filter, orderBy, includeProperties);
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
