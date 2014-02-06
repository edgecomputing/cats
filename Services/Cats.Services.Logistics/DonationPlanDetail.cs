using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Models;
using Cats.Data.UnitWork;

namespace Cats.Services.Logistics
{

    public class DonationPlanDetailService : IDonationPlanDetailService
    {
        private readonly IUnitOfWork _unitOfWork;


        public DonationPlanDetailService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddDonationPlanDetail(DonationPlanDetail donationPlanDetail)
        {
            _unitOfWork.DonationPlanDetailRepository.Add(donationPlanDetail);
            _unitOfWork.Save();
            return true;

        }
        public bool EditDonationPlanDetail(DonationPlanDetail donationPlanDetail)
        {
            _unitOfWork.DonationPlanDetailRepository.Edit(donationPlanDetail);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteDonationPlanDetail(DonationPlanDetail donationPlanDetail)
        {
            if (donationPlanDetail == null) return false;
            _unitOfWork.DonationPlanDetailRepository.Delete(donationPlanDetail);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.DonationPlanDetailRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.DonationPlanDetailRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<DonationPlanDetail> GetAllDonationPlanDetail()
        {
            return _unitOfWork.DonationPlanDetailRepository.GetAll();
        }
        public DonationPlanDetail FindById(int id)
        {
            return _unitOfWork.DonationPlanDetailRepository.FindById(id);
        }
        public List<DonationPlanDetail> FindBy(Expression<Func<DonationPlanDetail, bool>> predicate)
        {
            return _unitOfWork.DonationPlanDetailRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}


