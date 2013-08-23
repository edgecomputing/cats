using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;

namespace Cats.Services.EarlyWarning
{
    public class DonorService:IDonorService
    {
        private IUnitOfWork _unitOfWork;

        public DonorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool AddDonor(Models.Donor donor)
        {
            _unitOfWork.DonorRepository.Add(donor);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteDonor(Models.Donor donor)
        {
            if (donor == null) return false;
            _unitOfWork.DonorRepository.Delete(donor);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.DonorRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.DonorRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool EditDonor(Models.Donor donor)
        {
            _unitOfWork.DonorRepository.Edit(donor);
            _unitOfWork.Save();
            return true;
        }

        public Models.Donor FindById(int id)
        {
            return _unitOfWork.DonorRepository.FindById(id);
        }

        public List<Models.Donor> GetAllDonor()
        {
            return _unitOfWork.DonorRepository.GetAll();
        }

        public List<Models.Donor> FindBy(System.Linq.Expressions.Expression<Func<Models.Donor, bool>> predicate)
        {
            return _unitOfWork.DonorRepository.FindBy(predicate);
        }

        public IEnumerable<Models.Donor> Get(System.Linq.Expressions.Expression<Func<Models.Donor, bool>> filter = null, Func<IQueryable<Models.Donor>, IOrderedQueryable<Models.Donor>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.DonorRepository.Get(filter, orderBy, includeProperties);
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
