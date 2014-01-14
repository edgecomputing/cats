using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;

namespace Cats.Services.EarlyWarning
{
   public class HrdDonorCoverageService:IHrdDonorCoverageService
    {
        
       private readonly IUnitOfWork _unitOfWork;

       public HrdDonorCoverageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
       public bool AddHrdDonorCoverage(Models.HrdDonorCoverage hrdDonorCoverage)
       {
           _unitOfWork.HrdDonorCoverageRepository.Add(hrdDonorCoverage);
           _unitOfWork.Save();
           return true;
       }

        public bool DeleteHrdDonorCoverage(Models.HrdDonorCoverage hrdDonorCoverage)
        {
            if (hrdDonorCoverage == null) return false;
            _unitOfWork.HrdDonorCoverageRepository.Delete(hrdDonorCoverage);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.HrdDonorCoverageRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.HrdDonorCoverageRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool EditHrdDonorCoverage(Models.HrdDonorCoverage hrdDonorCoverage)
        {
            _unitOfWork.HrdDonorCoverageRepository.Edit(hrdDonorCoverage);
            _unitOfWork.Save();
            return true;
        }

        public Models.HrdDonorCoverage FindById(int id)
        {
            return _unitOfWork.HrdDonorCoverageRepository.FindById(id);
        }

        public List<Models.HrdDonorCoverage> GetAllHrdDonorCoverage()
        {
            return _unitOfWork.HrdDonorCoverageRepository.GetAll();
        }

        public List<Models.HrdDonorCoverage> FindBy(System.Linq.Expressions.Expression<Func<Models.HrdDonorCoverage, bool>> predicate)
        {
            return _unitOfWork.HrdDonorCoverageRepository.FindBy(predicate);
        }

        public IEnumerable<Models.HrdDonorCoverage> Get(System.Linq.Expressions.Expression<Func<Models.HrdDonorCoverage, bool>> filter = null, Func<IQueryable<Models.HrdDonorCoverage>, IOrderedQueryable<Models.HrdDonorCoverage>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.HrdDonorCoverageRepository.Get(filter, orderBy, includeProperties);
        }
        public int NumberOfCoveredWoredas(int donorCoverageID)
        {
            var donorCoverageDetail =_unitOfWork.HrdDonorCoverageDetailRepository.FindBy(m => m.HRDDonorCoverageID == donorCoverageID);
            return donorCoverageDetail.Count;
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }


       
    }
}
