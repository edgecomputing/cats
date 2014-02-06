using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Models;
using Cats.Data.UnitWork;

namespace Cats.Services.Logistics
{

    public class DonationPlanHeaderService : IDonationPlanHeaderService
    {
        private readonly IUnitOfWork _unitOfWork;


        public DonationPlanHeaderService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        #region Default Service Implementation

        public bool AddDonationPlanHeader(DonationPlanHeader donationPlanHeader)
        {
            _unitOfWork.DonationPlanHeaderRepository.Add(donationPlanHeader);
            _unitOfWork.Save();
            return true;

        }

        public bool EditDonationPlanHeader(DonationPlanHeader donationPlanHeader)
        {
            _unitOfWork.DonationPlanHeaderRepository.Edit(donationPlanHeader);
            _unitOfWork.Save();
            return true;

        }

        public bool DeleteDonationPlanHeader(DonationPlanHeader donationPlanHeader)
        {
            if (donationPlanHeader == null) return false;
            _unitOfWork.DonationPlanHeaderRepository.Delete(donationPlanHeader);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.DonationPlanHeaderRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.DonationPlanHeaderRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public List<DonationPlanHeader> GetAllDonationPlanHeader()
        {
            return _unitOfWork.DonationPlanHeaderRepository.GetAll();
        }

        public DonationPlanHeader FindById(int id)
        {
            return _unitOfWork.DonationPlanHeaderRepository.FindById(id);
        }

        public List<DonationPlanHeader> FindBy(Expression<Func<DonationPlanHeader, bool>> predicate)
        {
            return _unitOfWork.DonationPlanHeaderRepository.FindBy(predicate);
        }

        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}
   
 
      

