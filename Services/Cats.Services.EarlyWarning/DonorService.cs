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
    public class DonorService : IDonorService
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public DonorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Implementation of IDonorService

        public bool AddDonor(Donor donor)
        {
            _unitOfWork.DonorRepository.Add(donor);
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

        public bool EditDonor(Donor donor)
        {
            _unitOfWork.DonorRepository.Edit(donor);
            _unitOfWork.Save();
            return true;
        }

        public Donor FindById(int id)
        {
            return _unitOfWork.DonorRepository.FindById(id);
        }

        public List<Donor> GetAllDonor()
        {
            return _unitOfWork.DonorRepository.GetAll();
        }

        public List<Donor> FindBy(Expression<Func<Donor, bool>> predicate)
        {
            return _unitOfWork.DonorRepository.FindBy(predicate);
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        #endregion
    }
}
