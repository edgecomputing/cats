using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.Administration
{
   public class DonorService:IDonorService
    {
        private readonly IUnitOfWork _unitOfWork;


        public DonorService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddDonor(Donor donor)
        {
            _unitOfWork.DonorRepository.Add(donor);
            _unitOfWork.Save();
            return true;

        }
        public bool EditDonor(Donor donor)
        {
            _unitOfWork.DonorRepository.Edit(donor);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteDonor(Donor donor)
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
        public List<Donor> GetAllDonor()
        {
            return _unitOfWork.DonorRepository.GetAll();
        }
        public Donor FindById(int id)
        {
            return _unitOfWork.DonorRepository.FindById(id);
        }
        public List<Donor> FindBy(Expression<Func<Donor, bool>> predicate)
        {
            return _unitOfWork.DonorRepository.FindBy(predicate);
        }
        #endregion
        public void Dispose()
        {
            _unitOfWork.Dispose();

        }
    }
}
