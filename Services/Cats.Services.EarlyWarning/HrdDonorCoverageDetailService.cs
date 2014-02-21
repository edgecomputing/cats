using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;

namespace Cats.Services.EarlyWarning
{
   public class HrdDonorCoverageDetailService:IHrdDonorCoverageDetailService
   {
       private readonly IUnitOfWork _unitOfWork;

       public HrdDonorCoverageDetailService(IUnitOfWork unitOfWork)
       {
           _unitOfWork = unitOfWork;
       }
       public bool AddHrdDonorCoverageDetail(Models.HrdDonorCoverageDetail hrdDonorCoverageDetail)
       {
           _unitOfWork.HrdDonorCoverageDetailRepository.Add(hrdDonorCoverageDetail);
           _unitOfWork.Save();
           return true;
       }

        public bool DeleteHrdDonorCoverageDetail(Models.HrdDonorCoverageDetail hrdDonorCoverageDetail)
        {
            if (hrdDonorCoverageDetail == null) return false;
            _unitOfWork.HrdDonorCoverageDetailRepository.Delete(hrdDonorCoverageDetail);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.HrdDonorCoverageDetailRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.HrdDonorCoverageDetailRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool EditHrdDonorCoverageDetail(Models.HrdDonorCoverageDetail hrdDonorCoverageDetail)
        {
            _unitOfWork.HrdDonorCoverageDetailRepository.Edit(hrdDonorCoverageDetail);
            _unitOfWork.Save();
            return true;
        }

        public Models.HrdDonorCoverageDetail FindById(int id)
        {
            return _unitOfWork.HrdDonorCoverageDetailRepository.FindById(id);
        }

        public List<Models.HrdDonorCoverageDetail> GetAllHrdDonorCoverageDetail()
        {
            return _unitOfWork.HrdDonorCoverageDetailRepository.GetAll();
        }

        public List<Models.HrdDonorCoverageDetail> FindBy(System.Linq.Expressions.Expression<Func<Models.HrdDonorCoverageDetail, bool>> predicate)
        {
            return _unitOfWork.HrdDonorCoverageDetailRepository.FindBy(predicate);
        }

        public IEnumerable<Models.HrdDonorCoverageDetail> Get(System.Linq.Expressions.Expression<Func<Models.HrdDonorCoverageDetail, bool>> filter = null, Func<IQueryable<Models.HrdDonorCoverageDetail>, IOrderedQueryable<Models.HrdDonorCoverageDetail>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.HrdDonorCoverageDetailRepository.Get(filter, orderBy, includeProperties);
        }
        public bool AddWoredas(Models.HrdDonorCoverageDetail hrdDonorCoverageDetail)
        {
            var woredaExists =
                _unitOfWork.HrdDonorCoverageDetailRepository.FindBy(
                    m => m.HRDDonorCoverageID == hrdDonorCoverageDetail.HRDDonorCoverageID &&
                         m.WoredaID == hrdDonorCoverageDetail.WoredaID);
            if (woredaExists!=null)
            {
                return true;
            }
            _unitOfWork.HrdDonorCoverageDetailRepository.Add(hrdDonorCoverageDetail);
            _unitOfWork.Save();
            return true;
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
