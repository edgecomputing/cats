using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Models;
using Cats.Data.UnitWork;
using System.Linq;

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

         public bool DeleteDonation(DonationPlanHeader donationPlanHeader)
         {
             try
             {
                 var donationPlanDetails =
                 _unitOfWork.DonationPlanDetailRepository.FindBy(
                     m => m.DonationHeaderPlanID == donationPlanHeader.DonationHeaderPlanID);
                 foreach (var planDetail in donationPlanDetails)
                 {

                     _unitOfWork.DonationPlanDetailRepository.Delete(planDetail);
                     _unitOfWork.Save();
                 }
                 _unitOfWork.DonationPlanHeaderRepository.Delete(donationPlanHeader);
                 _unitOfWork.Save();
                 return true;
             }
             catch (Exception)
             {

                 return false;
             }
             

         }
       public bool DeleteReceiptAllocation(DonationPlanHeader donationPlanHeader)
         {

           try
           {
               var donationDetailHubIDs =
                   _unitOfWork.DonationPlanDetailRepository.FindBy(
                       m => m.DonationHeaderPlanID == donationPlanHeader.DonationHeaderPlanID).Select(m => m.HubID);
               var receiptPlans = _unitOfWork.ReceiptAllocationReository.FindBy(m => donationDetailHubIDs.Contains(m.HubID) && m.SINumber == donationPlanHeader.ShippingInstruction.Value 
                                                                               && m.ETA ==donationPlanHeader.ETA );
               foreach (var receiptAllocation in receiptPlans)
               {
                   _unitOfWork.ReceiptAllocationReository.Delete(receiptAllocation);
               }
               _unitOfWork.Save();
               return true;

           }
           catch (Exception)
           {

               return false;
           }
             
         }

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}
   
 
      

